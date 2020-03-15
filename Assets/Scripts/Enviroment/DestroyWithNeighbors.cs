using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithNeighbors : MonoBehaviour
{
    public bool destroyLast=false;
    public GameObject[] neighbors;
    void Update()
    {
        if (!destroyLast){
            foreach (GameObject n in neighbors)
            {
                if (n==null){
                    DestroyThis();
                }
            }
        }
        else{
            bool flag=true;
            foreach (GameObject n in neighbors)
            {
                if (n!=null){
                    flag=false;
                }
            }
            if (flag) {
                DestroyThis();
            }
        }
    }

    void DestroyThis(){
        Destroy(gameObject);
    }
}
