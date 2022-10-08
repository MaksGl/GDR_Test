using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private Text message;
    [SerializeField]
    private Text score;
    [SerializeField]
    private GameObject panel;

    private Camera myCamera;

    public void Initialization(Camera _myCamera, Spawner _spawner)
    {
        myCamera = _myCamera;
        _spawner.sceneIsSet += StartGame;
    }

    private void StartGame()
    {
        myCamera.cullingMask = 2147483647;
    }

    public void UpdateScore(string _score)
    {
        score.text = _score;
    }

    public void PanelActive(string _message)
    {
        message.text = _message;
        panel.SetActive(true);
    }
}
