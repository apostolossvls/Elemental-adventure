using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterThrow : MonoBehaviour
{
    public Animator animator;
    public GameObject waterProjectilePrefab;
    public GameObject sourceObject;
    public float cooldown=1f;
    public float timeBeforeEvent=0.15f;
    public float waterForce=500f;
    bool onCooldown;
    // Start is called before the first frame update
    void Start()
    {
        onCooldown=false;
        if (animator==null) animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !onCooldown){
            animator.SetTrigger("WaterThrow");
            StartCoroutine("WaitForEvent");
            StartCoroutine("SetOnCooldown");
        }
    }

    IEnumerator SetOnCooldown (){
        onCooldown=true;
        yield return new WaitForSeconds(cooldown);
        onCooldown=false;
        yield return null;
    }

    IEnumerator WaitForEvent (){
        yield return new WaitForSeconds(timeBeforeEvent);
        CreateWaterPojectile();
        yield return null;
    }

    public void CreateWaterPojectile(){
        GameObject wp = Instantiate(waterProjectilePrefab, sourceObject.transform.position, sourceObject.transform.rotation);
        wp.GetComponent<Rigidbody>().AddForce(sourceObject.transform.forward*waterForce);
        Destroy(wp, 5f);
    }
}
