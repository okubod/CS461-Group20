using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load_fromr_es : MonoBehaviour
{
	GameObject instance;
	GameObject iinstance;
	// Start is called before the first frame update
    void Start()
    {
        //Resources is a folder in Assets and pigY is the name of the obj model located at Resources
        //After run, it will import the obj model to display
		instance = Instantiate(Resources.Load("bu", typeof(GameObject))) as GameObject;
		iinstance = Instantiate(Resources.Load("bu", typeof(GameObject))) as GameObject;


    }

	void OnGUI() {
		float scaleFactor = 1.5f;

		if (GUI.RepeatButton (new Rect (40, 60, 20, 20), "+")) {
			instance.gameObject.transform.localScale *= 1.1f;
			iinstance.gameObject.transform.localScale *= 1.1f;
		}
		if (GUI.RepeatButton (new Rect (80, 60, 20, 20), "-")) {
			instance.gameObject.transform.localScale *= 0.9f;
			iinstance.gameObject.transform.localScale *= 0.9f;
		}
		if (GUI.RepeatButton (new Rect (40, 100, 20, 20), "x")) {
			instance.gameObject.transform.Rotate(10, 0, 0);
			iinstance.gameObject.transform.Rotate(10, 0, 0);
		}
		if (GUI.RepeatButton (new Rect (80, 100, 20, 20), "y")) {
			instance.gameObject.transform.Rotate(0,10,0);
			iinstance.gameObject.transform.Rotate(0,10,0);
		}
		if (GUI.RepeatButton (new Rect (120, 100, 20, 20), "z")) {
			instance.gameObject.transform.Rotate(0,0,10);
			iinstance.gameObject.transform.Rotate(0,0,10);
		}
		if (GUI.RepeatButton (new Rect (40, 140, 20, 20), "0")) {
			iinstance.gameObject.transform.position = new Vector3(5, 0, 0);
		}
		if (GUI.RepeatButton (new Rect (80, 140, 20, 20), "1")) {
			iinstance.gameObject.transform.position = new Vector3(-5, 0, 0);
		}
		if (GUI.RepeatButton (new Rect (120, 140, 20, 20), "1")) {
			Camera.main.transform.position += new Vector3(0,0,0.1f);
		}
		if (GUI.RepeatButton (new Rect (160, 140, 20, 20), "1")) {
			Camera.main.transform.position -= new Vector3(0,0, 0.1f);
		}
	}

    
}
