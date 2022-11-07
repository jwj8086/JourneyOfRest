using System;
using System.Collections;
using System.Collections.Generic;
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

public class Weaponary : MonoBehaviour {
    #region Components
    [Header("External Components")]
    [SerializeField] private ObjectPooler _bulletPooler = null;
    //TODO: ObjectPooler Ŭ���� ���� �ʿ�

    [Header("Weapon Inner Instances")]
    [SerializeField] private Transform _muzzleTransform = null;

    [Header("Sound Effects")]
    [Tooltip("���� AudioSource ������Ʈ")]
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

        //TODO: �����Ŭ���� _type�� ������ ���� ����
    }

    private void Start() {
        
    }

    private void Reset() {
        _audioSource = GetComponent<AudioSource>();
        _monsterFiringIntervalWait = new WaitForSeconds(_firingInterval);
        _reloadIntervalWait = new WaitForSeconds(_reloadInterval);
    }

    #endregion

    #region API
    /// <summary>
    /// �÷��̾� ���� ���� �Լ�
    /// </summary>
    public void PullTrigger(Vector2 dir) {
        if(CanFire)
            FireBulletOnce(dir);
        else
            PlaySound(WeaponSoundType.TriggerLock);
    }

    /// <summary>
    /// �� AI ���� ���� �Լ�
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
    private void FireBulletOnce(Vector2 dir) {
        if(_muzzleTransform == null)
            return;

        GameObject go  = _bulletPooler.Get();
        if(go == null)
            return;

        Bullet bullet = go.GetComponent<Bullet>();
        bullet.transform.position = _muzzleTransform.position;
        bullet.Initialize(dir);
        bullet.gameObject.SetActive(true);
        //TODO: ���� ��� ����
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
    /// ��� �ѱ� ���带 ȣ���ϴ� �Լ�.
    /// </summary>
    /// <param name="type">enum WeaponSoundType</param>
    private void PlaySound(WeaponSoundType type) {
        if(null == _audioSource) return;

        //TODO: type�� ���� �ش� �����Ŭ���� ������ �� ���� ����
    }

    #endregion
}