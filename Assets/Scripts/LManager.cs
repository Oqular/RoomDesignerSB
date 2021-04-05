using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objPrefab;

    public GameObject currentObj;
    private GameObject selectedObj;

    // Update is called once per frame
    void Update()
    {
        CreateObject();

        SelectExistingObj();

        if (currentObj)
        {
            MoveObject();
            SnapToGrid();
            PlaceObject();
        }
        if(selectedObj){
            if(Input.GetKeyDown(KeyCode.M)){
                //Move
                currentObj = selectedObj;
                selectedObj = null;
                //MoveObject();
            }else if(Input.GetKeyDown(KeyCode.Delete)){
                //Delete
                Destroy(selectedObj);
            }else if(Input.GetKeyDown(KeyCode.C)){
                //Change color
            }else if(Input.GetKeyDown(KeyCode.Escape)){
                //Cancel
                selectedObj.layer = 0;
                selectedObj = null;
            }
        }
    }

    private void SnapToGrid(){
        if(Input.GetKey(KeyCode.LeftShift)){
            //Debug.Log("Snapping");
            var x = Mathf.Round(currentObj.transform.position.x);
            var y = Mathf.Round(currentObj.transform.position.y) + 0.5f;
            var z = Mathf.Round(currentObj.transform.position.z);
            currentObj.transform.position = new Vector3(x, y, z);

        }
    }

    private void SelectExistingObj(){
        if(Input.GetMouseButtonUp(0) && currentObj == null){
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out hitInfo);
            if(hit){
                if(hitInfo.transform.gameObject.tag != "immovable"){
                    //currentObj = hitInfo.transform.parent.gameObject;
                    //var child = hitInfo.transform.gameObject;
                    //var parent = child.transform.parent.gameObject;
                    selectedObj = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.layer = 2;
                    //Debug.Log(child + "  " + parent);
                }
            }
        
        }
    }

    private void CreateObject(){
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentObj == null)
            {
                currentObj = Instantiate(objPrefab);
                currentObj.tag = "Player";
            }
            else
            {
                Destroy(currentObj);
            }
        }
    }

    private void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            var newY = hitInfo.point.y + currentObj.transform.localScale.y / 2;
            currentObj.transform.position = new Vector3(hitInfo.point.x, newY, hitInfo.point.z);
            // Debug.Log(hitInfo.point);
            // currentObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void PlaceObject()
    {
        OverlapController overlapController = currentObj.GetComponent<OverlapController>();
        if (Input.GetMouseButtonUp(0))
        {
            if(!overlapController.overlap){
                currentObj.layer = 0;
                currentObj.tag = "Untagged";
                currentObj = null;
            }
            // GameObject child = currentObj.transform.GetChild(0).gameObject;
            // child.layer = 0;
        }
    }
}
