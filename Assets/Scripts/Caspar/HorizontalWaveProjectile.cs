using UnityEngine;

public class HorizontalWaveProjectile : WaveProjectile
{
    protected override Vector3 CalculateForce()
    {
        var right = Vector3.Cross(dir.normalized, Vector3.up);

        return dir * speed + right * (Mathf.Sin(Time.time * waveFrequency) * waveAmplitude * easing);
    }
}
