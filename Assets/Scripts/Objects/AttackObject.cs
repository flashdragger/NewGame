using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    public float MoveSpeed = 8.0f;
    public float ExistTime = 5.0f;
    private float _existTimer;




    private void OnEnable() {
        _existTimer = 0.0f;
    }

    private void Update() {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        _existTimer += Time.deltaTime;
        if (_existTimer >= ExistTime) 
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }
}
