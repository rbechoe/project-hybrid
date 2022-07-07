using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ITurret))] 
[DisallowMultipleComponent]
public class ShipInputManager : MonoBehaviour
{
    //**Put only one ITurret script on the same GameObject.** 
    [SerializeField] private ITurret inputProvider;
    private ITurret  InputProvider
    {
        get
        {
            if (inputProvider == null)
            {
                inputProvider = GetComponent<ITurret>();
            }
            return inputProvider;
        }
    }

    private void Update()
    {
        //Insert functions that are needed
    }

}
