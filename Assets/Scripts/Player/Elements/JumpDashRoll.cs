using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDashRoll : MonoBehaviour
{
    public Collider col;
    Rigidbody rig;
    public Animator animator;
    PlayerTop playerTop;
    public float JumpForce = 250f;
    public float DashForce = 150f;
    public float Gravity = 14f;
    float distToGround;
    bool jumpedTwice=false;

    void Start()
    {
        if (col==null) col  = GetComponent<Collider>();
        rig = GetComponentInParent<Rigidbody>(); 
        distToGround = col.bounds.extents.y;
        if (animator==null) animator = GetComponent<Animator>();
        playerTop = GetComponentInChildren<PlayerTop>();
    }
    void Update()
    {
        if (IsGrounded()){
            jumpedTwice=false;
            playerTop.enabled=true;
        }
        else {
            rig.AddForce(new Vector3(0,-Gravity*Time.deltaTime,0));
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            if (IsGrounded()){
                rig.AddForce(new Vector3(0,JumpForce,0));
            }
            else if (!jumpedTwice){
                jumpedTwice=true;
                rig.AddForce(new Vector3(0,JumpForce/4,0));
                rig.AddForce(transform.forward*DashForce);
                animator.SetTrigger("JumpDashRoll");
                playerTop.enabled=false;
            }
        }
    }

    bool IsGrounded() {
        if (rig.velocity.y==0) return true;
        else return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.2f);
    }
}
