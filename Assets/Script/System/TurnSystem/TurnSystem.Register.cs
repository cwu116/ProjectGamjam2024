using UnityEngine;

namespace Game.System
{
    public partial class TurnSystem : BaseSystem
    {
        IState stateSystem;
        private void RegisterEvents()
        {
            EventSystem.Register<PlayerTurnBeginTrigger>(OnPlayerTurnBeginTrigger);
            EventSystem.Register<PlayerTurnEndTrigger>(OnPlayerTurnEndTrigger);
            EventSystem.Register<EnemyTurnBeginTrigger>(OnEnemyTurnBeginTrigger);
            EventSystem.Register<EnemyTurnEndTrigger>(OnEnemyTurnEndTrigger);
        }
        private void OnPlayerTurnBeginTrigger(PlayerTurnBeginTrigger obj)
        {
            _turnModel.CurrentTurn = TurnType.PlayerTurn;
            stateSystem.PlayerStatesStart();
            Player.instance.MoveTimes = Player.instance.MaxMoveTimes;
            AfterPlayerTurnBeginEvent info = new AfterPlayerTurnBeginEvent() { moveTimes=Player.instance.MoveTimes};
            EventSystem.Send(info);
        }
        private void OnPlayerTurnEndTrigger(PlayerTurnEndTrigger obj)
        {
            stateSystem.PlayerStatesEnd();
            AfterPlayerTurnEndEvent info = new AfterPlayerTurnEndEvent() { /*信息赋值*/};
            EventSystem.Send(info);
            OnEnemyTurnBeginTrigger(default);
        }

        private void OnEnemyTurnBeginTrigger(EnemyTurnBeginTrigger obj)
        {
            _turnModel.CurrentTurn = TurnType.EnemyTurn;
            stateSystem.EnemyStatesStart();
            AfterEnemyTurnBeginEvent info = new AfterEnemyTurnBeginEvent() { /*玩家位置赋值*/};
            EventSystem.Send(info);
        }

        private void OnEnemyTurnEndTrigger(EnemyTurnEndTrigger obj)
        {
            stateSystem.EnemyStatesEnd();
            AfterEnemyTurnEndEvent info = new AfterEnemyTurnEndEvent() { /*信息赋值*/};
            Debug.Log("enemy turnend");
            EventSystem.Send(info);
            EventSystem.Send<PlayerTurnBeginTrigger>();
        }
    }
}
