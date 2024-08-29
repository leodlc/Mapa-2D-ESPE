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
    int correctedPipes=0;

    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++) // Corregí "Lenght" a "Length"
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    public void correctMove(){
        correctedPipes+=1;
        Debug.Log("correct move"+totalPipes+" - "+correctedPipes);
        if (correctedPipes==totalPipes){
            Debug.Log("You Win!"+totalPipes+" - "+correctedPipes);
             SceneManager.UnloadSceneAsync("Puzzle2");
        }
    }

    public void wrongMove(){
        correctedPipes -=1;
    }


}
