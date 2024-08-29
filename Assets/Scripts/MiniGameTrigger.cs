using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameTrigger : MonoBehaviour
{
    public string miniGameSceneName = "Puzzle2"; // Nombre de la escena del minijuego
    public string mainSceneName = "SampleScene"; // Nombre de la escena principal
    public float detectionRadius = 5f; // Radio de detección para la proximidad
    private bool isPlayerNear = false;
    private bool isInMiniGame = false;

    void Update()
    {
        // Detectar si el jugador está dentro del radio de detección
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
            {
                isPlayerNear = true;
                break;
            }
        }

        // Si el jugador está cerca y presiona la tecla J
        if (isPlayerNear && Input.GetKeyDown(KeyCode.J))
        {
            if (!isInMiniGame)
            {
                // Cargar la escena del minijuego de forma aditiva
                SceneManager.LoadScene(miniGameSceneName, LoadSceneMode.Additive);
                isInMiniGame = true; // Indicamos que estamos en el minijuego
            }
            else
            {
                // Regresar a la escena principal sin perder el progreso
                SceneManager.UnloadSceneAsync(miniGameSceneName);
                isInMiniGame = false; // Indicamos que estamos de vuelta en la escena principal
            }
        }
        else
        {
            isPlayerNear = false;
        }
    }

    // Visualizar el radio de detección en la escena para depuración
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
