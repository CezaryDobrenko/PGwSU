using UnityEngine;
using System.Collections;

//This script make sure that object that is attached to
//always look forward at player position

public class FaceCamera : MonoBehaviour
{
    Vector3 cameraDirection;

    void Update()
    {
        cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}