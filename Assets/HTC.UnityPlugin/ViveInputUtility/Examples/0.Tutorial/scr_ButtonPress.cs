using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_ButtonPress : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("HelloWorld");
    }

    public void OnBackButtonPress()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
