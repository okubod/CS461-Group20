using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

[System.Serializable]
public class File
{
    public string file_name;
    public Sprite icon;
}


public class FileScrollList : MonoBehaviour
{
    public List<File> fileList;
    public Transform contentPanel;
    public FileDirectory fileDirectory;
    public List<string> path;
    GameObject dialog = null;

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

        // get initial path
        path = new List<string>();
        string temp = "";
        #if UNITY_ANDROID
            temp = @"/storage/";
        #else
            temp = @"/home/cooper";//GetStoragePath();
        #endif
        path.Add(temp);
        // get list of directories and files
        fileList = new List<File>();
        // get string of the current directory
        temp = GetCurrentPath();
        
        // check that we actually received a path
        if (temp != "")
        {
            List<string> dir = new List<string>(Directory.EnumerateDirectories(temp));
            // add new files into file list
            for (int i=0; i < dir.Count; i++)
            {
                fileList.Add(new File() {file_name=dir[i]});
            }

            RefreshDisplay();
        }

        //RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        AddButtons();
    }

    private void AddButtons()
    {
        for (int i = 0; i < fileList.Count; i++)
        {
            File file = fileList[i];
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

    private string GetStoragePath ()
    {
        string startPath = "";

        // try to get the path to android storage
        AndroidJavaClass java_class = new AndroidJavaClass("android.os.Environment");
        startPath = java_class.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
            
         // return the path
         return startPath;
    }

    public string GetCurrentPath ()
    {
        string dir = "";
        
        // loop over all indices in path and write them
        for (int i = 0; i < path.Count; i++)
        {
            dir = dir + path[i];
        }

        return dir;
    }
}
