using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    CinemachineTransposer transporter;
    public Animator animator;
    int camIndex;
    public float rotationSpeed = 1f;
    Vector3 camV;
    public Vector3 camCenter;

    void Start(){
        transporter = cam.GetCinemachineComponent<CinemachineTransposer>();
        camIndex=0;
        camCenter = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            camIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.E)){
            camIndex--;
        }
        if (camIndex<0) camIndex=3;
        if (camIndex>3) camIndex=0;
        //SetCamera();
        RotateAnimation();
    }

    void SetCamera(){
        Vector3 v;
        if (camIndex==0) {
            v = new Vector3(0,10,-10);
            camCenter = new Vector3(1,1,1);
        }
        else if (camIndex==1) {
            v = new Vector3(-10,10,0);
            camCenter = new Vector3(1,0,-1);
        }
        else if (camIndex==2) {
            v = new Vector3(0,10,10);
            camCenter = new Vector3(-1,1,-1);
        }
        else {
            v = new Vector3(10,10,0);
            camCenter = new Vector3(-1,0,1);
        }
        camV = v;
        transporter.m_FollowOffset = camV;
        //if (camV != transporter.m_FollowOffset) CameraDelayFollow();
    }

    void RotateAnimation(){
        animator.SetInteger("CameraIndex", camIndex);
        if (camIndex==0) {
            camCenter = new Vector3(1,1,1);
        }
        else if (camIndex==1) {
            camCenter = new Vector3(1,0,-1);
        }
        else if (camIndex==2) {
            camCenter = new Vector3(-1,1,-1);
        }
        else {
            camCenter = new Vector3(-1,0,1);
        }
    }

    /*void CameraDelayFollow(){
        transporter.m_FollowOffset = new Vector3(transporter.m_FollowOffset.x + camV.x * Time.deltaTime, transporter.m_FollowOffset.y, transporter.m_FollowOffset.z + camV.x * Time.deltaTime);
    }*/
}
