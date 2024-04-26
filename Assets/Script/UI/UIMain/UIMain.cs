using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using Game.System; 

public class UIMain : BasePanel
{
    Button btnCraft;

    public override void Close()
    {
        
    }

    public override void InitPanel()
    {
        btnCraft = transform.Find("BtnCraft").GetComponent<Button>();
        btnCraft.onClick.AddListener(() => EventSystem.Send<OpenCraftUITrigger>());//ÔÚCraftSystemÌí¼ÓUndo
    }

    public override void Refresh()
    {
       
    }

    public override void Show(IUiData uiData)
    {
        
    }
}
