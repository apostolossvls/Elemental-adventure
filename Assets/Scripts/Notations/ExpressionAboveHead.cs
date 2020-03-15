using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionAboveHead : MonoBehaviour
{
    public GameObject ExclamationMarkPrefab;
    public GameObject QuestionMarkPrefab;
    public Vector3 pos;
    public EnemyMovement1 enemyMovement;
    public GameObject nowNotation;

    void Start()
    {
        if (enemyMovement==null) enemyMovement = GetComponent<EnemyMovement1>();
    }

    void Update()
    {
        if (enemyMovement!=null){
            bool hasTarget = enemyMovement.hasTarget;
            bool searching = enemyMovement.SearchingTarget;
            if (hasTarget) {
                if (nowNotation!=null){
                    if (nowNotation.name!=ExclamationMarkPrefab.name)
                        ChangeNowNotation(ExclamationMarkPrefab);
                }
                else ChangeNowNotation(ExclamationMarkPrefab);
            }
            else if (searching){
                if (nowNotation!=null){
                    if (nowNotation.name!=QuestionMarkPrefab.name)
                        ChangeNowNotation(QuestionMarkPrefab);
                }
                else ChangeNowNotation(QuestionMarkPrefab);
            }
            else if (nowNotation!=null){
                ChangeNowNotation(null);
            }
        }
        else {
            Debug.Log("ExpressionAboveHead, null");
        }
    }

    void ChangeNowNotation(GameObject g){
        //Debug.Log("change");
        if (nowNotation!=null){
            Destroy(nowNotation);
        }
        if (g!=null) {
            nowNotation = Instantiate(g, transform.position, Quaternion.identity);
            nowNotation.transform.parent = transform;
            nowNotation.transform.localPosition = pos;
            nowNotation.name = g.name;
        }
        else nowNotation = null;
    }
}
