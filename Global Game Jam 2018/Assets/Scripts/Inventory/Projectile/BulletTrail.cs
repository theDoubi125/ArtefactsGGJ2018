using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Vector3 startPoint;

    public float fadeDuration = 0.5f;
    private float fadeTime = 0;
    
    Gradient g;
    GradientColorKey[] gck;
    GradientAlphaKey[] gak;

    void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, transform.position);

        g = new Gradient();
        gck = new GradientColorKey[2];
        gck[0].color = Color.white;
        gck[0].time = 0.0F;
        gck[1].color = Color.white;
        gck[1].time = 1.0F;
        gak = new GradientAlphaKey[2];
        gak[0].alpha = 1 - fadeTime/fadeDuration;
        gak[0].time = 0.0F;
        gak[1].alpha = 1 - fadeTime / fadeDuration;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);
    }
	
	void Update ()
    {
        fadeTime += Time.deltaTime;
        if (fadeTime > fadeDuration)
            Destroy(gameObject);
        gak[0].alpha = 1 - fadeTime / fadeDuration;
        gak[0].time = 0.0F;
        gak[1].alpha = 1 - fadeTime / fadeDuration;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);
        lineRenderer.colorGradient = g;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, transform.position);
    }
}
