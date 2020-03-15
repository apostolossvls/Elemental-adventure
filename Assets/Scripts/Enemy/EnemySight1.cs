using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight1 : MonoBehaviour
{
    public List<string> TargetTags;
    public List<GameObject> ObjectsInSight;
    //public  List<GameObject> TargetObjects;
    public float fieldOfViewAngle =110f;
    //public SphereCollider col;
    //public HexEnemyMovement enemyMovement;
    //------------
    //public  List<GameObject> ObjectsInSight;
    //public float fieldOfViewAngle =110f;
    //public SphereCollider col;
    public EnemyMovement1 enemyMovement;

    void Start(){
        ObjectsInSight = new List<GameObject>{};
    }
    
    void OnSight(GameObject g){
        ObjectsInSight.Add(g);
        if (g.tag == "Player"){
            enemyMovement.FindTarget(g);
        }
    }

    void LostSight(GameObject g){
        ObjectsInSight.Remove(g);
        if (g.tag == "Player"){
            enemyMovement.LoseTarget();
        }
        Debug.Log("lost tag: "+g.tag);
    }

    bool IsInObjectsInSight(GameObject g){
        if (ObjectsInSight.Find((x) => x.gameObject == g)) return true;
        else return false;
    }

    void OnTriggerStay(Collider other){
        //Vector3 direction = other.transform.position - transform.position;
        //float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log("trigger with: "+ other.name);

        if (other.tag=="Player"){
            RaycastHit[] hits;
            Vector3 direction = other.transform.position - transform.position;
            hits = Physics.RaycastAll(transform.position, direction.normalized, GetComponent<SphereCollider>().radius);
            //float angle = Vector3.Angle(direction, transform.forward);
            //Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.green, 1);
            if (hits!=null){
                Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.red);
                GameObject ghit=null;

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.tag=="Enemy") {
                        continue;
                    }
                    else {
                        ghit = hit.collider.gameObject;
                        break;
                    }
                }

                /*if (hits.Length>0){
                    if (hits[0].collider.gameObject==gameObject) {
                        if (hits.Length>1) ghit=hits[1].collider.gameObject;
                        else ghit=null;
                    }
                    else {
                        ghit = hits[0].collider.gameObject;
                    }
                }*/

                if (ghit==other.gameObject){
                    float angle = Vector3.Angle(direction, transform.forward);
                    if (angle < fieldOfViewAngle * 0.5f){
                        if (!IsInObjectsInSight(ghit)){
                            //Debug.Log("Found something");
                            OnSight(ghit);
                        }
                    }
                    else{
                        if (IsInObjectsInSight(ghit)){
                            Debug.Log("f1");
                            LostSight(ghit);
                        }
                    }
                }
                else {
                    if (IsInObjectsInSight(other.gameObject)){
                        if (ghit) Debug.Log("f2: "+ghit.name);
                        LostSight(other.gameObject);
                    }
                }
            }
        }

        /*RaycastHit hit;
            Vector3 direction = other.transform.position - transform.position;
            //float angle = Vector3.Angle(direction, transform.forward);
            //Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.green, 1);
            if (Physics.Raycast(transform.position + transform.up+ transform.forward*0.6f, direction.normalized, out hit, GetComponent<SphereCollider>().radius, 1)){
                Debug.DrawRay(transform.position + transform.up+ transform.forward*0.6f, direction.normalized, Color.red);
                GameObject ghit = hit.collider.gameObject;
                if (ghit==other.gameObject){
                    float angle = Vector3.Angle(direction, transform.forward);
                    if (angle < fieldOfViewAngle * 0.5f){
                        if (!IsInObjectsInSight(ghit)){
                            //Debug.Log("Found something");
                            OnSight(ghit);
                        }
                    }
                    else{
                        if (IsInObjectsInSight(ghit)){
                            Debug.Log("f1");
                            LostSight(ghit);
                        }
                    }
                }
                else {
                    if (IsInObjectsInSight(other.gameObject)){
                        Debug.Log("f2: "+ghit.name);
                        LostSight(other.gameObject);
                    }
                }
            }
        }
        */
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInObjectsInSight(other.gameObject)){
            Debug.Log("f3");
            LostSight(other.gameObject);
        }
    }
}
