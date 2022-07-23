
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public enum HandPriority
    {
        Primary,
        Secondary,
    }

    public Transform handAnchor;
    public HandPriority handPriority;

    public float inputIndex;
    public float inputThumb;
    public float inputGrip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(handPriority == HandPriority.Primary)
        {
            inputIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            inputThumb = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick) ? 1.0f : 0.0f;
            inputGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        }
        else if(handPriority == HandPriority.Secondary)
        {
            inputIndex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            inputThumb = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick) ? 1.0f : 0.0f;
            inputGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        }

        
    }
}
