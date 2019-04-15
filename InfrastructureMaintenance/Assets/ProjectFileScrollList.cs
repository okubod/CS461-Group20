using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Networking;

#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using UnityEngine.Android;
#endif

[System.Serializable]
public class ProjectFileJSON
{
    public ProjectFileData[] data;
}

[System.Serializable]
public class ProjectFileData
{
    public string id;
    public ProjectFileAttributes attributes;
}

[System.Serializable]
public class ProjectFileAttributes
{
     public string displayName;
     public string createTime;
     public string createUserId;
     public string createUserName;
}

public class ProjectFileScrollList : MonoBehaviour
{
    public ProjectFileJSON projectFiles;
    public Transform contentPanel;
    public ProjectFileDirectory fileDirectory;
    public string json = "";
    GameObject dialog = null;

    public GameObject selfPanel;
    public GameObject picPanel;
    public GameObject errorPanel;

    public Text toLoad;

    // Start is called before the first frame update
    void Start()
    {
	json = "{ \"jsonapi\": {    \"version\": \"1.0\"  },  \"links\": {    \"self\": {      \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/folders/urn:adsk.wipprod:fs.folder:co.uvDiLQ5DRYidDQ_EFW1OOg/contents\"    }  },  \"data\": [    {      \"type\": \"items\",      \"id\": \"urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q\",      \"attributes\": {        \"displayName\": \"House Design.rvt\",        \"createTime\": \"2016-05-24T19:25:23+00:00\",        \"createUserId\": \"38SCJGX4R4PV\",        \"createUserName\": \"John Doe\",        \"lastModifiedTime\": \"2016-05-24T19:25:23+00:00\",        \"lastModifiedUserId\": \"38SCJGX4R4PV\",        \"lastModifiedUserName\": \"John Doe\",        \"extension\": {          \"type\": \"items:autodesk.core:File\",          \"version\": \"1.0\",          \"schema\": {            \"href\": \"https://developer.api.autodesk.com/schema/v1/versions/items%3Aautodesk.core%3AFile-1.0\"          },          \"data\": {}        }      },      \"links\": {        \"self\": {          \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q\"        }      },      \"relationships\": {        \"tip\": {          \"data\": {            \"type\": \"versions\",            \"id\": \"urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q?version=1\"          },          \"links\": {            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q/tip\"            }          }        },        \"versions\": {          \"links\": {            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q/versions\"            }          }        },        \"parent\": {          \"data\": {            \"type\": \"folders\",            \"id\": \"urn:adsk.wipprod:fs.folder:co.uvDiLQ5DRYidDQ_EFW1OOg\"          },          \"links\": {            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q/parent\"            }          }        },        \"refs\": {          \"links\": {            \"self\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q/relationships/refs\"            },            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/items/urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q/refs\"            }          }        }      }    }  ],  \"included\": [    {      \"type\": \"versions\",      \"id\": \"urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q?version=1\",      \"attributes\": {        \"name\": \"House Design.rvt\",        \"displayName\": \"House Design.rvt\",        \"createTime\": \"2016-05-24T19:25:23+00:00\",        \"createUserId\": \"38SCJGX4R4PV\",        \"createUserName\": \"John Doe\",        \"lastModifiedTime\": \"2016-05-24T19:25:23+00:00\",        \"lastModifiedUserId\": \"38SCJGX4R4PV\",        \"lastModifiedUserName\": \"John Doe\",        \"versionNumber\": 1,        \"mimeType\": \"application/vnd.autodesk.revit\",        \"fileType\": \"rvt\",        \"storageSize\": 12550144,        \"extension\": {          \"type\": \"versions:autodesk.core:File\",          \"version\": \"1.0\",          \"schema\": {            \"href\": \"https://developer.api.autodesk.com/schema/v1/versions/versions%3Aautodesk.core%3AFile-1.0\"          },          \"data\": {}        }      },      \"links\": {        \"self\": {          \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1\"        }      },      \"relationships\": {        \"item\": {          \"data\": {            \"type\": \"items\",            \"id\": \"urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q\"          },          \"links\": {            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/item\"            }          }        },        \"refs\": {          \"links\": {            \"self\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/relationships/refs\"            },            \"related\": {              \"href\": \"https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/refs\"            }          }        },        \"derivatives\": {          \"data\": {            \"type\": \"derivatives\",            \"id\": \"dXJuOmFkc2sud2lwc3RnOmZzLmZpbGU6dmYuNmJWcjRFVkRTYU9weWtjemVRWVIyUT92ZXJzaW9uPTE\"          },          \"meta\": {            \"link\": {              \"href\": \"https://developer.api.autodesk.com/viewingservice/v1/dXJuOmFkc2sud2lwc3RnOmZzLmZpbGU6dmYuNmJWcjRFVkRTYU9weWtjemVRWVIyUT92ZXJzaW9uPTE\"            }          }        },        \"thumbnails\": {          \"data\": {            \"type\": \"thumbnails\",            \"id\": \"dXJuOmFkc2sud2lwc3RnOmZzLmZpbGU6dmYuNmJWcjRFVkRTYU9weWtjemVRWVIyUT92ZXJzaW9uPTE\"          },          \"meta\": {            \"link\": {              \"href\": \"https://developer.api.autodesk.com/viewingservice/v1/thumbnails/dXJuOmFkc2sud2lwc3RnOmZzLmZpbGU6dmYuNmJWcjRFVkRTYU9weWtjemVRWVIyUT92ZXJzaW9uPTE\"            }          }        },        \"storage\": {          \"data\": {            \"type\": \"objects\",            \"id\": \"urn:adsk.objects:os.object:wip.dm.prod/977d69b1-43e7-40fa-8ece-6ec4602892f3.rvt\"          },          \"meta\": {            \"link\": {              \"href\": \"https://developer.api.autodesk.com/oss/v2/buckets/wip.dm.prod/objects/977d69b1-43e7-40fa-8ece-6ec4602892f3.rvt\"}}}}}]}";
        
        projectFiles = new ProjectFileJSON();
        projectFiles = JsonUtility.FromJson<ProjectFileJSON>(json);
        
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
        for (int i = 0; i < projectFiles.data.Length; i++)
        {
            ProjectFileData file = projectFiles.data[i];
            GameObject newButton = fileDirectory.Get_Object();
            newButton.transform.SetParent(contentPanel);

            ProjectFileButton fileButton = newButton.GetComponent<ProjectFileButton>();
            //if (fileButton.FileText == null){
              //   fileButton.FileText = fileButton.GetComponent<Text>();
                // fileButton.FileImage = fileButton.GetComponent<Image>();
            //}
            fileButton.Setup(file, this);
        }
    }

    public void SelectProjectFile(ProjectFileData new_data)
    {
        // close error panel if open
        errorPanel.SetActive(false);
        // retrieve the file extension
        string name = new_data.attributes.displayName;
        string[] extension = name.Split('.');
        string ext = extension[extension.Length - 1].ToLower();

        // check if file is an image
        if (ext.Equals("jpg") || ext.Equals("jpeg") || ext.Equals("png"))
        {
            selfPanel.SetActive(false);
            picPanel.SetActive(true);
            DownloadImageWWW tex = picPanel.GetComponent<DownloadImageWWW>();
            tex.setImage();
        }

        // check if file is a pdf
        else if (ext.Equals("pdf"))
        {
            selfPanel.SetActive(false);
            picPanel.SetActive(true);
            DownloadImageWWW tex = picPanel.GetComponent<DownloadImageWWW>();
            tex.setImage();
        }

        // otherwise the format is unsupported give error
        else
        {
            errorPanel.SetActive(true);
        }
    }
}
