using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Pooling Object's Prefab")]
    public Poolable _poolTarget = null;
    private List<Poolable> _pool = new List<Poolable>();

    #region Unity Event Functions
    private void Awake() {
        if(transform.childCount <= 0)
            GenerateObject();

        for(int i = 0; i < transform.childCount; i++) {
            Poolable go = transform.GetChild(i).GetComponent<Poolable>();
            if(go == null)
                return;

            go.gameObject.SetActive(false);
            _pool.Add(go);
        }
    }

    #endregion

    #region API
    public GameObject Get() {
        GameObject go = null;

        for(int i = 0; i < _pool.Count; i++) {
            if(_pool[i].gameObject.activeSelf == false)
                go = _pool[i].gameObject;
        }

        if(go == null) {
            if(_poolTarget == null) return null;

            go = GenerateObject();
        }

        return go;
    }

    #endregion

    #region Internal Functions
    private GameObject GenerateObject(int count = 5) {
        if(_poolTarget == null)
            return null;

        GameObject go  = null;

        for(int i = 0; i < count; i++) {
            go = Instantiate(_poolTarget.gameObject, transform);
            Poolable poolable = go.GetComponent<Poolable>();
            if(poolable == null)
                return null;

            poolable.Init(DestroyObject);
        }

        return go;
    }

    private void DestroyObject(Poolable poolObject) {
        if(_pool.Contains(poolObject) == false)
            return;

        poolObject.gameObject.SetActive(false);
    }

    #endregion

}
