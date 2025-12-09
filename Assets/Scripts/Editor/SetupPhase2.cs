using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using AchengRestaurant.Interaction;

public class SetupPhase2 : MonoBehaviour
{
    [MenuItem("Acheng/Setup Phase 2 (Cooking MVP)")]
    public static void SetupCookingMVP()
    {
        Debug.Log("Setting up Cooking MVP...");

        // 1. Create Knife
        var knife = GameObject.CreatePrimitive(PrimitiveType.Cube);
        knife.name = "Knife";
        knife.transform.position = new Vector3(-0.2f, 0.9f, 1f);
        knife.transform.localScale = new Vector3(0.05f, 0.05f, 0.3f);
        knife.AddComponent<Rigidbody>();
        knife.AddComponent<XRGrabInteractable>();
        knife.AddComponent<SimpleKnife>();
        
        // 2. Create Chopped Pork Prefab (in scene for now, usually should be asset)
        var choppedPork = GameObject.CreatePrimitive(PrimitiveType.Cube);
        choppedPork.name = "ChoppedPork_Prefab";
        choppedPork.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
        choppedPork.AddComponent<Rigidbody>();
        choppedPork.AddComponent<XRGrabInteractable>();
        var ingChopped = choppedPork.AddComponent<Ingredient>();
        ingChopped.currentState = IngredientState.Chopped;
        ingChopped.ingredientName = "Pork";
        
        // Make it a prefab (simplified: just hide it and use as reference)
        choppedPork.SetActive(false);

        // 3. Create Raw Pork
        var rawPork = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rawPork.name = "RawPork";
        rawPork.transform.position = new Vector3(0f, 0.9f, 1f);
        rawPork.transform.localScale = new Vector3(0.15f, 0.1f, 0.15f);
        rawPork.GetComponent<Renderer>().material.color = Color.red; // Raw meat color
        rawPork.AddComponent<Rigidbody>();
        rawPork.AddComponent<XRGrabInteractable>();
        var ingRaw = rawPork.AddComponent<Ingredient>();
        ingRaw.currentState = IngredientState.Raw;
        ingRaw.ingredientName = "Pork";
        
        var choppable = rawPork.AddComponent<Choppable>();
        // We need to link the prefab. Since we can't easily drag-drop in code without AssetDatabase,
        // we'll use a trick or just tell the user to link it. 
        // Actually, we can't assign a scene object as a prefab reference easily if it's destroyed.
        // So for this script, we will just leave it null and log a message.
        Debug.LogWarning("ACTION REQUIRED: Please assign 'ChoppedPork_Prefab' to the 'Chopped Prefab' field on the 'RawPork' object.");

        // 4. Create Pot
        var pot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pot.name = "CookingPot";
        pot.transform.position = new Vector3(0.3f, 0.85f, 1f);
        pot.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);
        
        // Add Socket
        var socket = pot.AddComponent<XRSocketInteractor>();
        socket.attachTransform = pot.transform; // Attach to center
        // Configure socket to only accept ingredients? 
        // For MVP, accepts anything.
        
        pot.AddComponent<CookingPot>();
        
        Debug.Log("Cooking MVP Setup Complete. Don't forget to link the prefab!");
    }
}