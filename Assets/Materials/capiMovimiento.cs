using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capiMovimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] waypoints; // Array de puntos de referencia (waypoints)
    public float moveSpeed = 2f; // Velocidad de movimiento del NPC
    private int waypointIndex = 0; // Índice del waypoint actual

    private Animator animator; // Referencia al Animator del NPC

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // Mueve el NPC hacia el waypoint actual
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }

        Vector2 direction = waypoints[waypointIndex].position - transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
}
