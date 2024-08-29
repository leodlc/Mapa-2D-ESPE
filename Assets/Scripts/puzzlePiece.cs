using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2 originalPosition; // La posición original de la pieza
    public PuzzleManager puzzleManager; // Referencia al PuzzleManager

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPieceClicked);
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnPieceClicked()
    {
        Debug.Log($"Pieza {gameObject.name} clickeada.");
        puzzleManager.TryMovePiece(this); // Llama a TryMovePiece en el PuzzleManager
    }
}
