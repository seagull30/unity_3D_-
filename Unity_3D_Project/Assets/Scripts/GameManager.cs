using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool IsGameOver = false;
    public event UnityAction BookEvent;
    public event UnityAction<GameObject> Playersound;
    public event UnityAction EscapeEvent;
    public int ExitCount;
    public int BookCount { get; private set; }
    public GameObject Player;

    private void Awake()
    {
        Player.GetComponentInChildren<Inventory>().BookEvent += FindBook;
        Player.GetComponentInChildren<Inventory>().BookEvent += PlayerShoutOut;
        Player.GetComponentInChildren<ActionController>().PlayerSound += PlayerShoutOut;
    }

    internal void Ending()
    {
        Debug.Log("Å»ÃâÇÔ");
    }

    public void Start()
    {

    }

    public void GameOver()
    {

    }

    public void FindBook(GameObject player)
    {
        ++BookCount;
        BookEvent.Invoke();
        if (BookCount >= 7)
            EscapeEvent.Invoke();
    }

    public void PlayerShoutOut(GameObject player)
    {
        Playersound.Invoke(player);
    }

}
