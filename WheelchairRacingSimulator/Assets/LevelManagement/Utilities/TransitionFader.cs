using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFader : ScreenFader
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float delay = 0.3f;
    public float Delay { get { return delay; } }                                                

    protected void Awake()
    {
        // Ensure that lifeTime is at least FadeOnDuration + FadeOffDuration + delay
        lifeTime = Mathf.Clamp(lifeTime, FadeOnDuration + FadeOffDuration + delay, float.MaxValue);
    }

    private IEnumerator PlayRoutine()
    {
        SetAlpha(clearAlpha);

        yield return new WaitForSeconds(delay);

        FadeOn();
        float onTime = lifeTime - (FadeOffDuration + delay);
        yield return new WaitForSeconds(onTime);

        FadeOff();
        yield return new WaitForSeconds(FadeOffDuration);

        // Delay the destruction of the gameObject after FadeOff
        UnityEngine.Object.Destroy(gameObject);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    public static void PlayTransition(TransitionFader transitionPrefab)
    {
        if (transitionPrefab != null)
        {
            TransitionFader instance = Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
            instance.Play();
        }
    }
}
