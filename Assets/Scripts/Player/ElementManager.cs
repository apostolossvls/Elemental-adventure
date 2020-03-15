using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public string elementalType;
    public GameObject elementalGameObject;
    public GameObject emptyElementalGameObject;
    //public Transform elementParent;

    public void ChangeElement(ElementShrine el){
        if (el.elementalType != elementalType){
            string tempType = elementalType;
            GameObject tempG = elementalGameObject;
            GameObject tempEmptyG = emptyElementalGameObject;

            Destroy(transform.GetChild(0).gameObject);
            //transform.position = transform.position+Vector3.up;
            GameObject g = Instantiate(el.elementalGameObject, transform.position, transform.rotation);
            g.transform.SetParent(transform);
            g.transform.localPosition = Vector3.zero;
            //g.transform.rotation = transform.rotation;
            //g.transform.SetParent(transform);

            ElementalChanges(el);

            elementalType = el.elementalType;
            elementalGameObject = el.elementalGameObject;
            emptyElementalGameObject = el.emptyElementalGameObject;

            el.elementalType = tempType;
            el.elementalGameObject = tempG;
            el.emptyElementalGameObject = tempEmptyG;
            el.enabled=false;

            Movement movement = GetComponentInChildren<Movement>();
            if (movement)
            {
                movement.footstepPivot = elementalGameObject.transform.Find("bottom");
            }
        }
    }

    public void ElementalChanges(ElementShrine el){
        if (el.elementalType=="EarthElemental"){
            GetComponent<Rigidbody>().mass=5;
        }
        else {
            GetComponent<Rigidbody>().mass=1;
        }
    }
}
