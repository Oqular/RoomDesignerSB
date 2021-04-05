using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private List<GameObject> objList;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        objList = Resources.LoadAll<GameObject>("Prefabs").ToList();
        LoadModelList();
    }

    private void LoadModelList(){
        foreach (var item in objList)
        {
            Button temp = Instantiate(buttonPrefab);
            temp.transform.GetChild(0).GetComponent<Text>().text = item.name;
            temp.transform.SetParent(container.transform, false);
            temp.onClick.AddListener( delegate {LManager.Instance.CreateObject(item);});
        }
    }
}
