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
        public Item item;
    }

    struct UIPotionEnterEvent:IEvent
    {

    }

    struct UIRecipeElementClickEvent:IEvent
    {
        public UIRecipeElement element;
    }


}
