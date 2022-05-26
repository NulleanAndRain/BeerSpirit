using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float _maxHealth;
    public float MaxHealth => _maxHealth;

    private float _currHealth;
    public float CurrentHealth => _currHealth;

    [SerializeField]
    private bool _isImmortal;

    [SerializeField]
    private bool _canRegen;

    [SerializeField]
    private float _damageImmunityTime;
    private float _lastDamageTime;

    [SerializeField]
    private float _regenAfterDamageCD;
    private float _regCD = 0;

    [SerializeField]
    private float _regenPerInterval;

    [SerializeField]
    private float _regenInterval;

    [Header("Knockback")]
    public float knockbackResist;
    private Rigidbody rb;
    public Vector3 center;

    public event UnityAction OnDowned = delegate { };
    public event UnityAction<float, float> OnHealthUpdate = delegate { };

    public bool IsDowned { get; private set; } = false;

    void Start() {
        _currHealth = _maxHealth;

        rb = GetComponent<Rigidbody>();

        if (_canRegen) StartCoroutine(Regen());
    }

    private IEnumerator Regen() {
        while (true) {
            if (IsDowned) yield return new WaitForSeconds(GameManager.RespawnTime);
            if (_regCD == 0) {
                if (_currHealth != _maxHealth) {
                    if (_currHealth < _maxHealth) _currHealth += _regenPerInterval;
                    if (_currHealth > _maxHealth) _currHealth = _maxHealth;
                    OnHealthUpdate?.Invoke(_currHealth, _maxHealth);
                }
                yield return new WaitForSeconds(_regenInterval);

            } else {
                yield return new WaitForSeconds(_regCD);
                _regCD = 0;
            }
        }
    }

    private Vector3 _lastDamageDir;
    Vector3 _kbDir;
    public void GetDamage(float amount, Vector3 pos, float kbForce = 0) {
        if (Time.time - _lastDamageTime < _damageImmunityTime) return;
        _lastDamageTime = Time.time;
        _regCD = _regenAfterDamageCD;

        _lastDamageDir = transform.position + center - pos;

        float angle = Vector2.SignedAngle(Vector2.right, _lastDamageDir);

        if (Mathf.Abs(rb.velocity.x) >= 1e-4) _kbDir.x += -rb.velocity.x * 2;
        _kbDir.x = _lastDamageDir.x / _lastDamageDir.magnitude * 2;
        _kbDir.y = 1;

        kbForce *= (1 - knockbackResist) * rb.mass;
        if (kbForce < 0) kbForce = 0;
        _kbDir *= kbForce;
        rb.AddForce(_kbDir, ForceMode.Impulse);

        if (!_isImmortal) {
            _currHealth -= amount;
            if (_currHealth <= 0) {
                OnDowned?.Invoke();
                amount += _currHealth;
                _currHealth = 0;
                IsDowned = true;
            }
        }

        OnHealthUpdate?.Invoke(_currHealth, _maxHealth);
    }

    public void Heal(float amount) {
        if (amount <= 0) return;
        _currHealth += amount;
        if (_currHealth > _maxHealth) {
            amount -= _currHealth - _maxHealth;
            _currHealth = _maxHealth;
        }

        OnHealthUpdate?.Invoke(_currHealth, _maxHealth);
    }

    public void Revive(float percent = 1) {
        IsDowned = false;
        _currHealth = _maxHealth * percent;
        OnHealthUpdate?.Invoke(_currHealth, _maxHealth);
    }

    public void SetDamageImmunityForTime(float time) {
        _lastDamageTime = Time.time;
        StartCoroutine(EndImmunity(_damageImmunityTime, time));
        _damageImmunityTime = time;
    }

    private IEnumerator EndImmunity(float prevImmVal, float time) {
        yield return new WaitForSeconds(time);
        _damageImmunityTime = prevImmVal;
    }
}