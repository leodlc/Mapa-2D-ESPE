using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2 correctPosition; // Posición correcta de la pieza
    public GameObject emptySpace; // Espacio vacío del puzzle

    private Vector2 startPos; 
    private Vector2 emptyPos; 
    private Vector2 targetPos;

    private void Start()
    {
        startPos = transform.position;
        emptyPos = emptySpace.transform.position;
    }

    public void OnPieceClicked()
    {
        if (IsAdjacentToEmptySpace())
        {
            targetPos = emptyPos;
            emptySpace.transform.position = startPos;
            transform.position = targetPos;

            startPos = targetPos;
            emptyPos = emptySpace.transform.position;
            
            // Check if the puzzle is solved
            FindObjectOfType<PuzzleManager>().CheckIfSolved();
        }
    }

    private bool IsAdjacentToEmptySpace()
    {
        return (Mathf.Abs(startPos.x - emptyPos.x) == 1 && startPos.y == emptyPos.y) || 
               (Mathf.Abs(startPos.y - emptyPos.y) == 1 && startPos.x == emptyPos.x);
    }

    public bool IsInCorrectPosition()
    {
        return (Vector2)transform.localPosition == correctPosition;
    }
}