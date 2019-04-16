using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Vuforia;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using UnityEngine.Android;
#endif

[System.Serializable]
public class DisplayFile
{
    public string file_name;
    public string type;
    public Sprite icon;
}


public class FileScrollList : MonoBehaviour
{
    public List<DisplayFile> fileList;
    public Transform contentPanel;
    public FileDirectory fileDirectory;
    public List<string> path;
    public string load_path = "";
    GameObject dialog = null;
    public GameObject errorPanel;
    public GameObject selfPanel;

    public Button upButton;
    public Button loadButton;

    public Text toLoad;
	
	public TrackableBehaviour theTrackable;
	public Camera ar_camera;

    // Start is called before the first frame update
    void Start()
    {
        // check for the permission to access storage
        #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                dialog = new GameObject();
            }
        #endif

        // get button for load and up
        upButton = GameObject.Find ("ParentButton").GetComponent<Button>();
        loadButton = GameObject.Find ("LoadButton").GetComponent<Button>();

        // get text
        toLoad = GameObject.Find("FileName").GetComponent<Text>();

        // set up click listeners
        upButton.onClick.AddListener ((UnityEngine.Events.UnityAction)this.OnUpButtonClick);
        loadButton.onClick.AddListener ((UnityEngine.Events.UnityAction)this.OnLoadButtonClick);

        // get initial path
        path = new List<string>();
        string temp = "";
	    #if UNITY_IOS
            temp = @"/";
        #elif UNITY_ANDROID
            temp = @"/storage/";
        #elif UNITY_STANDALONE_WIN
	        temp = @"C:\";
        #else
            temp = @"/home/cooper";//GetStoragePath();
        #endif
        path.Add(temp);
        // get list of directories and files
        fileList = new List<DisplayFile>();

        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        // get directories from system and add buttons
        GetDirectories();
	    RemoveButtons();
        //Debug.Log("LOG: Removed Buttons");
        toLoad.text = "";
        load_path = "";
        AddButtons();
    }

    public void SetDirectory(string new_path){
        // add new directory to the path
#if UNITY_STANDALONE_WIN
        path.Add(new_path + "\\");
#else
        path.Add(new_path + "/");
#endif
        //Debug.Log("LOG: Added new path" + new_path);
        RefreshDisplay();
    }

    public void SetFileLoad(string file)
    {
        toLoad.text = file;
        load_path = GetCurrentPath();
        load_path = load_path + file;
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
        for (int i = 0; i < fileList.Count; i++)
        {
            DisplayFile file = fileList[i];
            GameObject newButton = fileDirectory.Get_Object();
            newButton.transform.SetParent(contentPanel);

            FileButton fileButton = newButton.GetComponent<FileButton>();
            //if (fileButton.FileText == null){
              //   fileButton.FileText = fileButton.GetComponent<Text>();
                // fileButton.FileImage = fileButton.GetComponent<Image>();
            //}
            fileButton.Setup(file, this);
        }
    }

    private void GetDirectories ()
    {
        string temp;
	// get string of the current directory
        temp = GetCurrentPath();
        
        // check that we actually received a path
        if (temp != "")
        {
            int num = fileList.Count;
	    // delete all files from fileList
            for (int i = 0; i < num; i++)
            {
                fileList.RemoveAt(0);
	        }
            List<string> dir;
            if (Directory.Exists(temp))
            {
                try
                {
                    dir = new List<string>(Directory.EnumerateDirectories(temp));
                }
                catch (Exception)
                {
                    // remove the last path
                    path.RemoveAt(path.Count - 1);
                    temp = GetCurrentPath();
                    dir = new List<string>(Directory.EnumerateDirectories(temp));
                }
            }
            else
            {
                // remove the last path
                path.RemoveAt(path.Count - 1);
                temp = GetCurrentPath();
                dir = new List<string>(Directory.EnumerateDirectories(temp));
            }
            // add new directories into file list
            for (int i=0; i < dir.Count; i++)
            {
#if UNITY_STANDALONE_WIN
                string[] names = dir[i].Split('\\');
#else
                string[] names = dir[i].Split('/');
#endif
                fileList.Add(new DisplayFile() { file_name = names[names.Length - 1], type = "dir" });
            }
            // get files into file list
            List<string> files = new List<string>(Directory.EnumerateFiles(temp, "*.obj"));
            for (int i=0; i < files.Count; i++)
            {
#if UNITY_STANDALONE_WIN
                string[] file_names = files[i].Split('\\');
#else
                string[] file_names = files[i].Split('/');
#endif
                fileList.Add(new DisplayFile() { file_name = file_names[file_names.Length - 1], type = "file" });
            }
        }
    }

    private string GetCurrentPath ()
    {
        string dir = "";
        
        // loop over all indices in path and write them
        for (int i = 0; i < path.Count; i++)
        {
            dir = dir + path[i];
        }

        return dir;
    }

    private void OnUpButtonClick()
    {
        if (path.Count > 1)
        {
            // remove the last path
            path.RemoveAt(path.Count - 1);
            // reload display
            RefreshDisplay();
        }
    }

    private void OnLoadButtonClick()
    {
        if (load_path != "")
        {
            Debug.Log("LOAD MODEL: " + load_path);
            GameObject model = GameObject.Find("ViewModel");
            MeshFilter mf = model.GetComponent<MeshFilter>();
			if (mf == null)
			{
				model.AddComponent<MeshFilter>();
				mf = model.GetComponent<MeshFilter>();
			}
			else
			{
				mf.mesh.Clear();
			}

			mf.mesh = model.GetComponent<ObjImporter>().ImportFile(load_path);
			errorPanel.SetActive(false);
			selfPanel.SetActive(false);
			if(ar_camera.isActiveAndEnabled == true){
				GameObject trackableGameObject = theTrackable.gameObject;
				for (int i = 0; i < trackableGameObject.transform.childCount; i++)
				{
					Transform child = trackableGameObject.transform.GetChild(i);
					child.gameObject.active = false;           
				}
				model.transform.parent = theTrackable.transform;
				model.transform.localPosition = new Vector3(0,0.2f,0);
				model.transform.localRotation = Quaternion.identity;
				model.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
				model.active = true;
			}
        }
        else{
            errorPanel.SetActive(true);
            Debug.Log("ERROR: No model selected");
        }
    }
}
