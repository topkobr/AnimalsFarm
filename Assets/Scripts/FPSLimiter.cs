using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private int _limitFps;
    // Start is called before the first frame update
    void Start()
    {
        _limitFps = Screen.currentResolution.refreshRate;
        Debug.Log("FPS Limiter set to " + _limitFps.ToString());
        Application.targetFrameRate = _limitFps;
    }
}
