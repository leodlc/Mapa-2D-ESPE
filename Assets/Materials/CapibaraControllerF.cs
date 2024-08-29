using UnityEngine;
using System.Collections.Generic;

public class CapibaraControllerF : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public Transform playerTransform; // Transform del robot

    private Animator animator;
    private bool encounteredPlayer = false; // Controla si el capibara ha encontrado al robot

    private Queue<Vector2> sourceQueue = new Queue<Vector2>(); // Cola para la búsqueda desde la posición del capibara
    private Queue<Vector2> destinationQueue = new Queue<Vector2>(); // Cola para la búsqueda desde la posición del jugador
    private Dictionary<Vector2, Vector2> previousNodes = new Dictionary<Vector2, Vector2>(); // Para rastrear el camino desde el capibara
    private Dictionary<Vector2, Vector2> previousNodesBidirectional = new Dictionary<Vector2, Vector2>(); // Para rastrear el camino desde el jugador

    void Start()
    {
        animator = GetComponent<Animator>();
        sourceQueue.Enqueue(transform.position);
        destinationQueue.Enqueue(playerTransform.position);
    }

    void Update()
    {
        if (encounteredPlayer)
        {
            FollowPlayer(); // Si ha encontrado al robot, lo sigue
        }
        else
        {
            BidirectionalSearch(); // Busca al jugador usando la búsqueda bidireccional
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

    // Método de búsqueda bidireccional
    void BidirectionalSearch()
    {
        if (sourceQueue.Count > 0)
        {
            Vector2 currentNodeFwd = sourceQueue.Dequeue();
            if (previousNodesBidirectional.ContainsKey(currentNodeFwd))
            {
                OnPathFound(currentNodeFwd); // Si hay una intersección, se encuentra el camino
                return;
            }
            ExploreNeighbors(currentNodeFwd, sourceQueue, previousNodes);
        }

        if (destinationQueue.Count > 0)
        {
            Vector2 currentNodeBack = destinationQueue.Dequeue();
            if (previousNodes.ContainsKey(currentNodeBack))
            {
                OnPathFound(currentNodeBack); // Si hay una intersección, se encuentra el camino
                return;
            }
            ExploreNeighbors(currentNodeBack, destinationQueue, previousNodesBidirectional);
        }
    }

    // Método para explorar los vecinos de un nodo
    void ExploreNeighbors(Vector2 node, Queue<Vector2> queue, Dictionary<Vector2, Vector2> previousNodeDict)
    {
        Vector2[] directions = new Vector2[] {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        foreach (Vector2 direction in directions)
        {
            Vector2 neighbor = node + direction;
            if (!previousNodeDict.ContainsKey(neighbor))
            {
                queue.Enqueue(neighbor);
                previousNodeDict[neighbor] = node;
            }
        }
    }

    // Método para manejar la intersección de los caminos
    void OnPathFound(Vector2 intersection)
    {
        encounteredPlayer = true; // Cuando se encuentra el camino, se considera que el capibara ha encontrado al jugador
        animator.SetTrigger("Encounter"); // Activar animación de encuentro

        // Aquí podrías reconstruir el camino si necesitas hacerlo
    }

    // Método para detectar cuando el capibara encuentra al robot
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            encounteredPlayer = true;
        }
    }
}
