using UnityEngine;

public class BossTestMovement : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float yaw;
    [SerializeField] private float roll;
    private void Update()
    {
        var rotation = transform.eulerAngles;
        rotation.y = Mathf.Sin(Time.time * frequency) * yaw;
        rotation.z = Mathf.Sin((Time.time + .2f) * frequency) * roll;
        transform.eulerAngles = rotation;
        
        transform.position = Mathf.Sin(Time.time * frequency) * Vector3.right;
    }
}
