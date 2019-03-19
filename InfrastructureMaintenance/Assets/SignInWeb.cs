using System.Collections;
using UnityEngine;

public class SignInWeb : MonoBehaviour
{
    public void OpenWeb()
    {
        Application.OpenURL("http://web.engr.oregonstate.edu/~coopchri/infrastructure_maintenance/autodesk_authorize.php");
    }
}
