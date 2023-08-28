using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5f;

    public Transform attack_Point;
    public GameObject bullet_Prefab;

    private Animator anim;

    public AudioSource source;
    public AudioClip explosionSound;
    public AudioClip fireSound;

    public GameObject floatingPoints;

    private int gothit = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        Invoke("StartShooting", Random.Range(1f, 3f));    
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;

    }

    void TurnoffGameObject()
    {
        gameObject.SetActive(false);
    }

    void StartShooting()
    {
        GameObject bullet = Instantiate(bullet_Prefab, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>();
        source.PlayOneShot(fireSound);
        Invoke("StartShooting", Random.Range(1f, 3f)); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power Up AS" || other.tag == "Power Up MB" || other.tag == "Power Up SH")
        {
            return;
        }

        gothit += 1;
        if (gothit == 1 && other.tag != "Spaceship")
        {
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<TextMesh>().text = "50";

            ScoreScript.scoreValue += 50;
        }
        

        source.PlayOneShot(explosionSound);

        CancelInvoke("StartShooting");

        anim.Play("Destroy");

        Invoke("TurnoffGameObject", 0.15f);
    }














}
