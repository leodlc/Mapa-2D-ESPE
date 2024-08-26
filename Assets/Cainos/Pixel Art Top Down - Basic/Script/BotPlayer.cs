using System.Collections;
using UnityEngine;

public class BotPlayer : MonoBehaviour
{
    [SerializeField] private Transform myGoal; // Arco que debe defender
    [SerializeField] private Transform ball; // La pelota
    [SerializeField] private Transform otherGoal; // Arco donde debe anotar
    [SerializeField] private float speed = 5f; // Velocidad del NPC
    [SerializeField] private Animator animator; // Controlador de animaciones del NPC

    private Vector2 movement;
    private bool hasBall = false; // Si el NPC tiene la pelota
    private bool isPlayerInRange = false;

    private enum State
    {
        CATCH = 0,  // Buscar la pelota
        DEFEND = 1, // Defender el arco
        ATTACK = 2, // Atacar y llevar la pelota al arco contrario
    }

    private State currentState;

    private void Start()
    {
        currentState = State.CATCH;
    }

    private void Update()
    {
        UpdateState();
        MoveNPC();
        UpdateAnimation();
    }

    private void UpdateState()
    {
        if (!hasBall)
        {
            if (Vector2.Distance(transform.position, ball.position) < 1f)
                currentState = State.CATCH;
            else if (Vector2.Distance(transform.position, myGoal.position) < 3f)
                currentState = State.DEFEND;
            else
                currentState = State.CATCH;
        }
        else
        {
            currentState = State.ATTACK;
        }
    }

    private void MoveNPC()
    {
        switch (currentState)
        {
            case State.CATCH:
                MoveTowards(ball);
                break;
            case State.DEFEND:
                MoveTowards(myGoal);
                break;
            case State.ATTACK:
                MoveTowards(otherGoal);
                break;
        }
    }

    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            animator.Play("run_ingame");
        }
        else
        {
            animator.Play("stay_ingame");
        }
    }

    private void MoveTowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        movement = direction * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pelota"))
        {
            hasBall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("pelota"))
        {
            hasBall = false;
        }
    }
}
