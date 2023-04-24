using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieSript2 : MonoBehaviour
{
    AudioSource normalAudio;
    AudioSource hitAudio;
    AudioSource deathAudio;
    public AudioClip audioClip;
    public float loopTime = 10.0f; 
    private AudioSource audioSource;
    private float timer = 0.0f;
    public float sightRange = 15f;
    int hp = 30;
    GameObject player;
    NavMeshAgent agent;
    private bool playerVisible = false;
    private bool playerHearable = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        normalAudio = transform.Find("normal").GetComponent<AudioSource>();
        hitAudio = transform.Find("hit").GetComponent<AudioSource>();
        deathAudio = transform.Find("death").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            return;
        timer += Time.deltaTime;

        if (timer >= loopTime)
        {
            audioSource.Stop();
            audioSource.Play();
            timer = 0.0f;
        }
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
            hitAudio.Play();
            if (hp <= 0)
            {
                normalAudio.Stop();
                deathAudio.Play();
                agent.enabled= false;
                transform.Translate(Vector3.up);
                transform.Rotate(Vector3.right * -90);
                GetComponent<BoxCollider>().enabled = true;
                Destroy(transform.gameObject, 3);
            };
        }

    }

}
