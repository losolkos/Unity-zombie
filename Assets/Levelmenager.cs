using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Levelmenager : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public GameObject Zombiee;
    public GameObject player;
    public GameObject healPrefab;
    public GameObject itemPrefab;
    public GameObject zombie2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4)
        {
            Instantiate(Zombiee, GetRandomPosition(), Quaternion.identity);    
            
            if (GameObject.FindGameObjectsWithTag("Lek").Length < 1)
            {
                Instantiate(healPrefab, GetRandomPosition(), Quaternion.identity);
            }
        }
        if (GameObject.FindGameObjectsWithTag("Enemy2").Length < 2)
        {
            Instantiate(zombie2, GetRandomPosition(), Quaternion.identity);
        }

    }
    Vector3 GetRandomPosition()
    {
        {
            Vector3 direction = Random.insideUnitSphere.normalized;
            direction.y = 0;
            Vector3 position = player.transform.position - (direction * Random.Range(15, 20));

            return position;
        }
    }


}

    
