using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
        [HideInInspector]
        public enum Elements {
            water,
            fire,
            ice
        }
        public Elements EleAttribute;
        public float HP = 100;
}
