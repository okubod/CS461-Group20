using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using UnityEngine.Android;
#endif

[System.Serializable]
public class HubJSON
{
    public HubData[] data;
}

[System.Serializable]
public class HubData
{
    public string id;
    public HubAttributes attributes;
}

[System.Serializable]
public class HubAttributes
{
     public string name;
}


public class HubScrollList : MonoBehaviour
{
    public HubJSON hubs;
    public Transform contentPanel;
    public HubDirectory fileDirectory;
    public string json = "";
    GameObject dialog = null;

    public GameObject selfPanel;
    public GameObject projectPanel;

    public Text toLoad;

    // Start is called before the first frame update
    void Start()
    {
	json = "{\"jsonapi\": {\"version\": \"1.0\"},\"links\": {\"self\": {\"href\": \"https://developer.api.autodesk.com/project/v1/hubs\"}},\"data\": [{\"type\": \"hubs\",\"id\": \"b.cGVyc29uYWw6cGUyOWNjZjMy\",\"attributes\": {\"name\": \"My First Account\",\"extension\": {\"type\": \"hubs:autodesk.bim360:Account\",\"version\": \"1.0\",\"schema\": {\"href\": \"https://developer.api.autodesk.com/schema/v1/versions/hubs:autodesk.bim360:Account-1.0\"},\"data\": {}}}}]}";
        
        hubs = new HubJSON();
        hubs = JsonUtility.FromJson<HubJSON>(json);
        
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        // get directories from system and add buttons
	RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            fileDirectory.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < hubs.data.Length; i++)
        {
            HubData file = hubs.data[i];
            GameObject newButton = fileDirectory.Get_Object();
            newButton.transform.SetParent(contentPanel);

            HubButton fileButton = newButton.GetComponent<HubButton>();
            //if (fileButton.FileText == null){
              //   fileButton.FileText = fileButton.GetComponent<Text>();
                // fileButton.FileImage = fileButton.GetComponent<Image>();
            //}
            fileButton.Setup(file, this);
        }
    }

    public void ViewProjects(HubData new_data)
    {
        selfPanel.SetActive(false);
        projectPanel.SetActive(true);
    }
}
