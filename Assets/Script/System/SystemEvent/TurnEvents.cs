using UnityEngine;

namespace Game.System
{
    struct PlayerTurnBeginTrigger : IEvent
    { }
    struct PlayerTurnEndTrigger : IEvent
    { }

    struct EnemyTurnBeginTrigger : IEvent
    { }

    struct EnemyTurnEndTrigger : IEvent
    { }

    struct AfterPlayerTurnBeginEvent:IEvent
    {

    }

    struct AfterPlayerTurnEndEvent : IEvent
    {

    }

    struct AfterEnemyTurnBeginEvent:IEvent
    {
        public Vector2 playerPos;
    }

    struct AfterEnemyTurnEndEvent:IEvent
    {

    }

    struct EnemyActionComplete:IEvent
    {
       public Enemy enemy;
    }
}
