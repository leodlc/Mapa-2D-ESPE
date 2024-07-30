using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSate { Dialog, Battle}
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameSate state;

    private void Update()
    {
        if(state == GameSate.Dialog)
        {
            

        }else if (state == GameSate.Battle)
        {

        }
    }
}
