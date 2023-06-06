using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using UnityEngine;

public class scr_Switch : MonoBehaviour
    , IColliderEventHoverEnterHandler
{
    public Transform switchObject;
    public bool enabled = false;

    public void SetEnabled(bool value, bool forceSet = false)
    {
        if (ChangeProp.Set(ref enabled, value) || forceSet)
        {
            // change the apperence the switch
            switchObject.localEulerAngles = new Vector3(0f, 0f, value ? 15f : -15f);

            // Change the global gravity in the scene
            if (value)
            {
                Debug.Log("Turned ON");
            }
            else
            {
                Debug.Log("Turned OFF");
            }
        }
    }

    private void Start()
    {
        SetEnabled(enabled, true);
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        SetEnabled(!enabled);
    }
}
