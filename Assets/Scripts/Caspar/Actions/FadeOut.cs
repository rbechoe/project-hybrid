using UnityEngine;

public class FadeOut : Fade
{
    protected override float SetFadeAmount()
    {
        return Mathf.Clamp01(progress);
    }
}
