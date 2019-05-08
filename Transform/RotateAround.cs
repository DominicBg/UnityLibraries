using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    [SerializeField] Transform orbitTransform;
    [SerializeField] float orbitSpeed;
    [SerializeField] float distance;

    [SerializeField] Vector3 rotationAxis;
    [SerializeField] float rotationSpeed;

    [SerializeField] float orbitOffset;
    float angle;

    private void OnValidate()
    {
        UpdateOrbit();
    }

    void Update()
    {
        UpdateAngle();
        UpdateOrbit();
        UpdateRotation();
    }

    void UpdateOrbit()
    {
        transform.position = orbitTransform.position - GameMath.RotateAroundAxisNormalized(orbitTransform.right, orbitTransform.up, angle + orbitOffset);
        AjustDistance();
    }

    void UpdateRotation()
    {
        transform.localEulerAngles += rotationAxis * rotationSpeed * Time.deltaTime;
    }

    void UpdateAngle()
    {
        angle += Time.deltaTime * orbitSpeed * 0.1f;
        if(angle > 360)
        {
            angle -= 360;
        }
    }

    void AjustDistance()
    {
        Vector3 diff = orbitTransform.position - transform.position;

        transform.position = orbitTransform.position - diff.normalized * distance;
    }
}

