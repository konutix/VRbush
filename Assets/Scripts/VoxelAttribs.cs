using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelAttribs : MonoBehaviour
{
    public bool cut = false;

    public int x = 0;
    public int y = 0;
    public int z = 0;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }
}
