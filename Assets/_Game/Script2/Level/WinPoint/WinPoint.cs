using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WinPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().Win();
            GameManager.Instance.ChangeState(GameState.Finish);
            LevelManager.Instance.OnFinish();
            UIManager.Instance.OpenUI<CanvasVictory>();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character>().Win();
            GameManager.Instance.ChangeState(GameState.Finish);
            LevelManager.Instance.OnFinish();
            UIManager.Instance.OpenUI<CanvasFail>();
        }

    }
}
