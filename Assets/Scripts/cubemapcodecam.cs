using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubemapcodecam : MonoBehaviour
{
    public Camera camera;
    public Cubemap map;

    private void Update()
    {
        transform.eulerAngles += Vector3.up * 10 * Time.deltaTime;
        camera.RenderToCubemap(map, 63);
    }
}
