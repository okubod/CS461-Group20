using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDirectory : MonoBehaviour
{
    // prefab that this object returns
    public GameObject prefab;
    // list of inactive instances of prefab
    private Stack<GameObject> inactive_objects = new Stack<GameObject>();

    public GameObject Get_Object()
    {
        GameObject spawned_object;

        //check if an inactive object exists
        if (inactive_objects.Count > 0)
        {
            spawned_object = inactive_objects.Pop();
        }
        // otherwise create a new object
        else
        {
            spawned_object = (GameObject)GameObject.Instantiate(prefab);

            // add to the prefab
            HubDirectoryObject directory_object = spawned_object.AddComponent<HubDirectoryObject>();
            directory_object.files = this;
        }

        spawned_object.transform.SetParent(null);
        spawned_object.SetActive(true);

        // return the object
        return spawned_object;
    }

    public void ReturnObject(GameObject return_object)
    {
        HubDirectoryObject directory_object = return_object.GetComponent<HubDirectoryObject>();

        // check if the instance is from this directory
        if (directory_object != null && directory_object.files == this)
        {
            // disable this instance
            return_object.transform.SetParent(transform);
            return_object.SetActive(false);

            // add as an inactive instance
            inactive_objects.Push(return_object);
        }

        // otherwise delete the object
        else
        {
            Debug.LogWarning(return_object.name + " was returned to the directory it isn't from.");
            Destroy(return_object);
        }
    }
}

// identifies the pool
public class HubDirectoryObject : MonoBehaviour
{
    public HubDirectory files;
}
