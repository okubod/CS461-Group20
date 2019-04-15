using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ObjImporter : MonoBehaviour
{

    private struct tempMesh
    {
        public Vector3[] vertices;
        public Vector3[] normals;
        public Vector2[] uv;
        public Vector2[] uv1;
        public Vector2[] uv2;
        public int[] triangles;
        public int[] faceVertices;
        public int[] faceUVs;
        public Vector3[] faceData;
        public string name;
        public string fileName;
    }

    // Use this for initialization
    public Mesh ImportFile(string filePath)
    {
        tempMesh newMesh = createTemp(filePath);
        populateTempMesh(ref newMesh);

        Vector3[] newVertices = new Vector3[newMesh.faceData.Length];
        Vector2[] newUVs = new Vector2[newMesh.faceData.Length];
        Vector3[] newNormals = new Vector3[newMesh.faceData.Length];
        int i = 0;
        /* The following foreach loops through the facedata and assigns the appropriate vertex, uv, or normal
         * for the appropriate Unity mesh array.
         */
        foreach (Vector3 v in newMesh.faceData)
        {
            newVertices[i] = newMesh.vertices[(int)v.x - 1];
            if (v.y >= 1)
                newUVs[i] = newMesh.uv[(int)v.y - 1];

            if (v.z >= 1)
                newNormals[i] = newMesh.normals[(int)v.z - 1];
            i++;
        }

        Mesh mesh = new Mesh();

        mesh.vertices = newVertices;
        mesh.uv = newUVs;
        mesh.normals = newNormals;
        mesh.triangles = newMesh.triangles;

        mesh.RecalculateBounds();
        ;

        return mesh;
    }

    private static tempMesh createTemp(string filename)
    {
        int triangles = 0;
        int vertices = 0;
        int vt = 0;
        int vn = 0;
        int face = 0;
        tempMesh mesh = new tempMesh();
        mesh.fileName = filename;
        StreamReader stream = File.OpenText(filename);
        string entireText = stream.ReadToEnd();
        stream.Close();
        using (StringReader reader = new StringReader(entireText))
        {
            string currentText = reader.ReadLine();
            char[] splitIdentifier = { ' ' };
            string[] splitString;
            while (currentText != null)
            {
                if (!currentText.StartsWith("f ") && !currentText.StartsWith("v ") && !currentText.StartsWith("vt ")
                    && !currentText.StartsWith("vn "))
                {
                    currentText = reader.ReadLine();
                    if (currentText != null)
                    {
                        currentText = currentText.Replace("  ", " ");
                    }
                }
                else
                {
                    currentText = currentText.Trim();                           //Trim the current line
                    splitString = currentText.Split(splitIdentifier, 50);      //Split the line into an array
                    switch (splitString[0])
                    {
                        case "v":
                            vertices++;
                            break;
                        case "vt":
                            vt++;
                            break;
                        case "vn":
                            vn++;
                            break;
                        case "f":
                            face = face + splitString.Length - 1;
                            triangles = triangles + 3 * (splitString.Length - 2); /* Length is 3 or more, representing one triangle.  each additional vertice, there is a new
                                                                                     triangle*/
                            break;
                    }
                    currentText = reader.ReadLine();
                    if (currentText != null)
                    {
                        currentText = currentText.Replace("  ", " ");
                    }
                }
            }
        }
        mesh.triangles = new int[triangles];
        mesh.vertices = new Vector3[vertices];
        mesh.uv = new Vector2[vt];
        mesh.normals = new Vector3[vn];
        mesh.faceData = new Vector3[face];
        return mesh;
    }

    private static void populateTempMesh(ref tempMesh mesh)
    {
        StreamReader stream = File.OpenText(mesh.fileName);
        string entireText = stream.ReadToEnd();
        stream.Close();
        using (StringReader reader = new StringReader(entireText))
        {
            string currentText = reader.ReadLine();

            char[] splitIdentifier = { ' ' };
            char[] splitIdentifier2 = { '/' };
            string[] splitString;
            string[] twiceSplitString;
            int f = 0;
            int f2 = 0;
            int v = 0;
            int vn = 0;
            int vt = 0;
            int vt1 = 0;
            int vt2 = 0;
            while (currentText != null)
            {
                if (!currentText.StartsWith("f ") && !currentText.StartsWith("v ") && !currentText.StartsWith("vt ") &&
                    !currentText.StartsWith("vn ") && !currentText.StartsWith("g ") && !currentText.StartsWith("usemtl ") &&
                    !currentText.StartsWith("mtllib ") && !currentText.StartsWith("vt1 ") && !currentText.StartsWith("vt2 ") &&
                    !currentText.StartsWith("vc ") && !currentText.StartsWith("usemap "))
                {
                    currentText = reader.ReadLine();
                    if (currentText != null)
                    {
                        currentText = currentText.Replace("  ", " ");
                    }
                }
                else
                {
                    currentText = currentText.Trim();
                    splitString = currentText.Split(splitIdentifier, 50);
                    switch (splitString[0])
                    {
                        case "g":
                            break;
                        case "usemtl":
                            break;
                        case "usemap":
                            break;
                        case "mtllib":
                            break;
                        case "v":
                            mesh.vertices[v] = new Vector3(System.Convert.ToSingle(splitString[1]), System.Convert.ToSingle(splitString[2]),
                                                     System.Convert.ToSingle(splitString[3]));
                            v++;
                            break;
                        case "vt":
                            mesh.uv[vt] = new Vector2(System.Convert.ToSingle(splitString[1]), System.Convert.ToSingle(splitString[2]));
                            vt++;
                            break;
                        case "vt1":
                            mesh.uv[vt1] = new Vector2(System.Convert.ToSingle(splitString[1]), System.Convert.ToSingle(splitString[2]));
                            vt1++;
                            break;
                        case "vt2":
                            mesh.uv[vt2] = new Vector2(System.Convert.ToSingle(splitString[1]), System.Convert.ToSingle(splitString[2]));
                            vt2++;
                            break;
                        case "vn":
                            mesh.normals[vn] = new Vector3(System.Convert.ToSingle(splitString[1]), System.Convert.ToSingle(splitString[2]),
                                                    System.Convert.ToSingle(splitString[3]));
                            vn++;
                            break;
                        case "vc":
                            break;
                        case "f":

                            int j = 1;
                            List<int> intArray = new List<int>();
                            while (j < splitString.Length && ("" + splitString[j]).Length > 0)
                            {
                                Vector3 temp = new Vector3();
                                twiceSplitString = splitString[j].Split(splitIdentifier2, 3);    //Separate the face into parts
                                temp.x = System.Convert.ToInt32(twiceSplitString[0]);
                                if (twiceSplitString.Length > 1)                                  //Some files skip UV and normal
                                {
                                    if (twiceSplitString[1] != "")                                    //Some files skip the uv but not the normal
                                    {
                                        temp.y = System.Convert.ToInt32(twiceSplitString[1]);
                                    }
                                    temp.z = System.Convert.ToInt32(twiceSplitString[2]);
                                }
                                j++;

                                mesh.faceData[f2] = temp;
                                intArray.Add(f2);
                                f2++;
                            }
                            j = 1;
                            while (j + 2 < splitString.Length)     //Create triangles out of the face data
                            {
                                mesh.triangles[f] = intArray[0];
                                f++;
                                mesh.triangles[f] = intArray[j];
                                f++;
                                mesh.triangles[f] = intArray[j + 1];
                                f++;

                                j++;
                            }
                            break;
                    }
                    currentText = reader.ReadLine();
                    if (currentText != null)
                    {
                        currentText = currentText.Replace("  ", " ");       //Some .obj files insert double spaces
                    }
                }
            }
        }
    }
}