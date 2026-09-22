using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D, ShadowCaster2D (URP 2D)

/// <summary>
/// Makes the area behind light blockers fully dark.
/// Put this on any GameObject in the scene (e.g. an empty "LightingSetup"),
/// drag the cube / walls into "Blockers", and assign the lights.
/// Runs in Edit mode too, so the result shows in the Scene view.
/// </summary>
[ExecuteAlways]
public class ShadowSetup : MonoBehaviour
{
    [Header("Objects that block light")]
    public List<GameObject> blockers = new List<GameObject>();

    [Tooltip("Blocker stays lit on the side facing the light (recommended: off).")]
    public bool selfShadows = false;

    [Header("Light source (the white ball's Spot Light 2D)")]
    public Light2D spotLight;
    [Range(0f, 1f)] public float shadowIntensity = 1f; // 1 = no light passes through

    [Header("Global Light 2D (ambient light)")]
    public Light2D globalLight;
    [Tooltip("Ambient light still reaches shadowed areas. 0 = shadows are pure black.")]
   

    void OnEnable()
    {
        Apply();
    }

    [ContextMenu("Apply Now")]
    public void Apply()
    {
        foreach (GameObject blocker in blockers)
        {
            if (blocker == null) continue;

            // Add a Shadow Caster 2D only if the blocker doesn't already have one
            ShadowCaster2D caster = blocker.GetComponent<ShadowCaster2D>();
            if (caster == null)
            {
                caster = blocker.AddComponent<ShadowCaster2D>();
            }

            caster.castsShadows = true;
            caster.selfShadows = selfShadows;
        }

        if (spotLight != null)
        {
            spotLight.shadowsEnabled = true;
            spotLight.shadowIntensity = shadowIntensity;
        }

    }
}
