using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceToplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private int direction;
    private int face=1;
    // Update is called once per frame
    void Start()
    {
        transform.localScale = new Vector2(face*(-1), 1)*2;
    }
    void FixedUpdate()
    {
       
            if (player.transform.position.x < transform.position.x)
            {
                direction = -1;//玩家在敌人的左边
            }
            else
            {
                direction = 1;//玩家在敌人的右边
            }
            if (direction == face)//表示方向不一致
            {
                //Debug.Log(direction);
                Flip();
            }
    }
    void Flip()//翻转角色方向
    {
        face = (face == 1) ? -1 : 1;
        transform.localScale = new Vector2(face*(-1), 1)*2;
    }


}
