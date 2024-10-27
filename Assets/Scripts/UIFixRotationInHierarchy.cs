using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFixRotationInHierarchy : MonoBehaviour
{
    private Quaternion fixedRotation;
    void Start()
    {
        fixedRotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = fixedRotation;
    }
}
