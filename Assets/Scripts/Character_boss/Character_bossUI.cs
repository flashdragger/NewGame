using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enemy.FSM;
public class Character_bossUI : MonoBehaviour
{
    private GameObject character_boss;
    private Attributes boss_attribute;
    public Image Boss;
    public Image BossBar;
    public Image Element;
    void Start()
    {
        Element.GetComponent<CanvasGroup>().alpha=0;
        character_boss=GameObject.Find("Boss");
        boss_attribute=character_boss.GetComponent<Attributes>();
    }
    void Update()
    {
        if(boss_attribute!=null)
        {
            Element.GetComponent<CanvasGroup>().alpha=1;
            Boss.fillAmount=boss_attribute.HP/boss_attribute.MaxHP;
            Debug.Log(boss_attribute.HP);
            Element.GetComponent<Image>().sprite=Resources.Load<Sprite>("ElementSprite/"+character_boss.GetComponent<CharacterFSM_boss>().AttachedElement.ToString());
            if(character_boss.GetComponent<CharacterFSM_boss>().AttachedElement.ToString().Equals("NULL"))
            {
                Element.GetComponent<CanvasGroup>().alpha=0;
            }
        }
        else
        {
            Boss.fillAmount=0;
        }
    }
   
}
