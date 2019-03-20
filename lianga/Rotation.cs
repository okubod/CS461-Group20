using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            //transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("s"))
        {
            //transform.Rotate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("d"))
        {
            //transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("a"))
        {
            //transform.Rotate(Vector3.left * speed * Time.deltaTime);
        }

    }
}
