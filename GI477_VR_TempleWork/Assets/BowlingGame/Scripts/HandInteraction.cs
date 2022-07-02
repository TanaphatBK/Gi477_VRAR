using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    public GameObject handDirectionObj;

    public OVRInput.Axis1D inputActive;
    public Transform holdObjPoint;
    public List<string> ignoreTags;
    public float activeThresold;

    private bool bIsActive;
    private GameObject selectedObj;
    private Vector3 lastPosition;
    private Vector3 forceDir;
    [SerializeField]private float forcePower;

    private void Update()
    {
        forceDir = this.transform.position - lastPosition;

        UpdateGrabObj();

        lastPosition = this.transform.position;
    }

    private void UpdateGrabObj()
    {
       if(OVRInput.Get(inputActive) >= activeThresold && !bIsActive)
        {
            OnGrabObject();
            bIsActive = true;
        }
       else if(OVRInput.Get(inputActive) < activeThresold && !bIsActive)
        {
            OnReleaseObject();
            bIsActive = false;
        }
    }

    public void OnGrabObject()
    {
        if(selectedObj == null)
        {
            return;
        }

        selectedObj.GetComponent<Rigidbody>().isKinematic = true;
        selectedObj.transform.SetParent(holdObjPoint);
        selectedObj.transform.localPosition = Vector3.zero;
    }

    public void OnReleaseObject()
    {
        if(selectedObj == null)
        {
            return;
        }

        selectedObj.transform.SetParent(null);
        Rigidbody rigid = selectedObj.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        //rigid.velocity = forceDir * forcePower;
        rigid.AddForce(forceDir * forcePower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(var tag in ignoreTags)
        {
            if(other.tag == tag)
            {
                return;
            }
        }
        selectedObj = other.gameObject;
    }    

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == selectedObj)
        {
            selectedObj = null;
        }
    }
}
