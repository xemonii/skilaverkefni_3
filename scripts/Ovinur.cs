using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ovinur : MonoBehaviour
{
    public static int health = 100;
    public Transform player;
    private  TextMeshProUGUI texti;
    private Rigidbody rb;
    private Vector3 movement;
    public float hradi = 5f;

    // Start is called before the first frame update
    void Start()
    {
        texti= GameObject.Find("Text2").GetComponent<TextMeshProUGUI>();
        rb = this.GetComponent<Rigidbody>();
        texti.text = "Líf: " + health.ToString(); // stillt upphafstextann til að sýna líf

    }

    // Update is called once per frame
    void Update()
    {
        // reiknað stefnu í átt að leikmanninum
        Vector3 stefna = player.position - transform.position;
        stefna.Normalize();
        movement = stefna;
        horfaPlayer();
    }
    private void FixedUpdate()
    {
        Hreyfing(movement);
    }

    // fall til að færa hlutinn
    void Hreyfing(Vector3 stefna)
    {
        rb.MovePosition(transform.position + (stefna * hradi * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        // athugar hvort hluturinn sem lenti í árekstri sé með tag "Player"
        if (collision.collider.tag == "Player")
        {
            // minnkar líf leikmannsins
            Debug.Log("Leikmaður fær í sig óvin");
            TakeDamage(15);
            gameObject.SetActive(false);
        }
    }

    // fall til að draga úr líf
    public void TakeDamage(int damage)
    {
        health -= damage;
        texti.text = "Líf: " + health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
            health = 100;
            texti.text = "Líf: " + health.ToString(); 
        }

    }
    // fall til að láta hlutinn horfa á leikmann
    public void horfaPlayer()
    {
        transform.LookAt(player);
    }

}
