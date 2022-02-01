using UnityEngine;

public class FadeIn : Fade
{
    protected override float SetFadeAmount()
    {
        return Mathf.Clamp01(1 - progress);
    }
}