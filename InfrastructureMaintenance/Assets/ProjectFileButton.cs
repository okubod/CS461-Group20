using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectFileButton : MonoBehaviour
{
    public Button button;
    public Text FileNameText;
    
    private ProjectFileData file;
    private ProjectFileScrollList file_list;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (HandleClick);        
    }

    public void Setup(ProjectFileData current_file, ProjectFileScrollList current_list)
    {
        file = current_file;
	if (FileNameText != null){
            FileNameText.text = file.attributes.displayName;
        }
        else{
            Debug.LogWarning("NameText is null");
        }
        
        file_list = current_list;
    }

    public void HandleClick()
    {
        file_list.SelectProjectFile(file);
    }
}
