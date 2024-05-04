namespace Game.System
{
    struct CraftTriggerEvent:IEvent
    {
    }

    struct CraftRemoveMaterialEvent:IEvent
    {
        public Item_s item;
    }

    struct CraftAddMaterialEvent:IEvent
    {
        public Item_s item;
    }

    struct CraftResultEvent:IEvent
    {
        public Item_Data result;
    }
}
