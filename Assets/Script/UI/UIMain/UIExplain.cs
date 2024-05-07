using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using UnityEngine.UI;
using Managers;

public class UIExplain : BasePanel
{
    public override void Close()
    {
    }

    public override void InitPanel()
    {
    }

    public override void Refresh()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("BtnBack").GetComponent<Button>().onClick.AddListener(() => UIManager.Close<UIExplain>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
