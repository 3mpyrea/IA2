using UnityEngine;

public class SearchRestState : IState
{
    private RestZoneProvider provider;
    private Creature animal;

    public void Enter()
    {
        var zone = provider.GetClosestAvailableZone(animal.transform.position);
        if (zone != null)
        {
            zone.Occupy(animal.transform);
            animal.SetDestination(zone.transform.position);
        }
    }
}