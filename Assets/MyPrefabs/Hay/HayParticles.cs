using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moveParticles;

    private bool _cooldown;

    public void PlayParticle()
    {
        if (_cooldown) return;
        _moveParticles.Play();
        _cooldown = true;
        StartCoroutine(ParticleCooldown());
    }

    public void SpawnParticleInWorld()
    {
        if (_cooldown) return;
        _cooldown = true;
        StartCoroutine(ParticleCooldown());
        ParticleSystem particle = Instantiate(_moveParticles, _moveParticles.transform.position, _moveParticles.transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, 5f);
    }

    private IEnumerator ParticleCooldown()
    {
        yield return new WaitForSeconds(1f);
        _cooldown = false;

    }
}
