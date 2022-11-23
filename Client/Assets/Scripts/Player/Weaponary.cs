using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum WeaponSoundType {
    Fire = 0,
    Reload,
    TriggerLock,

}

[Serializable]
public struct WeaponAudioClipFormat {
    public WeaponSoundType _type;
    public AudioClip _clip;
}

public enum PlayerDirectionType
{
    Left = 0,
    Right,
    Up
}

public class Weaponary : MonoBehaviour {
    #region Components
    [Header("External Components")]
    [SerializeField] private ObjectPooler _bulletPooler = null;
    //TODO: ObjectPooler 클래스 생성 필요

    [Header("Weapon Inner Instances")]
    [SerializeField] private Transform[] _muzzleTransforms = new Transform[3];
    public Transform _activatedMuzzleTransform = null;

    [Header("Sound Effects")]
    [Tooltip("총의 AudioSource 컴포넌트")]
    [SerializeField] private AudioSource _audioSource = null;

    [SerializeField] private List<WeaponAudioClipFormat> _audioClips = new List<WeaponAudioClipFormat>();

    #endregion

    #region Variables
    private const byte _magCapacity = 8;
    private byte _curAmmoCount = 0;
    private bool _isIdle = true;

    [SerializeField] private float _firingInterval = 0.3f;
    private WaitForSeconds _monsterFiringIntervalWait;
    [SerializeField] private float _reloadInterval = 1f;
    private WaitForSeconds _reloadIntervalWait;

    #endregion

    #region Properties
    public int CurrentAmmo { get => _curAmmoCount; }
    public int MaxAmmo { get => _magCapacity; }
    private bool CanFire { get => _curAmmoCount > 0 & _isIdle; }

    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(null == _audioSource)
            _audioSource = GetComponent<AudioSource>();

        if(null == _monsterFiringIntervalWait)
            _monsterFiringIntervalWait = new WaitForSeconds(_firingInterval);

        if(null == _reloadIntervalWait)
            _reloadIntervalWait = new WaitForSeconds(_reloadInterval);

        _activatedMuzzleTransform = _muzzleTransforms[(int)PlayerDirectionType.Right];
        //TODO: 오디오클립을 _type의 순서에 따라 정렬
    }

    private void Start() {
        Debug.Log($"Left:{_muzzleTransforms[0].transform.forward}");
        Debug.Log($"Right:{_muzzleTransforms[1].transform.forward}");
        Debug.Log($"Up:{_muzzleTransforms[2].transform.forward}");
    }

    private void Reset() {
        _audioSource = GetComponent<AudioSource>();
        _monsterFiringIntervalWait = new WaitForSeconds(_firingInterval);
        _reloadIntervalWait = new WaitForSeconds(_reloadInterval);
    }

    #endregion

    #region API
    /// <summary>
    /// 플레이어 전용 공격 함수
    /// </summary>
    public void PullTrigger() {
        if (CanFire)
            FireBulletOnce(_activatedMuzzleTransform.forward); 
        else
            PlaySound(WeaponSoundType.TriggerLock);
    }

    /// <summary>
    /// 적 AI 전용 공격 함수
    /// </summary>
    /// <param name="firingCount"></param>
    public void PullTrigger(Vector2 dir, int firingCount = 1) {

        StartCoroutine(CoStartFireBullets(dir, firingCount));
    }

    public void Reload() {
        StartCoroutine(CoStartReload());
    }

    #endregion

    #region Internal Functions
    private void FireBulletOnce(Vector3 dir) {
        if(_activatedMuzzleTransform == null)
            return;

        GameObject go  = _bulletPooler.Get();
        if(go == null)
            return;

        Bullet bullet = go.GetComponent<Bullet>();
        bullet.Initialize(dir, _activatedMuzzleTransform);
        bullet.gameObject.SetActive(true);
        //TODO: 사운드 재생 구현
    }

    private IEnumerator CoStartFireBullets(Vector2 dir, int firingCount) {
        for(int i = 0; i < firingCount; i++) {
            FireBulletOnce(dir);
            yield return _monsterFiringIntervalWait;
        }

        yield break;
    }

    private IEnumerator CoStartReload() {
        _isIdle = false;
        yield return _reloadIntervalWait;

        _curAmmoCount = _magCapacity;
        _isIdle = true;
        yield break;
    }

    /// <summary>
    /// 모든 총기 사운드를 호출하는 함수.
    /// </summary>
    /// <param name="type">enum WeaponSoundType</param>
    private void PlaySound(WeaponSoundType type) {
        if(null == _audioSource) return;

        //TODO: type에 따라서 해당 오디오클립을 가져온 후 사운드 실행
    }

    public void PlayerDirectionChangedTo(PlayerDirectionType dir)
    {
        if (_muzzleTransforms.Count() < Enum.GetNames(typeof(PlayerDirectionType)).Length)
            return;

        _muzzleTransforms[(int)PlayerDirectionType.Up].gameObject.SetActive(false);
        _muzzleTransforms[(int)PlayerDirectionType.Left].gameObject.SetActive(false);
        _muzzleTransforms[(int)PlayerDirectionType.Right].gameObject.SetActive(false);

        _muzzleTransforms[(int)dir].gameObject.SetActive(true);
        _activatedMuzzleTransform = _muzzleTransforms[(int)dir];
    }

    #endregion
}
