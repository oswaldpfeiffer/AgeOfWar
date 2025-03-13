using System.Collections.Generic;
using UnityEngine;

public class SpatialGrid : MonoBehaviour
{
    public static SpatialGrid Instance { get; private set; }

    public int GridWidth = 10;
    public int GridDepth = 8;
    public float CellSize = 2f;

    private Dictionary<Vector2Int, List<Unit>> _grid;
    private List<Base> _bases = new List<Base>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _grid = new Dictionary<Vector2Int, List<Unit>>();
    }

    public Vector2Int GetCell(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / CellSize);
        int z = Mathf.FloorToInt(position.z / CellSize);
        return new Vector2Int(x, z);
    }

    public void RegisterUnit(Unit unit)
    {
        var cell = GetCell(unit.transform.position);
        if (!_grid.ContainsKey(cell))
            _grid[cell] = new List<Unit>();
        _grid[cell].Add(unit);
    }

    public void UpdateUnitPosition(Unit unit, Vector2Int previousCell)
    {
        var newCell = GetCell(unit.transform.position);
        if (newCell != previousCell)
        {
            if (_grid.ContainsKey(previousCell))
                _grid[previousCell].Remove(unit);

            RegisterUnit(unit);
        }
    }

    public void UnregisterUnit(Unit unit, Vector2Int cell)
    {
        if (_grid.ContainsKey(cell))
            _grid[cell].Remove(unit);
    }

    public List<Unit> GetNearbyUnits(Vector3 position, float range)
    {
        var nearbyUnits = new List<Unit>();
        var cell = GetCell(position);

        int rangeInCells = Mathf.CeilToInt(range / CellSize);

        for (int x = -rangeInCells; x <= rangeInCells; x++)
        {
            for (int z = -rangeInCells; z <= rangeInCells; z++)
            {
                var checkCell = cell + new Vector2Int(x, z);

                if (_grid.ContainsKey(checkCell))
                    nearbyUnits.AddRange(_grid[checkCell]);
            }
        }

        return nearbyUnits;
    }

    public void RegisterBase(Base baseUnit)
    {
        _bases.Add(baseUnit);
    }

    public void UnregisterBase(Base baseUnit)
    {
        _bases.Remove(baseUnit);
    }

    public Base GetClosestBase(Vector3 position, ESide unitSide)
    {
        foreach (Base baseUnit in _bases)
        {
            if (baseUnit.Side != unitSide) return baseUnit;
        }
        return null;
    }
}
