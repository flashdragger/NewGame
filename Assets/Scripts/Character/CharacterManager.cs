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
    public AudioSource music;

    public List<GameObject> Characters {
        get {return _characters;}
    }

    private void Start() {
        foreach (var i in PrefabList) {
            _characters.Add(Instantiate<GameObject>(i, transform));
        }
        ChangeCharacter(DefaultCharacter);
        _coldTimer = 0f;
    }

    private void Update() {
        if(_coldTimer > 0) {
            _coldTimer -= Time.deltaTime;
            return;
        }
        
        for (int i = 0; i < KeyCodes.Length; i++) 
            if(Input.GetKeyDown(KeyCodes[i]) && CurrentCharacter != i && _characters[i].GetComponent<Attributes>().HP > 0) {
                ChangeCharacter(i);
                CurrentCharacter = i;
                music.Play();

            }
    }

    public void ChangeCharacter(int t) {
        foreach (var i in _characters) 
            i.SetActive(false);
        _characters[t].SetActive(true);
        _coldTimer = ColdTime;
    }
}
