using UnityEngine;

public class CapibaraController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float leftBoundary = -5f; // Límite izquierdo del movimiento
    public float rightBoundary = 5f; // Límite derecho del movimiento
    public Transform playerTransform; // Transform del robot

    private Animator animator;
    private bool movingRight = true; // Controla si el capibara se mueve a la derecha
    private bool encounteredPlayer = false; // Controla si el capibara ha encontrado al robot

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (encounteredPlayer)
        {
            FollowPlayer(); // Si ha encontrado al robot, lo sigue
        }
        else
        {
            Patrol(); // Si no, sigue patrullando
        }
    }

    // Método para patrullar entre los límites establecidos
    void Patrol()
    {
        if (movingRight)
        {
            // Mover hacia la derecha
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetFloat("Horizontal", 1f); // Animación de caminar hacia la derecha

            if (transform.position.x >= rightBoundary)
            {
                // Cambia la dirección si llega al límite derecho
                movingRight = false;
            }
        }
        else
        {
            // Mover hacia la izquierda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetFloat("Horizontal", -1f); // Animación de caminar hacia la izquierda

            if (transform.position.x <= leftBoundary)
            {
                // Cambia la dirección si llega al límite izquierdo
                movingRight = true;
            }
        }
    }

    // Método para seguir al jugador si es que lo ha encontrado
    void FollowPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x > 0)
        {
            animator.SetFloat("Horizontal", 1f); // Animación de caminar hacia la derecha
        }
        else
        {
            animator.SetFloat("Horizontal", -1f); // Animación de caminar hacia la izquierda
        }
    }

    // Método para detectar cuando el capibara encuentra al robot
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el robot tenga el tag "Player"
        {
            animator.SetTrigger("Encounter"); // Activar animación de encuentro
            encounteredPlayer = true;
        }
    }
}
