using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.System
{
    struct OpenCraftUITrigger:IEvent
    {
    }

    struct UICraftIconClickEvent:IEvent
    {
        public UICraftIcon craftIcon;
        public UICraftIconClickEvent(UICraftIcon c)
        {
            craftIcon = c;
        }
    }

    struct UICraftMaterialClickEvent:IEvent
    {
        public Item_s item;
    }

    struct UIPotionEnterEvent:IEvent
    {

    }

    struct UIRecipeElementClickEvent:IEvent
    {
        public UIRecipeElement element;
    }

    struct RefreshBackpackUIEvent : IEvent
    {
        public List<Item_s> normalItems;
        public List<Item_s> specialItems;
        public List<Item_Data> potions;
    }

    struct RefreshBackpackUIRequest:IEvent
    {

    }
    struct OnPotionClick:IEvent
    {
        public Item_Data potion;
    }

    struct TurnCountEvent:IEvent
    {
        public int count;
    }

}
