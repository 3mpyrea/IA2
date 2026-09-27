using TMPro;
using UnityEngine;

public class CreatureHUD : MonoBehaviour
{
    [SerializeField] private Creature creature;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI energyText;

    void Update()
    {
        hungerText.text = $"Hunger: {creature.Hunger:F0}";
        energyText.text = $"Energy: {creature.Energy:F0}";
    }
}
