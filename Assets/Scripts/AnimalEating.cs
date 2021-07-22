using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEating : MonoBehaviour
{
    [SerializeField] private Transform _hayParent;
    [SerializeField] private Transform _animalParent;
    [SerializeField] private GameObject _hungerCanvas;
    [SerializeField] private string _harvestName;
    [SerializeField] private Transform _harvestPoint;

    private Animator _hungerAnimator;
    private Animator _animalAnimator;

    private void Start()
    {
        _animalAnimator = _animalParent.GetChild(0).GetComponent<Animator>();
        _animalAnimator.enabled = false;
        _hungerAnimator = _hungerCanvas.transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(RandomizeAnimators());
    }

    private IEnumerator RandomizeAnimators()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        _animalAnimator.enabled = true;
    }

    public void GiveHay()
    {
        StopAllCoroutines();
        StartCoroutine(HandleHay());
    }

    private IEnumerator HandleHay()
    {
        _animalAnimator.SetBool("isEating", true);
        _hungerAnimator.SetBool("Enabled", false);
        yield return new WaitForSeconds(.1f);
        _hungerCanvas.SetActive(false);
        yield return new WaitForSeconds(Random.Range(5f, 15f));
        _hayParent.GetChild(0).GetComponent<HayObject>().Despawn();
        _animalAnimator.SetBool("isEating", false);
        yield return new WaitForSeconds(1f);
        // Harvest
        InstantiateHay();
        yield return new WaitForSeconds(1f);
        // Hunger
        _hungerCanvas.SetActive(true);
        _hungerAnimator.SetBool("Enabled", true);
    }

    private void InstantiateHay()
    {
        GameObject toSpawn = GameManager.instance.HarvestScriptableObject.GetHarvestPrefab();
        Transform spawned = Instantiate(toSpawn, _harvestPoint).transform;
        HarvestList.Harvest harv = GameManager.instance.HarvestScriptableObject.GetHarvest(_harvestName);
        spawned.GetComponent<HarvestObject>().SetObject(harv.sprite, harv.cost);
        spawned.localPosition = new Vector3(Random.Range(-.05f, .05f), 0, Random.Range(-.05f, .05f));
    }
}
