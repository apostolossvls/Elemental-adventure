using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rd;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10.0f;
    Vector3 movement;
    public CameraMovement cm;
    public Transform footstepPivot;
    public GameObject footstepsPrefab;

    void Start(){
        if (rd==null && gameObject.GetComponent<Rigidbody>()){
            rd = gameObject.GetComponent<Rigidbody>();
        }
    }

    void Update(){
        if (cm.enabled){
            if (cm.camCenter.y==1){
                movement.x = Input.GetAxisRaw("Horizontal") * cm.camCenter.x;
                movement.z = Input.GetAxisRaw("Vertical") * cm.camCenter.z;
            }
            else {
                movement.x = Input.GetAxisRaw("Vertical") * cm.camCenter.x;
                movement.z = Input.GetAxisRaw("Horizontal") * cm.camCenter.z;
            }
        }
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
        }
        //Debug.DrawLine(rd.position, rd.position +  movement, Color.red, 1f);

    }

    void FixedUpdate(){
        rd.MovePosition(rd.position +  movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * rotationSpeed);
            if (footstepsPrefab & footstepPivot)
            {
                GameObject g = GameObject.Instantiate(footstepsPrefab, footstepPivot.position + new Vector3(0,-0.5f,0), footstepPivot.rotation);
                Destroy(g, 5f);
            }
        }
    }
}
