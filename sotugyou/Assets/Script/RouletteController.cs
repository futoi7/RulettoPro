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
    private string result;
    [SerializeField] private TextMeshProUGUI resultText;
    public float rotationSpeed = 5.0f;
    private float lastScrollWheelInputTime; // 最後にマウススクロールホイールの入力があった時間
    public float stopThreshold = 1.0f; // ルーレットが停止したとみなす閾値（秒
    private bool ScrollWheel=false;
    [SerializeField] private float rouletteSpeed; // ルーレットの速度を保持する変数
    public float RouletteSpeed => rouletteSpeed; // プロパティを介して外部からアクセスできるようにする

    private Quaternion previousRotation;//ルーレットのｚ回転の変数
    private int frameCount = 0;
    private int comparisonInterval = 60; // 比較間隔

    Slider _slider;

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
                Debug.Log("回転は同じです。");
                ShowResult(roulette.transform.eulerAngles.z);
                ScrollWheel = false;
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
        for (int i = 1; i <= rMaker.choices.Count; i++)
        {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette))))
            {
                result = rMaker.choices[i - 1];
            }
        }
        resultText.text = resultText.text + result + "hit;";
        if (result == "kougeki")
        {
            //_slider.value = 0f;
        }
        if (result == "kaihuku")
        {
            //_slider.value = 1f;
        }
    }
}