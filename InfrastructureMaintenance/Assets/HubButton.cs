using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubButton : MonoBehaviour
{
    public HubScrollList file_list;
    public HubData file;
    public Button button;
    public Text name;
    
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (HandleClick);        
    }

    public void Setup(HubData current_file, HubScrollList current_list)
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
        file_list.ViewProjects(file);
    }
}
