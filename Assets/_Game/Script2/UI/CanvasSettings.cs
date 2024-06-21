using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons;

    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if ( canvas is CanvasMainMenu )
        {
            buttons[3].gameObject.SetActive(true);
        } else if (canvas is CanvasGamePlay)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);
        }
    }

    public void ContinueButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void RetryButton()
    {
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnRetry();
        Close(0);
    }

    public void CloseButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        Close(0);
    }

    public void SaveButton()
    {
        LevelManager.Instance.SaveLevel(KeyPlayerPrefs.CURRENT_LEVEL_INDEX_STRING);
    }
}
