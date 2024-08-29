using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PuzzleManager : MonoBehaviour
{
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>(); // Lista de todas las piezas
    public RectTransform emptySpace; // Referencia al espacio vacío

    private Vector2 emptySpacePosition;

    private void Start()
    {
        emptySpacePosition = emptySpace.anchoredPosition;
        ShufflePuzzle();
    }

    public void ShufflePuzzle()
    {
        List<Vector2> positions = new List<Vector2>();
        foreach (var piece in puzzlePieces)
        {
            positions.Add(piece.GetComponent<RectTransform>().anchoredPosition);
        }

        // Mezcla las posiciones
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2 temp = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // Asigna las posiciones mezcladas a las piezas
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].GetComponent<RectTransform>().anchoredPosition = positions[i];
        }

        emptySpacePosition = emptySpace.anchoredPosition;
    }

    public void TryMovePiece(PuzzlePiece piece)
    {
        Vector2 piecePosition = piece.GetComponent<RectTransform>().anchoredPosition;
        float distance = Vector2.Distance(piecePosition, emptySpacePosition);

        if (distance < 110) // Asegúrate de que esté lo suficientemente cerca para moverse
        {
            piece.GetComponent<RectTransform>().anchoredPosition = emptySpacePosition;
            emptySpace.anchoredPosition = piecePosition;
            emptySpacePosition = piecePosition;
        }

        if (IsPuzzleSolved())
        {
            Debug.Log("¡Puzzle completado!");
        }
    }

    bool IsPuzzleSolved()
    {
        foreach (var piece in puzzlePieces)
        {
            if (piece.GetComponent<RectTransform>().anchoredPosition != piece.originalPosition)
            {
                return false;
            }
        }
        return true;
    }
}
