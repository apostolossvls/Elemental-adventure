using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerOverTime : MonoBehaviour
{
    public List<string> tags = new List<string>();
    public List<string> ElementalException = new List<string>();
    public float damage=1;
    public float tick=1;
    float timer=0;
    List<GameObject> insideGameObjects;

    void Start(){
        insideGameObjects = new List<GameObject>{};
        timer=0;
    }

    void Update(){
        timer+=Time.deltaTime;
        if (timer>=tick){
            timer=0;
            for (int i = 0; i < insideGameObjects.Count; i++)
            {
                GameObject g = insideGameObjects[i];
                if (g==null){
                    insideGameObjects.RemoveAt(i);
                }
                else DealDamage(g);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        bool onList = false;
        foreach (string t in tags)
        {
            if (other.tag==t){
                onList=true;
                break;
            }
        }
        if (onList){
            AddToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool onList = false;
        foreach (string t in tags)
        {
            if (other.tag==t){
                onList=true;
                break;
            }
        }
        if (onList){
            RemoveFromList(other.gameObject);
        }
    }

    void AddToList(GameObject newG){
        bool onList = false;
        foreach (GameObject g in insideGameObjects)
        {
            if (newG==g){
                onList=true;
                break;
            }
        }
        if (!onList){
            insideGameObjects.Add(newG);
        }
    }

    void RemoveFromList(GameObject newG){
        foreach (GameObject g in insideGameObjects)
        {
            if (newG==g){
                insideGameObjects.Remove(g);
                break;
            }
        }
    }

    void DealDamage(GameObject other){
        if (other.GetComponent<Health>()){
            if (other.tag=="Player" && other.GetComponent<ElementManager>()){
                ElementManager em = other.GetComponent<ElementManager>();
                bool flag = false;
                foreach (string s in ElementalException)
                {
                    if (s==em.elementalType){
                        flag=true;
                        break;
                    }
                }
                if (!flag){
                    Health h = other.GetComponent<Health>();
                    h.health -= damage;
                }
            }
            else {
                Health h = other.GetComponent<Health>();
                h.health -= damage;
            }
        }
    }
}
