using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D

/// <summary>
/// Kills the player when they stand inside this police officer's spot light.
/// Put this on the police prefab root. It uses the child Spot Light 2D's
/// radius and angle, so the kill zone always matches what you see on screen.
/// Walls (anything with a Collider2D) block the light.
/// </summary>
public class PoliceVision : MonoBehaviour
{
    [Tooltip("The police flashlight. Found automatically in children if left empty.")]
    public Light2D spotLight;

    [Tooltip("Layers that can block the light or be hit by it (walls + player).")]
    public LayerMask raycastMask = ~0;

    private die player;

    void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponentInChildren<Light2D>();
        }

        player = FindFirstObjectByType<die>();
    }

    void Update()
    {
        if (spotLight == null || player == null || player.IsDead) return;

        if (CanSee(player.transform.position))
        {
            player.Kill();
        }
    }

    private bool CanSee(Vector2 target)
    {
        Vector2 origin = spotLight.transform.position;
        Vector2 toTarget = target - origin;
        float distance = toTarget.magnitude;

        // 1. Within the light's reach?
        if (distance > spotLight.pointLightOuterRadius) return false;

        // 2. Within the cone? (a Spot Light 2D shines along its local up axis)
        float halfAngle = spotLight.pointLightOuterAngle * 0.5f;
        if (Vector2.Angle(spotLight.transform.up, toTarget) > halfAngle) return false;

        // 3. Not hidden behind a wall? The first collider along the ray must be the player.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, toTarget.normalized, distance + 0.01f, raycastMask);
        foreach (RaycastHit2D hit in hits)
        {
            // Ignore the police officer's own colliders
            if (hit.transform.IsChildOf(transform)) continue;

            return hit.transform.IsChildOf(player.transform);
        }

        // Player has no collider in the way: nothing blocked the ray, so they are seen
        return true;
    }

    void OnDrawGizmosSelected()
    {
        Light2D light = spotLight != null ? spotLight : GetComponentInChildren<Light2D>();
        if (light == null) return;

        Vector3 origin = light.transform.position;
        float radius = light.pointLightOuterRadius;
        float halfAngle = light.pointLightOuterAngle * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Quaternion.Euler(0, 0, halfAngle) * light.transform.up * radius);
        Gizmos.DrawLine(origin, origin + Quaternion.Euler(0, 0, -halfAngle) * light.transform.up * radius);
    }
}
