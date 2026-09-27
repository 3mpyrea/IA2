using UnityEngine;

public class RoamState : States
{
    private Creature animal;
    private float waitTimer;
    private bool isWaiting;

    public RoamState(Creature animal)
    {
        this.animal = animal;
    }

    public override void OnEnter()
    {
        Vector3 randomPoint = GetRandomPointInBounds();
        animal.SetDestination(randomPoint);
        isWaiting = false;
    }

    public override void OnUpdate()
    {
        var nextState = animal.NeedsAction();
        if (nextState != CreatureStateMachine.CreatureStates.roam)
        {
            animal.ChangeState(nextState);
            return;
        }

        if (!isWaiting && Vector3.Distance(animal.transform.position, animal.Destination) < 0.5f)
        {
            isWaiting = true;
            waitTimer = Random.Range(1f, 3f);
        }

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                isWaiting = false;
                animal.SetDestination(GetRandomPointInBounds());
            }
        }
    }

    public override void OnExit() { }

    private Vector3 GetRandomPointInBounds()
    {
        Bounds bounds = GameManager.instance.limits.GetComponent<Renderer>().bounds;
        float x = Random.Range(bounds.min.x + 2f, bounds.max.x - 2f);
        float z = Random.Range(bounds.min.z + 2f, bounds.max.z - 2f);
        return new Vector3(x, animal.transform.position.y, z);
    }
}
