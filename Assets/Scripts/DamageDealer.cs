using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public List<string> tags = new List<string>();
    public List<string> ElementalException = new List<string>();
    public float damage=1;
    
    private void OnTriggerEnter(Collider other)
    {
        bool onList = false;
        foreach (string t in tags)
        {
            if (other.tag==t){
                onList=true;
            }
        }
        if (onList){
            DealDamage(other.gameObject);
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
