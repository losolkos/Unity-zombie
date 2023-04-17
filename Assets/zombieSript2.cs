using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieSript2 : MonoBehaviour
{
    public float sightRange = 15f;
    int hp = 30;
    GameObject player;
    NavMeshAgent agent;
    private bool playerVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raySource = transform.position + Vector3.up * 1.8f;
        Vector3 rayDirection = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(raySource, rayDirection, out hit, sightRange))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.CompareTag("Player"))
                playerVisible = true;
            else
                playerVisible = false;
        }
        agent.isStopped = !playerVisible;
        if (hp > 0)
        {

            agent.destination = player.transform.position;
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
                Destroy(transform.gameObject, 3);
            };
        }

    }

}
