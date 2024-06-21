using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] GameObject[] buttons;

    public void SetState()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if (!PlayerPrefs.HasKey(KeyPlayerPrefs.CURRENT_LEVEL_INDEX_STRING))
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
        }
        else
        {
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
        }
    }
    public void PlayButton()
    {
        Close(0);
        LevelManager.Instance.OnStart();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }

    public void ContinueButton()
    {
        Close(0);
        LevelManager.Instance.LoadSaveLevel(KeyPlayerPrefs.CURRENT_LEVEL_INDEX_STRING);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void NewGameButton()
    {
        Close(0);
        if (PlayerPrefs.HasKey(KeyPlayerPrefs.CURRENT_LEVEL_INDEX_STRING))
        {
            PlayerPrefs.DeleteKey(KeyPlayerPrefs.CURRENT_LEVEL_INDEX_STRING);
        }
        LevelManager.Instance.OnStart();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }


}
