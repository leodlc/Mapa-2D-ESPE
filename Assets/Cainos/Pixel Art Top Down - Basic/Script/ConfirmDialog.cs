using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private TMP_Text confirmText;  // Texto de confirmación

    private GameObject confirmPanel;  // Panel de confirmación (Canvas)
    private GameObject confirmWindow;  // Ventana de confirmación (Image)
    private Button btnSi;  // Botón "Sí"
    private Button btnNo;  // Botón "No"

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    void Start()
    {
        // Buscar el panel de confirmación, la ventana, y los botones usando tags
        confirmPanel = GameObject.FindGameObjectWithTag("confirmp");
        confirmWindow = GameObject.FindGameObjectWithTag("confirmar");  // Esto es opcional, ya que confirmPanel es el padre
        btnSi = GameObject.FindGameObjectWithTag("btnsi").GetComponent<Button>();
        btnNo = GameObject.FindGameObjectWithTag("btnno").GetComponent<Button>();

        // Verificar que los componentes se encontraron correctamente
        if (confirmPanel == null)
        {
            Debug.LogError("El canvas de confirmación (ConfirmPanel) no se encontró. Asegúrate de que el tag 'confirmp' esté asignado correctamente.");
        }
        if (btnSi == null)
        {
            Debug.LogError("El botón 'Sí' (btnsi) no se encontró. Asegúrate de que el tag 'btnsi' esté asignado correctamente.");
        }
        if (btnNo == null)
        {
            Debug.LogError("El botón 'No' (btnno) no se encontró. Asegúrate de que el tag 'btnno' esté asignado correctamente.");
        }

        // Asegúrate de que el panel de confirmación esté desactivado al inicio
        confirmPanel?.SetActive(false);

        // Asignar los métodos a los botones
        btnSi?.onClick.AddListener(OnConfirmYes);
        btnNo?.onClick.AddListener(OnConfirmNo);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);

        lineIndex = 0;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);

        // Reanudar el tiempo antes de mostrar el panel de confirmación
        Time.timeScale = 1f;

        // Mostrar el panel de confirmación al final del diálogo
        ShowConfirmDialog("¿Aceptas el desafío?");
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            Debug.Log("Se puede iniciar un dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            Debug.Log("No Se puede iniciar un dialogo");
        }
    }

    private void ShowConfirmDialog(string message)
    {
        if (confirmPanel != null)
        {
            // Mostrar el mensaje de confirmación
            confirmText.text = message;

            // Mostrar el panel de confirmación (Canvas)
            confirmPanel.SetActive(true);
            Debug.Log("Panel de confirmación activado.");
        }
        else
        {
            Debug.LogError("No se puede mostrar el panel de confirmación porque no se encontró el objeto.");
        }
    }

    private void OnConfirmYes()
    {
        // Reanudar el juego
        Time.timeScale = 1f;

        // Cargar la escena "JuegoFutbol"
        SceneManager.LoadScene("JuegoFutbol");
    }

    private void OnConfirmNo()
    {
        // Reanudar el juego (por si acaso el tiempo estuviera en pausa)
        Time.timeScale = 1f;

        // Ocultar el panel de confirmación
        confirmPanel.SetActive(false);
    }
}
