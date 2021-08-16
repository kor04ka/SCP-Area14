using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_delayDuringBleeding;
    [SerializeField] float m_increasingTimeForBleeding;
    [SerializeField] Sprite m_imageForBleedingCell;
    [SerializeField] Sprite m_imageForFullCell;
    [Inject] readonly PlayerHealth m_playerHealthSystem;

    WaitForSeconds m_timeoutDuringBleeding;
    IEnumerator m_bleedCoroutine;

    public bool IsPlayerBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }
    public Action OnPlayerBleedingStarted { get; set; }
    public Action OnPlayerBleedingEnded { get; set; }
    public float DelayDuringBleeding { get => m_delayDuringBleeding; set => m_delayDuringBleeding = value; }

    void Start()
    {
        m_bleedCoroutine = BleedCoroutine();
        CreateBleedingTimeout(DelayDuringBleeding);

    }

    public void Bleed()
    {
        if (IsPlayerBleeding) { return; }

        IsPlayerBleeding = true;
        StartCoroutine(m_bleedCoroutine);
    }

    public void StopBleeding()
    {
        IsPlayerBleeding = false;
        StopCoroutine(m_bleedCoroutine);
        m_bleedCoroutine = BleedCoroutine();
        m_playerHealthSystem.GetCurrentHealthCell().SetSprite(m_imageForFullCell);
    }

    IEnumerator BleedCoroutine()
    {
        OnPlayerBleedingStarted?.Invoke();
        while (m_playerHealthSystem.GetCurrentHealthPercent() > 0)
        {
            m_playerHealthSystem.GetCurrentHealthCell().SetSprite(m_imageForBleedingCell);

            yield return m_timeoutDuringBleeding;

            OnPlayerBleeding?.Invoke();
            m_playerHealthSystem.Damage();
            m_delayDuringBleeding -= m_increasingTimeForBleeding;
        }
        IsPlayerBleeding = false;
        CreateBleedingTimeout(DelayDuringBleeding);
        OnPlayerBleedingEnded?.Invoke();
    }

    public void CreateBleedingTimeout(float time)
    {
        m_timeoutDuringBleeding = new WaitForSeconds(time);
    }
}
