using UnityEngine;

public class SpirallingWaveProjectile : WaveProjectile
{
    protected override Vector3 CalculateForce()
    {
        var up = Vector3.Cross(dir.normalized, Vector3.left);
        var right = Vector3.Cross(dir.normalized, Vector3.up);

        var sin = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude * easing;
        var cos = Mathf.Cos(Time.time * waveFrequency) * waveAmplitude * easing;
        
        return dir * speed + up * sin + right * cos;
    }
}
