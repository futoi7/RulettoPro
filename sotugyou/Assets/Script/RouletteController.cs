using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RouletteController : MonoBehaviour
{
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;

    private string result;//ルーレットの結果の格納変数
    [SerializeField] private TextMeshProUGUI resultText;//結果の表示TEXT
    public float rotationSpeed = 5.0f;//ルーレットの回転スピード
    private float lastScrollWheelInputTime; // 最後にマウススクロールホイールの入力があった時間
    public float stopThreshold = 1.0f; // ルーレットが停止したとみなす閾値（秒
    private bool ScrollWheel=false;//最初のマウスホイール制御変数
    [SerializeField] private float rouletteSpeed; // ルーレットの速度を保持する変数
    public float RouletteSpeed => rouletteSpeed; // プロパティを介して外部からアクセスできるようにする
    private Quaternion previousRotation;//ルーレットのｚ回転の変数
    private int frameCount = 0;//回り始めてからのフレームカウンター
    private int comparisonInterval = 180; // 比較間隔

    Slider _slider; //HPバー
    [SerializeField]GameObject shieldRoulettoObject;//装備決めのシーンで使用
    [SerializeField]GameObject WponsRoulrtto;//装備決めのシーンで使用
    [SerializeField] GameObject[] RoulettoORButton;//スキルルーレット
    UIManager UIManager;
    ScrollSelect ScrollSelect;
    HPmanegment HPmanegment;



    private void Start()
    {
        previousRotation = roulette.transform.rotation;
        //_slider = GameObject.Find("EnemyHP").GetComponent<Slider>();
        //_slider.value = 1f;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)     // マウスホイールが回された場合
            ScrollWheel = true; // フラグを下ろして、以降の処理を実行可能にする

        rouletteSpeed = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed; // ルーレットの速度を更新する
        roulette.transform.Rotate(Vector3.forward, rouletteSpeed, Space.World);

        if (frameCount % comparisonInterval == 0 && ScrollWheel==true)
        {
            if (Quaternion.Angle(roulette.transform.rotation, previousRotation) == 0f&&ScrollWheel==true)
            { 
                ScrollWheel = false;
                Debug.Log("回転は同じです。");
                ShowResult(roulette.transform.eulerAngles.z);
            }
            else
            {
                Debug.Log("回転は異なります。");
            }
            
            previousRotation = roulette.transform.rotation;
        }
        frameCount++;
    }

    private void ShowResult(float x)
    {
        //resultText.text = "Wepon:";
        for (int i = 1; i <= rMaker.choices.Count; i++)
        {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette))))
            {
                result = rMaker.choices[i - 1];
            }
        }
        
        switch (result)
        {
            //武器選択ルーレット
            case "ken":
                //武器が剣のとき
                ShowWeponRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "yari":
                //武器が槍のとき
                ShowWeponRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "kobusi":
                //武器が拳のとき
                ShowWeponRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "tue":
                //武器が杖のとき
                ShowWeponRouletto(true, false, "\nWponsRoulrtto:");
                break;


            //防具選択ルーレット
            case "a":
                //防具がのとき
                ShowWeponRouletto(false, true, "\nshieldRouletto:");
                break;
            case "b":
                //防具がのとき
                ShowWeponRouletto(false, true, "\nshieldRouletto:");
                break;
            case "c":
                //防具がのとき
                ShowWeponRouletto(false, true, "\nshieldRouletto:");
                break;
            case "d":
                //防具がのとき
                ShowWeponRouletto(false, true, "\nshieldRouletto:");
                break;


            //技選択ルーレット
            case "kyou":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdateEnemyDownHP(50);
                break;
            case "zyaku":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdateEnemyDownHP(30);
                break;
            case "misu":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdateEnemyDownHP(0);
                break;

            //技選択ルーレット
            case "oisii":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdatePlayerUPHP(50);
                break;
            case "nigai":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdatePlayerUPHP(30);
                break;
            case "karai":
                //とき
                SkillRouletto("\nSkillRuletto:");
                HPmanegment.UpdatePlayerUPHP(10);
                break;

            case "kougeki":
                //_slider.value = 0f;
                break;
            case "kaihuku":
                //_slider.value = 1f;
                break;

            default:
                break;
        }
    }

    private void ShowWeponRouletto(bool activ, bool notactiv, string HitText)
    {
        resultText.text = resultText.text + HitText + result + "hit;";
        shieldRoulettoObject.SetActive(activ);
        WponsRoulrtto.SetActive(notactiv);
    }

    private void SkillRouletto(string HitText)
    {
        resultText.text = resultText.text + HitText + result + "hit;";
        RoulettoORButton[0].SetActive(true);
        RoulettoORButton[1].SetActive(true);
        RoulettoORButton[2].SetActive(false);
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        UIManager.StartCountDown();
        ScrollSelect = GameObject.Find("ScrollSelect").GetComponent<ScrollSelect>();
        ScrollSelect.currentTime = 0f;
        HPmanegment = GameObject.Find("HPManegment").GetComponent<HPmanegment>();
    }
}