using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float damageTime;
    bool damageCheck = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!damageCheck)
                StartCoroutine(GiveDamage());
        }
    }

    IEnumerator GiveDamage()
    {
        damageCheck = true;
        FindObjectOfType<characterController>().getDamage(10);
        yield return new WaitForSeconds(damageTime);
        damageCheck = false;
    }
}
