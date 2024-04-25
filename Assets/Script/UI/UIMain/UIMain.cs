using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using DG.Tweening;

public class UIMain : BasePanel
{
    Image _btnCraft;

    public override void Close()
    {
        
    }

    public override void InitPanel()
    {
        _btnCraft = transform.Find("BtnCraft").GetComponent<Image>();
    }

    public override void Refresh()
    {
       
    }

    public override void Show(IUiData uiData)
    {
        
    }
}
