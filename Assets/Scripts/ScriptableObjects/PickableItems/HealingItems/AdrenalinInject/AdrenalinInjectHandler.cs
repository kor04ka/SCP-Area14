﻿using Zenject;

public class AdrenalinInjectHandler : InjectableItemHandler, IAdrenalinInjectable
{
    [Inject] readonly StaminaUseDisabler m_staminaUseDisabler;

    public AdrenalinInject_SO AdrenalinInject_SO => m_pickableItem_SO as AdrenalinInject_SO;

    public new void Inject()
    {
        m_staminaUseDisabler.Disable(AdrenalinInject_SO.m_adrenalineTime);
    }

    public override bool ShouldItemNotBeUsed => false;
}