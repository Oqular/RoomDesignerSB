using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapController : MonoBehaviour
{

    public bool overlap = false;
    public Color originalColor;

    //private LManager lManager;

    public LayerMask m_LayerMask;
    

    // Start is called before the first frame update
    void Start()
    {
        // lManager = GameObject.FindObjectOfType<LManager>();
        originalColor = LManager.Instance.currentObj.transform.GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(!other.gameObject.CompareTag("immovable") && this.gameObject.tag == "Player" && !overlap){
    //         overlap = true;
    //         originalColor = lManager.currentObj.transform.GetComponent<Renderer>().material.color;
    //         lManager.currentObj.transform.GetComponent<Renderer>().material.color = Color.red;
    //     }
        
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if(!other.gameObject.CompareTag("immovable") && this.gameObject.tag == "Player"){
    //         overlap = false;
    //         lManager.currentObj.transform.GetComponent<Renderer>().material.color = originalColor;
    //     }
        
    // }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        var currentObj = LManager.Instance.currentObj;
        if(currentObj != null){
            Vector3 newScale = new Vector3(currentObj.transform.localScale.x - 0.01f, currentObj.transform.localScale.y - 0.01f, currentObj.transform.localScale.z - 0.01f);
            Collider[] hitColliders = Physics.OverlapBox(currentObj.transform.position, newScale / 2, Quaternion.identity, m_LayerMask);
            // if(!overlap){
            //     originalColor = lManager.currentObj.transform.GetComponent<Renderer>().material.color;
            // }
            if(hitColliders.Length > 0){
                overlap = true;
                LManager.Instance.currentObj.transform.GetComponent<Renderer>().material.color = Color.red;
            }else{
                overlap = false;
                LManager.Instance.currentObj.transform.GetComponent<Renderer>().material.color = originalColor;
            }
        }
    }
}
