using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Translation (Movement)")]
    public float moveSpeed = 3f;
    public List<Vector2> positionWaypoints = new List<Vector2>();

    [Header("Rotation")]
    public float rotationSpeed = 90f; // Degrees per second
    public List<float> rotationWaypoints = new List<float>();

    // State tracking
    private int currentPosIndex = 0;
    private int currentRotIndex = 0;

    void Start()
    {
        // Default behavior: If lists are empty, default to starting position/rotation
        if (positionWaypoints.Count == 0)
        {
            positionWaypoints.Add(transform.position);
        }
        
        if (rotationWaypoints.Count == 0)
        {
            rotationWaypoints.Add(transform.eulerAngles.z);
        }
    }

    void Update()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        Vector2 targetPos = positionWaypoints[currentPosIndex];
        
        // Move smoothly towards the current target position
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Check if we reached the target (using a tiny threshold to prevent floating point errors)
        if (Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            currentPosIndex++; // Move to next waypoint
            
            // Loop back to the start if we reach the end of the list
            if (currentPosIndex >= positionWaypoints.Count)
            {
                currentPosIndex = 0;
            }
        }
    }

    private void HandleRotation()
    {
        float targetAngle = rotationWaypoints[currentRotIndex];
        float currentAngle = transform.eulerAngles.z;
        
        // Move smoothly towards the current target angle
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        // Check if we reached the target angle (Mathf.DeltaAngle handles 360 wrap-around safely)
        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 0.1f)
        {
            currentRotIndex++; // Move to next angle
            
            // Loop back to the start if we reach the end of the list
            if (currentRotIndex >= rotationWaypoints.Count)
            {
                currentRotIndex = 0;
            }
        }
    }
}