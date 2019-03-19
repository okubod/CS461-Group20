using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectButton : MonoBehaviour
{
    public ProjectScrollList file_list;
    public ProjectData file;
    public Button button;
    public Text name;
    
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);        
    }

    public void Setup(ProjectData current_file, ProjectScrollList current_list)
    {
        file = current_file;
	if (name != null){
            name.text = file.attributes.name;
        }
        else{
            Debug.LogWarning("NameText is null");
        }
        
        file_list = current_list;
    }

    public void HandleClick()
    {
        file_list.SelectProject(file);
    }
}
