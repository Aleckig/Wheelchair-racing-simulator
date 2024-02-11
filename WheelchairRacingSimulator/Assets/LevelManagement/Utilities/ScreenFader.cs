using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float solidAlpha = 1f;
    [SerializeField] private float clearAlpha = 0f;
    [SerializeField] private float fadeDuration = 2f;

    public float FadeDuration { get { return fadeDuration; } }


    [SerializeField] private MaskableGraphic[] graphicsTOFade;

    private void setAlpha(float alpha)
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
        Fade(clearAlpha, fadeDuration);
    }

    public void FadeOn()
    {
        setAlpha(clearAlpha);
        Fade(solidAlpha, fadeDuration);
    }
   
}
