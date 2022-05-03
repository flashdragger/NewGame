using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float healthPoints = 50.0f;
    public float maxHealthPoints = 100.0f;
    public GameObject bloodStainPrefab;
    public AudioClip hurtAudio;
    protected AudioSource aSource;
    virtual public void CharacterDie()
    {
        if (bloodStainPrefab != null)
        {
            Instantiate(bloodStainPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    virtual public IEnumerator TakeDamage(float damageAmount, float interval)
    {
        while (true)
        {
            if (hurtAudio != null)
            {
                aSource.clip = hurtAudio;
                aSource.Play();
            }
            StartCoroutine(CharacterFlick());
            healthPoints -= damageAmount;
            if (healthPoints <= float.Epsilon)
            {
                CharacterDie();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }

    }
    private IEnumerator CharacterFlick()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
