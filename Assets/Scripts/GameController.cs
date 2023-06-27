using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject bushShape;

    public List<scr_Switch> switches;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void TurnOff()
    {
        foreach(scr_Switch sw in switches) 
        {
            sw.SetEnabled(false);
        }
    }
}
