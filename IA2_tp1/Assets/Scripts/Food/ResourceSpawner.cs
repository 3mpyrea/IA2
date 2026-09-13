using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceSpawner 
{
    public static Vector3 SpawnPoint(GameObject limits)
    {
        
        Vector3 boundsSize = limits.GetComponent<Renderer>().bounds.size;
        float halfX = boundsSize.x * 0.5f;
        float halfZ = boundsSize.z * 0.5f;

       
        float newValueX = Random.Range(-halfX + 5, halfX - 5);
        float newValueZ = Random.Range(-halfZ + 5, halfZ - 5);

        
        Vector3 pointInTerrain = limits.transform.position + new Vector3(newValueX, 0, newValueZ);

        Debug.Log("pointInTerrain " + pointInTerrain);

        Vector3 hitPointDir = new Vector3(pointInTerrain.x , pointInTerrain.y + 1000f, pointInTerrain.z);
        Debug.Log("hit point dir " + hitPointDir);

        int layerMask = 1 << limits.layer;
        if (Physics.Raycast(hitPointDir, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(" hit at " + hit.point);
            return hit.point;
        }
        else { Debug.Log("out of bounds"); }

        return Vector3.zero;
    }
}
