using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    
    public void SaveGame(){
        List<ObjectInfo> allObjects = FindObjectsOfType<ObjectInfo>().ToList();
        foreach(ObjectInfo item in allObjects){
            item.SetCurrentValues();
        }

        BayatGames.SaveGameFree.SaveGame.Save<List<ObjectInfo>>("Objects", allObjects);
    }

    public void LoadGame(){
        List<ObjectInfo> allObjects = FindObjectsOfType<ObjectInfo>().ToList();
        foreach(ObjectInfo item in allObjects){
            Destroy(item.gameObject);
        }
        allObjects = BayatGames.SaveGameFree.SaveGame.Load<List<ObjectInfo>>("Objects");
        if(allObjects != null){
            foreach (var item in allObjects)
            {
                GameObject objPrefab = Resources.Load<GameObject>("Prefabs/" + item.objectName);
                ObjectInfo obj = Instantiate(objPrefab).GetComponent<ObjectInfo>();
                obj.SetValues(item);
            }
        }
    }
}
