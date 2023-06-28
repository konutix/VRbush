using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_MenuButton : MonoBehaviour
    , IColliderEventHoverEnterHandler
{
    public Transform buttonTransform;
    public string nextScene;

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        buttonTransform.position = buttonTransform.position + new Vector3(0,-.025f,0);

        GameObject ctrl = GameObject.Find("GameController(Clone)");

        if (ctrl != null)
        {
            if (!ctrl.GetComponent<GameController>().isTran())
            {
                ctrl.GetComponent<GameController>().ChangeScene(nextScene);
            }
        }
    }
}
