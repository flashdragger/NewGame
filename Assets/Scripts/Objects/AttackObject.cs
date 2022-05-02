using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private float baseDamage=2.0f;
    public float MoveSpeed = 8.0f;
    public float ExistTime = 5.0f;
    private float _existTimer;

    private Attributes.Elements element=Attributes.Elements.NULL;

    private void OnEnable() {
        Sprite sp;
        SpriteRenderer sr=GetComponent<SpriteRenderer>();
        int CurrentCharacter=GameObject.Find("GameManager").GetComponent<CharacterManager>().CurrentCharacter;
        Debug.Log(CurrentCharacter);
        sp=Resources.Load<Sprite>("sprites/ice");
        switch(CurrentCharacter){
            case 2:
            sp= Resources.Load<Sprite>("sprites/fire_");
            element=Attributes.Elements.fire;
            break;
            case 1:
            element=Attributes.Elements.ice;
            sp=Resources.Load<Sprite>("sprites/ice");
            break;
            case 0:
            element=Attributes.Elements.water;
            sp=Resources.Load<Sprite>("sprites/water");
            break;
        }

        sr.sprite=sp;
        _existTimer = 0.0f;
    }

    private void Update() {
        
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        _existTimer += Time.deltaTime;
        if (_existTimer >= ExistTime) 
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Boss")){
            BossAttributes bossAttributes=collision.gameObject.GetComponent<BossAttributes>();
            if(bossAttributes.haveAttachedElement() ==true){
                   Attributes.Elements attachedElement=bossAttributes.getAttachedElement();
                   if(attachedElement==element)bossAttributes.damaged(baseDamage,element);
                   else{
                       //attache element is water
                       if(attachedElement==Attributes.Elements.water){
                           if(element==Attributes.Elements.fire){
                               bossAttributes.damaged(baseDamage*1.5f,Attributes.Elements.water);
                           }
                           else if(element==Attributes.Elements.ice){
                               //water+ice->same effect:
                               //ice remain
                               bossAttributes.damaged(baseDamage,Attributes.Elements.ice);
                           }
                       }
                       //fire
                        else if(attachedElement==Attributes.Elements.fire){
                            if(element==Attributes.Elements.water){
                                bossAttributes.damaged(2.0f*baseDamage,Attributes.Elements.water);
                            }
                            else if(element==Attributes.Elements.ice){
                                bossAttributes.damaged(1.5f*baseDamage,Attributes.Elements.fire);
                            }
                        }
                        //ice
                        else{
                            if(element==Attributes.Elements.fire){
                                bossAttributes.damaged(2.0f*baseDamage,Attributes.Elements.fire);
                            }
                            else if(element==Attributes.Elements.water){
                                bossAttributes.damaged(baseDamage,Attributes.Elements.ice);
                            }
                        }
                   }
            }
            else{
                bossAttributes.setAttachedElement(element);
            }
            //set damage based on the 
        }
        else{
            Debug.Log("aa");
        }
    }

}
