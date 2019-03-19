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
public class IssueJSON
{
    public IssueData[] data;
}

[System.Serializable]
public class IssueData
{
    public IssueAttribute attributes;
}

[System.Serializable]
public class IssueAttribute
{
    public string created_at;
    public string created_by;
    public string title;
    public string description;
}


public class IssueScrollList : MonoBehaviour
{
    public IssueJSON issues;
    public Transform contentPanel;
    public IssueDirectory fileDirectory;
    public List<string> path;
    public string json = "";
    GameObject dialog = null;

    public GameObject issueView;
    public GameObject issueDetail;
    public Text issueTitle;
    public Text issueCreatedBy;
    public Text issueCreatedAt;
    public Text issueDescription;

    // Start is called before the first frame update
    void Start()
    {
	json = "{\"data\": [{\"id\": \"ad00f32e-c03c-4c7b-9ec4-d2614bf1980cwed\",\"type\": \"issues\",\"links\": {\"self\": \"https://developer.api.autodesk.com/issues/v1/containers/be00f32e-c03c-4c7b-9ec4-d2614bf1980cfor\"},\"attributes\": {\"created_at\": \"2017-07-18T13:58:03.708Z\",\"synced_at\": \"2017-07-18T13:58:03.708Z\",\"updated_at\": \"2017-07-30T07:28:30.644Z\",\"close_version\": null,\"closed_at\": null,\"closed_by\": null,\"created_by\": \"XDESJEKWTGBG\",\"starting_version\": 1,\"title\": \"My first issue\",\"description\": \"The first issue\",\"resource_urns\": {\"svg\": \"urn:adsk.some.svg\"},\"target_urn\": \"urn:adsk.wipprod:dm.lineage:7_5NcbL1Q1GSjRLJe9eeiw\",\"target_urn_page\": null}},{\"id\": \"he33g21f-c03c-4c7b-9ec4-d2614bf1765okj\",\"type\": \"issues\",\"links\": {\"self\": \"https://developer.api.autodesk.com/issues/v1/containers/be00f32e-c03c-4c7b-9ec4-d2614bf1980cfor\"},\"attributes\": {\"created_at\": \"2017-07-18T13:58:03.708Z\",\"synced_at\": \"2017-07-18T13:58:03.708Z\",\"updated_at\": \"2017-07-30T07:28:30.644Z\",\"close_version\": null,\"closed_at\": null,\"closed_by\": null,\"created_by\": \"XDESJEKWTGBG\",\"starting_version\": 1,\"title\": \"My second issue\",\"description\": \"The second issue\"}}]}";
        
        issues = new IssueJSON();
        issues = JsonUtility.FromJson<IssueJSON>(json);

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
        for (int i = 0; i < issues.data.Length; i++)
        {
            IssueData file = issues.data[i];
            GameObject newButton = fileDirectory.Get_Object();
            newButton.transform.SetParent(contentPanel);

            IssueButton fileButton = newButton.GetComponent<IssueButton>();
            //if (fileButton.FileText == null){
              //   fileButton.FileText = fileButton.GetComponent<Text>();
                // fileButton.FileImage = fileButton.GetComponent<Image>();
            //}
            fileButton.Setup(file, this);
        }
    }

    public void ViewDetail(IssueData new_data)
    {
        issueTitle.text = new_data.attributes.title;
        issueCreatedBy.text = new_data.attributes.created_by;
        issueCreatedAt.text = new_data.attributes.created_at;
        issueDescription.text = new_data.attributes.description;
        issueDetail.SetActive(true);
        issueView.SetActive(false);
    }
}
