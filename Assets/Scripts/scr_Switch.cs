using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using UnityEngine;

public class scr_Switch : MonoBehaviour
    , IColliderEventHoverEnterHandler
{
    public Transform switchObject;
    public bool enabled = false;

    public GameObject bushShape;

    public void SetEnabled(bool value, bool forceSet = false)
    {
        if (ChangeProp.Set(ref enabled, value) || forceSet)
        {
            // change the apperence the switch
            switchObject.localEulerAngles = new Vector3(0f, 0f, value ? 15f : -15f);

            if (value)
            {
                GameObject ctrl = GameObject.Find("GameController(Clone)");

                if (ctrl != null)
                {
                    ctrl.GetComponent<GameController>().bushShape = bushShape;
                }
            }
        }
    }

    private void Start()
    {
        SetEnabled(enabled, true);
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        GameObject.Find("GameController(Clone)").GetComponent<GameController>().TurnOff();
        SetEnabled(!enabled);
    }
}
