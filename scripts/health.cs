using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class healthCode : MonoBehaviour
{

    public static healthCode Instance;

    public float health = 10f;
    private Animator animator;
    private Rigidbody rb;
    private AudioSource audioSource;
    private TextMeshProUGUI countText;
    private static int count = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        countText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        SetCountText(); // stillir upphafstalning
    }

    // fall til a� draga �r l�f
    public void TakeDamage(float amount)
    {

        health -= amount;

        if (health <= 0f)
        {
            Die(); // deyr ef l�f n�r 0 e�a l�gri
        }
    }

    void Die()
    {
        animator.SetTrigger("Die"); // kveikir � "die" animation

        // st��var alla e�lisfr��ikrafta � st�fan l�kama
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        audioSource.Stop();

        // aukar fj�lda um 10 og uppf�rir talning
        count += 10;
        SetCountText();

        // ey�ileggi hlutinn eftir 5 sek�ndur
        Destroy(gameObject, 5f);

        Debug.Log("wolf dies");

        // vinnur leikinn ef fj�ldinn er meiri en e�a jafnt og 100
        if (count >= 100)
        {
            WinGame();
        }
    }

    // fall til a� auka fj�lda
    public void IncrementCount()
    {
        count += 5;
        SetCountText();

        if (count >= 100)
        {
            WinGame();
        }
    }

    void SetCountText()
    {
        // uppf�rir talning
        countText.text = "Stig: " + count.ToString();
    }

    void WinGame()
    {
        SceneManager.LoadScene(3);
    }
}