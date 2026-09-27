using UnityEngine;

public class SearchRestState : States
{
    private RestZoneProvider provider;
    private Creature animal;

    public SearchRestState(RestZoneProvider provider, Creature animal)
    {
        this.provider = provider;
        this.animal = animal;
    }

    public override void OnEnter()
    {
        var zone = provider.GetClosestAvailableZone(animal.transform.position);
        if (zone != null)
        {
            zone.Occupy(animal.transform);
            animal.CurrentRestZone = zone;
            animal.SetDestination(zone.transform.position);
        }
        else
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
        }
    }


    public override void OnUpdate()
    {
        if (Vector3.Distance(animal.transform.position, animal.Destination) < 0.5f)
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.rest);
        }
    }

    public override void OnExit()
    {
        animal.IsResting = false;
        if (animal.CurrentRestZone != null)
        {
            animal.CurrentRestZone.Release(animal.transform);
            animal.CurrentRestZone = null;
        }
    }
    
}
