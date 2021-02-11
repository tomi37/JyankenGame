using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankenController : MonoBehaviour
{
    // プレイヤーのじゃんけんの手
    private readonly GameObject[] playerJanken = new GameObject[3];

    // 敵のじゃんけんの手
    private GameObject enemyJanken;

    // プレイヤーの選んだ手を表示する位置
    private readonly int showPoistionY = -3;

    // 監督オブジェクト
    private GameObject director;

    // 一回だけ選択できるようにするため、フラグを追加
    private bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyJanken = GameObject.Find("mark_question");
        this.playerJanken[0] = GameObject.Find("janken_gu");
        this.playerJanken[1] = GameObject.Find("janken_choki");
        this.playerJanken[2] = GameObject.Find("janken_pa");
        this.director = GameObject.Find("GameDirector");
        isClicked = false;
    }


    /// <summary>
    /// グーがクリックされたときの処理
    /// </summary>
    public void OnClickGu()
    {
        int playerScore = 0;
        int enemyScore = Random.Range(0, 3);
        ImageClick(playerScore, enemyScore);
    }


    /// <summary>
    /// チョキがクリックされたときの処理
    /// </summary>
    public void OnClickTyoki()
    {
        int playerScore = 1;
        int enemyScore = Random.Range(0, 3);
        ImageClick(playerScore, enemyScore);
    }


    /// <summary>
    /// パーがクリックされたときの処理
    /// </summary>
    public void OnClickPa()
    {
        int playerScore = 2;
        int enemyScore = Random.Range(0, 3);
        ImageClick(playerScore, enemyScore);
    }


    /// <summary>
    /// グー、チョキ、パーがクリックされたときの共通の処理を記述します。
    /// </summary>
    /// <param name="playerScore">プレイヤーのじゃんけん結果</param>
    /// <param name="enemyScore">敵のじゃんけん結果</param>
    private void ImageClick(int playerScore, int enemyScore)
    {
        if (!isClicked)
        {
            isClicked = true;
            DisablePlayerJanken();
            ShowPlayerJanken(playerScore);
            ShowEnemyJanken(enemyScore);
            this.director.GetComponent<GameDirector>().ShowResult(playerScore, enemyScore);
        }
    }


    /// <summary>
    /// プレイヤーのじゃんけん画像を非表示にします。
    /// </summary>
    private void DisablePlayerJanken()
    {
        for (int i = 0; i < this.playerJanken.Length; ++i)
        {
            this.playerJanken[i].SetActive(false);
        }
    }

    
    /// <summary>
    /// プレイヤーの選択した画像を表示します。
    /// </summary>
    /// <param name="index">playerJankenの要素</param>
    private void ShowPlayerJanken(int index)
    {
        this.playerJanken[index].transform.position = new Vector3(0, this.showPoistionY, 0);
        this.playerJanken[index].SetActive(true);
    }


    /// <summary>
    /// 敵のじゃんけん画像を表示します。
    /// </summary>
    /// <param name="index">enemyJankenの要素</param>
    private void ShowEnemyJanken(int index)
    {
        this.enemyJanken.GetComponent<SpriteRenderer>().sprite = this.playerJanken[index].GetComponent<SpriteRenderer>().sprite;
    }
}
