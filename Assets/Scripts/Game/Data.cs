using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//存放游戏进行时间和通关时间记录以及关卡数
public class Data : MonoBehaviour
{
    public static float[] record=new float[5];
    public static int currentLevel;
    public static float time;
    public static void setTime(float t)
    {   
        time = t;
    }
    public static void setLevel(int l)
    {
        currentLevel = l;
    }
    public static bool newRecord()
    {
        if (time < record[currentLevel])
        {
            record[currentLevel] = time;
            return true;
        }
        return false;
    }
}
