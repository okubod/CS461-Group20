using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGUI : MonoBehaviour
{
    void OnGUI() {
      if (GUI.RepeatButton (new Rect (40, 60, 20, 20), "+")) {
            gameObject.transform.localScale *= 1.1f;
      }
      if (GUI.RepeatButton (new Rect (80, 60, 20, 20), "-")) {
            gameObject.transform.localScale *= 0.9f;
      }
      if (GUI.RepeatButton (new Rect (40, 100, 20, 20), "x")) {
            gameObject.transform.Rotate(10, 0, 0);
      }
      if (GUI.RepeatButton (new Rect (80, 100, 20, 20), "y")) {
            gameObject.transform.Rotate(0,10,0);
      }
      if (GUI.RepeatButton (new Rect (120, 100, 20, 20), "z")) {
            gameObject.transform.Rotate(0,0,10);
      }
      if (GUI.RepeatButton (new Rect (120, 140, 20, 20), "1")) {
        Camera.main.transform.position += new Vector3(0,0,0.1f);
      }
      if (GUI.RepeatButton (new Rect (160, 140, 20, 20), "1")) {
        Camera.main.transform.position -= new Vector3(0,0, 0.1f);
      }
    }
}
