using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

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

    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {

        // CAMERA 
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
	} 
}
