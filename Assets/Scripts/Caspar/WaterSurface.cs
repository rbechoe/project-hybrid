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
        material.SetVector("_SunDirection", -transform.forward);
    }
}
