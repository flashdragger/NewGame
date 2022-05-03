using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AI.FSM;

public class CharacterUI : MonoBehaviour
{
    private CharacterManager character;
    public Image Health;
    public Image PS;
    public Image Charge;
    private Image _headPicture;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterManager>();
        _headPicture = GetComponentInChildren<Image>();
    }
    
    void Update()
    {
        if (character != null) {
            Health.fillAmount = character.GetComponentInChildren<Attributes>().HP / character.GetComponentInChildren<Attributes>().MaxHP;
            PS.fillAmount = character.GetComponent<CharacterFSM>().PS / character.GetComponent<CharacterFSM>().MaxPS;
            Charge.fillAmount = character.GetComponent<CharacterFSM>().KeyTime / character.GetComponent<CharacterFSM>().ChargeTime;
            _headPicture.GetComponent<Image>().sprite = Resources.Load<Sprite>("HeadPicture/Character" + ((int)character.GetComponentInChildren<Attributes>().EleAttribute)); 
        }
        else {
            Health.fillAmount = 0;
            PS.fillAmount = 0;
        }
    }
}
