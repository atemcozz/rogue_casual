using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTrail : MonoBehaviour
{
    [SerializeField] Color32 averageColor;
    [SerializeField] TrailRenderer trail;
    SpriteRenderer spriteRenderer;
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        averageColor = GetAverageColorFromTexture(spriteRenderer.sprite.texture);
       Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(averageColor, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(0f, 1.0f) }
        );
        trail.colorGradient = gradient;
   
       
    }
    Color32 GetAverageColorFromTexture(Texture2D tex)
        {
        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        float r = 0;
        float g = 0;
        float b = 0;
        for(int i = 0; i < total; i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }
        return new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 0);
    }
}
