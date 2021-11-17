﻿using UnityEngine;

public abstract class BobbingChangeWhileMoveAction : MonoBehaviour
{
    [SerializeField] private float _bobFrequencyWhileAction;
    [SerializeField] private float _bobVerticalAmplitudeWhileAction;

    protected CameraBobbing _cameraBobbing;
    protected MoveController _moveController;

    private void Start()
    {
        _cameraBobbing = GetComponent<CameraBobbing>();

        _moveController.OnPlayerStartedUseOfMove += ChangeBobbingDuringRun;
        _moveController.OnPlayerStoppedUseOfMove += _cameraBobbing.ResetBobbingValues;
    }

    protected void ChangeBobbingDuringRun()
    {
        _cameraBobbing.BobFrequency = _bobFrequencyWhileAction;
        _cameraBobbing.BobVerticalAmplitude = _bobVerticalAmplitudeWhileAction;
    }

    private void OnDestroy()
    {
        _moveController.OnPlayerStartedUseOfMove -= ChangeBobbingDuringRun;
        _moveController.OnPlayerStoppedUseOfMove -= _cameraBobbing.ResetBobbingValues;
    }
}

