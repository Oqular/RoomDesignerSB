using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using BayatGames.SaveGameFree;

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

        foreach (var item in allObjects)
        {
            GameObject objPrefab = Resources.Load<GameObject>("Prefabs/" + item.objectName);
            ObjectInfo obj = Instantiate(objPrefab).GetComponent<ObjectInfo>();
            obj.SetValues(item);
        }
    }


    // public void Save()
    //     {
    //         SaveGame.Save<Vector3Save>(identifier, target.position, SerializerDropdown.Singleton.ActiveSerializer);
    //     }

    //     public void Load()
    //     {
    //         target.position = SaveGame.Load<Vector3Save>(
    //             identifier,
    //             Vector3.zero,
    //             SerializerDropdown.Singleton.ActiveSerializer);
    //     }
}
