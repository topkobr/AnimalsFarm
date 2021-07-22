using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatGrowtScript : MonoBehaviour
{
    [SerializeField] private Transform _pointsParent;
    [SerializeField] private GameObject _wheatPrefab;
    [SerializeField] private LayerMask _wheatMask;
    [SerializeField] private GameObject _hayPrefab;

    private float _growtTimer = 1f;
    private float _growtTime;

    private void Start()
    {

        int startingGrowtCount = Mathf.RoundToInt(_pointsParent.childCount * .5f);
        for (int i = 0; i < startingGrowtCount; i++)
            WheatGrowt();
    }

    private void Update()
    {
        GrowtTimer();
    }

    private void GrowtTimer()
    {
        _growtTime += Time.deltaTime;
        if (_growtTime >= _growtTimer)
        {
            _growtTime = 0f;
            TryGrowt();
        }
    }

    private void TryGrowt()
    {
        if (Random.Range(0,100) < 30)
        {
            WheatGrowt();
        }
    }

    private void WheatGrowt()
    {
        Transform spawnTransform = _pointsParent.GetChild(Random.Range(0, _pointsParent.childCount));
        if (spawnTransform.childCount == 0)
        {
            GameObject spawned = Instantiate(_wheatPrefab, spawnTransform);
            spawned.transform.rotation = Quaternion.Euler(Random.Range(0f, 20f), Random.Range(0f, 360f), Random.Range(0f, 20f));
        }
    }

    public void DestroyWheat(GameObject target)
    {
        if (target == null) return;
        Instantiate(_hayPrefab, target.transform.parent);
        Destroy(target);
    }
}
