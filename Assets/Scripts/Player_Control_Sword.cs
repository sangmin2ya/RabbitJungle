using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control_Sword : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    public float speed;
    bool dashState = false;
    Rigidbody2D myRigid;
    float knockback = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        //StartCoroutine("Flip");
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            baseSkill();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = 10.0f;
        }

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void baseSkill()
    {
        speed = 20f;
        StartCoroutine("DashCutter");
    }

    IEnumerator DashCutter()
    {
        dashState = true;
        this.gameObject.layer = 10;
        yield return new WaitForSeconds(0.25f);
        dashState = false;
        this.gameObject.layer = 0;
    }

    /*IEnumerator Flip()
    {
        while (true)
        {
            yield return null;
            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (dashState)
            {
                //Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("아야!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (dashState)
            {
                // 체력을 깎거나 체력이 적으면 죽인다.
                //Destroy(collision.gameObject);
            }
            else
            {
                Vector2 dir = transform.position - collision.gameObject.transform.position;
                // 맞았으니 플레이어에게 데미지를 준다
                //
                //myRigid.AddForce(dir.normalized * knockback);
            }
        }
    }
}
