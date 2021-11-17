﻿using UnityEngine;
using Zenject;

public class PickableItemsInteraction : MonoBehaviour
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    [Inject] public PickableItemsInventory PickableItemsInventory { get; }

    public void UseItem(PickableItemSlot pickableItemSlot)
    {
        IClickable itemSlot = pickableItemSlot.ItemHandler as IClickable;
        itemSlot.Clicked(pickableItemSlot.SlotIndex);
    }

    public void DropItem(PickableItemSlot pickableItemSlot)
    {
        pickableItemSlot.ItemHandler.GameObject.transform.position = _playerTransform.position + _playerTransform.forward;
        pickableItemSlot.ItemHandler.GameObject.SetActive(true);

        pickableItemSlot.Clear();
        PickableItemsInventory.RemoveItem(pickableItemSlot.SlotIndex);
    }
}
