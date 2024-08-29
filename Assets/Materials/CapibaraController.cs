using UnityEngine;

public class CapibaraController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float patrolDistance = 5f; // Distancia a patrullar desde la posición inicial
    public Transform playerTransform; // Transform del robot

    private Animator animator;
    private bool movingRight = true; // Controla si el capibara se mueve a la derecha
    private bool encounteredPlayer = false; // Controla si el capibara ha encontrado al robot
    private float startX; // La posición X inicial

    void Start()
    {
        animator = GetComponent<Animator>();
        startX = transform.position.x; // Guardamos la posición X inicial
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

    void Patrol()
    {
        if (movingRight)
        {
            // Mover hacia la derecha
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetFloat("Horizontal", 1f); // Animación de caminar hacia la derecha

            // Cambia la dirección si llega a la distancia de patrullaje
            if (transform.position.x >= startX + patrolDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            // Mover hacia la izquierda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetFloat("Horizontal", -1f); // Animación de caminar hacia la izquierda

            // Cambia la dirección si regresa al punto de inicio
            if (transform.position.x <= startX)
            {
                movingRight = true;
            }
        }
    }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el robot tenga el tag "Player"
        {
            animator.SetTrigger("Encounter"); // Activar animación de encuentro
            encounteredPlayer = true;
        }
    }
}
