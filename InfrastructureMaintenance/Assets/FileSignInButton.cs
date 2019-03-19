﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSignInButton : MonoBehaviour
{
    public Button button;
    public Text FileText;
    public Image FileImage;
    
    private DisplaySignInFile file;
    private FileSignInScrollList file_list;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (HandleClick);        
    }

    public void Setup(DisplaySignInFile current_file, FileSignInScrollList current_list)
    {
        file = current_file;
	if (FileText != null){
            FileText.text = file.file_name;
        }
        else{
            Debug.LogWarning("FileText is null");
        }
        if (FileImage != null){
            FileImage.sprite = file.icon;    
        }
        else{
            Debug.LogWarning("FileImage is null");
        }
        file_list = current_list;
    }

    public void HandleClick()
    {
        if (file.type == "dir")
        {
	    file_list.SetDirectory(file.file_name);
	    Debug.Log("LOG: Change Directory");
        }
        else{
            file_list.SetFileLoad(file.file_name);
        }
    }
}
