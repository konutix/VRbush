using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject bushShape;

    public List<scr_Switch> switches;

    GameObject darkPanel;

    bool transitioning = false;
    string sceneToGo = "NewestMenu";
    float timer = 0.5f;
    public float tranTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (transitioning)
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;

                if (darkPanel != null)
                {
                    darkPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1.0f - 2.0f * (timer / tranTimer));
                }
            }
            else
            {
                transitioning = false;
                timer = tranTimer;
                SceneManager.LoadScene(sceneToGo);
            }
        }
    }

    public void TurnOff()
    {
        foreach(scr_Switch sw in switches) 
        {
            sw.SetEnabled(false);
        }
    }

    public void ChangeScene(string sceneName)
    {
        transitioning = true;
        sceneToGo = sceneName;
        timer = 1.0f;

        darkPanel = GameObject.Find("DarkPanel");
    }

    public bool isTran() { return transitioning; }
}
