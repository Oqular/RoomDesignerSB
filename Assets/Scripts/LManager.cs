using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LManager : MonoBehaviour
{
    public static LManager Instance { get; private set;}

    [SerializeField]
    private FlexibleColorPicker colorPicker;

    public GameObject currentObj;
    private GameObject selectedObj;

    [SerializeField]
    private GameObject selectionPanel;
    [SerializeField]
    private GameObject colorPanel;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                MoveButton();
            }else if(Input.GetKeyDown(KeyCode.Delete)){
                //Delete
                DestroyButton();
            }else if(Input.GetKeyDown(KeyCode.C)){
                //Change color
                ColorButton();
            }else if(Input.GetKeyDown(KeyCode.Escape)){
                //Cancel
                CancelButton();
            }
        }
    }
    private void SnapToGrid(){
        if(Input.GetKey(KeyCode.LeftShift)){
            var x = Mathf.Round(currentObj.transform.position.x);
            var y = Mathf.Round(currentObj.transform.position.y);
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
                    selectedObj = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.layer = 2;
                    OpenPanelBox(true, selectionPanel);
                }
            }
        
        }
    }

   public void CreateObject(GameObject gameObj){
        if (selectedObj == null)
        {
            if (currentObj == null)
            {
                currentObj = Instantiate(gameObj);
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
        }
    }

    private void PlaceObject()
    {
        OverlapController overlapController = currentObj.GetComponent<OverlapController>();
        if (Input.GetMouseButtonDown(0))
        {
            if(!overlapController.overlap){
                currentObj.layer = 0;
                currentObj.tag = "Untagged";
                currentObj = null;
            }
        }
    }

    private void OpenPanelBox(bool selected, GameObject panel){
        panel.SetActive(selected);
    }

    //button clicks
    public void MoveButton(){
        currentObj = selectedObj;
        selectedObj = null;
        MoveObject();
    }

    public void DestroyButton(){
        OpenPanelBox(false, selectionPanel);
        Destroy(selectedObj); 
    }

    public void CancelButton(){
        OpenPanelBox(false, selectionPanel);
        selectedObj.layer = 0;
        selectedObj = null;
    }

    public void ColorButton(){
        OpenPanelBox(false, selectionPanel);
        OpenPanelBox(true, colorPanel);
    }

    public void ChangeColor(){
        selectedObj.GetComponent<Renderer>().material.color = colorPicker.color;
        selectedObj.GetComponent<OverlapController>().originalColor = colorPicker.color;
        OpenPanelBox(true, selectionPanel);
        OpenPanelBox(false, colorPanel);
    }
}
