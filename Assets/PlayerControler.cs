using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    float Hp = 10;
    public GameObject BulletPrefab;
    public float bulletSpeed = 20.0f;
    public float playerSpeed = 2;
    Vector2 movementVector;
    Transform bulletSpawn;
    public GameObject hpBar;
    Scrollbar hpScrollBar;
    // Start is called before the first frame update
    void Start()
    {
        movementVector = Vector2.zero;
        bulletSpawn = transform.Find("BulletSpawn");
        hpScrollBar = hpBar.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, movementVector.x, 0));
        transform.Translate(Vector3.forward * movementVector.y * Time.deltaTime* playerSpeed); 
    }

    void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();

        //Debug.Log(movementVector.ToString());
    }

    void OnFire()
    {
        GameObject bullet = Instantiate(BulletPrefab, bulletSpawn);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*bulletSpeed, ForceMode.VelocityChange);
        Destroy(bullet, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Hp--;
            if (Hp <= 0) Die();
            hpScrollBar.size = Hp / 10;
            Vector3 pushVector = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(pushVector.normalized * 5, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Lek"))
        {
            Hp = 10;
            hpScrollBar.size = Hp / 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy2"))
        {
            Hp = 0;
            if (Hp <= 0) Die();
            hpScrollBar.size = Hp / 10;
        }
    }
    void Die()
    {
        transform.Translate(Vector3.up);
        transform.Rotate(Vector3.right * -90);
        GetComponent<BoxCollider>().enabled = true;
        Destroy(transform.gameObject, 3);
        Time.timeScale = 0;
        SceneManager.LoadScene("DIE");
    }
}
