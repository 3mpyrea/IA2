using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Necesidades")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float hungerDecayPerSecond = 2f;
    [SerializeField] private float energyDecayPerSecond = 1f;
    [SerializeField] private float hungerThreshold = 40f;
    [SerializeField] private float energyThreshold = 30f;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Referencias")]
    [SerializeField] private RestZoneProvider restZoneProvider;
    [SerializeField] private FoodProvider foodProvider;

    private CreatureStateMachine fsm;
    private Vector3 destination;
    private bool hasDestination;

    public Vector3 Destination => destination;
    public float Hunger { get; private set; }
    public float Energy { get; private set; }
    public float MaxEnergy => maxEnergy;
    public bool IsTired => Energy < energyThreshold;
    public bool IsHungry => Hunger < hungerThreshold;
    public bool IsResting { get; set; }
    public RestZone CurrentRestZone { get; set; }


    void Start()
    {
        Hunger = maxHunger;
        Energy = maxEnergy;

        fsm = new CreatureStateMachine();
        fsm.AddState(CreatureStateMachine.CreatureStates.seekResource, new SearchRestState(restZoneProvider, this));
        fsm.AddState(CreatureStateMachine.CreatureStates.rest, new RestState(this));
        fsm.AddState(CreatureStateMachine.CreatureStates.consumeResource, new EatState(foodProvider, this));
        fsm.AddState(CreatureStateMachine.CreatureStates.roam, new RoamState(this));
        fsm.ChangeState(CreatureStateMachine.CreatureStates.roam);
    }

    void Update()
    {
        if (!IsResting)
            Hunger = Mathf.Max(0f, Hunger - hungerDecayPerSecond * Time.deltaTime);

        Energy = Mathf.Max(0f, Energy - energyDecayPerSecond * Time.deltaTime);

        if (hasDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) < 0.1f)
                hasDestination = false;
        }

        fsm.Update();
    }

    public CreatureStateMachine.CreatureStates NeedsAction()
    {
        if (IsHungry) return CreatureStateMachine.CreatureStates.consumeResource;
        if (IsTired) return CreatureStateMachine.CreatureStates.seekResource;
        return CreatureStateMachine.CreatureStates.roam;
    }

    public void SetDestination(Vector3 destiny)
    {
        destination = destiny;
        hasDestination = true;
    }

    public void RecoverHunger(float amount) => Hunger = Mathf.Min(maxHunger, Hunger + amount);
    public void RecoverEnergy(float amount) => Energy = Mathf.Min(maxEnergy, Energy + amount);

    public void ChangeState(CreatureStateMachine.CreatureStates state)
    {
        fsm.ChangeState(state);
    }
}
