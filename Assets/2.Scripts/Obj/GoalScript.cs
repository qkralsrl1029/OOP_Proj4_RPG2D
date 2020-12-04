using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    
    [SerializeField] Sprite TreasureBox_Open;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") //플레이어가 골 컬라이더 진입
        {
          //  AudioManager.instance.PlaySFX("StageClear");    //스테이지 클리어
            this.GetComponent<SpriteRenderer>().color = Color.white;
            this.GetComponent<SpriteRenderer>().sprite = TreasureBox_Open;

            GameManager.instance.ChangeScene();
        }
    }
}
