using System.Collections;
using UnityEngine;

public class JuegoFutbol : MonoBehaviour
{
    [SerializeField] private Transform playerStartPosition; // Posición inicial del jugador
    [SerializeField] private Transform botStartPosition; // Posición inicial del bot
    [SerializeField] private Transform ballStartPosition; // Posición inicial de la pelota
    [SerializeField] private GameObject player; // Referencia al jugador
    [SerializeField] private GameObject bot; // Referencia al bot
    [SerializeField] private GameObject ball; // Referencia a la pelota
    [SerializeField] private BoxCollider2D playerGoal; // Arco del jugador
    [SerializeField] private BoxCollider2D botGoal; // Arco del bot
    [SerializeField] private int maxGoles = 3; // Goles necesarios para ganar

    private int playerGoles = 0; // Contador de goles del jugador
    private int botGoles = 0; // Contador de goles del bot
    private bool gameEnded = false; // Verificar si el juego ha terminado

    private void Start()
    {
        ResetPositions();
        Debug.Log("El juego ha comenzado. ¡Buena suerte!");
    }

    private void Update()
    {
        if (gameEnded)
            return;

        // Detectar si la pelota entra en algún arco
        if (playerGoal.bounds.Contains(ball.transform.position))
        {
            botGoles++;
            Debug.Log("Gol del Bot! Total de goles: " + botGoles);
            StartCoroutine(HandleGoal());
        }
        else if (botGoal.bounds.Contains(ball.transform.position))
        {
            playerGoles++;
            Debug.Log("Gol del Jugador! Total de goles: " + playerGoles);
            StartCoroutine(HandleGoal());
        }

        // Verificar si alguien ha ganado
        if (playerGoles >= maxGoles)
        {
            Debug.Log("¡El jugador ha ganado el partido!");
            gameEnded = true;
            EndGame();
        }
        else if (botGoles >= maxGoles)
        {
            Debug.Log("¡El bot ha ganado el partido!");
            gameEnded = true;
            EndGame();
        }
    }

    private IEnumerator HandleGoal()
    {
        // Congelar el juego durante 3 segundos
        Time.timeScale = 0f;
        Debug.Log("Juego pausado durante 3 segundos.");
        yield return new WaitForSecondsRealtime(3f);

        // Restablecer posiciones después de 3 segundos
        ResetPositions();
        Debug.Log("Posiciones restablecidas. Juego reanudado.");

        // Reanudar el juego después de 1 segundo
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
    }

    private void ResetPositions()
    {
        // Restablecer las posiciones de los jugadores y la pelota
        player.transform.position = playerStartPosition.position;
        bot.transform.position = botStartPosition.position;
        ball.transform.position = ballStartPosition.position;

        // Reiniciar las velocidades
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.velocity = Vector2.zero;
        ballRb.angularVelocity = 0f;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;

        Rigidbody2D botRb = bot.GetComponent<Rigidbody2D>();
        botRb.velocity = Vector2.zero;
        botRb.angularVelocity = 0f;
    }

    private void EndGame()
    {
        // Implementar lógica para cuando termine el juego, por ejemplo:
        Debug.Log("El juego ha terminado.");
        // Aquí podrías mostrar una pantalla de victoria/derrota o cargar otra escena.
    }
}
