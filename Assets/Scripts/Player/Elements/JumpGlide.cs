using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGlide : MonoBehaviour
{
    public Collider col;
    Rigidbody rig;
    public Animator animator;
    PlayerTop playerTop;
    public float JumpForce = 250f;
    public float GlideForce = -50f;
    public float Gravity = 14f;
    float distToGround;

    void Start()
    {
        if (col==null) col  = GetComponent<Collider>();
        if (animator==null) animator = GetComponent<Animator>();
        if (rig==null) rig = GetComponentInParent<Rigidbody>(); 
        playerTop = GetComponentInChildren<PlayerTop>();
        distToGround = col.bounds.extents.y;
    }
    void Update()
    {
        if (rig.velocity.y>0)Debug.Log("y>0");
        if (Input.GetKeyDown(KeyCode.Space)){
            if (IsGrounded()){
                rig.AddForce(new Vector3(0,JumpForce,0));
            }
        }
        if (Input.GetKey(KeyCode.Space) && !IsGrounded() && rig.velocity.y<0){
            rig.AddForce(new Vector3(0,GlideForce*Time.deltaTime,0));
            rig.useGravity=false;
            animator.SetBool("JumpGlide", true);
            playerTop.enabled=false;
        }
        if (Input.GetKeyUp(KeyCode.Space) || IsGrounded()){
            rig.useGravity=true;
            animator.SetBool("JumpGlide", false);
            playerTop.enabled=true;
        }
        else if (!IsGrounded()){
            rig.AddForce(new Vector3(0,-Gravity*Time.deltaTime,0));
        }
    }

    bool IsGrounded() {
        if (rig.velocity.y==0) return true;
        else return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.2f);
    }

    void OnDestroy(){
        rig.useGravity=true;
        animator.SetBool("JumpGlide", false);
        playerTop.enabled=true;
    }
}
