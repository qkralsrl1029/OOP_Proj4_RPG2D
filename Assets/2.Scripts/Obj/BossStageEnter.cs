using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageEnter : MonoBehaviour
{
    bool bgmStart = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !bgmStart)
        {
            bgmStart = true;
            AudioManager.instance.StopBGM();
            AudioManager.instance.PlayBGM("Boss");
        }
    }
}
