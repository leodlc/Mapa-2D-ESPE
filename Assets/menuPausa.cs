using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public string pauseSceneName = "PantallaPausa";  // Nombre de la escena de pausa

    void Update()
    {
        // Detectar la tecla "Esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        // Verificar si la escena de pausa ya est� cargada
        if (SceneManager.GetSceneByName(pauseSceneName).isLoaded)
        {
            // Si est� cargada, reanudar el juego
            SceneManager.UnloadSceneAsync(pauseSceneName);
            Time.timeScale = 1;  // Reanudar el tiempo
            Debug.Log("Reanudar el juego.");
        }
        else
        {
            // Si no est� cargada, pausar el juego
            SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
            Time.timeScale = 0;  // Pausar el tiempo
            Debug.Log("Juego pausado.");
        }
    }
}