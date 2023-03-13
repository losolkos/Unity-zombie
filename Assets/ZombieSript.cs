using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSript : MonoBehaviour
{
    float hp = 10;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (hp > 0)
        {
            transform.LookAt(player.transform.position);

            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            hp--;
            if (hp <= 0)
            {
                transform.Translate(Vector3.up);
                transform.Rotate(Vector3.right * -90);
                GetComponent<BoxCollider>().enabled = true;
                Destroy(transform.gameObject, 10);
            };
        }
       
    }

}

