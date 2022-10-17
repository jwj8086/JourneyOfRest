using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Extensions;

public class Poolable : MonoBehaviour {
    private Action<Poolable> DestroyAction;

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
