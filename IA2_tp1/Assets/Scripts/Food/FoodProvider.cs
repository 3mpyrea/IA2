using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodProvider : MonoBehaviour
{
    public static FoodProvider Instance { get; private set; }

    [SerializeField] private List<Food> allFood = new List<Food>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Register(Food food) => allFood.Add(food);
    public void Unregister(Food food) => allFood.Remove(food);

    public Food GetClosestAvailableFood(Vector3 origin)

    {
        return allFood
            .Where(f => f != null && !f.IsConsumed)
            .OrderBy(f => Vector3.Distance(origin, f.transform.position))
            .FirstOrDefault();
    }

    public bool HasAvailableFood()
    {
        return allFood.Any(f => f != null && !f.IsConsumed);
    }
}
