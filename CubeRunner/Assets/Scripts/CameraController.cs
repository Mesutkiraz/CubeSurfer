using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform cameraFocus, target;
    public float followS;
    public Vector3 distance;


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position+distance, followS * Time.deltaTime);
        transform.LookAt(target);
        
    }
}
