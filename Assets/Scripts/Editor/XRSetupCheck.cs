using UnityEngine;
using UnityEditor;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using UnityEditor.XR.OpenXR.Features;

public class XRSetupCheck : MonoBehaviour
{
    [MenuItem("Acheng/Check XR Setup")]
    public static void CheckSetup()
    {
        Debug.Log("Checking XR Setup...");

        // 1. Check OpenXR Settings
        var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Standalone);
        if (settings == null)
        {
            Debug.LogError("OpenXR Settings not found for Standalone build target!");
            return;
        }

        Debug.Log($"OpenXR Loader: {(settings != null ? "Found" : "Missing")}");
        
        // 2. Check Interaction Profiles
        var features = settings.GetFeatures<OpenXRInteractionFeature>();
        bool hasProfile = false;
        foreach (var feature in features)
        {
            if (feature.enabled)
            {
                Debug.Log($"Enabled Profile: {feature.name}");
                hasProfile = true;
            }
        }

        if (!hasProfile)
        {
            Debug.LogWarning("NO INTERACTION PROFILES ENABLED! Please enable 'Oculus Touch Controller Profile' and 'HTC Vive Controller Profile' in Project Settings > XR Plug-in Management > OpenXR.");
        }
        else
        {
            Debug.Log("Interaction Profiles check passed.");
        }

        Debug.Log("XR Setup Check Complete.");
    }
}