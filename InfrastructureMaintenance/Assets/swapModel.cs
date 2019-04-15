using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class swapModel : MonoBehaviour
{
	public TrackableBehaviour theTrackable;

	private bool swap = false;
	private bool isPig = false;
	private bool isHouse = true;
    // Start is called before the first frame update
    void Start()
    {
		if (theTrackable == null)
		{
			Debug.Log ("Warning: Trackable not set !!");
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (swap && theTrackable != null) {
			SwapModel();
			swap = false;
		}
    }
	void onClick(){
		swap = true;
	}
	public void SwapModel() {

		GameObject trackableGameObject = theTrackable.gameObject;

		Debug.Log("Remove current model");
		//disable any pre-existing augmentation
		for (int i = 0; i < trackableGameObject.transform.childCount; i++)
		{
			Transform child = trackableGameObject.transform.GetChild(i);
			child.gameObject.SetActive(false);           
		}
		Debug.Log("Starting Swap");
		if(!isPig){
			// Create a simple cube object
			//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			GameObject pig = Instantiate(Resources.Load("pigY", typeof(GameObject))) as GameObject;
			// Re-parent the cube as child of the trackable gameObject
			pig.transform.parent = theTrackable.transform;

			// Adjust the position and scale
			// so that it fits nicely on the target
			pig.transform.localPosition = new Vector3(0,0.2f,0);
			pig.transform.localRotation = Quaternion.identity;
			pig.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

			// Make sure it is active
			pig.SetActive(true);
			isHouse = false;
			isPig = true;
			Debug.Log("Swapped to Pig");
		}else if(!isHouse){
			// Create a simple cube object
			//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			GameObject house = Instantiate(Resources.Load("tinker", typeof(GameObject))) as GameObject;
			// Re-parent the cube as child of the trackable gameObject
			house.transform.parent = theTrackable.transform;

			// Adjust the position and scale
			// so that it fits nicely on the target
			house.transform.localPosition = new Vector3(0,0.2f,0);
			house.transform.localRotation = Quaternion.Euler(-90.0F, 0, 0);
			house.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

			// Make sure it is active
			house.SetActive(true);
			isPig = false;
			isHouse = true;
			Debug.Log("Swapped to House");
		}
	}
}
