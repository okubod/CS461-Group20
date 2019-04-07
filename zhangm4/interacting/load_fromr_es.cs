using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load_fromr_es : MonoBehaviour
{
	GameObject instance;
	Ray ray;
 	RaycastHit hit;
	// Start is called before the first frame update
    void Start()
    {
        //Resources is a folder in Assets and pigY is the name of the obj model located at Resources
        //After run, it will import the obj model to display
				instance = Instantiate(Resources.Load("bu", typeof(GameObject))) as GameObject;
				var boxCollider = instance.gameObject.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
				instance.layer = 2;
				//GameObject sphere = Instantiate(Resources.Load("pigY", typeof(GameObject))) as GameObject;;
				//sphere.gameObject.transform.position = new Vector3(0,0,0);
				//sphere.gameObject.transform.localScale *= 10;
				//sphere.layer = 2;




    }

		//private void Update() {
	     //  if (Input.GetMouseButtonDown(0))
	  //     {
	       //    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				//		 sphere.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
				//		 sphere.gameObject.transform.localScale *= 5f;
				//		 sphere.gameObject.GetComponent<Renderer> ().material.color = Color.red;
	      // }
	  // }

	void Update(){

	}

	void OnGUI() {
		if (GUI.RepeatButton (new Rect (40, 60, 20, 20), "+")) {
			instance.gameObject.transform.localScale *= 1.1f;
		}
		if (GUI.RepeatButton (new Rect (80, 60, 20, 20), "-")) {
			instance.gameObject.transform.localScale *= 0.9f;
		}
		if (GUI.RepeatButton (new Rect (40, 100, 20, 20), "x")) {
			instance.gameObject.transform.Rotate(10, 0, 0);
		}
		if (GUI.RepeatButton (new Rect (80, 100, 20, 20), "y")) {
			instance.gameObject.transform.Rotate(0,10,0);
		}
		if (GUI.RepeatButton (new Rect (120, 100, 20, 20), "z")) {
			instance.gameObject.transform.Rotate(0,0,10);
		}
		if (GUI.RepeatButton (new Rect (120, 140, 20, 20), "1")) {
			Camera.main.transform.position += new Vector3(0,0,0.1f);
		}
		if (GUI.RepeatButton (new Rect (160, 140, 20, 20), "1")) {
			Camera.main.transform.position -= new Vector3(0,0, 0.1f);
		}
	}


}
