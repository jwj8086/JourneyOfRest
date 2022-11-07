using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Extensions;
using System.Timers;

public class Poolable : MonoBehaviour {
    private Action<Poolable> DestroyAction;

    public bool _needWithdraw = false;

    public void Init(Action<Poolable> destroyAction) {
        DestroyAction = destroyAction;
    }

    public void Destroy() {
        if(DestroyAction != null)
            DestroyAction.Invoke(this);
        else
            Destroy(gameObject);
    }
}
