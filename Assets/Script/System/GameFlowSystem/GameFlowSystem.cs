
using System;
using Managers;
using Game;
using UnityEngine;

namespace Game.System
{
    public partial class GameFlowSystem:BaseSystem
    {
        public override void InitSystem()
        {
            RegisterEvents();
            EventSystem.Send<ShowUIStartPanelTriggerEvent>();
            GameBody.GetSystem<AudioSystem>().soundSource = Camera.main.transform.Find("SoundSource").GetComponent<AudioSource>();
            GameBody.GetSystem<AudioSystem>().musicSource = Camera.main.transform.Find("MusicSource").GetComponent<AudioSource>();
            AudioManager.PlayMusic(AudioPath.FightMusic);
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
