using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestObject : MonoBehaviour
{
    [SerializeField] private Image _harvestImage;
    [SerializeField] private Animator _animator;
    private float _moveToSpeed = 15f;

    private Transform _moveToTransform;
    private bool _isMoveToTarget;
    private bool _isDestroyingNow;

    private Sprite Img { set { _harvestImage.sprite = value; } }
    public int Cost { get; private set; }

    public void SetObject(Sprite image, int cost)
    {
        Img = image;
        Cost = cost;
    }

    public void DestroyObject()
    {
        if (!_isDestroyingNow)
        {
            _isDestroyingNow = true;
            StartCoroutine(HandleDestroy());
        }
    }

    private IEnumerator HandleDestroy()
    {
        Debug.Log("Try to give money");
        MoneySystem.GiveMoney(Cost);
        _animator.SetBool("Enabled", false);
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    public void MoveTo(Transform trans)
    {
        _isMoveToTarget = true;
        //transform.parent = trans;
        _moveToTransform = trans;
        DestroyObject();
    }

    private void Update()
    {
        if (!_isMoveToTarget) return;
        transform.position = Vector3.Lerp(transform.position, _moveToTransform.position, _moveToSpeed * Time.deltaTime);
    }
}
