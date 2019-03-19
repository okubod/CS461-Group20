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
public class ProjectJSON
{
    public ProjectData[] data;
}

[System.Serializable]
public class ProjectData
{
    public string id;
    public ProjectAttributes attributes;
    public ProjectRelationships relationships;
}

[System.Serializable]
public class ProjectAttributes
{
     public string name;
}

[System.Serializable]
public class ProjectRelationships
{
    public ProjectRootFolder rootFolder;
}

[System.Serializable]
public class ProjectRootFolder
{
    public RootFolderData data;
}

[System.Serializable]
public class RootFolderData
{
    public string id;
}


public class ProjectScrollList : MonoBehaviour
{
    public ProjectJSON projects;
    public Transform contentPanel;
    public ProjectDirectory fileDirectory;
    public string json = "";
    GameObject dialog = null;

    public GameObject selfPanel;

    public Text toLoad;

    // Start is called before the first frame update
    void Start()
    {
	json = "{\"jsonapi\": {\"version\": \"1.0\"},\"links\": {\"self\": {\"href\": \"https://developer.api.autodesk.com/project/v1/hubs/a.cGVyc29uYWw6cGUyOWNjZjMy/projects\"}},\"data\": [{\"type\": \"projects\",\"id\": \"a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY\",\"attributes\": {\"name\": \"Demo Project\",\"extension\": {\"type\": \"projects:autodesk.core:Project\",\"version\": \"1.0\",\"schema\": {\"href\": \"https://developer.api.autodesk.com/schema/v1/versions/projects%3Aautodesk.core%3AProject-1.0\"},\"data\": {}}},\"links\": {\"self\": {\"href\": \"https://developer.api.autodesk.com/project/v1/hubs/a.cGVyc29uYWw6cGUyOWNjZjMy/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY\"}},\"relationships\": {\"hub\": {\"data\": {\"type\": \"hubs\",\"id\": \"a.cGVyc29uYWw6cGUyOWNjZjMy\"},\"links\": {\"related\": {\"href\": \"https://developer.api.autodesk.com/project/v1/hubs/a.cGVyc29uYWw6cGUyOWNjZjMy\"}}},\"rootFolder\": {\"data\": {\"type\": \"folders\",\"id\": \"urn:adsk.wipprod:fs.folder:co.uvDiLQ5DRYidDQ_EFW1OOg\"},\"meta\": {\"link\": {\"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/folders/urn%3Aadsk.wipprod%3Afs.folder%3Aco.uvDiLQ5DRYidDQ_EFW1OOg\"}}}}}]}";
        
        projects = new ProjectJSON();
        projects = JsonUtility.FromJson<ProjectJSON>(json);
        
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
        for (int i = 0; i < projects.data.Length; i++)
        {
            ProjectData file = projects.data[i];
            GameObject newButton = fileDirectory.Get_Object();
            newButton.transform.SetParent(contentPanel);

            ProjectButton fileButton = newButton.GetComponent<ProjectButton>();
            //if (fileButton.FileText == null){
              //   fileButton.FileText = fileButton.GetComponent<Text>();
                // fileButton.FileImage = fileButton.GetComponent<Image>();
            //}
            fileButton.Setup(file, this);
        }
    }

    public void SelectProject(ProjectData new_data)
    {
        selfPanel.SetActive(false);
    }
}
