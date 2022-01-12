using UnityEngine;

public class VerticalWaveProjectile : WaveProjectile
{
    protected override Vector3 CalculateForce()
    {
        var up = Vector3.Cross(dir.normalized, Vector3.left);

        return dir * speed + up * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude * easing;
    }
}
