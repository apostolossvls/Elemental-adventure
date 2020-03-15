using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour
{
    public Transform lookAt;
    public float rotationSpeed = 20f;
    public EnemyMovement1 enemyMovement;
    void Update()
    {
        if (enemyMovement!=null) lookAt = enemyMovement.target;  

        if (lookAt!=null){
            Vector3 pointToLook = lookAt.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pointToLook-transform.position), Time.deltaTime * rotationSpeed);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyMovement.transform.forward), Time.deltaTime * rotationSpeed);
        }
    }
}
