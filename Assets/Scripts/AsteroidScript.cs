using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotate_Speed = 50f;
    private Animator anim;
    private AudioSource explosionSound;

    public GameObject floatingPoints;

    private int gothit = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
        
    }

    void Start()
    {
        if (Random.Range(0, 2) > 0) {
            rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
            rotate_Speed *= -1f;
        } else {
            rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
        }     
    }

    
    void Update()
    {
        Move();
        RotateEnemy();
    }


    void Move()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;

    }

    void RotateEnemy()
    {
        transform.Rotate(new Vector3(0f, 0f, rotate_Speed * Time.deltaTime), Space.World);
    }

    void TurnoffGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power Up AS" || other.tag == "Power Up MB")
        {
            return;
        }

        gothit += 1; //epeidi mporei na ekane trigger 2 fores 
        if (gothit == 1 && other.tag != "Spaceship")
        {
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<TextMesh>().text = "25";
            ScoreScript.scoreValue += 25;
        }
              
        Invoke("TurnoffGameObject", 0.15f);
        anim.Play("Destroy");
        explosionSound.Play();
    }
}
