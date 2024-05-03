
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
}
