﻿using UnityEngine;
using Zenject;

public class WearableItemActivator : MonoBehaviour
{
    [SerializeField] private KeyCode _key;

    [Inject] protected readonly WearableItemsInventory _wearableItemsInventory;
    [Inject] private readonly InventoryEnablerDisabler _inventoryAcviteStateSetter;

    protected WearableItemHandler _wearableItemHandler;

    protected Transform _itemParent;

    protected virtual WearableItemSlot WearableItemSlot { get; }

    protected void Awake()
    {
        _itemParent = transform;
    }

    protected void Start()
    {
        WearableItemSlot.WearableItemActivator = this;

        WearableItemSlot.OnItemChanged += SetItem;
        WearableItemSlot.OnItemRemoved += DeactivateWeapon;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled += SetActiveState;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key) || _wearableItemHandler == null) { return; }

        SetItemActiveState(!_wearableItemHandler.GameObjectForPlayer.activeSelf);
    }

    public virtual void SetItemActiveState(bool itemActiveState)
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(itemActiveState);
    }

    protected void SetItem(WearableItemHandler wearableItemHandler)
    {
        _wearableItemHandler = wearableItemHandler;
        WearableIte_SO ite_SO = (WearableIte_SO)_wearableItemHandler.GetItem();

        _wearableItemHandler.GameObjectForPlayer.transform.SetParent(_itemParent);
        _wearableItemHandler.GameObjectForPlayer.transform.localPosition = ite_SO.playerGameObjectspawnOffset;
        _wearableItemHandler.GameObjectForPlayer.transform.localRotation = Quaternion.identity;

        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
    }

    private void DeactivateWeapon()
    {
        _wearableItemHandler.GameObjectForPlayer.SetActive(false);
        _wearableItemHandler = null;
    }

    private void SetActiveState()
    {
        enabled = !enabled;
    }

    protected void OnDestroy()
    {
        WearableItemSlot.OnItemChanged -= SetItem;
        WearableItemSlot.OnItemRemoved -= DeactivateWeapon;
        _inventoryAcviteStateSetter.OnInventoryEnabledDisabled -= SetActiveState;
    }
}