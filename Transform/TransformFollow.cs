using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollow : MonoBehaviour {

    [SerializeField] bool fixedUpdate;
    [SerializeField] Transform target;

    [SerializeField] bool followPosition;
    [SerializeField] bool followRotation;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Vector3 eulerOffset;

    // Update is called once per frame
    void Update ()
    {
	    if(!fixedUpdate)
        {

            UpdateTransform();
        }

    }
    void FixedUpdate()
    {
        if(fixedUpdate)
        {
            UpdateTransform();
        }
    }

    void UpdateTransform()
    {
        if (followPosition)
        {
            transform.position = target.position + positionOffset;
        }
        if (followRotation)
        {
            transform.rotation = target.rotation;
            transform.eulerAngles += eulerOffset;
        }

    }
}
