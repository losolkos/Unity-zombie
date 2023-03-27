using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Levelmenager : MonoBehaviour
{
    public GameObject Zombiee;
    public GameObject player;
    public GameObject healPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length<4)
        {
            Instantiate(Zombiee, GetRandomPosition(), Quaternion.identity);
        }
        if (GameObject.FindGameObjectsWithTag("Lek").Length < 1)
        {
            Instantiate(healPrefab, GetRandomPosition(), Quaternion.identity);
        }

    }
    Vector3 GetRandomPosition()
    {
        {
            Vector3 direction = Random.insideUnitSphere.normalized;
            direction.y = 0;
            Vector3 position = player.transform.position - (direction * Random.Range(10, 15));

            return position;
        }
    }

}

    
