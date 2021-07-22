using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHayScript : MonoBehaviour
{
    [SerializeField] private WheatGrowtScript _wheatGrowtScript;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private float _checkSphereRadius;
    [SerializeField] private LayerMask _hayMask;
    [SerializeField] private LayerMask _dropHayMask;
    [SerializeField] private LayerMask _wheatMask;
    [SerializeField] private LayerMask _harvestMask;

    private int _currentHayAmount = 0;
    private float _checkTimerTime = 0f;
    private float _checkTimer = .1f;

    private float _wheatTimer = .3f;
    private float _wheatTime;

    private GameObject _targetWheat;

    private bool _waitAnimation;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _checkSphereRadius);
    }

    private void Update()
    {
        ChecksTimer();
        WheatDestroyTimer();
    }

    private void ChecksTimer()
    {
        _checkTimerTime += Time.deltaTime;
        if (_checkTimerTime >= _checkTimer)
        {
            CheckPickup();
            CheckDrop();
            CheckWheat();
            CheckHarvest();
            _checkTimerTime = 0f;
        }
    }

    private void WheatDestroyTimer()
    {
        if (!_playerMove.IsJoystickClick())
        {
            _wheatTime += Time.deltaTime;
        }
        else
        {
            _waitAnimation = false;
            _playerMove.WheatAnimation(false);
            _targetWheat = null;
            _wheatTime = 0f;
        }

        if (_wheatTime >= _wheatTimer)
        {
            _wheatGrowtScript.DestroyWheat(_targetWheat);
            _playerMove.WheatAnimation(false);
            _wheatTime = 0f;
            _waitAnimation = false;
        }
    }

    private void CheckPickup()
    {
        if (_currentHayAmount >= transform.childCount) return;
        Collider[] hays = Physics.OverlapSphere(transform.position, _checkSphereRadius, _hayMask);
        for (int i = 0; i < hays.Length; i++)
        {
            if (_currentHayAmount >= transform.childCount) break;
            HayObject hayScript = hays[i].GetComponent<HayObject>();
            if (!hayScript._isPicked && !hayScript._isDropped)
            {
                hayScript.MoveTo(new Vector3(), new Quaternion(), transform.GetChild(_currentHayAmount));
                hayScript._isPicked = true;
                _currentHayAmount++;
            }
        }
    }

    private void CheckDrop()
    {
        Collider[] dropHay = Physics.OverlapSphere(transform.position, _checkSphereRadius, _dropHayMask);
        for (int i = 0; i < dropHay.Length; i++)
        {
            if (_currentHayAmount <= 0) break;
            if (dropHay[i].transform.childCount > 0) continue;
            HayObject hayScript = transform.GetChild(_currentHayAmount - 1).GetChild(0).GetComponent<HayObject>();
            hayScript.MoveTo(new Vector3(), new Quaternion(), dropHay[i].transform);
            hayScript._isDropped = true;
            dropHay[i].transform.parent.GetComponent<AnimalEating>().GiveHay();
            _currentHayAmount--;
        }
    }

    private void CheckHarvest()
    {
        Collider[] harvest = Physics.OverlapSphere(transform.position, _checkSphereRadius, _harvestMask);
        for (int i = 0; i < harvest.Length; i++)
        {
            harvest[i].GetComponent<HarvestObject>().MoveTo(transform);
        }
    }

    private void CheckWheat()
    {
        Collider[] wheats = null;
        if (!_playerMove.IsJoystickClick())
            wheats = Physics.OverlapSphere(transform.position, _checkSphereRadius / 1.5f, _wheatMask);
        if (wheats != null && wheats.Length > 0)
        {
            _playerMove.SetWheatDirection(wheats[0].transform);
            _targetWheat = wheats[0].gameObject;
            if (!_waitAnimation)
            {
                _waitAnimation = true;
                _playerMove.WheatAnimation(true);
            }
        }
        else
        {
            _playerMove.SetWheatDirection(null);
        }
    }
}
