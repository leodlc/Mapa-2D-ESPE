using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Method to call when a pipe is correctly placed
    public void CorrectMove()
    {
        correctedPipes += 1;
        Debug.Log("Correct move: " + totalPipes + " - " + correctedPipes);
        if (correctedPipes == totalPipes)
        {
            Debug.Log("You Win! " + totalPipes + " - " + correctedPipes);
            SceneManager.UnloadSceneAsync("Puzzle2");
        }
    }

    // Method to call when a pipe is incorrectly placed
    public void WrongMove()
    {
        correctedPipes -= 1;
    }
}
