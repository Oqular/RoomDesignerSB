using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public Vector3 position;
    public Color color;
    public int layer;
    public string objectName;

    public void SetValues(ObjectInfo info){
        this.transform.position = info.position;
        this.transform.GetComponent<Renderer>().material.color = info.color;
        this.gameObject.layer = info.layer;
    }

    public void SetCurrentValues(){
        position = this.transform.position;
        color = this.transform.GetComponent<Renderer>().material.color;
        layer = this.gameObject.layer;
    }

}
