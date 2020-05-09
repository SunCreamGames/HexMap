using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float totalSecons, duration, currentSeconds;
    bool isRunning, isStarted;
    // Start is called before the first frame update
    public float Duration { get { return duration; } set { duration = value; } }
    private void Start()
    {
        isRunning = false;
        isStarted = false;
    }
    public void Run()
    {
        if (duration > 0)
        {
            currentSeconds = 0f;
            totalSecons = duration;
            isStarted = true;
            isRunning = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentSeconds += Time.deltaTime;
        }
        if (currentSeconds >= totalSecons)
        {
            isRunning = false;
        }
    }
    public bool Finished
    {
        get { return !isRunning && isStarted; }
    }
}
