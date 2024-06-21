using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    Level currentLevel;
    public int currentLevelIndex = 1;

    public Player player;
    public List<Enemy> enemy;

    public void OnStart()
    {
        currentLevelIndex = 1;
        OnReset();
        LoadLevel();
    }

    public void OnReset()
    {
        player.joystick.gameObject.SetActive(false);
        player.joystick.gameObject.SetActive(true);
        //CLear BrickInStage
        if (currentLevel != null)
        {
            for (int i = 0; i < currentLevel.floors.Count; i++)
            {
                currentLevel.floors[i].ClearBrickInStage();
            }
        }


        //Clear BrickStack
        player.ClearBrick();
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].ClearBrick();
        }
    }

    public void LoadLevel()
    {

        UIManager.Instance.OpenUI<CanvasGamePlay>();
        LoadLevel(currentLevelIndex);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (indexLevel <= levels.Count)
        {
            currentLevel = Instantiate(levels[indexLevel - 1]);
        }
    }

    public void OnInit()
    {

        //Player init
        player.gameObject.SetActive(true);
        player.transform.position = currentLevel.playerStartPoint.position;
        Debug.Log(player.transform.position);
        player.score = 0;
        player.OnInit();

        //Enemy init
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].gameObject.SetActive(true);
            enemy[i].transform.position = currentLevel.enemyStartPoint[i].position;
            Debug.Log(enemy[i].transform.position);
            enemy[i].OnInit();
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].winPos = currentLevel.winPoint.position;
        }
    }

    public void OnFinish()
    {
        OnReset();
        UIManager.Instance.CloseUI<CanvasGamePlay>(0);
    }

    public void OnNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex <= levels.Count)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].agent.speed *= 1.5f;
            }
            OnReset();
            LoadLevel();
        }
        else
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<CanvasNotAvailable>();
        }
    }

    public void OnRetry()
    {
        OnReset();
        LoadLevel();
    }

    public void SaveLevel(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.DeleteKey(keyName);
        }

        PlayerPrefs.SetInt(keyName, currentLevelIndex);
        PlayerPrefs.Save();
    }

    public void LoadSaveLevel(string keyName)
    {
        currentLevelIndex = PlayerPrefs.GetInt(keyName);
        OnReset();
        LoadLevel();
    }
}
