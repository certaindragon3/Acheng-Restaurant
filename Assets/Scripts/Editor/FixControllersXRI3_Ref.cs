using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System.Linq;

public class FixControllersXRI3_Ref : MonoBehaviour
{
    [MenuItem("Acheng/Fix Controllers (Use References)")]
    public static void Fix()
    {
        Debug.Log("Fixing Controllers to Use References...");

        var leftController = GameObject.Find("Left Controller");
        var rightController = GameObject.Find("Right Controller");

        if (leftController == null || rightController == null)
        {
            Debug.LogError("Controllers not found!");
            return;
        }

        // Load all InputActionReferences from the asset
        string actionAssetPath = "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions";
        var allReferences = AssetDatabase.LoadAllAssetsAtPath(actionAssetPath)
            .OfType<InputActionReference>()
            .ToList();

        if (allReferences.Count == 0)
        {
            Debug.LogError($"No InputActionReferences found at {actionAssetPath}. Make sure the asset is imported correctly.");
            return;
        }

        EnsureTrackedPoseDriver(leftController, "Left", allReferences);
        EnsureTrackedPoseDriver(rightController, "Right", allReferences);

        Debug.Log("Controllers Fixed with References.");
    }

    private static void EnsureTrackedPoseDriver(GameObject go, string hand, System.Collections.Generic.List<InputActionReference> refs)
    {
        var driver = go.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
        if (driver == null) driver = go.AddComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();

        // Find references
        // Naming convention in XRI Starter Assets is usually "MapName/ActionName"
        // e.g. "XRI LeftHand/Position"
        
        var posRef = refs.FirstOrDefault(r => r.name == $"XRI {hand}Hand/Position");
        var rotRef = refs.FirstOrDefault(r => r.name == $"XRI {hand}Hand/Rotation");

        if (posRef != null)
        {
            driver.positionInput = new InputActionProperty(posRef);
            Debug.Log($"Assigned Position Reference for {hand}");
        }
        else Debug.LogWarning($"Could not find Position reference for {hand}");

        if (rotRef != null)
        {
            driver.rotationInput = new InputActionProperty(rotRef);
            Debug.Log($"Assigned Rotation Reference for {hand}");
        }
        else Debug.LogWarning($"Could not find Rotation reference for {hand}");
    }
}