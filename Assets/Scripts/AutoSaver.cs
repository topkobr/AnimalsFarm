using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaver : MonoBehaviour
{
    private float _autosaverTimer = 1f;
    private float _autosaverTime;

    private void Update()
    {
        _autosaverTime += Time.deltaTime;
        if (_autosaverTime >= _autosaverTimer)
        {
            _autosaverTime = 0f;
            if (SaveSystem._isChanged)
            {
                Debug.Log("Last save changed. Save progress");
                SaveSystem.SaveProgress(SaveSystem.GetSave());
            }
        }
    }
}
