using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float wanderIntervalTime = 3.0f;
    public float wanderSpeed = 1.5f;
    public float pursuitSpeed = 2.5f;
    private float currentSpeed;
    private Rigidbody2D rb2d;
    private Coroutine moveCoroutine;
    private Vector2 endPointPositon;
    private Animator anim;
    //private CircleCollider2D circle;
    private Transform playerTransform = null;
    private bool followPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //anim = gameObject.GetComponent<Animator>();
        //circle = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine(WanderCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, endPointPositon, Color.red);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, circle.radius);
    //}
    IEnumerator WanderCoroutine()
    {
        while (true)
        {
            ChooseEndPoint();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCoroutine());
            yield return new WaitForSeconds(wanderIntervalTime);
        }
    }

    void ChooseEndPoint()
    {
        float wanderAngle = Random.Range(0, 360);
        float wanderRadius = Random.Range(2, 5);
        wanderAngle = wanderAngle * Mathf.Deg2Rad;
        endPointPositon = rb2d.position + new Vector2(Mathf.Cos(wanderAngle), Mathf.Sin(wanderAngle)) * wanderRadius;
    }

    IEnumerator MoveCoroutine()
    {
        float remainingDistance = (rb2d.position - endPointPositon).sqrMagnitude;
        while (remainingDistance > float.Epsilon)
        {
            //anim.SetBool("isWalking", true);
            currentSpeed = wanderSpeed;
            if (playerTransform != null)
            {
                endPointPositon = playerTransform.position;
                currentSpeed = pursuitSpeed;
            }
            Vector2 newPostion = Vector2.MoveTowards(rb2d.position, endPointPositon, currentSpeed * Time.fixedDeltaTime);
            rb2d.MovePosition(newPostion);
            remainingDistance = (rb2d.position - endPointPositon).sqrMagnitude;
            yield return new WaitForFixedUpdate();
        }
        //anim.SetBool("isWalking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            playerTransform = collision.gameObject.GetComponent<Transform>();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            anim.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;
            playerTransform = null;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }
    }
}
