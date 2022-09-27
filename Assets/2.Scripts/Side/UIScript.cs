using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject Title;
    [SerializeField] GameObject IngameUI;
    [SerializeField] GameObject Explanations;


    [SerializeField] Image Panel;     //페이드 아웃용 검은 화면
    float currentTime = 0;  //현재 시간
    float fadeoutTime = 2;  //페이드아웃이 진행될 시간


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void gotoPlay()
    {
        Title.SetActive(false);       
        GameManager.instance.ChangeScene();
    }


    public void SetExplanation()
    {
        Explanations.SetActive(true);
    }

    public void HideExplanation()
    {
        Explanations.SetActive(false);
    }
}
