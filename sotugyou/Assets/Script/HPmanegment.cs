using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPmanegment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerHPText;
    [SerializeField] private TextMeshProUGUI EnemyHPText;
    public int PlayerHP = 100; // �v���C���[��HP��������
    public int EnemyHP = 100; // �G�l�~�[��HP��������

    // Start is called before the first frame update
    void Start()
    {
        // HP�̏����l��UI�ɔ��f
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

    // �G�l�~�[��HP���X�V���郁�\�b�h
    public void UpdateEnemyDownHP(int newHP)
    {
        EnemyHP -= newHP;
        UpdateUI();
    }

    // �G�l�~�[��HP���X�V���郁�\�b�h
    public void UpdateEnemyUPHP(int newHP)
    {
        EnemyHP += newHP;
        UpdateUI();
    }

    // UI��HP�̒l�𔽉f���郁�\�b�h
    void UpdateUI()
    {
        PlayerHPText.text = "�Ղꂢ��[: " + PlayerHP.ToString();
        EnemyHPText.text = "�Ă�: " + EnemyHP.ToString();
    }
}
