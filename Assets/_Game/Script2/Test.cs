using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && UIManager.Instance.IsOpened<CanvasGamePlay>())
        {
            UIManager.Instance.GetUI<CanvasGamePlay>().UpdateScore(++score);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<CanvasVictory>().SetBestScore(score);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<CanvasFail>().SetBestScore(score);
        }
    }
}
