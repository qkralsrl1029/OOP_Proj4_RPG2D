using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") //플레이어가 골 컬라이더 진입
        {
            AudioManager.instance.PlaySFX("Portal");    //스테이지 클리어           
            GameManager.instance.ChangeScene();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
