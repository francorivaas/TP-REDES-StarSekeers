using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotationFix : MonoBehaviour
{
    private Transform mainCameraTransform;
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCameraTransform.position);
    }
}
