using UnityEngine;

public class UnitDetection : MonoBehaviour
{
    private Unit _unit;
    private Vector2Int _previousCell;

    void Start()
    {
        _unit = GetComponent<Unit>();
        _previousCell = SpatialGrid.Instance.GetCell(transform.position);
        SpatialGrid.Instance.RegisterUnit(_unit);
    }

    void Update()
    {
        SpatialGrid.Instance.UpdateUnitPosition(_unit, _previousCell);
        _previousCell = SpatialGrid.Instance.GetCell(transform.position);

        // Recherche de la cible la plus proche
        var nearbyUnits = SpatialGrid.Instance.GetNearbyUnits(transform.position, _unit.ATTACKRANGE * 2f);

        Unit closestUnit = null;
        float closestDistance = float.MaxValue;

        foreach (var unit in nearbyUnits)
        {
            if (unit == _unit) continue;
            if (unit.Side == _unit.Side) continue;
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = unit;
            }
        }

        if (closestUnit != null)
        {
            _unit.SetTarget(closestUnit.transform);
        }
        else
        {
            // Si aucune unité n'est trouvée, viser la base ennemie
            Base closestBase = SpatialGrid.Instance.GetClosestBase(transform.position, _unit.Side);

            if (closestBase != null)
            {
                _unit.SetTarget(closestBase.transform);
            }
        }
    }

    private void OnDestroy()
    {
        SpatialGrid.Instance.UnregisterUnit(_unit, _previousCell);
    }
}
