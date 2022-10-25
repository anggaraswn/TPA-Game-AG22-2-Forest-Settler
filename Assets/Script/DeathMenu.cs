using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("TPA");
    }

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
