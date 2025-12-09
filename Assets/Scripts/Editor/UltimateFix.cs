using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using System.Linq;
using System.Collections.Generic;

public class UltimateFix : MonoBehaviour
{
    [MenuItem("Acheng/Ultimate Fix (Controllers & Movement)")]
    public static void RunFix()
    {
        Debug.Log("Starting Ultimate Fix...");

        // 1. Load Input Action Asset
        string assetPath = "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions";
        var actionAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(assetPath);
        
        if (actionAsset == null)
        {
            Debug.LogError("Could not find Input Action Asset!");
            return;
        }

        // Load all sub-assets (References)
        var allAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
        var references = allAssets.OfType<InputActionReference>().ToList();
        Debug.Log($"Found {references.Count} Input Action References.");

        // 2. Fix Controllers (Tracked Pose Driver)
        FixController("Left Controller", "XRI LeftHand/Position", "XRI LeftHand/Rotation", references);
        FixController("Right Controller", "XRI RightHand/Position", "XRI RightHand/Rotation", references);

        // 3. Fix Movement (Add Standard Providers)
        var xrOrigin = GameObject.Find("XR Origin (VR)");
        if (xrOrigin != null)
        {
            // Continuous Move
            var moveProvider = xrOrigin.GetComponent<ContinuousMoveProvider>();
            if (moveProvider == null) moveProvider = xrOrigin.AddComponent<ContinuousMoveProvider>();
            
            var moveRef = references.FirstOrDefault(r => r.name == "XRI LeftHand/Move");
            if (moveRef != null)
            {
                // XRI 3.3 uses XRInputValueReader
                var reader = new UnityEngine.XR.Interaction.Toolkit.Inputs.Readers.XRInputValueReader<Vector2>("Left Hand Move");
                reader.inputActionReference = moveRef;
                moveProvider.leftHandMoveInput = reader;
                Debug.Log("Configured Continuous Move Provider.");
            }

            // Snap Turn
            var turnProvider = xrOrigin.GetComponent<SnapTurnProvider>();
            if (turnProvider == null) turnProvider = xrOrigin.AddComponent<SnapTurnProvider>();
            
            var turnRef = references.FirstOrDefault(r => r.name == "XRI RightHand/Turn");
            if (turnRef != null)
            {
                var reader = new UnityEngine.XR.Interaction.Toolkit.Inputs.Readers.XRInputValueReader<Vector2>("Right Hand Turn");
                reader.inputActionReference = turnRef;
                turnProvider.leftHandTurnInput = reader;
                turnProvider.rightHandTurnInput = reader;
                Debug.Log("Configured Snap Turn Provider.");
            }
            
            // Disable custom controller if it exists to avoid conflict
            var customCtrl = xrOrigin.GetComponent<AchengRestaurant.Core.XRMovementController>();
            if (customCtrl != null)
            {
                customCtrl.enabled = false;
                Debug.Log("Disabled custom XRMovementController to use standard XRI providers.");
            }
        }

        Debug.Log("Ultimate Fix Complete!");
    }

    private static void FixController(string objName, string posActionName, string rotActionName, List<InputActionReference> refs)
    {
        var go = GameObject.Find(objName);
        if (go == null) return;

        var driver = go.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
        if (driver == null) driver = go.AddComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();

        var posRef = refs.FirstOrDefault(r => r.name == posActionName);
        var rotRef = refs.FirstOrDefault(r => r.name == rotActionName);

        if (posRef != null)
        {
            // Create property with reference
            var prop = new InputActionProperty(posRef);
            driver.positionInput = prop;
        }
        
        if (rotRef != null)
        {
            var prop = new InputActionProperty(rotRef);
            driver.rotationInput = prop;
        }
        
        EditorUtility.SetDirty(driver);
        Debug.Log($"Fixed {objName}");
    }
}