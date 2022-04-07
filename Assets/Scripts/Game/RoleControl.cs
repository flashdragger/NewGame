using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour
{
    private float xMin=-14.9f;
    private float xMax=14f;
    private float yMin=-8.6f;
    private float yMax=6.9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        //position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.x = Mathf.Clamp(position.x + 5.0f * horizontal * Time.deltaTime,xMin,xMax);
        position.y = Mathf.Clamp(position.y + 5.0f * vertical * Time.deltaTime, yMin, yMax);
        //position.y = position.y + 3.0f * vertical * Time.deltaTime;
        transform.position = position;
    }
}
