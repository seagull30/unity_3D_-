using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool IsGameOver = false;
    public event UnityAction BookEvent;
    public event UnityAction<GameObject> Playersound;
    public GameObject Player;

    private void Awake()
    {
        Player.GetComponentInChildren<Inventory>().BookEvent += FindBook;
        Player.GetComponentInChildren<ActionController>().PlayerSound += PlayerShoutOut;
    }

    public void Start()
    {

    }

    public void GameOver()
    {

    }

    public void FindBook()
    {
        BookEvent.Invoke();
    }

    public void PlayerShoutOut(GameObject player)
    {
        Playersound.Invoke(player);
    }

}
