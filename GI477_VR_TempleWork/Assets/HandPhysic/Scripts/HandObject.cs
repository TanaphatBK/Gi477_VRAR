using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandObject : MonoBehaviour
{
    public enum HandMode
    {
        Snap,
        PhysicObj,
    }

    public HandController handController;
    public HandMode handMode;
    public Animator handAnimator;
    public float handSpeed = 5.0f;
    public float handRotateSpeed = 5.0f;
    private Rigidbody rigidbody;
    



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;

        handAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        switch(handMode)
        {
            case HandMode.Snap:
            {
              break;
            }
            case HandMode PhysicObj:
            {
                    UpdatePhysicObj();
              break;
            }
        }

        UpdateAnimation();
    }
    void UpdatePhysicObj()
    {
        Vector3 dir = handController.handAnchor.position - this.transform.position;

        this.transform.rotation = Quaternion.Slerp
            (this.transform.rotation, handController.handAnchor.rotation, handRotateSpeed * Time.deltaTime);


        rigidbody.velocity = dir * handSpeed;
        rigidbody.angularVelocity = Vector3.zero;

    }

    private void UpdateAnimation()
    {
        handAnimator.SetFloat("Index", handController.inputIndex);
        handAnimator.SetFloat("Grip", handController.inputGrip);
        handAnimator.SetFloat("Thumb", handController.inputThumb);
    }
}
