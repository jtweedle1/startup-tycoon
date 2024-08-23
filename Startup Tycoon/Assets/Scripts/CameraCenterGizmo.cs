using UnityEngine;

// This script creates vertical and horizontal center lines on the camera
public class CameraCenterGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            // Calculate the center position
            Vector3 cameraCenter = cam.transform.position;
            Gizmos.color = Color.red;

            // Draw a vertical line in the center of the camera view
            Gizmos.DrawLine(new Vector3(cameraCenter.x, cameraCenter.y - cam.orthographicSize, cameraCenter.z),
                           new Vector3(cameraCenter.x, cameraCenter.y + cam.orthographicSize, cameraCenter.z));

            // Draw a horizontal line in the center of the camera view
            Gizmos.DrawLine(new Vector3(cameraCenter.x - cam.orthographicSize * cam.aspect, cameraCenter.y, cameraCenter.z),
                           new Vector3(cameraCenter.x + cam.orthographicSize * cam.aspect, cameraCenter.y, cameraCenter.z));
        }
    }
}