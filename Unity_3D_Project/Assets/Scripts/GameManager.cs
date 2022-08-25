using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool IsGameOver = false;
    public event UnityAction FindBook;
    public event UnityAction<GameObject> Playersound;
    public GameObject Player;

    public void Start()
    {

    }

    public void GameOver()
    {

    }

}
