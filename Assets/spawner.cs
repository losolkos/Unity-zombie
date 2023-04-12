using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject spawnerPrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    public float distanceBetweenCheck;
    public float heightOfCheck = 10f, rangeOfCheck = 200f;
    public LayerMask layerMask;
    public Vector2 positivePosition, negativePosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenCheck)
        {
            for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenCheck)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    if (spawnChance > Random.Range(0f, 101f))
                    {
                        GameObject spawnedObject = Instantiate(spawnerPrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = true;
                        }
                    }
                }
            }
        }
    }
}
