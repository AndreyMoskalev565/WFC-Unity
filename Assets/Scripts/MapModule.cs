using System.Collections.Generic;
using UnityEngine;

public class MapModule : MonoBehaviour
{
    [SerializeField] Map _map;
    [SerializeField] string _forwardContactType;
    [SerializeField] string _backContactType;
    [SerializeField] string _rightContactType;
    [SerializeField] string _leftContactType;

    string[] _contactTypes => new string[]
    {
        _forwardContactType,
        _rightContactType,
        _backContactType,
        _leftContactType
    };

    Vector2[] _contactDirections => new Vector2[]
    {
        ContactDirectionInMap.Forward,
        ContactDirectionInMap.Right,
        ContactDirectionInMap.Back,
        ContactDirectionInMap.Left
    };

    public List<MapModuleState> GetMapModulesFromPrefab()
    {
        var contactTypes = _contactTypes;
        var contactDirections = _contactDirections;
        List<MapModuleState> mapModules = new List<MapModuleState>();
        var rotationY = 0;

        for (int i = 0; i < contactDirections.Length; i++)
        {
            MapModuleState module = new MapModuleState(this, Vector3.up * rotationY);

            for (int j = 0; j < contactTypes.Length; j++)
            {
                var typeIndex = (i + j) % contactTypes.Length;
                var contact = _map.GetContact(contactTypes[typeIndex]);
                module.Contacts.Add(contactDirections[j], contact);
            }

            mapModules.Add(module);
            rotationY -= 90;
        }
        return mapModules;
    }
}
