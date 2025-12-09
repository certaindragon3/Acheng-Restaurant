using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;

public class FixControllers : MonoBehaviour
{
    [MenuItem("Acheng/Fix Controllers")]
    public static void Fix()
    {
        Debug.Log("Fixing Controllers...");

        var leftController = GameObject.Find("Left Controller");
        var rightController = GameObject.Find("Right Controller");

        if (leftController == null || rightController == null)
        {
            Debug.LogError("Controllers not found!");
            return;
        }

        // 1. Add ActionBasedController (The core XRI component for tracking)
        EnsureActionBasedController(leftController, "Left");
        EnsureActionBasedController(rightController, "Right");

        Debug.Log("Controllers Fixed. Please check Inspector to verify Input Actions are assigned.");
    }

    private static void EnsureActionBasedController(GameObject go, string hand)
    {
        var controller = go.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>();
        // Actually, we need the Controller component that drives the position/rotation
        // In XRI 3.x, this is often handled by XRController (Action-based) or similar.
        // Let's add the standard ActionBasedController.
        
        var actionController = go.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>(); 
        // Note: In XRI 3.0, it might be named differently or moved. 
        // Let's try adding the component by string to be safe or use the standard one.
        // The standard component is "ActionBasedController" in older versions, 
        // but in 3.0 it's often just "XRController" with action backing.
        
        // Wait, XRI 3.3 uses "XR Controller (Action-based)" which is the class `ActionBasedController`.
        // But the namespace might be `UnityEngine.XR.Interaction.Toolkit`.
        
        // Let's try to find it.
        var comp = go.GetComponent<UnityEngine.XR.Interaction.Toolkit.ActionBasedController>();
        if (comp == null)
        {
            comp = go.AddComponent<UnityEngine.XR.Interaction.Toolkit.ActionBasedController>();
        }

        // We need to assign the Position and Rotation actions!
        // This is hard to do via script without knowing the exact asset structure.
        // But we can try to load the default asset and find the actions.
        
        var inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(
            "Assets/Samples/XR Interaction Toolkit/3.3.0/Starter Assets/XRI Default Input Actions.inputactions");

        if (inputActions != null)
        {
            if (hand == "Left")
            {
                comp.positionAction = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Position"));
                comp.rotationAction = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Rotation"));
                comp.selectAction = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Select"));
                comp.activateAction = new InputActionProperty(inputActions.FindAction("XRI LeftHand/Activate"));
                comp.uiPressAction = new InputActionProperty(inputActions.FindAction("XRI LeftHand/UI Press"));
            }
            else
            {
                comp.positionAction = new InputActionProperty(inputActions.FindAction("XRI RightHand/Position"));
                comp.rotationAction = new InputActionProperty(inputActions.FindAction("XRI RightHand/Rotation"));
                comp.selectAction = new InputActionProperty(inputActions.FindAction("XRI RightHand/Select"));
                comp.activateAction = new InputActionProperty(inputActions.FindAction("XRI RightHand/Activate"));
                comp.uiPressAction = new InputActionProperty(inputActions.FindAction("XRI RightHand/UI Press"));
            }
            
            // Important: Update Tracking Type
            comp.updateTrackingType = UnityEngine.XR.Interaction.Toolkit.XRController.UpdateType.UpdateAndBeforeRender;
        }
        else
        {
            Debug.LogError("Input Actions asset not found for controller setup!");
        }
    }
}