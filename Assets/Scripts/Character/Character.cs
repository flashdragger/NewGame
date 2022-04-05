using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

public class Character : FSMBase
{
    public enum Elements {
        water,
        fire,
        ice
    }
    public int HP;
    //角色体力
    public int PS;
    public Elements EleAttribute;

    
}
