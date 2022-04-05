using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public double timeSwitch = 1.5;
    public double switchTimer;
    private bool isReady;
    public List<GameObject> PrefabList = new List<GameObject>(); 
    public List<GameObject> CharacterList = new List<GameObject>();
    public int defaultCharacter;
    private int currentCharacter;

    private void Start() {
        // PrefabList.Add(Resources.Load("Prefabs/Capsule") as GameObject);
        // PrefabList.Add(Resources.Load("Prefabs/Circle") as GameObject);
        // PrefabList.Add(Resources.Load("Prefabs/Square") as GameObject);
        for (int i = 0; i < PrefabList.Count; i++) 
            CharacterList.Add(Instantiate(PrefabList[i]));
        currentCharacter = defaultCharacter = 0;
        switchTimer = -1.0;
        UpdateCharacter();
    }

    private void Update() {
        if(!isReady) {
            switchTimer -= Time.deltaTime;
            if(switchTimer < 0) 
                isReady = true;
        }
        Reason();
    }

    private void Reason() {
        if(!isReady) 
            return;

        if(Input.GetKeyDown(KeyCode.Alpha1) && currentCharacter != 0)
        {
            isReady = false;
            switchTimer = timeSwitch;
            currentCharacter = 0;
            UpdateCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currentCharacter != 1)
        {
            isReady = false;
            switchTimer = timeSwitch;
            currentCharacter = 1;
            UpdateCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && currentCharacter != 2)
        {
            isReady = false;
            switchTimer = timeSwitch;
            currentCharacter = 2;
            UpdateCharacter();
        }
    }
    private void UpdateCharacter() {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i != currentCharacter) {
                CharacterList[i].SetActive(false);
            }
        }
        CharacterList[currentCharacter].SetActive(true);
    }
}
