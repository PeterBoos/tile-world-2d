using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    //RaycastHit hit;
    //Ray ray;
    //GameObject SelectedGamebject = null;
    //Vector3 dragStartPosition;
    //bool dragging = false;
    
    //public GameObject Marker;
    //public List<GameObject> dragPreviewMarkers;

    //// Use this for initialization
    //void Start()
    //{
    //    dragPreviewMarkers = new List<GameObject>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    switch (WorldController.Instance.CurrentState)
    //    {
    //        case WorldController.GameStates.Normal:
    //            CheckNormalMouseInput();
    //            break;
    //        case WorldController.GameStates.BuildMode:
    //            CheckDragging();
    //            break;
    //    }
    //}

    //#region Normal mode
    //void CheckNormalMouseInput()
    //{
    //    if (WorldController.isBuildMode == false)
    //    {
    //        if (Input.GetKeyUp(KeyCode.Mouse0))
    //        {
    //            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                var target = hit.collider.gameObject;
    //                //if (target.layer == LayerMask.NameToLayer("Selectable"))
    //                //if (target == this.gameObject)
    //                {
    //                    SelectedGamebject = target;
    //                    Debug.Log("Hit " + SelectedGamebject.name);
    //                }
    //            }
    //        }
    //    }
    //}
    //#endregion

    //#region Build mode
    //void CheckDragging()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (!dragging)
    //        {
    //            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                var target = hit.collider.gameObject;

    //                if (target.tag == "Block")
    //                {
    //                    var pos = target.transform.position;
    //                    var tile = WorldController.Instance.World.GetTileAt((int)pos.x, (int)pos.z);
    //                    dragStartPosition = tile.Position;
    //                    dragging = true;
    //                    Debug.Log("Start drag at " + tile.Position);
    //                }
    //            }

    //        }
    //    }

    //    // Clean up old previews
    //    while (dragPreviewMarkers.Count > 0)
    //    {
    //        GameObject marker = dragPreviewMarkers[0];
    //        dragPreviewMarkers.RemoveAt(0);
    //        //Destroy(marker);
    //        SimplePool.Despawn(marker);
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        // Display a preview of the drag area
    //        if (dragging)
    //        {
    //            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                var target = hit.collider.gameObject;

    //                if (target.tag == "Block")
    //                {
    //                    var pos = target.transform.position;
    //                    var tile = WorldController.Instance.World.GetTileAt((int)pos.x, (int)pos.z);

    //                    var start_x = Mathf.FloorToInt(dragStartPosition.x);
    //                    var end_x = Mathf.FloorToInt(tile.Position.x);
    //                    if (end_x < start_x)
    //                    {
    //                        var temp = end_x;
    //                        end_x = start_x;
    //                        start_x = temp;
    //                    }

    //                    var start_z = Mathf.FloorToInt(dragStartPosition.z);
    //                    var end_z = Mathf.FloorToInt(tile.Position.z);
    //                    if (end_z < start_z)
    //                    {
    //                        var temp = end_z;
    //                        end_z = start_z;
    //                        start_z = temp;
    //                    }

    //                    for (int x = start_x; x <= end_x; x++)
    //                    {
    //                        for (int z = start_z; z <= end_z; z++)
    //                        {
    //                            Tile t = WorldController.Instance.World.GetTileAt(x, z);
    //                            Vector3 markerPos = new Vector3(x + 0.5f, 1f, z + 0.5f);
    //                            //GameObject marker = (GameObject)Instantiate(Marker, markerPos, Quaternion.identity);
    //                            GameObject marker = (GameObject)SimplePool.Spawn(Marker, markerPos, Quaternion.identity);
    //                            marker.transform.SetParent(this.transform, true);
    //                            dragPreviewMarkers.Add(marker);
    //                        }
    //                    }
    //                }
    //            }
    //        }



    //        /*** Deprecated code saved if find use for free pinting markers ***/

    //        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        //if (Physics.Raycast(ray, out hit))
    //        //{
    //        //    var target = hit.collider.gameObject;

    //        //    if (target.tag == "Block" && !SelectedBlocks.Contains(target))
    //        //    {
    //        //        //SelectedBlocks.Add(target);
    //        //        var pos = target.transform.position;
    //        //        var tile = WorldController.Instance.World.GetTileAt((int)pos.x, (int)pos.z);
    //        //        SelectedTiles.Add(tile);
    //        //        var markerPosition = new Vector3((pos.x + 0.5f), (pos.y + 1.1f), (pos.z + 0.5f));
    //        //        var marker = Instantiate(Marker, markerPosition, Quaternion.identity);

    //        //        Debug.Log("Added Tile_" + tile.Position.x + "_" + tile.Position.z + " to Selected blocks list");
    //        //    }

    //        //    Debug.Log("Added " + target.name + " to Selected blocks list");
    //        //}
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if (dragging)
    //        {
    //            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                var target = hit.collider.gameObject;

    //                if (target.tag == "Block")
    //                {
    //                    var pos = target.transform.position;
    //                    var tile = WorldController.Instance.World.GetTileAt((int)pos.x, (int)pos.z);

    //                    var start_x = Mathf.FloorToInt(dragStartPosition.x);
    //                    var end_x = Mathf.FloorToInt(tile.Position.x);
    //                    if (end_x < start_x)
    //                    {
    //                        var temp = end_x;
    //                        end_x = start_x;
    //                        start_x = temp;
    //                    }

    //                    var start_z = Mathf.FloorToInt(dragStartPosition.z);
    //                    var end_z = Mathf.FloorToInt(tile.Position.z);
    //                    if (end_z < start_z)
    //                    {
    //                        var temp = end_z;
    //                        end_z = start_z;
    //                        start_z = temp;
    //                    }

    //                    Debug.Log("Ended drag at " + tile.Position);

    //                    for (int x = start_x; x <= end_x; x++)
    //                    {
    //                        for (int z = start_z; z <= end_z; z++)
    //                        {
    //                            Tile t = WorldController.Instance.World.GetTileAt(x, z);
    //                            Vector3 markerPos = new Vector3(x + 0.5f, 1f, z + 0.5f);
    //                            //var marker = Instantiate(Marker, markerPos, Quaternion.identity);
    //                            GameObject marker = (GameObject)SimplePool.Spawn(Marker, markerPos, Quaternion.identity);
    //                            marker.transform.SetParent(this.transform, true);
    //                            dragPreviewMarkers.Add(marker);
    //                        }
    //                    }

    //                    dragging = false;
    //                }
    //            }
    //        }
    //    }
    //}
    //#endregion
}
