using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public GameObject Root;
    RootBush RootScr;

    public float speed = 2.0f;

    int bXsize;
    int bYsize;
    int bZsize;

    // Start is called before the first frame update
    void Start()
    {
        RootScr = Root.GetComponent<RootBush>();
        bXsize = RootScr.xSize;
        bYsize = RootScr.ySize;
        bZsize = RootScr.zSize;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "StaticBush")
        {
            other.gameObject.SetActive(false);

            VoxelAttribs attribs = other.gameObject.GetComponent<VoxelAttribs>();
            attribs.cut = true;

            int x = attribs.x;
            int y = attribs.y;
            int z = attribs.z;

            GameObject adjacent;

            //right voxel check
            if (x + 1 < bXsize - 1)
            {
                adjacent = RootScr.VoxelPosArr[x + 1, y, z];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }

            //left voxel check
            if (x - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x - 1, y, z];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }

            //up voxel check
            if (y + 1 < bYsize - 1)
            {
                adjacent = RootScr.VoxelPosArr[x, y + 1, z];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }

            //bot voxel check
            if (y - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y - 1, z];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }

            //back voxel check
            if (z + 1 < bZsize - 1)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z + 1];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }

            //front voxel check
            if (z - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z - 1];
                if (!adjacent.GetComponent<VoxelAttribs>().cut)
                    adjacent.SetActive(true);
            }
        }
    }
}
