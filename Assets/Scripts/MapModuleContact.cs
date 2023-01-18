using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapModuleContact
{
    [SerializeField] string _contactType;
    [SerializeField] List<string> _notSuitableContactTypes;

    public string ContactType => _contactType;
    public List<string> NotSuitableContactTypes => _notSuitableContactTypes;

    public bool IsMatchingContacts(MapModuleContact other)
    {
        return !other.NotSuitableContactTypes.Contains(ContactType) &&
               !NotSuitableContactTypes.Contains(other.ContactType);
    }
}

public static class ContactDirectionInMap
{
    public static Vector2 Forward => new Vector2(0, 1);
    public static Vector3 Back => new Vector2(0, -1);
    public static Vector3 Right => new Vector2(1, 0);
    public static Vector3 Left => new Vector2(-1, 0);
}
