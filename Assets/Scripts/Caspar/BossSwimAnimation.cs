using UnityEngine;

public class BossSwimAnimation : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float yaw;
    [SerializeField] private float roll;

    private Vector3 baseRotation;

    private void Start()
    {
        baseRotation = transform.localEulerAngles;
    }
    
    private void Update()
    {
        var rotation = Vector3.zero;
        rotation.y = Mathf.Sin(Time.time * frequency) * yaw;
        rotation.z = Mathf.Sin((Time.time + .2f) * frequency) * roll;
        transform.localEulerAngles = baseRotation + rotation;
    }
}
