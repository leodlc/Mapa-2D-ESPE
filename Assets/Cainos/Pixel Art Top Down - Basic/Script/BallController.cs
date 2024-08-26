using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bounceMultiplier = 1.5f; // Multiplicador de rebote
    public float cornerBounceMultiplier = 2f; // Multiplicador para esquinas
    public float forceOutOfCorner = 5f; // Fuerza adicional para sacar la pelota de la esquina
    private Rigidbody2D rb;
    private float stuckTimeThreshold = 1f; // Tiempo máximo antes de aplicar fuerza adicional
    private float stuckTimer = 0f;
    private Vector2 lastPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }

    private void Update()
    {
        // Comprobar si la pelota se ha quedado atrapada
        if (Vector2.Distance(rb.position, lastPosition) < 0.1f)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer > stuckTimeThreshold)
            {
                // Aplicar una fuerza en una dirección aleatoria para sacar la pelota de la esquina
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * forceOutOfCorner, ForceMode2D.Impulse);
                stuckTimer = 0f; // Reiniciar el temporizador
            }
        }
        else
        {
            stuckTimer = 0f; // Reiniciar el temporizador si la pelota se mueve
        }

        lastPosition = rb.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 velocity = rb.velocity;

        float angle = Vector2.Angle(velocity, normal);

        if (angle > 45f && angle < 135f)
        {
            rb.velocity = Vector2.Reflect(velocity, normal) * cornerBounceMultiplier;
        }
        else
        {
            rb.velocity = Vector2.Reflect(velocity, normal) * bounceMultiplier;
        }
    }
}
