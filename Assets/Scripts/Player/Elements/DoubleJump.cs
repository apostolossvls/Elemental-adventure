using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public Collider col;
    Rigidbody rig;
    public float JumpForce = 250f;
    public float Gravity = 14f;
    float distToGround;
    bool jumpedTwice=false;

    void Start()
    {
        if (col==null) col  = GetComponent<Collider>();
        if (rig==null) rig = GetComponentInParent<Rigidbody>(); 
        distToGround = col.bounds.extents.y;
    }
    void Update()
    {
        if (IsGrounded()){
            jumpedTwice=false;
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
                rig.AddForce(new Vector3(0,JumpForce,0));
            }
        }
    }

    bool IsGrounded() {
        if (rig.velocity.y==0) return true;
        else return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.2f);
    }
}
