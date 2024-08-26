using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class edifPuzzle : MonoBehaviour
{
    public Button[] puzzleButtons; // Asigna los botones desde el Inspector
    public int gridSize = 3; // Tamaño de la cuadrícula (3x3, 4x4, etc.)
    private Vector2 emptySpacePosition;
    private int[] puzzleState;

    private void Start()
    {
        // Inicializar la posición vacía
        emptySpacePosition = new Vector2(gridSize - 1, gridSize - 1);
        puzzleState = new int[puzzleButtons.Length];

        for (int i = 0; i < puzzleButtons.Length; i++)
        {
            int index = i;
            puzzleButtons[i].onClick.AddListener(() => MovePiece(index));
            puzzleState[i] = i;
        }
    }

    private void MovePiece(int index)
    {
        Vector2 piecePosition = new Vector2(index % gridSize, index / gridSize);

        if (Vector2.Distance(piecePosition, emptySpacePosition) == 1)
        {
            puzzleButtons[index].transform.position = emptySpacePosition * (puzzleButtons[index].GetComponent<RectTransform>().sizeDelta);
            emptySpacePosition = piecePosition;

            // Actualizar el estado del puzzle
            puzzleState[index] = -1;
            puzzleState[(int)(emptySpacePosition.x + emptySpacePosition.y * gridSize)] = index;

            if (IsPuzzleSolved())
            {
                Debug.Log("Puzzle resuelto!");
                // Llama a la función para abrir la puerta o continuar el juego
                PuzzleSolved();
            }
        }
    }

    private bool IsPuzzleSolved()
    {
        for (int i = 0; i < puzzleState.Length; i++)
        {
            if (puzzleState[i] != i)
                return false;
        }
        return true;
    }

    private void PuzzleSolved()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
