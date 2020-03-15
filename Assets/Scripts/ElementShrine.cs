using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementShrine : MonoBehaviour
{
    public string elementalType;
    public GameObject elementalGameObject;
    public GameObject emptyElementalGameObject;
    public Transform display;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"){
            if (other.GetComponent<ElementManager>()){
                ElementManager e = other.GetComponent<ElementManager>();
                e.ChangeElement(this);
                Destroy(display.GetChild(0).gameObject);
                GameObject g = Instantiate(emptyElementalGameObject, transform.position, transform.rotation);
                g.transform.SetParent(display.transform);
                g.transform.localPosition = Vector3.zero;
            }
        }
    }
}
