using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] pieces;

    public void CheckIfSolved()
    {
        foreach (PuzzlePiece piece in pieces)
        {
            if (!piece.IsInCorrectPosition())
                return;
        }
        OnPuzzleSolved();
    }

    void OnPuzzleSolved()
    {
        Debug.Log("Puzzle Resuelto!");
        // Aquí puedes abrir la puerta, mostrar un mensaje, o volver a la escena principal
    }
}