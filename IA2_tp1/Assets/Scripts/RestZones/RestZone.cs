using System.Collections.Generic;
using UnityEngine;

public class RestZone : MonoBehaviour
{
    public int capacity = 1;
    private List<Transform> occupants = new List<Transform>();

    public bool HasSpace => occupants.Count < capacity;

    public void Occupy(Transform who) => occupants.Add(who);
    public void Release(Transform who) => occupants.Remove(who);
}