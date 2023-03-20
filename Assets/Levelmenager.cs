using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Levelmenager : MonoBehaviour
{
    public GameObject Zombiee;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length<4)
        {
            Instantiate(Zombiee, GetRandomPosition(), Quaternion.identity);

          
        }

    }
    Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        position = position.normalized * Random.Range(10, 15);
        return position;
    }

}

    
