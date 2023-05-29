using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RootBush : MonoBehaviour
{
    public GameObject VoxelPrefab;

    public Voxel[,,] VoxelPosArr;
    public bool shape = false;

    public int xSize = 0;
    public int ySize = 0;
    public int zSize = 0;

    public float voxelSize = 1.0f;

    Vector3 voxelScale;
    float voxelHalf = 0.5f;

    public void GenerateMesh()
    {
        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                for( int z = 0; z < zSize; z++)
                {
                    float xPos = voxelHalf + (x * voxelSize);
                    float yPos = voxelHalf + (y * voxelSize);
                    float zPos = voxelHalf + (z * voxelSize);

                    Vector3 voxPos = new Vector3(xPos, yPos, zPos) + transform.position;
                    VoxelPosArr[x, y, z] = new Voxel();
                    VoxelPosArr[x, y, z].vox = Instantiate(VoxelPrefab, voxPos, Quaternion.identity);
                    VoxelPosArr[x, y, z].vox.transform.localScale = voxelScale;

                    if (shape)
                    {
                        VoxelPosArr[x, y, z].vox.SetActive(false);
                        VoxelPosArr[x, y, z].cut = true;

                        Collider[] hitColliders = Physics.OverlapSphere(voxPos, 0.0f);
                        foreach (var hitCollider in hitColliders)
                        {
                            VoxelPosArr[x, y, z].vox.SetActive(true);
                            VoxelPosArr[x, y, z].cut = false;
                        }
                    }
                    else
                    {
                        if (!(x == xSize - 1 || y == ySize - 1 || z == zSize - 1 || x == 0 || z == 0))
                            VoxelPosArr[x, y, z].vox.SetActive(false);
                    }

                    VoxelAttribs atb = VoxelPosArr[x, y, z].vox.GetComponent<VoxelAttribs>();
                    atb.x = x; 
                    atb.y = y; 
                    atb.z = z;

                    if(y == 0)
                    {
                        VoxelPosArr[x, y, z].root = true;
                    }
                }
            }
        }

        //check adjacent voxels
        if (shape)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < zSize; z++)
                    {
                        Voxel atb = VoxelPosArr[x, y, z];

                        Voxel adjacent;
                        int adj = 0;

                        if (!atb.cut)
                        {
                            //right voxel check
                            if (x + 1 < xSize - 1)
                            {
                                adjacent = VoxelPosArr[x + 1, y, z];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //left voxel check
                            if (x - 1 >= 0)
                            {
                                adjacent = VoxelPosArr[x - 1, y, z];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //up voxel check
                            if (y + 1 < ySize - 1)
                            {
                                adjacent = VoxelPosArr[x, y + 1, z];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //bot voxel check
                            if (y - 1 >= 0)
                            {
                                adjacent = VoxelPosArr[x, y - 1, z];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //back voxel check
                            if (z + 1 < zSize - 1)
                            {
                                adjacent = VoxelPosArr[x, y, z + 1];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //front voxel check
                            if (z - 1 >= 0)
                            {
                                adjacent = VoxelPosArr[x, y, z - 1];
                                if (!adjacent.cut)
                                    adj++;
                            }

                            //if covered all sides deactivate
                            if (adj == 6)
                            {
                                VoxelPosArr[x, y, z].vox.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    void Start()
    {
        voxelHalf = voxelSize * 0.5f;
        voxelScale = new Vector3(voxelSize, voxelSize, voxelSize);

        VoxelPosArr = new Voxel[xSize, ySize, zSize];

        GenerateMesh();
    }
}
