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

    // fall til að draga úr líf
    public void TakeDamage(float amount)
    {

        health -= amount;

        if (health <= 0f)
        {
            Die(); // deyr ef líf nær 0 eða lægri
        }
    }

    void Die()
    {
        animator.SetTrigger("Die"); // kveikir á "die" animation

        // stöðvar alla eðlisfræðikrafta á stífan líkama
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        audioSource.Stop();

        // aukar fjölda um 10 og uppfærir talning
        count += 10;
        SetCountText();

        // eyðileggi hlutinn eftir 5 sekúndur
        Destroy(gameObject, 5f);

        Debug.Log("wolf dies");

        // vinnur leikinn ef fjöldinn er meiri en eða jafnt og 100
        if (count >= 100)
        {
            WinGame();
        }
    }

    // fall til að auka fjölda
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
        // uppfærir talning
        countText.text = "Stig: " + count.ToString();
    }

    void WinGame()
    {
        SceneManager.LoadScene(3);
    }
}