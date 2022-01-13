using UnityEngine;

public class Fade : Action
{
    [SerializeField] private Material material;
    [SerializeField] private float fadeDuration;

    private float timePassed;
    protected float progress;

    public override bool PerformAction()
    {
        timePassed += Time.deltaTime;

        progress = timePassed / fadeDuration;
        
        material.SetFloat("_fadeAmount", SetFadeAmount());

        return timePassed >= fadeDuration;
    }

    protected virtual float SetFadeAmount()
    {
        return 0f;
    }

    protected override void Reset()
    {
        timePassed = 0;
        progress = 0;
    }
}
