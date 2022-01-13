using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Flashbang : Action
{
    [SerializeField] private Light lantern;
    [SerializeField] private Volume volume;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private float targetLanternIntensity;
    [SerializeField] private float targetBloomIntensity;
    
    private Bloom bloom;
    private bool isAnimating;
    private bool isDone;

    private void Start()
    { 
        volume.profile.TryGet(out bloom);
    }
    
    public override bool PerformAction()
    {
        if (isDone) return true;

        if (!isAnimating)
        {
            StartCoroutine(Flash(lantern.intensity, targetLanternIntensity, bloom.intensity.value, targetBloomIntensity));
        }
        
        return false;
    }

    protected override void Reset()
    {
        isDone = false;
    }
    
    private IEnumerator Flash(float lightStart, float lightTarget, float bloomStart, float bloomTarget)
    {
        isAnimating = true;

        var journey = 0f;
        while (journey <= duration)
        {
            journey += Time.deltaTime;
            var percent = Mathf.Clamp01(journey / duration);
            
            var curvePercent = curve.Evaluate(percent);
            lantern.intensity = Mathf.LerpUnclamped(lightStart, lightTarget, curvePercent);
            bloom.intensity.value = Mathf.LerpUnclamped(bloomStart, bloomTarget, curvePercent);

            yield return null;
        }

        isDone = true;
        isAnimating = false;
    }
}
