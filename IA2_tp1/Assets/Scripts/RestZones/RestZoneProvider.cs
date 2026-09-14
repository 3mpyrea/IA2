using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestZoneProvider : MonoBehaviour
{
    [SerializeField] private List<RestZone> allZones;

    public IEnumerable<RestZone> GetAvailableZones()
    {
        foreach (var zone in allZones)
        {
            if (zone == null) continue;
            if (!zone.HasSpace) continue;

            yield return zone;
        }
    }

    public RestZone GetClosestAvailableZone(Vector3 origin)
    {
        return GetAvailableZones()
            .OrderBy(z => Vector3.Distance(origin, z.transform.position))
            .FirstOrDefault(); 
    }

    public bool HasAnyAvailableZone()
    {
        return GetAvailableZones().Any();
    }
}