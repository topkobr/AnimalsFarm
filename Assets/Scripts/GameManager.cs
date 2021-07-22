using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singletion
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Found more than one GameManager instance");
    }

    #endregion

    public HarvestList HarvestScriptableObject { get { return _harvestObject; } }
    [SerializeField] private HarvestList _harvestObject;
}
