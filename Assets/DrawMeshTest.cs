using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DrawMeshTest : MonoBehaviour
{
    public Mesh _mesh;
    public Material _material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMesh(_mesh,Vector3.zero,Quaternion.identity,_material,0);
    }
}
