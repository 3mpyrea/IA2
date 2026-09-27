using UnityEngine;

public class DrinkState : States
{
    private Creature animal;

    public DrinkState(Creature animal)
    {
        this.animal = animal;
    }

    public override void OnEnter() { }
    public override void OnUpdate() { }
    public override void OnExit() { }
}