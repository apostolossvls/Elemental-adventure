using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public bool destroyOnImpact=true;
    public bool destroyOnDdImpact=true;
    public List<string> ExceptionTags = new List<string>();
    public DamageDealer dd;

    private void OnTriggerEnter(Collider other)
    {
        bool flag = false;
        foreach (string s in ExceptionTags)
        {
            if (s==other.tag){
                flag=true;
                break;
            }
        }
        if (!flag){
            if (other.tag!="PlayerPart" && other.tag!="Player"){
                if (destroyOnImpact){
                    if (destroyOnDdImpact){
                        DestroyThis();
                    }
                    else {
                        bool f2=true;
                        foreach (string t in dd.tags)
                        {
                            if (t==other.tag){
                                f2=false;
                                break;
                            }
                        }
                        if (f2){
                            DestroyThis();
                        }
                    }
                }
                else if (destroyOnDdImpact && dd!=null){
                    foreach (string t in dd.tags)
                    {
                        if (t==other.tag){
                            DestroyThis();
                            break;
                        }
                    }
                }
            }
        }
    }

    void DestroyThis(){
        Destroy(gameObject);
    }
}
