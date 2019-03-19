using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load_fromr_es : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Resources is a folder in Assets and pigY is the name of the obj model located at Resources
        //After run, it will import the obj model to display
        GameObject instance = Instantiate(Resources.Load("pigY", typeof(GameObject))) as GameObject;
    }

    
}
