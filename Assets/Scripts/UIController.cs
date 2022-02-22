using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private Image _popUp = null;

    public Text Score;
    public Text PopUpText;
    public Image[] Lives;
    private const int LIVES_NUM = 3;

    // Start is called before the first frame update
    void Start()
    {
        _popUp.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "SCORE: " + _sceneController.Score;

        for (int i = 0; i < LIVES_NUM; i++)
        {
            if(i > _player.Lives - 1 && Lives[i] != null)
            {
                Destroy(Lives[i]);
            }
        }

        if(_player.Lives == 0)
        {
            _popUp.gameObject.SetActive(true);
            PopUpText.text = "YOU LOSE!";
            Time.timeScale = 0;
        }

        if(_sceneController.EnemiesAlive == 0)
        {
            _popUp.gameObject.SetActive(true);
            PopUpText.text = "YOU WON!";
            Time.timeScale = 0;
        }
    }
}
