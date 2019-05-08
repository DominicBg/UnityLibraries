using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DollyZoomTester : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Transform target;
    [SerializeField] float distanceZoom;
    [SerializeField] float duration;
    [SerializeField] float waitDuration;

    DollyZoom dollyZoom;
    Camera camera;

    void Start()
    {
        camera = Camera.main;

        dollyZoom = camera.gameObject.AddComponent<DollyZoom>();
        dollyZoom.Init(camera, target, distanceZoom, duration);
    }

    [ContextMenu("Reset dolly")]
    public void ResetDolly()
    {
        dollyZoom.Init(camera, target, distanceZoom, duration);
    }

    [ContextMenu("Fade in")]
    public void FadeIn()
    {
        dollyZoom.FadeIn();
    }

    [ContextMenu("Fade Out")]
    public void FadeOut()
    {
        dollyZoom.FadeOut();
    }

    [ContextMenu("Fade in out")]
    public void FadeInOut()
    {
        dollyZoom.FadeInOut(waitDuration);
    }


}
