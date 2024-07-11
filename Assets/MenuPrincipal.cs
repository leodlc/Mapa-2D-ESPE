using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    public void OnclickPlay()
    {
        LoadingScreenManager.Instance.SwitchToScene(2);
    }
/*     public void Inicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    } */

    public void salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
