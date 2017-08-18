using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : Singleton<InputController>
{

    #region Fields
    /* KEYBOARD */
    public bool EnableMousePan = false;

    public float CameraPanSpeed = 20f;
    public float ScrollSpeed = 20f;
    public float CameraPanBorderThickness = 10f;
    public Camera mainCamera;
    public float LimitLeft;
    public float LimitRight;
    public float LimitFar;
    public float LimitNear;
    public float LimitUp;
    public float LimitDown;
    /* KB END */

    /* MOUSE */
    public GameObject circleCursorPrefab;

    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // The world-position start of our left-mouse drag operation
    Vector3 dragStartPosition;
    List<GameObject> dragPreviewGameObjects;

    //BuildModeController bmc;
    //FurnitureSpriteController fsc;

    bool isDragging = false;

    /* END MOUSE */

    /* DEBUG STUFF */
    public GameObject character;
    #endregion

    // Use this for initialization
    void Start()
    {
        // Keyboard
        mainCamera = Camera.main;

        // Mouse
        dragPreviewGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyboard();
        UpdateMouse();
    }

    #region Keyboard
    void UpdateKeyboard()
    {
        // CAMERA (somewhat combined with mouse)
        var pos = mainCamera.transform.position;

        if (Input.GetKey(KeyCode.W) || (EnableMousePan && Input.mousePosition.y >= (Screen.height - CameraPanBorderThickness)))
        {
            pos.y += CameraPanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || (EnableMousePan && Input.mousePosition.y <= (CameraPanBorderThickness)))
        {
            pos.y -= CameraPanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || (EnableMousePan && Input.mousePosition.x <= (CameraPanBorderThickness)))
        {
            pos.x -= CameraPanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || (EnableMousePan && Input.mousePosition.x >= (Screen.width - CameraPanBorderThickness)))
        {
            pos.x += CameraPanSpeed * Time.deltaTime;
        }

        // Mouse ScrollWheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //pos.z -= scroll * ScrollSpeed * 10f * Time.deltaTime;
        mainCamera.orthographicSize -= scroll * 10f * ScrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, LimitLeft, LimitRight);
        pos.y = Mathf.Clamp(pos.y, LimitDown, LimitUp);
        //pos.z = Mathf.Clamp(pos.z, LimitNear, LimitFar);
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, LimitNear, LimitFar);

        mainCamera.transform.position = pos;

        // Debug inputs
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (WorldController.Instance.CurrentInterfaceState == WorldController.InterfaceState.Default)
            {
                WorldController.Instance.CurrentInterfaceState = WorldController.InterfaceState.Construction;
            }
            else
            {
                WorldController.Instance.CurrentInterfaceState = WorldController.InterfaceState.Default;
            }

            Debug.Log("Interface mode changed to: " + WorldController.Instance.CurrentInterfaceState.ToString());
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            var mousePos = Input.mousePosition;
            var worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            var jobPos = new Vector3((int)worldPos.x, (int)worldPos.y, 0);

            WorkController.AddNewWork(Work.WorkType.Chop_tree, jobPos);

            Debug.Log("New work added at: " + jobPos);
        }
    }
    #endregion

    #region Mouse    

    void UpdateMouse()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        switch (WorldController.Instance.CurrentInterfaceState)
        {
            case WorldController.InterfaceState.Default:
                //CheckNormalMouseInput();
                break;
            case WorldController.InterfaceState.Construction:
                CheckDragging();
                break;
        }
    }

    /// <summary>
	/// Gets the mouse position in world space.
	/// </summary>
	public Vector3 GetMousePosition()
    {
        return currFramePosition;
    }

    public Tile GetMouseOverTile()
    {
        return WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
    }

    void CheckDragging()
    {
       // If we're over a UI element, then bail out from this.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Clean up old drag previews
        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (WorldController.Instance.CurrentInterfaceState != WorldController.InterfaceState.Construction)
        {
            return;
        }

        // Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
            isDragging = true;
        }
        else if (isDragging == false)
        {
            dragStartPosition = currFramePosition;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Escape))
        {
            // The RIGHT mouse button was released, so we
            // are cancelling any dragging/build mode.
            isDragging = false;
        }

        //if (bmc.IsObjectDraggable() == false)
        //{
        //    dragStartPosition = currFramePosition;
        //}

        int start_x = Mathf.FloorToInt(dragStartPosition.x + 0.5f);
        int end_x = Mathf.FloorToInt(currFramePosition.x + 0.5f);
        int start_y = Mathf.FloorToInt(dragStartPosition.y + 0.5f);
        int end_y = Mathf.FloorToInt(currFramePosition.y + 0.5f);

        // We may be dragging in the "wrong" direction, so flip things if needed.
        if (end_x < start_x)
        {
            int tmp = end_x;
            end_x = start_x;
            start_x = tmp;
        }
        if (end_y < start_y)
        {
            int tmp = end_y;
            end_y = start_y;
            start_y = tmp;
        }

        //if( isDragging ) {
        // Display a preview of the drag area
        for (int x = start_x; x <= end_x; x++)
        {
            for (int y = start_y; y <= end_y; y++)
            {
                Tile t = WorldController.Instance.World.GetTileAt(x, y);
                if (t != null)
                {
                    // show the generic dragging visuals
                    GameObject go = SimplePool.Spawn(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(this.transform, true);
                    dragPreviewGameObjects.Add(go);

                }
            }
        }
        //}

        // End Drag
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Loop through all the tiles
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);

                    if (t != null)
                    {
                        // Draw installed object (work to construct)

                        //FIXME: Right now we're only making walls


                        // Call BuildModeController::DoBuild()
                        //bmc.DoBuild(t);
                    }
                }
            }
        }
    }
    #endregion
}
