using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsParticles1 : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.ShapeModule shape;

    void Start()
    {
        ps = transform.parent.GetComponent<ParticleSystem>();
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
        Debug.Log(c.ToString());

        ParticleSystem.MainModule psm = ps.main;
        psm.startColor = c;
    }
}
