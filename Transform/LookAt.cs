using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Vector3 lookAtPosition;
    [SerializeField] Transform lookAtTransform;

    [SerializeField] bool useTransform;


    // Update is called once per frame
    void Update()
    {
        if (useTransform)
            transform.LookAt(lookAtTransform);
        else
            transform.LookAt(lookAtPosition);
    }
}
