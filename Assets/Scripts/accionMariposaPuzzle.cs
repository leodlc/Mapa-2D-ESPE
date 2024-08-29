using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accionMariposaPuzzle : MonoBehaviour
{
    public GameObject dialogBox; // El panel del diálogo
    public string dialogText; // El texto que quieres mostrar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en contacto
        {
            ShowDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador sale del contacto
        {
            HideDialog();
        }
    }

    void ShowDialog()
    {
        dialogBox.SetActive(true);
        // Aquí puedes asignar el texto al UI de diálogo
        // Ejemplo: dialogBox.GetComponentInChildren<Text>().text = dialogText;
    }

    void HideDialog()
    {
        dialogBox.SetActive(false);
    }
}
