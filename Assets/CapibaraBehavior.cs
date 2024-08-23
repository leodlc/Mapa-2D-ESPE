using UnityEngine;

public class CapibaraBehavior : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints para la patrulla
    public float moveSpeed = 2f; // Velocidad de movimiento
    public Transform robot; // Referencia al transform del robot
    public float detectionRange = 5f; // Rango de detección del robot

    private int waypointIndex = 0; // Índice de waypoint actual
    private bool isFollowing = false; // Indica si está siguiendo al robot
    private Animator animator; // Referencia al Animator del capibara

    void Start()
    {
        // Posicionar el capibara en el primer waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isFollowing)
        {
            FollowRobot(); // Seguir al robot si está en modo seguimiento
        }
        else
        {
            Patrol(); // De lo contrario, patrullar
        }
    }

    void Patrol()
    {
        // Mover al capibara hacia el waypoint actual
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        // Cuando el capibara llega al waypoint actual, ir al siguiente
        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }

        // Verificar si el robot está dentro del rango de detección
        float distanceToRobot = Vector2.Distance(transform.position, robot.position);
        if (distanceToRobot <= detectionRange)
        {
            isFollowing = true; // Cambiar a modo seguimiento si el robot está cerca
        }

        // Configurar animaciones de patrulla
        Vector2 direction = waypoints[waypointIndex].position - transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void FollowRobot()
    {
        // Mover al capibara hacia el robot
        transform.position = Vector2.MoveTowards(transform.position,
            robot.position, moveSpeed * Time.deltaTime);

        // Configurar animaciones de seguimiento
        Vector2 direction = robot.position - transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    // Detección de colisión con el robot
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            isFollowing = true; // Activar el modo seguimiento al detectar el robot
        }
    }

    // Dejar de seguir al robot si se sale del rango de detección
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            isFollowing = false; // Volver al modo patrulla si el robot se aleja
        }
    }
}
