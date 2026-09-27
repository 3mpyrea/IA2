using UnityEngine;

public class EatState : States
{
    private FoodProvider provider;
    private Creature animal;
    private Food target;

    public EatState(FoodProvider provider, Creature animal)
    {
        this.provider = provider;
        this.animal = animal;
    }

    public override void OnEnter()
    {
        target = provider.GetClosestAvailableFood(animal.transform.position);

        if (target != null)
        {
            animal.SetDestination(target.transform.position);
        }
        else
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
        }
    }

    public override void OnUpdate()
       {
        if (target == null || target.IsConsumed)
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
            return;
        }

        if (Vector3.Distance(animal.transform.position, target.transform.position) < 0.5f)
        {
            target.Consume();
            animal.RecoverHunger(target.nutritionValue);
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
        }
        else if (!animal.IsHungry)
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
        }
    }

    public override void OnExit() { }
}
