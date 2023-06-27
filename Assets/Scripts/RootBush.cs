using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RootBush : MonoBehaviour
{
    public GameObject VoxelPrefab;
    public GameObject VoxeltoCutPrefab;
    public GameObject Shape;
    public GameObject FinalShape;

    public Transform marker;
    public Transform bushBase;

    public Voxel[,,] VoxelPosArr;
    public bool shape = false;
    public bool finalShape = false;

    public int xSize = 0;
    public int ySize = 0;
    public int zSize = 0;

    public float voxelSize = 1.0f;

    Vector3 voxelScale;
    float voxelHalf = 0.5f;

    public void GenerateMesh()
    {
        //move marker to the center of bush
        marker.position = new Vector3(
            (float)xSize * 0.5f * voxelSize, (float)ySize * 0.5f * voxelSize, (float)zSize * 0.5f * voxelSize)
            + transform.position;

        GameObject finalShapeObj = Instantiate(FinalShape, marker.position, Quaternion.identity); ;
        GameObject shapeObj = Instantiate(Shape, marker.position, Quaternion.identity);

        for (int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                for( int z = 0; z < zSize; z++)
                {
                    //calculate position
                    float xPos = voxelHalf + (x * voxelSize);
                    float yPos = voxelHalf + (y * voxelSize);
                    float zPos = voxelHalf + (z * voxelSize);
                    Vector3 voxPos = new Vector3(xPos, yPos, zPos) + transform.position;

                    //create voxel
                    VoxelPosArr[x, y, z] = new Voxel();

                    //mark if should be cut
                    GameObject prefabToInst = VoxelPrefab;
                    if (finalShape)
                    {
                        Collider[] hitCollidersToCut = Physics.OverlapSphere(voxPos, 0.0f);
                        VoxelPosArr[x, y, z].shouldCut = true;
                        foreach (var hitCollider in hitCollidersToCut)
                        {
                            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Final"))
                            {
                                VoxelPosArr[x, y, z].shouldCut = false;
                            }
                        }

                        if (VoxelPosArr[x, y, z].shouldCut)
                        {
                            prefabToInst = VoxeltoCutPrefab;
                        }
                    }

                    //Instantiate
                    Vector3 eulerRandRot = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
                    Quaternion randQat = Quaternion.Euler(eulerRandRot);

                    VoxelPosArr[x, y, z].vox = Instantiate(prefabToInst, voxPos, Quaternion.identity);
                    VoxelPosArr[x, y, z].vox.transform.GetChild(0).rotation = randQat;
                    VoxelPosArr[x, y, z].vox.transform.GetChild(1).rotation = randQat;
                    VoxelPosArr[x, y, z].vox.transform.localScale = voxelScale;

                    if (shape)
                    {
                        VoxelPosArr[x, y, z].vox.SetActive(false);
                        VoxelPosArr[x, y, z].cut = true;

                        Collider[] hitColliders = Physics.OverlapSphere(voxPos, 0.0f);
                        foreach (var hitCollider in hitColliders)
                        {
                            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Shape"))
                            {
                                VoxelPosArr[x, y, z].vox.SetActive(true);
                                VoxelPosArr[x, y, z].cut = false;
                            }
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
            for (int db = 0; db < 2; db++)
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
                            bool crs = false;

                            if (!atb.cut && !atb.cross)
                            {
                                //right voxel check
                                if (x + 1 <= xSize - 1)
                                {
                                    adjacent = VoxelPosArr[x + 1, y, z];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //left voxel check
                                if (x - 1 >= 0)
                                {
                                    adjacent = VoxelPosArr[x - 1, y, z];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //up voxel check
                                if (y + 1 <= ySize - 1)
                                {
                                    adjacent = VoxelPosArr[x, y + 1, z];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //bot voxel check
                                if (y - 1 >= 0)
                                {
                                    adjacent = VoxelPosArr[x, y - 1, z];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //back voxel check
                                if (z + 1 <= zSize - 1)
                                {
                                    adjacent = VoxelPosArr[x, y, z + 1];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //front voxel check
                                if (z - 1 >= 0)
                                {
                                    adjacent = VoxelPosArr[x, y, z - 1];
                                    if (!adjacent.cut)
                                    {
                                        adj++;
                                        if (db == 1 && adjacent.cross)
                                            crs = true;
                                    }
                                }

                                //if covered all sides deactivate
                                if (crs)
                                {
                                    VoxelPosArr[x, y, z].vox.SetActive(true);
                                    VoxelPosArr[x, y, z].vox.transform.GetChild(0).gameObject.SetActive(false);
                                    VoxelPosArr[x, y, z].vox.transform.GetChild(1).gameObject.SetActive(true);
                                }
                                else
                                {
                                    if (adj == 6)
                                    {
                                        VoxelPosArr[x, y, z].vox.SetActive(false);
                                        //VoxelPosArr[x, y, z].vox.transform.GetChild(0).gameObject.SetActive(false);
                                        //VoxelPosArr[x, y, z].vox.transform.GetChild(1).gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        VoxelPosArr[x, y, z].vox.transform.GetChild(0).gameObject.SetActive(true);
                                        VoxelPosArr[x, y, z].vox.transform.GetChild(1).gameObject.SetActive(false);
                                        atb.cross = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (finalShape)
        {
            //spawn bush initial and demanded shapes
            Destroy(finalShapeObj);
        }

        if (shape)
        {
            Destroy(shapeObj);
        }
    }

    void Start()
    {
        voxelHalf = voxelSize * 0.5f;
        voxelScale = new Vector3(voxelSize, voxelSize, voxelSize);

        VoxelPosArr = new Voxel[xSize, ySize, zSize];

        //adjust bush base
        bushBase.position = new Vector3(
            (float)xSize * 0.5f * voxelSize, 0.0f, (float)zSize * 0.5f * voxelSize) + transform.position;

        bushBase.localScale =
            new Vector3(((float)xSize + 0.5f) * voxelSize, 0.05f, ((float)zSize + 0.5f) * voxelSize);

        if (Application.isPlaying)
        {
            GenerateMesh();
        }
    }
}
