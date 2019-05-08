using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManagerTester : MonoBehaviour
{
    public Intensity intensity;
    public Intensity duration;

    [ContextMenu("Shake")]
    public void Shake()
    {
        EffectManager.Instance.ShakeScreen(intensity, duration, false);
    }

    [ContextMenu("Shake Dynamic")]
    public void ShakeDynamic()
    {
        EffectManager.Instance.ShakeScreen(intensity, duration, true);
    }

    [ContextMenu("Freeze")]
    public void FreezeFrame()
    {
        EffectManager.Instance.FreezeScreen(intensity);
    }

    [ContextMenu("Slow time")]
    public void Slowtime()
    {
        EffectManager.Instance.SlowTime(intensity, duration);
    }

    [ContextMenu("Activate PostProcess")]
    public void PostProcess()
    {
        EffectManager.Instance.PostProcess(duration);
    }
}
