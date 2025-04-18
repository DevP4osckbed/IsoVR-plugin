using UnityEngine;
using UnityEditor;

public class IsoXRRigMenu
{
    [MenuItem("GameObject/XR/Iso XR Rig", false, 10)]
    static void CreateIsoXRRig(MenuCommand menuCommand)
    {
        // Load your IsoXRRig prefab from Resources
        GameObject prefab = Resources.Load<GameObject>("IsoXRRig");

        if (prefab == null)
        {
            Debug.LogError("IsoXRRig prefab not found in Resources folder!");
            return;
        }

        // Instantiate prefab
        GameObject rig = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // Unpack the prefab
        PrefabUtility.UnpackPrefabInstance(rig, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

        // Align it to context if there's one (e.g., currently selected GameObject)
        GameObjectUtility.SetParentAndAlign(rig, menuCommand.context as GameObject);

        // Register undo and select
        Undo.RegisterCreatedObjectUndo(rig, "Create IsoXRRig");
        Selection.activeObject = rig;
    }
}
