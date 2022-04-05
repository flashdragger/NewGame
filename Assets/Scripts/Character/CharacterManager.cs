using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> PrefabList;
    private List<GameObject> _characters = new List<GameObject>();
    public KeyCode[] KeyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3
    };
    public int CurrentCharacter;
    public int DefaultCharacter;
    public float ColdTime;
    private float _coldTimer;
 
    private void Start() {
        foreach (var i in PrefabList)
            _characters.Add(Instantiate<GameObject>(i));
        ChangeCharacter(DefaultCharacter);
        _coldTimer = 0f;
    }

    private void Update() {
        if(_coldTimer > 0) {
            _coldTimer -= Time.deltaTime;
            return;
        }
        
        for (int i = 0; i < KeyCodes.Length; i++) 
            if(Input.GetKeyDown(KeyCodes[i]) && CurrentCharacter != i) {
                ChangeCharacter(i);
                CurrentCharacter = i;
            }
    }

    private void ChangeCharacter(int t) {
        foreach (var i in _characters) 
            i.SetActive(false);
        _characters[t].SetActive(true);
        _coldTimer = ColdTime;
    }
}
