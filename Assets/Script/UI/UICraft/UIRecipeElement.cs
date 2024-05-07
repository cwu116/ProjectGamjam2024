using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.System;
using TMPro;
using UnityEngine.UI;

public class UIRecipeElement : MonoBehaviour, IPointerClickHandler
{
    public Item_Data potion;
    TextMeshProUGUI recipeName;
    Image background;
    Image icon;

    public void Init(Item_Data potion)
    {
        background = GetComponent<Image>();
        recipeName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        icon = transform.Find("Icon").GetComponent<Image>();
        this.potion = potion;
        recipeName.text = this.potion.Name;

        Sprite sprite;
        if (potion.Name.Equals("����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�������"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�Ļ�"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�˷ܶ�ҩ"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("��ʮ����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("��ħ���"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("��ħ����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("���볾��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("Ӽ"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("Ѫ��ף��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("ȼ��ƿ"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("��Ϣ"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("�ڰ�֮��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("����"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("���澻��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("��������"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("���ۺ�"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("ʵ����1��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("ʵ����2��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("ʵ����3��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else if (potion.Name.Equals("���ҩ��"))
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        else
        {
            sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "��");
        }
        icon.sprite = sprite; 
        //this.sprite = sprite;
        //image.sprite = sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.System.EventSystem.Send<UIRecipeElementClickEvent>(new UIRecipeElementClickEvent() { element = this });
    }

    public void Select()
    {
        background.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "ѡ�п�");
    }

    public void UnSelect()
    {
        background.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + "δѡ�п�");
    }
}
