using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class hlutursnu : MonoBehaviour
{
    private TextMeshProUGUI countText;

    void Start()
    {
        countText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // athugar hvort hluturinn hafi fallið niður fyrir ákveðna y stöðu
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // athugar hvort hluturinn sem lenti í árekstri hafi tag „Player“
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("picked up fruit");
            Destroy(gameObject);
            // sækir í IncrementCount aðferð heilsuCode tilviksins til að auka fjölda stiga
            healthCode.Instance.IncrementCount();

        }
    }
}
