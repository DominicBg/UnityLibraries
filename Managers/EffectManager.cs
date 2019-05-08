using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public enum Intensity { Low, Medium, Hard}
public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] IntensitiesValue freezeDuration;
    [SerializeField] IntensitiesValue shakeScreenDuration;
    [SerializeField] IntensitiesValue shakeScreenIntensity;
    [SerializeField] IntensitiesValue slowTimeDuration;
    [SerializeField] IntensitiesValue slowTimeIntesity;
    [SerializeField] IntensitiesValue postProcess;

    Camera mainCamera;
    Coroutine slowTimeCoroutine;

    [SerializeField] PostProcessVolume postProcessEffect;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void FreezeScreen(Intensity intensity)
    {
        StartCoroutine(FreezeFrameEffect(freezeDuration.GetValue(intensity)));
    }

    public void ShakeScreen(Intensity intensity, Intensity duration, bool dynamic = true)
    {
        GameEffect.ShakeDynamic(mainCamera.gameObject, shakeScreenIntensity.GetValue(intensity), shakeScreenDuration.GetValue(duration));
    }

    public void ShakeScreen(Intensity intensity)
    {
        GameEffect.ShakeDynamic(mainCamera.gameObject, shakeScreenIntensity.GetValue(intensity), shakeScreenDuration.GetValue(intensity));
    }

    public void ShakeScreen(Intensity intensity, float duration, bool dynamic = true)
    {
        if(dynamic)
        {
            GameEffect.ShakeDynamic(mainCamera.gameObject, shakeScreenIntensity.GetValue(intensity), duration);
        }
        else
        {
            GameEffect.Shake(mainCamera.gameObject, shakeScreenIntensity.GetValue(intensity), duration);
        }
    }

    IEnumerator FreezeFrameEffect(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);

    }

    public void SlowTime(Intensity intensity, Intensity duration, Ease ease = Ease.Linear)
    {
        Time.timeScale = slowTimeIntesity.GetValue(intensity);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, slowTimeDuration.GetValue(duration)).SetEase(ease).SetUpdate(UpdateType.Normal, isIndependentUpdate: true);
    }

    public void SlowTime(Intensity intensity, float duration, Ease ease = Ease.Linear)
    {
        Time.timeScale = slowTimeIntesity.GetValue(intensity);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, duration).SetEase(ease).SetUpdate(UpdateType.Normal, isIndependentUpdate: true);
    }

    public void PostProcess(Intensity duration)
    {
        postProcessEffect.weight = postProcess.GetValue(duration);
        DOTween.To(() => postProcessEffect.weight, x => postProcessEffect.weight = x, 0, 1);
    }

    [System.Serializable]
    public class IntensitiesValue
    {
        public float low = 0.05f;
        public float medium = 0.1f;
        public float hard = 0.2f;

        public IntensitiesValue(float low, float medium, float hard)
        {
            this.low = low;
            this.medium = medium;
            this.hard = hard;
        }

        public float GetValue(Intensity intensity)
        {
            switch(intensity)
            {
                case Intensity.Low:return low;
                case Intensity.Medium: return medium;
                case Intensity.Hard: return hard;
            }
            return 0;
        }
    }
}
