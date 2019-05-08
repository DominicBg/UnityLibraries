using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSUi : MonoBehaviour
{
    [SerializeField] int meanCount = 10;

    float[] frameTime;
    int index = 0;

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        frameTime = new float[meanCount];
    }

    void ShowFPS()
    {
        float sum = 0;
        for (int i = 0; i < meanCount; i++)
        {
            sum += frameTime[i];
        }

        text.text = (sum / meanCount) + " fps";
        index = 0;
        frameTime = new float[meanCount];
    }

    // Update is called once per frame
    void Update()
    {     
        frameTime[index] = 1 / Time.deltaTime;
        index++;

        if (index == meanCount)
        {
            ShowFPS();
        }
    }
}
