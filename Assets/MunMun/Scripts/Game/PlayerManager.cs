using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MeatManager _meatManager;
    [SerializeField] private List<GameObject> _units;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private Transform _unitsContainer;

    public void TrySpawnUnit(int index)
    {
        if (index >= _units.Count) return;
        GameObject unit = _units[index];
        Unit u = unit.GetComponent<Unit>();
        if (_meatManager.GetMeatAmount() >= u.Price)
        {
            _meatManager.PayMeat(u.Price);
            GameObject spawned = Instantiate(unit, _unitsContainer);
            spawned.transform.position = _spawner.transform.position;
            Unit spawnedU = spawned.GetComponent<Unit>();
            spawnedU.Side = ESide.left;
        }
    }
}
