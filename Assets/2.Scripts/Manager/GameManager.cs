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
        AudioManager.instance.PlayBGM("Start");
        DontDestroyOnLoad(gameObject);
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static int StageNum = 0;
    public static bool isEnd = false;
    [SerializeField] Image Panel;     //페이드 아웃용 검은 화면
    [SerializeField] Texture2D cursorImg;
    float currentTime = 0;  //현재 시간
    float fadeoutTime = 2;  //페이드아웃이 진행될 시간

    public void ChangeScene()
    {
        StartCoroutine(fadeOut());
      
    }

    public void ChangeStage()
    {
        StageNum++;
        SceneManager.LoadScene("Stage" + StageNum.ToString(), LoadSceneMode.Single);
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM("Stage" + StageNum.ToString() + "Bgm");
        
        //FindObjectOfType<monsterGenerator>().createMonster();
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
        if (alpha.a > 0.9f)
        {
            ChangeStage();
            StartCoroutine(fadeIn());
        }
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
