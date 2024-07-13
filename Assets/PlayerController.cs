using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
     private Animator animator;
    private Rigidbody2D rb2d;

void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
       // Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        //transform.position += movement * speed * Time.deltaTime;
    }
}

