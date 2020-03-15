using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Animator animator;
    public GameObject fireballPrefab;
    public GameObject sourceObject;
    public float cooldown=1f;
    public float timeBeforeEvent=0.15f;
    public float fireballForce=500f;
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
            animator.SetTrigger("Fireball");
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
        CreateFireball();
        yield return null;
    }

    public void CreateFireball(){
        GameObject fball = Instantiate(fireballPrefab, sourceObject.transform.position, sourceObject.transform.rotation);
        Vector3 v = new Vector3(sourceObject.transform.forward.x, 0, sourceObject.transform.forward.z);
        fball.GetComponent<Rigidbody>().AddForce(sourceObject.transform.forward*fireballForce);
        Destroy(fball, 5f);
    }
}
