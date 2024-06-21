using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNotAvailable : UICanvas
{
    public void MainMenuButton()
    {
        Close(0);
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasVictory>();
    }
}
