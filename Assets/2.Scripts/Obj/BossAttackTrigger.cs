using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    bool isAttacking = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttacking)
            StartCoroutine(giveDamage());
    }

    IEnumerator giveDamage()
    {
        isAttacking = true;
        FindObjectOfType<characterController>().getDamage(8);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
