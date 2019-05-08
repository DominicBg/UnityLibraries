using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DollyZoom : MonoBehaviour
{
    Camera camera;
    Transform target;
    float distanceZoom;
    float duration;

    Vector3 startPos;
    float initialHeight;

    public void Init(Camera camera, Transform target, float distanceZoom, float duration)
    {
        this.camera = camera;
        this.target = target;
        this.distanceZoom = distanceZoom;
        this.duration = duration;

        startPos = camera.transform.position;
        initialHeight = CalculateFrustumHeight(DistanceCameraTarget());
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(target, distanceZoom, duration, fadeIn: true, loop : false, pauseDuration : 0));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(target, distanceZoom, duration, fadeIn: false, loop : false, pauseDuration : 0));
    }

    public void FadeInOut(float pauseDuration)
    {
        StartCoroutine(Fade(target, distanceZoom, duration, fadeIn: true, loop: true, pauseDuration: pauseDuration));
    }


    float DistanceCameraTarget()
    {
        return (camera.transform.position - target.transform.position).magnitude;
    }

    IEnumerator Fade(Transform target, float distanceZoom, float duration, bool fadeIn, bool loop, float pauseDuration)
    {
        float t = 0;
        float speed = 1 / duration;

        float distance = (camera.transform.position - target.position).magnitude;
        float height = CalculateFrustumHeight(distance);

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            float tt = (fadeIn) ? t : 1 - t;

            camera.transform.position = Vector3.Lerp(startPos, startPos + camera.transform.forward * distanceZoom, tt);
            camera.fieldOfView = CalculateFieldOfView(height, DistanceCameraTarget());

            yield return null;
        }
        if(loop)
        {
            yield return new WaitForSeconds(pauseDuration);
            StartCoroutine(Fade(target, distanceZoom, duration, fadeIn: false, loop: false, pauseDuration: 0));
        }
    }

    float CalculateFrustumHeight(float distance)
    {
        //soh-cah-toa
        //tan(a) = opposite / adjacent
        //on plug les variables
        //tan(half field of view) = (half height) / distance
        //distance * tan(half field of view) = half height
        // 2 * distance * tan(half field of view) = height

        return 2 * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }
    float CalculateFieldOfView(float height, float distance)
    {
        //soh-cah-toa
        //tan(a) = opposite/ adjacent
        //on plug les variables

        //tan(a) = halfHeight / distance
        //a = arctan(halfHeight / distance)
        //half height vue qu'on fait un triangle avec le upper part
        //2 * a la fin vue qu'on double l'angle 

        return 2 * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }
}
