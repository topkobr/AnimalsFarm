using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayObject : MonoBehaviour
{
    [SerializeField] HayParticles _hayParticles;
    private Vector3 _targetPos;
    private Quaternion _targetRotation;
    private float _moveTime = 8f;
    public bool _isPicked;
    public bool _isDropped;

    private void Start()
    {
        _targetPos = transform.position;
    }

    public void MoveTo(Vector3 position, Quaternion rotation, Transform parent)
    {
        _hayParticles.PlayParticle();
        _targetPos = position;
        _targetRotation = rotation;
        transform.parent = parent;
        transform.localRotation = _targetRotation;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (transform.position != _targetPos && (_isPicked || _isDropped) )
            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, _moveTime * Time.deltaTime);
        if (transform.localScale != new Vector3(1,1,1))
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,1,1), _moveTime * Time.deltaTime);
    }

    private void OnEnable()
    {
        _hayParticles.PlayParticle();
        this.GetComponent<Animator>().Play("Spawn");
    }

    public void Despawn()
    {
        StartCoroutine(HandleDespawn());
    }

    private IEnumerator HandleDespawn()
    {
        _hayParticles.SpawnParticleInWorld();
        this.GetComponent<Animator>().Play("Despawn");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
