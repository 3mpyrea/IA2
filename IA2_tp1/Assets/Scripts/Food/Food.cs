using UnityEngine;

public class Food : MonoBehaviour
{
    public float nutritionValue = 30f;
    public bool IsConsumed { get; private set; }

    void OnEnable()
       {
        FoodProvider.Instance.Register(this);
    }

    void OnDisable()
       {
        if (FoodProvider.Instance != null)
            FoodProvider.Instance.Unregister(this);
    }

    public void Consume()
       {
        IsConsumed = true;
        Destroy(gameObject);
    }
}
