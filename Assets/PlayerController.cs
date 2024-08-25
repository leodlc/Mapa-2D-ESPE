using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    private Animator animator;
    private Rigidbody2D rb2d;

    GameObject inventory_com;
    private bool inventoryVisible = false;

    void Start()
    {
        // Verificamos si los componentes fueron asignados correctamente
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        // Asegúrate de que el Rigidbody2D no esté en modo Kinematic
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        inventory_com = GameObject.FindGameObjectWithTag("inventario-com");
        inventory_com.SetActive(false);
    }

    void Update()
    {
        // Obtener las entradas del jugador
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Crear un vector de movimiento basado en las entradas
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // Aplicar la velocidad al Rigidbody2D
        rb2d.velocity = movement * speed;

        // Determinar cuál animación reproducir según la dirección del movimiento
        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
            {
                // Movimiento horizontal tiene prioridad
                if (moveHorizontal > 0)
                {
                    animator.Play("Walk_Right");
                }
                else
                {
                    animator.Play("Walk_Left");
                }
            }
            else
            {
                // Movimiento vertical tiene prioridad
                if (moveVertical > 0)
                {
                    animator.Play("Walk_Up");
                }
                else
                {
                    animator.Play("Walk_Down");
                }
            }
        }
        else
        {
            // Si no hay movimiento, volver al estado Idle
            animator.SetBool("isWalking", false);
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            if(!inventoryVisible)
            {
                inventoryVisible = true;
                inventory_com.SetActive(inventoryVisible);
                GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().showInventory();
            }
            else
            {
                inventoryVisible = false;
                inventory_com.SetActive(inventoryVisible);
            }
            
        }
        
    }

}
