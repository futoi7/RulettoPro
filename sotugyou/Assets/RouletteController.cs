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

    [SerializeField] private float rouletteSpeed; // ルーレットの速度を保持する変数
    public float RouletteSpeed => rouletteSpeed; // プロパティを介して外部からアクセスできるようにする

    Slider _slider;

    private void Start()
    {
        _slider = GameObject.Find("EnemyHP").GetComponent<Slider>();
        _slider.value = 1f;
    }
    private void Update()
    {
        rouletteSpeed = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed; // ルーレットの速度を更新する
        roulette.transform.Rotate(Vector3.forward, rouletteSpeed * rotationSpeed, Space.World);
        if (rouletteSpeed==0)
        {
            ShowResult(roulette.transform.eulerAngles.z);
        }
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
        resultText.text = result + "HIT.";
        if(result=="kougeki")
        {
            _slider.value = 0f;
        }
        if(result=="kaihuku")
        {
            _slider.value = 1f;
        }
    }
}