using UnityEngine;

public class TorniqueteController : MonoBehaviour
{
    public GameObject objBtnPrefab; // Prefab de obj_btn
    private bool isPlayerInRange = false;
    private bool torniqueteDeshabilitado = false;

    void Update()
    {
        // Solo comprobar el inventario si el jugador está cerca y el torniquete no está ya deshabilitado
        if (isPlayerInRange && CheckInventoryForObjBtn() && !torniqueteDeshabilitado)
        {
            // Deshabilitar el torniquete
            gameObject.SetActive(false);
            torniqueteDeshabilitado = true;
            Debug.Log("Torniquete deshabilitado.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // El jugador está cerca del torniquete
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // El jugador se aleja del torniquete
            isPlayerInRange = false;

            // Reactivar el torniquete si fue deshabilitado
            if (torniqueteDeshabilitado)
            {
                gameObject.SetActive(true);
                torniqueteDeshabilitado = false;
                Debug.Log("Torniquete reactivado.");
            }
        }
    }

    bool CheckInventoryForObjBtn()
    {
        // Obtener los slots del inventario
        GameObject[] inventario = GameObject.FindGameObjectWithTag("general_events").GetComponent<InventoryController>().getSlots();

        // Recorrer los slots y verificar si alguno es igual a obj_btn
        foreach (GameObject slot in inventario)
        {
            if (slot != null && slot.tag == objBtnPrefab.tag)
            {
                return true; // Encontró el objeto en el inventario
            }
        }
        return false; // No encontró el objeto en el inventario
    }
}
