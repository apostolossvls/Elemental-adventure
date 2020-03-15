using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : MonoBehaviour
{
    public float evadeForce=300f;
    public float cooldown=3f;
    Rigidbody rig;
    bool onCooldown;
    void Start()
    {
        rig = GetComponentInParent<Rigidbody>();
        onCooldown=false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !onCooldown){
            rig.AddForce(transform.forward*evadeForce);
            StartCoroutine("SetOnCooldown");
        }
    }

    IEnumerator SetOnCooldown (){
        onCooldown=true;
        yield return new WaitForSeconds(cooldown);
        onCooldown=false;
        yield return null;
    }
}
