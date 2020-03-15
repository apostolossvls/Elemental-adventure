using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTop : MonoBehaviour
{
    public Transform body;
    public Transform chest;
    public float rotationSpeed = 5f;
    void Update()
    {
        //Debug.DrawRay(chest.transform.position, chest.transform.forward*3, Color.blue, 1);
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(m);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength)){
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //pointToLook = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
            //transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, pointToLook.z));
            //if (Quaternion.LookRotation(pointToLook-transform.position) * Quaternion.Inverse(body.transform.rotation))
            //Quaternion thisRot = Quaternion.LookRotation(pointToLook-transform.position) * Quaternion.Inverse(body.transform.rotation);
            //bool f=false;
            //if (thisRot.x<-0.8) f=true;
            //else if (thisRot.y<-0.8) f=true;
            //else if (thisRot.z<-0.8) f=true;
            //else if (thisRot.w<-0.8) f=true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pointToLook-transform.position), Time.deltaTime * rotationSpeed);

            //Quaternion chestRot = Quaternion.LookRotation(pointToLook-transform.position) * Quaternion.Inverse(body.transform.rotation);
            //chestRot = Quaternion.Slerp(chest.transform.rotation, chestRot, 0.5f);
            //chest.transform.rotation = Quaternion.Slerp(chest.transform.rotation, chestRot, Time.deltaTime * rotationSpeed);
            //transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }
        //transform.LookAt(new Vector3(m.x, transform.position.y, m.z));
    }
}
