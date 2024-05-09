
namespace Game.System
{
    public struct ShowUIStartPanelTriggerEvent:IEvent
    { }

    public struct SwitchMapEvent:IEvent
    {
        public int currentMap;
        public int nextMap;
    }

    struct GameReStartTrigger:IEvent
    { }

    struct GameOverEvent:IEvent
    { }

    struct GameSuccessEvent:IEvent
    { }

}
