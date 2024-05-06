﻿
using System;

namespace Game.System
{
    public partial class GameFlowSystem:BaseSystem
    {
        public override void InitSystem()
        {
            RegisterEvents();
            EventSystem.Send<ShowUIStartPanelTriggerEvent>();
        }


        private void RegisterEvents()
        {
            EventSystem.Register<SwitchMapEvent>(SwitchMap);
            EventSystem.Register<PlayerDieEvent>(OnPlayerDie);
        }

        private void OnPlayerDie(PlayerDieEvent obj)
        {
            EventSystem.Send<GameOverEvent>();
        }

        private void SwitchMap(SwitchMapEvent obj)
        {
            //Loading Map
            //await 展示路线图
        }
    }
}
