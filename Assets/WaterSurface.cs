using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField] private Material material;
    
    private void Start()
    {
        material.SetVector("_SunDirection", transform.forward);
    }
}
