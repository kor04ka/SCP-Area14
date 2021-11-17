using UnityEngine;
using Zenject;

public class TestHealthHealInteractable : IInteractable
{
    [SerializeField] private int _damage;

    [Inject] private readonly PlayerHealth _playerBleeding;

    public override void Interact()
    {
        _playerBleeding.Damage(_damage);
    }
}
