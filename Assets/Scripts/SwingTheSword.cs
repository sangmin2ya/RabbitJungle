using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTheSword : MonoBehaviour
{
    public float swingSpeed = 500.0f;
    public GameObject swordObject;
    float deltaAngle = 0;
    float swingAngle = 90.0f;
    public bool bClass = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && bClass) 
        {
            if(!swordObject.activeSelf)
            {
                swordObject.SetActive(true);

                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = rotation;

                deltaAngle = 0;

                StartCoroutine("Swing");

                /*while (deltaAngle < swingAngle)
                {
                    float delta = swingSpeed * Time.deltaTime;
                }
                Swing();*/
            }
        }
    }

    IEnumerator Swing()
    {
        while (deltaAngle < swingAngle)
        {
            yield return null;
        
            float delta = swingSpeed * Time.deltaTime;

            transform.Rotate(-Vector3.forward * delta);
            deltaAngle += delta;

            if (deltaAngle > swingAngle)
            {
                swordObject.SetActive(false);
            }
        }
        /*if (deltaAngle < swingAngle)
        {
            
        }
        else
        {
            swordObject.SetActive(false);
        }*/
    }
}
