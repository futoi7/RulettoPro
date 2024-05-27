using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPmanegment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerHPText;
    [SerializeField] private TextMeshProUGUI EnemyHPText;
    public int PlayerHP = 100; // プレイヤーのHPを初期化
    public int EnemyHP = 100; // エネミーのHPを初期化

    // Start is called before the first frame update
    void Start()
    {
        // HPの初期値をUIに反映
        UpdateUI();
    }

    public void UpdatePlayerDownHP(int newHP)
    {
        PlayerHP -= newHP;
        UpdateUI();
    }
    public void UpdatePlayerUPHP(int newHP)
    {
        PlayerHP += newHP;
        UpdateUI();
    }

    // エネミーのHPを更新するメソッド
    public void UpdateEnemyDownHP(int newHP)
    {
        EnemyHP -= newHP;
        UpdateUI();
    }

    // エネミーのHPを更新するメソッド
    public void UpdateEnemyUPHP(int newHP)
    {
        EnemyHP += newHP;
        UpdateUI();
    }

    // UIにHPの値を反映するメソッド
    void UpdateUI()
    {
        PlayerHPText.text = "ぷれいやー: " + PlayerHP.ToString();
        EnemyHPText.text = "てき: " + EnemyHP.ToString();
    }
}
