using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSript : MonoBehaviour
{
    public AudioClip audioClip;
    public float loopTime = 10.0f;
    private AudioSource audioSource;
    private float timer = 0.0f;
    public float sightRange = 15f;
    public float hearRange = 5f;
    float hp = 10;
    GameObject player;
    NavMeshAgent agent;
    private bool playerVisible = false;
    private bool playerHearable = false;
    AudioSource normalAudio;
    AudioSource hitAudio;
    AudioSource deathAudio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        normalAudio = transform.Find("normal").GetComponent<AudioSource>();
        hitAudio = transform.Find("hit").GetComponent<AudioSource>();
        deathAudio = transform.Find("death").GetComponent<AudioSource>();


        //audioSource =  transform.Find("normal").GetComponent<AudioSource>();
        //audioSource.clip = audioClip;
        //audioSource.loop = true;
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            return;
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
        Collider[] heardObjects = Physics.OverlapSphere(transform.position, hearRange);

        playerHearable = false;
        foreach (Collider collider in heardObjects)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                playerHearable = true;
            }
        }       agent.isStopped = !playerVisible && !playerHearable;
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

            
            hitAudio.Play();
            if (hp <= 0)
            {;
                normalAudio.Stop();
                deathAudio.Play();
                agent.enabled = false;
                transform.Translate(Vector3.up);
                transform.Rotate(Vector3.right * -90);
                GetComponent<BoxCollider>().enabled = true;
                agent.speed = 0;
                Destroy(transform.gameObject, 3);
            };
        }
       
    }

}

