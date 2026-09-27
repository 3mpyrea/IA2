using UnityEngine;

public class RestState : States
{
    private Creature animal;

    public RestState(Creature animal)
    {
        this.animal = animal;
    }

    public override void OnEnter()
    {
        animal.IsResting = true;
    }

    public override void OnUpdate()
    {
        animal.RecoverEnergy(15f * Time.deltaTime);

        if (animal.Energy >= animal.MaxEnergy)
        {
            animal.ChangeState(CreatureStateMachine.CreatureStates.roam);
        }
    }

    public override void OnExit()
    {
        animal.IsResting = false;
    }
}
