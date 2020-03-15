using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsParticles1 : MonoBehaviour
{
    public Transform feet;
    public bool activated = true;
    ParticleSystem ps;
    ParticleSystem.ShapeModule shape;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        shape = ps.shape;

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, 1))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Color c = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

        ParticleSystem.MainModule psm = ps.main;
        psm.startColor = c;
    }

    void Update()
    {
        if (activated)
        {
            shape.position = new Vector3(feet.position.x, feet.position.z, feet.position.y);
            shape.rotation = new Vector3(shape.rotation.x, shape.rotation.y, feet.rotation.eulerAngles.y);
        }

        //tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        //tex.Apply();
    }

    public void ChangeTarget(Transform t)
    {
        feet = t;
    }
}
