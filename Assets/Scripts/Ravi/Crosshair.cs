using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairNormal, crosshairHighlight;
    public GameObject crosshairObj;
    public GameObject playerCenter;
    Material crosshairMat;
    float scaleMod = 1;
    float multiplier = 4;

    void Start()
    {
        crosshairMat = crosshairObj.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        crosshairMat.mainTexture = crosshairNormal;
        scaleMod = 10 * multiplier;
        crosshairObj.transform.localPosition = new Vector3(0, 0, 4) * (scaleMod / 10f);
        crosshairObj.transform.localScale = new Vector3(38, 18, 1) * scaleMod * 2;

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
