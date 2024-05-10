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

    private string result;//���[���b�g�̌��ʂ̊i�[�ϐ�
    [SerializeField] private TextMeshProUGUI resultText;//���ʂ̕\��TEXT
    public float rotationSpeed = 5.0f;//���[���b�g�̉�]�X�s�[�h
    private float lastScrollWheelInputTime; // �Ō�Ƀ}�E�X�X�N���[���z�C�[���̓��͂�����������
    public float stopThreshold = 1.0f; // ���[���b�g����~�����Ƃ݂Ȃ�臒l�i�b
    private bool ScrollWheel=false;//�ŏ��̃}�E�X�z�C�[������ϐ�
    [SerializeField] private float rouletteSpeed; // ���[���b�g�̑��x��ێ�����ϐ�
    public float RouletteSpeed => rouletteSpeed; // �v���p�e�B����ĊO������A�N�Z�X�ł���悤�ɂ���
    private Quaternion previousRotation;//���[���b�g�̂���]�̕ϐ�
    private int frameCount = 0;//���n�߂Ă���̃t���[���J�E���^�[
    private int comparisonInterval = 180; // ��r�Ԋu

    Slider _slider; //HP�o�[
    [SerializeField]GameObject shieldRoulettoObject;//�������߂̃V�[���Ŏg�p
    [SerializeField]GameObject WponsRoulrtto;//�������߂̃V�[���Ŏg�p



    private void Start()
    {
        previousRotation = roulette.transform.rotation;
        //_slider = GameObject.Find("EnemyHP").GetComponent<Slider>();
        //_slider.value = 1f;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)     // �}�E�X�z�C�[�����񂳂ꂽ�ꍇ
            ScrollWheel = true; // �t���O�����낵�āA�ȍ~�̏��������s�\�ɂ���

        rouletteSpeed = Input.GetAxis("Mouse ScrollWheel") * rotationSpeed; // ���[���b�g�̑��x���X�V����
        roulette.transform.Rotate(Vector3.forward, rouletteSpeed, Space.World);

        if (frameCount % comparisonInterval == 0 && ScrollWheel==true)
        {
            if (Quaternion.Angle(roulette.transform.rotation, previousRotation) == 0f&&ScrollWheel==true)
            { 
                ScrollWheel = false;
                Debug.Log("��]�͓����ł��B");
                ShowResult(roulette.transform.eulerAngles.z);
               
            }
            else
            {
                Debug.Log("��]�͈قȂ�܂��B");
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
            //����I�����[���b�g
            case "ken":
                //���킪���̂Ƃ�
                ShowRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "yari":
                //���킪���̂Ƃ�
                ShowRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "kobusi":
                //���킪���̂Ƃ�
                ShowRouletto(true, false, "\nWponsRoulrtto:");
                break;
            case "tue":
                //���킪��̂Ƃ�
                ShowRouletto(true, false, "\nWponsRoulrtto:");
                break;


            //�h��I�����[���b�g
            case "a":
                //�h��̂Ƃ�
                ShowRouletto(false, true, "\nshieldRouletto:");
                break;
            case "b":
                //�h��̂Ƃ�
                ShowRouletto(false, true, "\nshieldRouletto:");
                break;
            case "c":
                //�h��̂Ƃ�
                ShowRouletto(false, true, "\nshieldRouletto:");
                break;
            case "d":
                //�h��̂Ƃ�
                ShowRouletto(false, true, "\nshieldRouletto:");
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

    private void ShowRouletto(bool activ, bool notactiv, string HitText)
    {
        resultText.text = resultText.text + HitText + result + "hit;";
        shieldRoulettoObject.SetActive(activ);
        WponsRoulrtto.SetActive(notactiv);
    }
}