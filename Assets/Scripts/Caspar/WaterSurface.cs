using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    private Material material;
    
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void LateUpdate()
    {
        var dir = transform.forward;
        material.SetVector("_SunDirection", -dir);
    }
}
