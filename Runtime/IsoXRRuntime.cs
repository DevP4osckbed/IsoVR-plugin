using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoXRRuntime : MonoBehaviour
{
    public static IsoXRRuntime runtime;

    public IsoXRRig rig;

    public void Start()
    {
        if (IsoXRRig.device == null) Application.Quit();
        rig = IsoXRRig.device;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            IsoAndroidMenu.Toggle();
        }
    }
}
