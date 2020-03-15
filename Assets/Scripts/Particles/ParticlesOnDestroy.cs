using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDestroy : MonoBehaviour
{
    public GameObject particleSystemG;
    bool quitting=false;

    void Start(){
        quitting=false;
    }
    void OnDestroy(){
        if (!quitting){
            //Debug.Log("death: "+gameObject.name);
            Renderer r = GetComponent<Renderer>();
            Material[] m = r.materials;
            Vector3 size  = r.bounds.size;
            particleSystemG.GetComponent<ParticleSystemRenderer>().materials = m;
            //ParticleSystem particleSystem = particleSystemG.GetComponent<ParticleSystem>();
            //var shape = particleSystem.shape;
            //shape.scale = size;
            GameObject g = Instantiate(particleSystemG, transform.position, Quaternion.identity);
            ParticleSystem ps = g.GetComponent<ParticleSystem>();
            float scaler = Mathf.Max(transform.lossyScale.x,0.5f)*Mathf.Max(transform.lossyScale.y,0.5f)*Mathf.Max(transform.lossyScale.z,0.5f);
            var psmain = ps.main;
            psmain.startLifetime = Mathf.Clamp(scaler, 0.5f, 3f);
            var shape = ps.shape;
            shape.scale = new Vector3(Mathf.Max(transform.lossyScale.x,0.5f),Mathf.Max(transform.lossyScale.y,0.5f),Mathf.Max(transform.lossyScale.z,0.5f));
            shape.rotation = transform.rotation.eulerAngles;
            //Debug.Log("LossyScale: "+scaler+" | clamp: "+Mathf.Clamp(scaler, 0.5f, 3f));
            var em = ps.emission;
            em.rateOverTime=100*scaler;
            ps.Play();
            Destroy(g, Mathf.Clamp(scaler, 0.5f, 3f)+psmain.duration);
        }
    } 

    void OnApplicationQuit()
    {
        quitting = true;
    }
}
