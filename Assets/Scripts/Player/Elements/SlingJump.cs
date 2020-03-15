using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingJump : MonoBehaviour
{
    public Collider col;
    Rigidbody rig;
    public Animator animator;
    PlayerTop playerTop;
    public float JumpForce = 400f;
    public float GlideForce = -50f;
    public float Gravity = 14f;
    float distToGround;
    float holdTime=0;
    public float HoldNeeded=1f;

    void Start()
    {
        if (col==null) col  = GetComponent<Collider>();
        if (rig==null) rig = GetComponentInParent<Rigidbody>(); 
        if (animator==null) animator = GetComponent<Animator>();
        playerTop = GetComponentInChildren<PlayerTop>();
        distToGround = col.bounds.extents.y;
        holdTime=0;
    }
    void Update()
    {
        //if (rig.velocity.y>0)Debug.Log("y>0");
        if (IsGrounded()){
            if (Input.GetKeyUp(KeyCode.Space)){
                rig.AddForce(new Vector3(0,JumpForce*holdTime,0));
                animator.SetBool("SlingJump", false);
                holdTime=0;
            }
            if (Input.GetKeyDown(KeyCode.Space)){
                if (IsGrounded()){
                    animator.SetBool("SlingJump", true);
                }
            }
            if (Input.GetKey(KeyCode.Space)){
                holdTime+=Time.deltaTime;
                if (!animator.GetBool("SlingJump")){
                    animator.SetBool("SlingJump", true);
                }
                //playerTop.enabled=false;
            }
        }
        else {
            rig.AddForce(new Vector3(0,-Gravity*Time.deltaTime,0));
            animator.SetBool("SlingJump", false);
            holdTime=0;
        }
        if (holdTime>HoldNeeded) holdTime = HoldNeeded;
    }

    bool IsGrounded() {
        if (rig.velocity.y==0) return true;
        else return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.2f);
    }
    
    void OnDestroy(){
        animator.SetBool("SlingJump", false);
    }
}
