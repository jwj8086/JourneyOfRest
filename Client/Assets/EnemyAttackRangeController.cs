using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangeController : MonoBehaviour
{
    [SerializeField] private EnemyMove enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        enemyController.Player = collision.gameObject.GetComponent<PlayerController>();
        Debug.Log("Player감지");
        //TODO: 추가적인 공격 함수 작성 필요
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        Debug.Log("Player제거");
        enemyController.Player = null;
    }


}
