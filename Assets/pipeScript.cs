using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0f, 90f, 180f, 270f };
    public float[] correctRotation;
    
    [SerializeField]
    bool isPlaced = false;
    int possibleRots = 1;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        possibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        CheckCorrectPlacement();
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        CheckCorrectPlacement();
    }

    private void CheckCorrectPlacement()
    {
        bool wasPlaced = isPlaced;

        if (possibleRots > 1)
        {
            isPlaced = transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1];
        }
        else
        {
            isPlaced = transform.eulerAngles.z == correctRotation[0];
        }

        if (isPlaced && !wasPlaced)
        {
            gameManager.CorrectMove();
        }
        else if (!isPlaced && wasPlaced)
        {
            gameManager.WrongMove();
        }
    }
}
