using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using System.Collections.Generic;

public class SetupPhase1 : MonoBehaviour
{
    [MenuItem("Acheng/Run Setup Phase 1")]
    public static void Run()
    {
        Debug.Log("Starting Setup Phase 1...");
        
        // 1. Setup Input Action Manager
        var xrOrigin = GameObject.Find("XR Origin (VR)");
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin (VR) not found!");
            return;
        }

        var inputManager = xrOrigin.GetComponent<InputActionManager>();
        if (inputManager == null) inputManager = xrOrigin.AddComponent<InputActionManager>();

        var inputActions = AssetDatabase.LoadAssetAtPath<UnityEngine.InputSystem.InputActionAsset>(
            "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions");
        
        if (inputActions != null)
        {
            if (inputManager.actionAssets == null) inputManager.actionAssets = new List<UnityEngine.InputSystem.InputActionAsset>();
            if (!inputManager.actionAssets.Contains(inputActions))
            {
                inputManager.actionAssets.Add(inputActions);
                EditorUtility.SetDirty(inputManager);
                Debug.Log("Input Action Manager configured.");
            }
        }
        else
        {
            Debug.LogError("Input Actions asset not found!");
        }

        // 2. Add Teleportation Provider
        var teleProvider = xrOrigin.GetComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider>();
        if (teleProvider == null)
        {
            teleProvider = xrOrigin.AddComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider>();
            Debug.Log("Teleportation Provider added.");
        }

        // 3. Setup Floor
        var floor = GameObject.Find("Floor");
        if (floor != null)
        {
            var teleArea = floor.GetComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationArea>();
            if (teleArea == null)
            {
                teleArea = floor.AddComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationArea>();
                Debug.Log("Teleportation Area added to Floor.");
            }
        }
        
        Debug.Log("Setup Phase 1 Complete!");
    }
}