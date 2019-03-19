using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IssueButton : MonoBehaviour
{
    public Button button;
    public Text NameText;
    public Text DateText;
    
    private IssueData file;
    private IssueScrollList file_list;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (HandleClick);        
    }

    public void Setup(IssueData current_file, IssueScrollList current_list)
    {
        file = current_file;
	if (NameText != null){
            NameText.text = file.attributes.title;
        }
        else{
            Debug.LogWarning("NameText is null");
        }
        if (DateText != null){
            DateText.text = file.attributes.created_at;    
        }
        else{
            Debug.LogWarning("FileImage is null");
        }
        file_list = current_list;
    }

    public void HandleClick()
    {
        file_list.ViewDetail(file);
    }
}
