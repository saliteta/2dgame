using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// The exit of a level. Attach it to any GameObject (e.g. a Square sprite).
/// When the player's centre is inside the zone they win and the next scene
/// in File > Build Profiles (scene list) is loaded.
/// On the last level it shows a final win screen; press R to play again.
/// The zone is the object's sprite by default, or a custom Size/Offset.
/// No collider is needed.
/// </summary>
public class WinZone : MonoBehaviour
{
    [Tooltip("Use the SpriteRenderer's bounds as the zone (if there is one).")]
    public bool matchSprite = true;

    [Tooltip("Zone size in local units (scaled by the object). Used when there is no sprite or Match Sprite is off.")]
    public Vector2 size = Vector2.one;

    [Tooltip("Zone centre offset from the object, in local units.")]
    public Vector2 offset = Vector2.zero;

    [Tooltip("Seconds to show LEVEL COMPLETE before loading the next level.")]
    public float delayBeforeNextLevel = 2f;

    private die player;
    private bool won;
    private bool isLastLevel;

    void Start()
    {
        player = FindAnyObjectByType<die>();
    }

    /// <summary>The zone in world space.</summary>
    public Bounds GetZone()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (matchSprite && sprite != null && sprite.sprite != null)
        {
            return sprite.bounds;
        }

        Vector3 scale = transform.lossyScale;
        Vector3 centre = transform.TransformPoint(offset);
        return new Bounds(centre, new Vector3(Mathf.Abs(size.x * scale.x), Mathf.Abs(size.y * scale.y), 1f));
    }

    void Update()
    {
        if (!won && player != null && !player.IsDead)
        {
            Bounds zone = GetZone();
            Vector3 playerPos = player.transform.position;
            playerPos.z = zone.center.z; // 2D: ignore depth

            if (zone.Contains(playerPos)) Win();
        }

        // After the last level, R starts again from the first one
        if (won && isLastLevel && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }

    private void Win()
    {
        won = true;

        // The police can no longer catch the player
        foreach (PoliceVision police in FindObjectsByType<PoliceVision>())
        {
            police.enabled = false;
        }

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        Time.timeScale = 0f;

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        isLastLevel = nextIndex >= SceneManager.sceneCountInBuildSettings;

        if (!isLastLevel)
        {
            StartCoroutine(LoadNextLevel(nextIndex));
        }
    }

    private IEnumerator LoadNextLevel(int nextIndex)
    {
        // Realtime, because the game is frozen with timeScale = 0
        yield return new WaitForSecondsRealtime(delayBeforeNextLevel);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextIndex);
    }

    void OnGUI()
    {
        if (!won) return;

        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 48,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            richText = true
        };
        style.normal.textColor = Color.green;

        string text = isLastLevel
            ? "YOU ESCAPED!\n<size=24>Press R to play again</size>"
            : "LEVEL COMPLETE";

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text, style);
    }

    void OnDrawGizmos()
    {
        Bounds zone = GetZone();
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(zone.center, zone.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(zone.center, zone.size);
    }
}
