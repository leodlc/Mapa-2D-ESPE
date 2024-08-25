using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool isPlayerInRange; // Verifica si el jugador est� dentro del rango
    public GameObject obj; // El objeto a recoger
    public int cantidad = 1; // Cantidad del objeto

    private void Update()
    {
        // Si el jugador est� en rango y presiona la tecla R, se recoge el objeto
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        // Obtiene el inventario del jugador
        GameObject[] inventario = GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().getSlots();

        // Busca un espacio vac�o en el inventario para colocar el objeto
        for (int i = 0; i < inventario.Length; i++)
        {
            if (!inventario[i])
            {
                // A�ade el objeto al inventario y destruye el objeto en la escena
                GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().setSlot(obj, i, cantidad);
                Destroy(gameObject);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador entra en el rango de colisi�n, habilita la recolecci�n
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si el jugador sale del rango de colisi�n, deshabilita la recolecci�n
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
