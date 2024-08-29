using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaContinuar : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public string pauseSceneName = "PantallaPausa";  // Nombre de la escena de pausa

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        // Descargar la escena de pausa si está cargada
        if (SceneManager.GetSceneByName(pauseSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(pauseSceneName);
        }

        Debug.Log("Reanudar el juego.");
    }

    void Pause()
    {
        // Pausar el juego
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        // Cargar la escena de pausa si no está ya cargada
        if (!SceneManager.GetSceneByName(pauseSceneName).isLoaded)
        {
            SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
        }

        Debug.Log("Juego pausado.");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PantallaMenuOpciones");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
