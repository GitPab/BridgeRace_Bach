using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Pause }
public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);     
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.MainMenu:
                OnStart();
                break;
            case GameState.GamePlay:
                break;
            case GameState.Finish:
                break;
            case GameState.Pause:
                break;
            default:
                break;
        }
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }

    private void OnStart()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>().SetState();
        LevelManager.Instance.OnStart();
    }

}
