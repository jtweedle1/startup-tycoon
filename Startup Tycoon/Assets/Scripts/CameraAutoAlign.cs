using UnityEngine;

// This script auto-aligns the camera at all times.
public class CameraGridAlign : MonoBehaviour
{
    public float gridSize = 1.0f;

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / gridSize) * gridSize;
        position.y = Mathf.Round(position.y / gridSize) * gridSize;
        transform.position = position;
    }
}