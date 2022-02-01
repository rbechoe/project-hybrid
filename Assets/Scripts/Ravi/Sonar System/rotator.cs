using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 50;

    void Update()
    {
        transform.localEulerAngles += Vector3.right * (speed * Time.deltaTime);
    }
}
