using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class FixControllersXRI3 : MonoBehaviour
{
    [MenuItem("Acheng/Fix Controllers (XRI 3.3)")]
    public static void Fix()
    {
        Debug.Log("Fixing Controllers for XRI 3.3...");

        var leftController = GameObject.Find("Left Controller");
        var rightController = GameObject.Find("Right Controller");

        if (leftController == null || rightController == null)
        {
            Debug.LogError("Controllers not found!");
            return;
        }

        // In XRI 3.3, we use XRInputModalityManager or separate components for tracking.
        // But the simplest way to get tracking is adding "Tracked Pose Driver (Input System)"
        // usually found in UnityEngine.InputSystem.XR namespace.
        
        EnsureTrackedPoseDriver(leftController, "Left");
        EnsureTrackedPoseDriver(rightController, "Right");
        
        // Also ensure InputActionManager is set up on XR Origin
        var xrOrigin = GameObject.Find("XR Origin (VR)");
        if (xrOrigin != null)
        {
            var iam = xrOrigin.GetComponent<InputActionManager>();
            if (iam == null) iam = xrOrigin.AddComponent<InputActionManager>();
            
            var inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(
                "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions");
            
            if (inputActions != null)
            {
                if (iam.actionAssets == null || !iam.actionAssets.Contains(inputActions))
                {
                    iam.actionAssets = new System.Collections.Generic.List<InputActionAsset> { inputActions };
                    EditorUtility.SetDirty(iam);
                }
            }
        }

        Debug.Log("Controllers Fixed. Added TrackedPoseDriver.");
    }

    private static void EnsureTrackedPoseDriver(GameObject go, string hand)
    {
        // Remove old ActionBasedController if it exists (it's deprecated)
        // var oldController = go.GetComponent<UnityEngine.XR.Interaction.Toolkit.ActionBasedController>();
        // if (oldController != null) DestroyImmediate(oldController);

        // Add Tracked Pose Driver
        var driver = go.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
        if (driver == null)
        {
            driver = go.AddComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
        }

        // Configure Actions
        var inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(
            "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions");

        if (inputActions != null)
        {
            if (hand == "Left")
            {
                driver.positionInput = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Position"));
                driver.rotationInput = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Rotation"));
            }
            else
            {
                driver.positionInput = new InputActionProperty(inputActions.FindAction("XRI RightHand/Position"));
                driver.rotationInput = new InputActionProperty(inputActions.FindAction("XRI RightHand/Rotation"));
            }
        }
    }
}