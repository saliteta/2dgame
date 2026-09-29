using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Player death + game over. Put this on the player.
/// PoliceVision calls Kill() when the player is caught in a police light.
/// Press R on the game over screen to restart the level.
/// </summary>
public class die : MonoBehaviour
{
    public bool IsDead { get; private set; }

    public void Kill()
    {
        if (IsDead) return;
        IsDead = true;

        // Stop the player from moving
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // Freeze the whole game (police stop patrolling too)
        Time.timeScale = 0f;
        Debug.Log("Caught by the police! Game over.");
    }

    void Update()
    {
        // Update still runs while timeScale is 0, so we can listen for restart
        if (IsDead && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnGUI()
    {
        if (!IsDead) return;

        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 48,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            richText = true
        };
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "GAME OVER\n<size=24>Press R to restart</size>", style);
    }
}
