using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRMovement : MonoBehaviour
{
    public enum MoveType
    {
        Continue,
        Teleport
    }
    public MoveType moveType;

    public float moveSpeed;
    public float teleportSpeed;

    public Transform eyeTransform;
    public Transform rightHand;

    public LineRenderer lineRend;

    public float teleportRange;

    private bool isIndexTrigger;
    private CharacterController characterController;
    private Vector3 targetPoint;
    public bool isFade;
    public Image image;
    public float speedFade;
    
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();

        targetPoint = this.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (moveType)
        {
            case MoveType.Continue:
            {
                Vector3 velocity = ((OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y * eyeTransform.forward) +
                                   (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y * eyeTransform.right)) * moveSpeed * Time.deltaTime;

                velocity.y -= 10;

                characterController.Move(velocity * Time.deltaTime);
                break;
            }
            case MoveType.Teleport:
            {
                    
                    if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.5f && lineRend.enabled == false)
                    {
                        lineRend.enabled = true;
                    }
                    else if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.5f && lineRend.enabled == true)
                    {
                        lineRend.enabled = false;
                    }

                    if (lineRend.enabled)
                    {
                        lineRend.SetPosition(0, rightHand.position);
                        lineRend.SetPosition(1, rightHand.position + (rightHand.forward * teleportRange));

                        if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.5f && isIndexTrigger == false)
                        {
                            Vector3 originPoint = rightHand.position;
                            Vector3 dir = rightHand.forward;
                            RaycastHit hitInfo;
                            bool isHit = Physics.Raycast(originPoint, dir, out hitInfo, teleportRange);

                            if (isHit == true)
                            {
                                targetPoint = hitInfo.point;
                                isFade = true;
                            }
                            isIndexTrigger = true;
                        }

                        else if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.5f && isIndexTrigger == true)
                        {
                            
                            isIndexTrigger = false;
                        }
                    }

                    Vector3 dirMove = targetPoint - this.transform.position;
                    if(dirMove.magnitude< 0.1f)
                    {
                        isFade = false;
                    }
                    characterController.Move(dirMove * teleportSpeed * Time.deltaTime);

                    if(isFade == true)
                    {
                        Color color = image.color;
                        color.a += speedFade * Time.deltaTime;
                        if (color.a >= 1)
                        {
                            color.a = 1;
                        }
                        image.color = color;
                    }
                    else if(isFade == false)
                    {
                        Color color = image.color;
                        color.a -= speedFade * Time.deltaTime;
                        if (color.a < 0)
                        {
                            color.a = 0;
                        }
                        image.color = color;
                    }
                    break;
            }
        }
    }
}
