using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttributes  : Attributes
{
    private float existTime=5.0f;
    private bool isFrozend=false;
    private float frozenTime=2.0f;
    public bool isDied=false;
    //attached element
    private Elements attachedElement=Elements.NULL;        

    public void setAttachedElement(Elements _first){
        attachedElement=_first;
        existTime=5.0f;
    }
    public Elements getAttachedElement(){return attachedElement;}
    public bool haveAttachedElement(){
        if(attachedElement!=Elements.NULL)return true;

        else return false;
    }

    public float getHP(){return HP;}

    //boss get damage
    //(damagevalue,attachedElement)
    public void damaged(float damage,Elements element){
        HP-=damage;
        this.attachedElement=element;
        this.existTime=5.0f;
        if(element==Elements.ice){
            frozenTime=2.0f;
            isFrozend=true;
        }
        if(HP<=0){
            isDied=true;
            //死亡状态
            /*
            
            
            
            */
        }
        Debug.Log("受到伤害:"+damage);
        Debug.Log("附着的元素:"+element);
    }

    void Update() {
        if(haveAttachedElement()){
             existTime-=Time.deltaTime;
            if(existTime<=0){
                attachedElement=Attributes.Elements.NULL;
                existTime=0;
            }
        }
       
        if(isFrozend){
            //此处还需要实现被冻住的状态!!

            frozenTime-=Time.deltaTime;
            if(frozenTime<=0){
                isFrozend=false;
            }
        }
        else{
            //恢复正常状态

        }
    }
    
}
