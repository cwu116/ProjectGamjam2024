
namespace Game.System
{
    public struct ShowUIStartPanelTriggerEvent:IEvent
    { }

    public struct SwitchMapEvent:IEvent
    {
        public int currentMap;
        public int nextMap;
    }

    struct GameStartTrigger:IEvent
    { }

}
