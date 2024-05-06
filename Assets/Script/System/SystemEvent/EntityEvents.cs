using UnityEngine;

namespace Game.System
{
    struct EntityHurtEvent:IEvent
    {
        public BaseEntity enetity;
    }

    struct HighLightWarningBlockEvent:IEvent
    {
        public Vector2 pos;
        public int distance;
    }

    struct ClearWarningBlockEvent : IEvent
    {

    }

    struct HighLightAttackBlockEvent : IEvent
    {
        public Vector2 pos;
        public int distance;
    }

    struct ClearAttackBlockEvent : IEvent
    {

    }

    struct PlayerDieEvent:IEvent
    { }


}
