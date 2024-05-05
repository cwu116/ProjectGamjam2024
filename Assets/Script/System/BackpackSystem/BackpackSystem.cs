using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;
using Game;

public class BackpackSystem : BaseSystem
{
    BackpackModel backpackModel;
    public override void InitSystem()
    {
        backpackModel = GameBody.GetModel<BackpackModel>();
        EventSystem.Register<RefreshBackpackUIRequest>(v=>OnRefreshBackpackUIRequest());
    }

    void OnRefreshBackpackUIRequest()
    {
        EventSystem.Send(new RefreshBackpackUIEvent()
        {
            normalItems = backpackModel.normalMaterials,
            specialItems = backpackModel.specialMaterials,
            potions = backpackModel.potions
        });
    }
}
