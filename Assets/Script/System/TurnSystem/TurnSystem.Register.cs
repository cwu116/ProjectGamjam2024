using Buff.Tool;
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

        ~TurnSystem()
        {
            EventSystem.UnRegister<PlayerTurnBeginTrigger>(OnPlayerTurnBeginTrigger);
            EventSystem.UnRegister<PlayerTurnEndTrigger>(OnPlayerTurnEndTrigger);
            EventSystem.UnRegister<EnemyTurnBeginTrigger>(OnEnemyTurnBeginTrigger);
            EventSystem.UnRegister<EnemyTurnEndTrigger>(OnEnemyTurnEndTrigger);
        }
        private void OnPlayerTurnBeginTrigger(PlayerTurnBeginTrigger obj)
        {
            _turnModel.CurrentTurn = TurnType.PlayerTurn;
            stateSystem.PlayerStatesStart();
            Player.instance.MoveTimes = new Buff.Tool.ValueInt( Player.instance.MaxMoveTimes);
            Player.instance.moveStep = Player.instance.MoveTimes * Player.instance.StepLength;
            stateSystem.CheckForDelay();
            AfterPlayerTurnBeginEvent info = new AfterPlayerTurnBeginEvent() { moveTimes=Player.instance.MoveTimes};
            EventSystem.Send(info);
        }
        private void OnPlayerTurnEndTrigger(PlayerTurnEndTrigger obj)
        {
            stateSystem.PlayerStatesEnd();
            AfterPlayerTurnEndEvent info = new AfterPlayerTurnEndEvent() { /*??????*/};
            EventSystem.Send(info);
            OnEnemyTurnBeginTrigger(default);
        }

        private void OnEnemyTurnBeginTrigger(EnemyTurnBeginTrigger obj)
        {
            _turnModel.CurrentTurn = TurnType.EnemyTurn;
            Debug.LogError("Start");
            stateSystem.EnemyStatesStart();
            AfterEnemyTurnBeginEvent info = new AfterEnemyTurnBeginEvent() { /*???��????*/};
            EventSystem.Send(info);
        }

        private void OnEnemyTurnEndTrigger(EnemyTurnEndTrigger obj)
        {
            foreach (var enemy in GameObject.FindObjectsOfType<Enemy>())
            {
                enemy.WatchRange.RemoveChange();
            }
            stateSystem.EnemyStatesEnd();
            AfterEnemyTurnEndEvent info = new AfterEnemyTurnEndEvent() { /*??????*/};
            Debug.Log("enemy turnend");
            EventSystem.Send(info);
            EventSystem.Send<PlayerTurnBeginTrigger>();
            _turnModel.turnCount++;
            EventSystem.Send<TurnCountEvent>(new TurnCountEvent() { count = _turnModel.turnCount });
            if (_turnModel.turnCount > 20)
                EventSystem.Send<GameOverEvent>();
        }
    }
}
