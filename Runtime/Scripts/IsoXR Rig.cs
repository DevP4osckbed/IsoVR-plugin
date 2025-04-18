using Isorld.XR.IsoVR;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Management;
using static UnityEngine.GraphicsBuffer;

public class IsoXRRig : MonoBehaviour
{
    public static IsoXRRig device;

    public Camera cameraViewer;
    public CameraOffset cameraOffset;
    public UnityEvent onResume;
    public UnityEvent onPause;
    

    public void Awake()
    {
        if (device == null)
        {
            device = this;
        }
    }

    private void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("Menu/IsoMenu");

        if (prefab != null)
        {
            GameObject plop = Instantiate(prefab, transform);

        }
        else
        {
            Debug.LogError("Could not find prefab in Resources/Prefabs/MenuPrefab");
        }


    }
}

[CustomEditor(typeof(IsoXRRig))]
public class IsoXRRigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the target
        IsoXRRig rig = (IsoXRRig)target;

        // Draw default inspector fields
        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("IsoXR Controls", EditorStyles.boldLabel);
            if (GUILayout.Button("Open Menu"))
            {
                IsoAndroidMenu.Open();
            }

            if (GUILayout.Button("Close Menu"))
            {
                IsoAndroidMenu.Close();
            }

            if (GUILayout.Button("Toggle Menu"))
            {
                IsoAndroidMenu.Toggle();
            }

            EditorGUILayout.HelpBox("This rig handles camera tracking and UI menu for IsoVR. You can control the menu here for quick testing.", MessageType.Info);
        }
    }
}