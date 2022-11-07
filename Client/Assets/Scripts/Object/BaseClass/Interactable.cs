using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour {
    #region Components
    private BoxCollider2D _interactRangeTrigger = null;

    #endregion

    #region References
    //TODO: UIManager에서 상호작용 관리하는 친구 가지고 있기

    #endregion

    #region Variables


    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(null == _interactRangeTrigger)
            _interactRangeTrigger = GetComponent<BoxCollider2D>();


    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player") == false)
            return;

        //TODO: 상호작용 UI 표시
    }

    #endregion

    #region Internal Functions


    #endregion
}