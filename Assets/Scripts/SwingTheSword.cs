using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTheSword : MonoBehaviour
{
    public float swingSpeed = 500.0f;
    public GameObject swordObject;
    public GameObject yeolpacham;
    GameObject powerSlash;
    float deltaAngle = 0;
    float deltaTime = 0;
    float coolTime = 10.0f;
    float lifespan = 2.0f;
    float swingAngle = 90.0f;
    // you must get class identification number from player status
    //int playerClass = 404; // 
    public bool bClass = true;
    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && bClass/*playerClass == 404*/) 
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
            }
        }
        if (Input.GetMouseButtonDown(1) && (deltaTime == 0 || deltaTime >= 5.0f) && bClass)
        {
            deltaTime = 0;

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            powerSlash = Instantiate(yeolpacham, new Vector2(transform.position.x, transform.position.y) + direction.normalized * 2, rotation);

            StartCoroutine(PowerSlash(rotation));
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
    }

    IEnumerator PowerSlash(Quaternion rot)
    {
        while (true)
        {
            yield return null;
            Vector3 dir3 = rot.eulerAngles;
            Vector3 dir = new Vector3(Mathf.Cos(dir3.z * Mathf.Deg2Rad), Mathf.Sin(dir3.z * Mathf.Deg2Rad), 0);
            powerSlash.transform.position += dir.normalized * Time.deltaTime * 50;
            deltaTime += Time.deltaTime;

            if (deltaTime >= lifespan)
                break;
        }

        deltaTime = 0;
        Destroy(powerSlash);
    }
}
