using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    SpriteRenderer currentImg;
    [SerializeField] Sprite collapseIng;
    BoxCollider2D col;

    private void Start()
    {
        currentImg = GetComponentInChildren<SpriteRenderer>();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collapse();
        }
    }

    void collapse()
    {
        currentImg.sprite = collapseIng;
        //부숴지는 소리 추가
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 2f);
        
    }
}
