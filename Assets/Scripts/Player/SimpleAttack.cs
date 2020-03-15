using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    public Animator animator;

    void Start(){
        if (animator==null) animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            animator.SetTrigger("PunchAttack");
        }
    }
}
