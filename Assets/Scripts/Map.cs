using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize = new Vector2Int(5, 5);
    [SerializeField] float _cellSize;
    [SerializeField] MapModule[] _mapModules;
    [SerializeField] List<MapModuleContact> _contactTypes = new List<MapModuleContact>();
    public MapCell[,] MapCellsMatrix;
    public int RowsCount => MapCellsMatrix.GetLength(0);
    public int ColumnsCount => MapCellsMatrix.GetLength(1);
    private MapCell[] _mapCellsArray;

    private void Start()
    {
        InizializeMap();
        FillCells();
        CreateMap();
    }

    void InizializeMap()
    {
        MapCellsMatrix = new MapCell[_mapSize.x, _mapSize.y];

        var mapModules = GetMapModules();
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
                MapCellsMatrix[i, j] = new MapCell(this, new Vector2Int(i, j), new List<MapModuleState>(mapModules));
        }
        _mapCellsArray = MapCellsMatrix.Cast<MapCell>().ToArray();
    }

    void FillCells()
    {
        MapCell cell = null;
        
        do
        {
            var cellsWithUnselectedState = _mapCellsArray.Where(c => c.States.Count > 1).ToArray();

            if (cellsWithUnselectedState.Length == 0)
                return;

            var minStatesCount = cellsWithUnselectedState.Min(c => c.States.Count);

            cell = cellsWithUnselectedState.First(c => c.States.Count == minStatesCount);
        }
        while (cell.TrySelectState(states => states[Random.Range(0, states.Count)]));
    }

    void CreateMap()
    {
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                var localPosition = new Vector3(i * _cellSize, 0, j * _cellSize);
                MapCellsMatrix[i, j].States[0].InstantiatePrefab(this, localPosition);
            }
        }
    }

    List<MapModuleState> GetMapModules()
    {
        List<MapModuleState> mapModules = new List<MapModuleState>();
        foreach (var module in _mapModules)
        {
            mapModules.AddRange(module.GetMapModulesFromPrefab());
        }     
        return mapModules;
    }

    public MapModuleContact GetContact(string contactType)
    {
        return _contactTypes.First(contact => contact.ContactType == contactType);
    }
}