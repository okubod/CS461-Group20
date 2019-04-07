using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{
    //create symbol represent access to data
    void Start()
    {
      GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); //difine the shape
      sphere.gameObject.transform.position = new Vector3(0, 0, 0);        //location  of symbol
                                                                          //should be attach to object
      //var boxCollider = sphere.gameObject.AddComponent<BoxCollider>();
      //boxCollider.isTrigger = true;
      onClick oc = sphere.gameObject.AddComponent(typeof(onClick)) as onClick;    //onclick reaction, should open text mesh

      onGUI og = sphere.gameObject.AddComponent(typeof(onGUI)) as onGUI;
      //add other componment to match the change with main component


      GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere); //difine the shape
      sphere1.gameObject.transform.position = new Vector3(3, 0, 0);        //location  of symbol
                                                                          //should be attach to object
      //var boxCollider = sphere.gameObject.AddComponent<BoxCollider>();
      //boxCollider.isTrigger = true;
      onClick oc1 = sphere1.gameObject.AddComponent(typeof(onClick)) as onClick;    //onclick reaction, should open text mesh

      onGUI og1 = sphere1.gameObject.AddComponent(typeof(onGUI)) as onGUI;
      //add other componment to match the change with main component


      GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere); //difine the shape
      sphere2.gameObject.transform.position = new Vector3(-3, 0, 0);        //location  of symbol
                                                                          //should be attach to object
      //var boxCollider = sphere.gameObject.AddComponent<BoxCollider>();
      //boxCollider.isTrigger = true;
      onClick oc2 = sphere2.gameObject.AddComponent(typeof(onClick)) as onClick;    //onclick reaction, should open text mesh

      onGUI og2 = sphere2.gameObject.AddComponent(typeof(onGUI)) as onGUI;
      //add other componment to match the change with main component



    }

}
