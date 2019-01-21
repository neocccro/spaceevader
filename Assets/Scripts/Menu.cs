using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _difficultyMenu;
    [SerializeField]
    private GameObject _gameOverMenu;
    [SerializeField]
    private GameObject _winMenu;
    [SerializeField]
    private GameObject _instructionsMenu;
    [SerializeField]
    private GameObject _ExpertButtonText;

    private WorldGen _worldGen; //level generator

    int columns;
    int rows;
    int minMagnitude;
    int ballAmount;
    float ballSpeed;
    float playerSpeed;

    int ExpertLevel = 0;
    int ExpertLevelUnlocked = 0;

    private void Start()
    {
        _worldGen = GetComponent<WorldGen>();
    }

    public void ToMenu()//main menu
    {
        _worldGen.DestroyMap();
        SetMenuActive(_mainMenu);
    }

    public void ToDifficulty()//choice of difficulty. normal or hard
    {
        _worldGen.DestroyMap();
        SetMenuActive(_difficultyMenu);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UselessButton()
    {
        Debug.Log("Nothing");
    }

    public void PlayNormal()
    {
        ExpertLevel = 0;
        _worldGen.DestroyMap();
        SetMenuActive(null);

        columns = 20;
        rows = 6;
        minMagnitude = 6;
        ballAmount = 2;
        ballSpeed = 2;
        playerSpeed = 2;

        _worldGen.GenerateMap(columns, rows, minMagnitude, ballAmount, ballSpeed, playerSpeed);
    }

    public void PlayHard()
    {
        ExpertLevel = 0;
        _worldGen.DestroyMap();
        SetMenuActive(null);

        columns = 30;
        rows = 8;
        minMagnitude = 8;
        ballAmount = 3;
        ballSpeed = 3;
        playerSpeed = 3;

        _worldGen.GenerateMap(columns, rows, minMagnitude, ballAmount, ballSpeed, playerSpeed);
    }

    public void PlayExpert()
    {
        _worldGen.DestroyMap();
        SetMenuActive(null);

        columns = 40 + 5 * ExpertLevel;
        rows = 10 + 2 * ExpertLevel;
        minMagnitude = 10 + 2 * ExpertLevel;
        ballAmount = 4 + 1 * ExpertLevel;
        ballSpeed = 4 + 1 * ExpertLevel;
        playerSpeed = 4 + 1 * ExpertLevel;

        _worldGen.GenerateMap(columns, rows, minMagnitude, ballAmount, ballSpeed, playerSpeed);
    }

    public void Replay()
    {
        _worldGen.DestroyMap();
        SetMenuActive(null);

        _worldGen.GenerateMap(columns, rows, minMagnitude, ballAmount, ballSpeed, playerSpeed);
    }

    public void GameOver()
    {
        _worldGen.DestroyMap();
        SetMenuActive(_gameOverMenu);
    }

    public void Win()
    {
        _worldGen.DestroyMap();
        SetMenuActive(_winMenu);
        ExpertLevelUnlocked = Mathf.Max(ExpertLevel + 1, ExpertLevelUnlocked);
    }

    public void Instuctions()
    {
        _worldGen.DestroyMap();
        SetMenuActive(_instructionsMenu);
    }

    public void ExpertPlus()
    {
        //ExpertLevel = Mathf.Min(ExpertLevel + 1, ExpertLevelUnlocked);
        ExpertLevel += 1;
        _ExpertButtonText.GetComponent<Text>().text = "Expert + " + ExpertLevel;
    }

    public void ExpertMin()
    {
        ExpertLevel = Mathf.Max(ExpertLevel - 1, 0);
        _ExpertButtonText.GetComponent<Text>().text = "Expert + " + ExpertLevel;
    }

    public void SetMenuActive(GameObject _active)//input menu to activate and deactivate others
    {
        _winMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _difficultyMenu.SetActive(false);
        _instructionsMenu.SetActive(false);
        if(_active != null)
        {
            _active.SetActive(true);
        }
    }
}
