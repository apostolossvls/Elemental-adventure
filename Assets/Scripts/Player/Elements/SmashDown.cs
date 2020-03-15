using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDown : MonoBehaviour
{
    public Animator animator;
    public float cooldown=1f;
    bool onCooldown;
    // Start is called before the first frame update
    void Start()
    {
        if (animator==null) animator = GetComponent<Animator>();
        onCooldown=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !onCooldown){
            animator.SetTrigger("SmashDown");
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
