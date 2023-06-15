using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SawScript : MonoBehaviour
{
    public GameObject Root;
    RootBush RootScr;

    HashSet<Voxel> toDestroy;

    public TMP_Text pointsTMP;

    public int points = 0;

    int bXsize;
    int bYsize;
    int bZsize;

    // Start is called before the first frame update
    void Start()
    {
        RootScr = Root.GetComponent<RootBush>();

        toDestroy = new HashSet<Voxel>();

        bXsize = RootScr.xSize;
        bYsize = RootScr.ySize;
        bZsize = RootScr.zSize;
    }

    // Update is called once per frame
    void Update()
    {
        HashSet<Voxel> newToDestroy = new HashSet<Voxel>();
        
        foreach (Voxel v in toDestroy)
        {
            v.vox.SetActive(false);
            v.cut = true;

            //give or sub points and update score
            if(v.shouldCut) 
            {
                points++;
            }
            else
            {
                points--;
            }
            pointsTMP.text = points.ToString();

            VoxelAttribs attribs = v.vox.gameObject.GetComponent<VoxelAttribs>();

            int x = attribs.x;
            int y = attribs.y;
            int z = attribs.z;

            Voxel adjacent;

            //right voxel check
            if (x + 1 < bXsize)
            {
                adjacent = RootScr.VoxelPosArr[x + 1, y, z];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

            //left voxel check
            if (x - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x - 1, y, z];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

            //up voxel check
            if (y + 1 < bYsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y + 1, z];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

            //bot voxel check
            if (y - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y - 1, z];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

            //back voxel check
            if (z + 1 < bZsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z + 1];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

            //front voxel check
            if (z - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z - 1];
                if (!adjacent.cut)
                {
                    if (!newToDestroy.Contains(adjacent))
                    {
                        newToDestroy.Add(adjacent);
                    }
                }
            }

        }

        toDestroy = newToDestroy;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "StaticBush")
        {
            other.gameObject.SetActive(false);

            VoxelAttribs attribs = other.gameObject.GetComponent<VoxelAttribs>();

            int x = attribs.x;
            int y = attribs.y;
            int z = attribs.z;

            RootScr.VoxelPosArr[x, y, z].cut = true;

            //give or sub points and update score
            if (RootScr.VoxelPosArr[x, y, z].shouldCut)
            {
                points++;
            }
            else
            {
                points--;
            }
            pointsTMP.text = points.ToString();

            Voxel adjacent;

            //up voxel check
            if (y + 1 < bYsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y + 1, z];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x, y + 1, z);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }

            //right voxel check
            if (x + 1 < bXsize)
            {
                adjacent = RootScr.VoxelPosArr[x + 1, y, z];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x + 1, y, z);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }

            //left voxel check
            if (x - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x - 1, y, z];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x - 1, y, z);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }

            //back voxel check
            if (z + 1 < bZsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z + 1];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x, y, z + 1);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }

            //front voxel check
            if (z - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z - 1];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x, y, z - 1);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }

            //bot voxel check
            if (y - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y - 1, z];
                if (!adjacent.cut)
                {
                    bool rooted = CheckRooted(x, y - 1, z);

                    if (rooted)
                    {
                        adjacent.vox.SetActive(true);
                    }
                    else
                    {
                        toDestroy.Add(adjacent);
                    }
                }
            }
        }
    }

    private bool CheckRooted(int x, int y, int z)
    {
        HashSet<Voxel> visited = new HashSet<Voxel>();
        Stack<Voxel> stack = new Stack<Voxel>();
        stack.Push(RootScr.VoxelPosArr[x, y, z]);

        while (stack.Count > 0)
        {
            Voxel current = stack.Pop();
            visited.Add(current);

            VoxelAttribs atb = current.vox.GetComponent<VoxelAttribs>();
            x = atb.x;
            y = atb.y;
            z = atb.z;

            if (current.root)
            {
                return true;
            }

            Voxel adjacent;

            //up voxel check
            if (y + 1 < bYsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y + 1, z];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }

            //right voxel check
            if (x + 1 < bXsize)
            {
                adjacent = RootScr.VoxelPosArr[x + 1, y, z];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }

            //left voxel check
            if (x - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x - 1, y, z];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }

            //back voxel check
            if (z + 1 < bZsize)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z + 1];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }

            //front voxel check
            if (z - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y, z - 1];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }

            //bot voxel check
            if (y - 1 >= 0)
            {
                adjacent = RootScr.VoxelPosArr[x, y - 1, z];
                if (!adjacent.cut)
                {
                    if (!visited.Contains(adjacent))
                    {
                        stack.Push(adjacent);
                    }
                }
            }
        }

        return false;
    }
}
