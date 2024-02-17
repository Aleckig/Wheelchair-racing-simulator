using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    [SerializeField] protected float solidAlpha = 1f;
    [SerializeField] protected float clearAlpha = 0f;
    
    [SerializeField] private float fadeOnDuration = 2f;
    public float FadeOnDuration { get { return fadeOnDuration; } }

    [SerializeField] private float fadeOffDuration = 2f;
    public float FadeOffDuration { get { return fadeOffDuration; } }


    [SerializeField] private MaskableGraphic[] graphicsTOFade;

    protected void setAlpha(float alpha)
    {
        foreach (MaskableGraphic graphic in graphicsTOFade)
        {
            if (graphic != null)
            {
                graphic.canvasRenderer.SetAlpha(alpha);
            }
        }
        
    }

    private void Fade(float targetAlpha, float duration)
    {
        foreach (MaskableGraphic graphic in graphicsTOFade)
        {
            if (graphic != null)
            {
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }
    }

    public void FadeOff()
    {
        setAlpha(solidAlpha);
        Fade(clearAlpha, fadeOffDuration);
    }

    public void FadeOn()
    {
        setAlpha(clearAlpha);
        Fade(solidAlpha, fadeOnDuration);
    }
   
}
