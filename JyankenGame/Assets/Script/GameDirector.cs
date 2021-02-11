using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private GameObject jankenText;
    private GameObject restartButton;
    private GameObject exitButton;

    private float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.jankenText = GameObject.Find("JankenText");
        this.restartButton = GameObject.Find("RestartButton");
        this.exitButton = GameObject.Find("ExitButton");
        this.restartButton.SetActive(false);
        this.exitButton.SetActive(false);
    }


    /// <summary>
    /// もう一度ボタンがクリックされたら、シーンを再読み込みします。
    /// </summary>
    public void onClickRestart()
    {
        SceneManager.LoadScene("GameScene");
    }


    /// <summary>
    /// やめるボタンがクリックされたら、ゲームを終了します。
    /// </summary>
    public void onClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        UnityEngine.Application.Quit();
#endif
    }


    /// <summary>
    /// JankenControllerから呼び出される関数。勝敗を判定する関数を呼び出します。
    /// </summary>
    /// <param name="playerScore">プレイヤーのじゃんけん結果</param>
    /// <param name="enemyScore">敵のじゃんけん結果</param>
    public void ShowResult(int playerScore, int enemyScore)
    {
        this.jankenText.GetComponent<Text>().text = "ぽん！";
        StartCoroutine(Judgement(delay, playerScore, enemyScore));
    }


    /// <summary>
    /// 勝敗を判定するコルーチン。
    /// </summary>
    /// <param name="delay">ぽん！が表示された後何秒後に実行されるかを指定</param>
    /// <param name="playerScore">プレイヤーのじゃんけん結果</param>
    /// <param name="enemyScore">敵のじゃんけん結果<</param>
    /// <returns>IEnumerator型</returns>
    private IEnumerator Judgement(float delay, int playerScore, int enemyScore)
    {
        yield return new WaitForSeconds(delay);

        if (playerScore == enemyScore)
        {
            this.jankenText.GetComponent<Text>().text = "あいこだよ";
            ActiveButtons();
            yield break;
        }

        // プレイヤーが勝つパターン
        // 1. プレイヤー: グー,   敵: チョキ
        // 2. プレイヤー: チョキ, 敵: パー
        // 3. プレイヤー: パー,   敵: グー
        if ((playerScore == 0 && enemyScore == 1)
            || (playerScore == 1 && enemyScore == 2)
            || (playerScore == 2 && enemyScore == 0))
        {
            this.jankenText.GetComponent<Text>().text = "あなた の 勝ち！！！";
            this.jankenText.GetComponent<Text>().color = Color.red;
            ActiveButtons();
            yield break;
        }
        else
        {
            this.jankenText.GetComponent<Text>().text = "まけてしまった...";
            this.jankenText.GetComponent<Text>().color = Color.blue;
            ActiveButtons();
            yield break;
        }
    }


    /// <summary>
    /// RestartButtonとExitButtonをアクティブにします。
    /// </summary>
    private void ActiveButtons()
    {
        this.restartButton.SetActive(true);
        this.exitButton.SetActive(true);
    }
}
