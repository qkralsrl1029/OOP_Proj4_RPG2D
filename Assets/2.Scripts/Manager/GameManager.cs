using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Main = 0,
        Playing = 1,
        Ending,
    }
    GameState gameState = GameState.Main;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    static int StageNum = 2;
    [SerializeField] Image Panel;     //페이드 아웃용 검은 화면
    float currentTime = 0;  //현재 시간
    float fadeoutTime = 2;  //페이드아웃이 진행될 시간

    public void ChangeScene()
    {
        StartCoroutine(fadeOut());
        
    }

    public IEnumerator ChangeStage()
    {
        AsyncOperation asOperation;
        
        asOperation = SceneManager.LoadSceneAsync("Stage" + StageNum++.ToString(), LoadSceneMode.Single);
        while (!asOperation.isDone)
        {
            yield return null;
        }
    }


    IEnumerator fadeOut()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        currentTime = 0;
        while (alpha.a < 1)
        {
            currentTime += Time.deltaTime / fadeoutTime;
            alpha.a = Mathf.Lerp(0, 1, currentTime);
            Panel.color = alpha;
            yield return null;
        }
        StartCoroutine(ChangeStage());
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        Color alpha = Panel.color;
        currentTime = 0;
        while (alpha.a > 0)
        {
            currentTime += Time.deltaTime / fadeoutTime;
            alpha.a = Mathf.Lerp(1, 0, currentTime);
            Panel.color = alpha;
            yield return null;
        }
        //IngameUI.SetActive(true);
    }
}
