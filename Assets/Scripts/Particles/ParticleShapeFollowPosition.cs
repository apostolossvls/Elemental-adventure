using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShapeFollowPosition : MonoBehaviour
{
    ParticleSystem.ShapeModule shape;
    public Transform target;
    public bool activated = true;
    void Start()
    {
        shape = GetComponent<ParticleSystem>().shape;
    }

    void Update()
    {
        if (activated)
        {
            shape.position = target.position;
        }
    }
}
