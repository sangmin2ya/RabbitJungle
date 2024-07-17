using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Control : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void LateUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Weapon") && !other.gameObject.CompareTag("Skill"))
        {
            Destroy(gameObject);
        }
    }
    
    
}
