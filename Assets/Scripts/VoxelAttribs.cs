using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel
{
    public GameObject vox;

    public bool cut;
    public bool root;
    public bool marked;

    public Voxel()
    {
        vox = null;
        cut = false;
        root = false;
        marked = false;
    }
}

public class VoxelAttribs : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    public int z = 0;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }
}
