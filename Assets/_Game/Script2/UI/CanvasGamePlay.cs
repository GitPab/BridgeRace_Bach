using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI brickText;

    public override void Setup()
    {
        base.Setup();
        UpdateScore(0);
    }

    public void UpdateScore(int coin)
    {
        brickText.text = coin.ToString();
    }

    public void SettingButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
        GameManager.Instance.ChangeState(GameState.Pause);
    }

    internal void UpdateScore()
    {
        throw new NotImplementedException();
    }
}
