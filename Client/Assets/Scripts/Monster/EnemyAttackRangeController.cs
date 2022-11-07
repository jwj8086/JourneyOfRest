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
        Debug.Log("Player추적 시작");
        //TODO: 추가적인 공격 함수 작성 필요
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        Debug.Log("Player추적 해제");
        enemyController.Player = null;
    }
}
