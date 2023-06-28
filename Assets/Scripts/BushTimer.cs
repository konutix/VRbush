using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BushTimer : MonoBehaviour
{
    public TMP_Text timeTMP;
    public float time = 30.0f;
    public bool isTiming = false;
    public bool inTime = true;

    // Start is called before the first frame update
    void Start()
    {
        if (timeTMP != null)
        {
            timeTMP.text = ((int)time).ToString() + " sec";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                inTime = false;
                time = 0;
            }
        }

        if (timeTMP != null)
        {
            timeTMP.text = ((int)time).ToString() + " sec";
        }
    }
}
