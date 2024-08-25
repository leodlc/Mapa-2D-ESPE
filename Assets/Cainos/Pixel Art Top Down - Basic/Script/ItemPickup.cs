using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool isPlayerInRange; // Verifica si el jugador está dentro del rango
    public GameObject obj; // El objeto a recoger
    public int cantidad = 1; // Cantidad del objeto

    private void Update()
    {
        // Si el jugador está en rango y presiona la tecla R, se recoge el objeto
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        // Obtiene el inventario del jugador
        GameObject[] inventario = GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().getSlots();

        // Busca un espacio vacío en el inventario para colocar el objeto
        for (int i = 0; i < inventario.Length; i++)
        {
            if (!inventario[i])
            {
                // Añade el objeto al inventario y destruye el objeto en la escena
                GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().setSlot(obj, i, cantidad);
                Destroy(gameObject);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador entra en el rango de colisión, habilita la recolección
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si el jugador sale del rango de colisión, deshabilita la recolección
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
