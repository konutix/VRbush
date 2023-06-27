using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCSpawner : MonoBehaviour
{
    public GameObject GCprefab;

    public List<scr_Switch> switches;

    void Start()
    {
        GameObject gc = GameObject.Find("GameController(Clone)");

        if(gc == null)
        {
            gc = Instantiate(GCprefab);
        }

        gc.GetComponent<GameController>().switches = switches;
    }
}
