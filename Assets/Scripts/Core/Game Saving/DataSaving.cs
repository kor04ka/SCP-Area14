﻿using UnityEngine;
using Zenject;

public abstract class DataSaving : MonoBehaviour
{
    [Inject] protected readonly GameSaving _gameSaving;

    public abstract void Save();

    public virtual void LoadData() { }

    public virtual void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        LoadData();
    }

    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
