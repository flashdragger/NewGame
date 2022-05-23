using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
     [HideInInspector]
    public enum Attack {
        Normal,
        Charge
    }
    public float BaseDamage=2.0f;
    public float MoveSpeed = 8.0f;
    public float ExistTime = 5.0f;
    private float _existTimer;

    public Attributes.Elements element = Attributes.Elements.NULL;

    private void OnEnable() {
        // Sprite sp;
        // SpriteRenderer sr=GetComponent<SpriteRenderer>();
        // int CurrentCharacter=GameObject.Find("GameManager").GetComponent<CharacterManager>().CurrentCharacter;
        // Debug.Log(CurrentCharacter);
        // sp=Resources.Load<Sprite>("sprites/ice");
        // switch(CurrentCharacter){
        //     case 2:
        //     sp= Resources.Load<Sprite>("sprites/fire_");
        //     element=Attributes.Elements.fire;
        //     break;
        //     case 1:
        //     element=Attributes.Elements.ice;
        //     sp=Resources.Load<Sprite>("sprites/ice");
        //     break;
        //     case 0:
        //     element=Attributes.Elements.water;
        //     sp=Resources.Load<Sprite>("sprites/water");
        //     break;
        // }

        // sr.sprite=sp;
        _existTimer = 0.0f;
    }

    private void Update() {
        
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        _existTimer += Time.deltaTime;
        if (_existTimer >= ExistTime) 
        {
            Destroy(gameObject);
            Debug.Log("de");
        }
              
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        _existTimer = ExistTime;
    }

}