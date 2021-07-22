using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HarvestData", menuName = "HarvestList/HarvestList", order = 1)]
public class HarvestList : ScriptableObject
{
    [SerializeField] private Harvest[] _harvestList;
    [SerializeField] private GameObject _harvestPrefab;

    public GameObject GetHarvestPrefab()
    {
        return _harvestPrefab;
    }

    public Harvest GetHarvest(string name)
    {
        foreach (Harvest harvest in _harvestList)
        {
            if (harvest.name.Equals(name))
                return harvest;
        }

        Debug.LogError("Error harvest name");
        return new Harvest();
    }

    [System.Serializable]
    public struct Harvest
    {
        public string name;
        public Sprite sprite;
        public int cost;
    }
}