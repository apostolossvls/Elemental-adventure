using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement1 : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    NavMeshAgent agent;
    public bool hasTarget = true;
    public bool FoundOnce=false;
    public bool SearchingTarget=false;
    public Vector3 lastTargetPoint;
    public float rotationSpeed=10;
    public float rotationRandomAmount=100f;
    public float pushForce=100f;
    public float SearchingTargetTimer=0;
    public float SearchingTargetTime=5;
    public float rotationTimer=0;
    public float rotationTime=5;
    bool rotating=false;
    bool hittingarget;
    Quaternion rr;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastTargetPoint=transform.position;
        rr = Random.rotationUniform;
    }


    void Update()
    {
        //animator.SetBool("Run", true);

        if (hasTarget) {
            lastTargetPoint=target.position;
            rotating=false;
            animator.SetBool("Walk", true);
            SearchingTargetTimer=0;
        }
        else{
            if (SearchingTarget) {
                SearchingTargetTimer+=Time.deltaTime;
                if (agent.remainingDistance<=agent.stoppingDistance+0.2f && SearchingTargetTimer>=SearchingTargetTime){
                    SearchingTarget=false;
                    SearchingTargetTimer=0;
                }
                rotating =true;
            }
            if (agent.remainingDistance<=agent.stoppingDistance+0.2f) {
                if (!SearchingTarget) {
                    lastTargetPoint=transform.position;
                }
                animator.SetBool("Walk", false);
                rotationTimer+=Time.deltaTime * (SearchingTarget ? 4 : 1);
                if (rotationTimer>rotationTime+Random.Range(-0.5f,0.5f)){
                    rotationTimer=0;
                    rotating = true;
                    rr = Quaternion.Euler(0,Random.Range(-180f,180f),0);
                }
            }
        }
        agent.SetDestination(lastTargetPoint);
        if (rotating){
            //rr.y = 0; // keep the direction strictly horizontal
            //Quaternion rot = Quaternion.LookRotation(rr);
            //Debug.Log(rot);
            /*if (rr.x<0){
                animator.Play("Walk Left");
            }else {
                animator.Play("Walk Right");
            }*/
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rr, Time.deltaTime * (SearchingTarget ? rotationSpeed*3 : rotationSpeed));
        }
        if (hittingarget){
            hittingarget=false;
            //Debug.Log("Enemy Hit");
            animator.SetTrigger("Attack1");
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag=="Player") {
            FindTarget(g);
            hittingarget=true;
            collision.rigidbody.AddForce((collision.transform.position-transform.position).normalized*pushForce);
            GetComponent<Rigidbody>().AddForce(-(collision.transform.position-transform.position).normalized*pushForce);
            if (collision.relativeVelocity.magnitude > 8){
                Debug.Log("MaxHit");
                //collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(3);
            }
            else {
                Debug.Log("LiteHit");
                //collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(1);
            }
        }
        //Debug.Log("Hit: "+g);
    }

    public void LoseTarget(){
        /*if (hasTarget){
            SearchingTarget=true;
            hasTarget=false;
            target=null;
        }*/
        SearchingTarget=true;
        hasTarget=false;
        target=null;
    }

    public void FindTarget(GameObject g){
        SearchingTarget=false;
        FoundOnce=true;
        hasTarget=true;
        target = g.transform;
    }
}
