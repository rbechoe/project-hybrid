using System;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [Serializable]
    private class FogProfile
    {
        public float density;
        public Color color;
        public BoxCollider volume;
    }
    
    [SerializeField] private FogProfile[] profiles;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private Transform player;
    
    private Color currentColor;
    private Color targetColor;

    private float currentDensity;
    private float targetDensity;

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
    }
    
    private void Update()
    {
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * transitionSpeed);
        currentDensity = Mathf.Lerp(currentDensity, targetDensity, Time.deltaTime * transitionSpeed);

        RenderSettings.fogColor = currentColor;
        RenderSettings.fogDensity = currentDensity;
    }
    
    private void FixedUpdate()
    {
        foreach (var profile in profiles)
        {
            if (profile.volume.bounds.Contains(player.position))
            {
                targetColor = profile.color;
                targetDensity = profile.density;
            }
        }
    }
}
