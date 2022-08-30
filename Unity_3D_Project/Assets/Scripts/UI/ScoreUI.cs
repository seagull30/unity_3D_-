using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Image _image;
    private Text _text;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<Text>();
        GameManager.Instance.BookEvent += UpdateScore;
    }

    private void Update()
    {

    }

    void UpdateScore()
    {
        _text.text = $"Book : {GameManager.Instance.BookCount} / 7";
        if (GameManager.Instance.BookCount>=7)
        {
            _text.text = "<color=red>Find Exit</color>";
            _image.enabled = false;
        }
    }
}
