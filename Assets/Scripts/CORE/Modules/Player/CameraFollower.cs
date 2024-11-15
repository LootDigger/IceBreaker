using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CameraFollower : MonoBehaviour
{
    public Transform target;  // The character the camera will follow
    public Vector3 offset;    // Offset distance between the camera and the character
    public float smoothSpeed = 0.125f;  // Smooth speed of camera movement

    void LateUpdate()
    {
        if (target == null) return; // Make sure there is a target to follow

        // Desired position is the target's position plus the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Optionally, make the camera look at the target
        transform.LookAt(target);
    }
}
