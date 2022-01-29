using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairNormal, crosshairHighlight;
    public GameObject crosshairObj;
    public GameObject playerCenter;
    private Material crosshairMat;
    private float scaleMod = 1;
    private float multiplier = 4;

    private void Start()
    {
        crosshairMat = crosshairObj.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // scale crosshair object based on distance
        crosshairMat.mainTexture = crosshairNormal;
        scaleMod = 10 * multiplier;
        crosshairObj.transform.localPosition = new Vector3(0, 0, 4) * (scaleMod / 10f);
        crosshairObj.transform.localScale = new Vector3(38, 18, 1) * scaleMod * 2;

        // make sure crosshair is in front of an enemy with proper scale
        RaycastHit objectHit;
        if (Physics.Raycast(crosshairObj.transform.position, crosshairObj.transform.forward, out objectHit, 40))
        {
            if (objectHit.transform.CompareTag("Enemy"))
            {
                crosshairMat.mainTexture = crosshairHighlight;
                scaleMod = Vector3.Distance(crosshairObj.transform.parent.position, objectHit.transform.position) / 4;
                crosshairObj.transform.localPosition = new Vector3(0, 0, 1 * multiplier) * (scaleMod / 10f);
                crosshairObj.transform.localScale = new Vector3(38, 18, 1) * scaleMod / 2f;
            }
        }
    }
}
