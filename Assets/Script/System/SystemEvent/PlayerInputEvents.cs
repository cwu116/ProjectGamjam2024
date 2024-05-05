using UnityEngine;

namespace Game.System
{
   struct OnMouseRightClick:IEvent
    { }

    struct OnClickPotion:IEvent
    {

    }

    struct UndoEvent:IEvent
    {
        public ICanUndo undoOperation;
    }

    struct UsePotionEvent:IEvent
    {
        public Item_Data potion;
    }

    struct GirdCilckEvent:IEvent
    {
        public HexCell cell;
        public Transform transfrom;
    }

    struct PlayerMoveEvent:IEvent
    {
        public HexCell currentCell;
        public int moveTimes;
    }
}
