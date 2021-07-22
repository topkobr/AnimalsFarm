using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasToCamera : MonoBehaviour
{
    private Transform _mainCamTransform;

    private void Start()
    {
        this._mainCamTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation((transform.position - _mainCamTransform.position).normalized, transform.up);
    }
}
