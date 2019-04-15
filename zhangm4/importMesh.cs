using UnityEngine;

public class Example : MonoBehaviour
{
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {

        //pare mesh from target Mesh Mesh
        //attach to a game object
        //or directly add to the componment of the object
        //like objectName.gameObject.AddComponent(typeof(compontment)) as componment;
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }
}
