using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Takki : MonoBehaviour
{
    private void Update()
    {
        // gerir bendilinn sýnilegan
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // byrjar leikinn
    public void Byrja()
    {
        SceneManager.LoadScene(1);
    }

    // endar leikinn
    public void Endir()
    {
        SceneManager.LoadScene(0);
       
    }
    
}
