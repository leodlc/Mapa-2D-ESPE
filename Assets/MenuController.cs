using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Detecta si se presiona la tecla Enter
        {
            IniciarJuego();
        }
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene("Inicio 3"); // Asegúrate de que el nombre de la escena coincida con el que deseas cargar
    }
}
