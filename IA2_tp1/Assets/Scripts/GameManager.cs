using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject limits;


    [SerializeField] GameObject _berry;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        StartCoroutine(StartSpawning(_berry, limits, 5f));

    }

    public IEnumerator StartSpawning(GameObject resource, GameObject limits, float spawnCD)
    {
        while (true)
        {

            Debug.Log("going to spawn");
            yield return new WaitForSeconds(spawnCD);

            Renderer[] renderers = resource.GetComponentsInChildren<Renderer>();
           
            Bounds combinedBounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                combinedBounds.Encapsulate(renderers[i].bounds);
            }
            float resourceHeightDiff = combinedBounds.size.y * 0.5f;
            Vector3 spawnPoint = ResourceSpawner.SpawnPoint(limits);
            Vector3 correctedSpawn = new Vector3(spawnPoint.x, spawnPoint.y + resourceHeightDiff, spawnPoint.z);
            Instantiate(resource, correctedSpawn, resource.transform.rotation);
        }


    }
}
