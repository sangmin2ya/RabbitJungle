using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;

   

    public float speed;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        StartCoroutine("Flip");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Hit();

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

        //RayTrace2D를 이용하여 이동 멈추기
    }

    public void baseSkill()
    {
        speed = 20f;
    }

    public void Hit()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(player.transform.position, Vector2.down);
        if (hitdown.transform != null)
        {
            if(hitdown.distance < 1 && hitdown.collider.CompareTag("Wall"))
            {
                Debug.Log("아래 충돌!");
                if(verticalInput < 0)
                {
                    verticalInput = 0;
                }
            }

        }
        RaycastHit2D hitup = Physics2D.Raycast(player.transform.position, Vector2.up );
        if (hitup.transform != null)
        {
            if (hitup.distance < 1 && hitup.collider.CompareTag("Wall"))
            {
                Debug.Log("위 충돌!");
                if (verticalInput > 0)
                {
                    verticalInput = 0;
                }
            }
        }
        RaycastHit2D hitleft = Physics2D.Raycast(player.transform.position, Vector2.left);
        if (hitleft.transform != null)
        {
            if (hitleft.distance < 0.5 && hitleft.collider.CompareTag("Wall"))
            {
                Debug.Log("옆 충돌!");
                if (horizontalInput < 0)
                {
                    horizontalInput = 0;
                }
            }

        }
        RaycastHit2D hitright = Physics2D.Raycast(player.transform.position, Vector2.right);
        if (hitright.transform != null)
        {
            if (hitright.distance < 0.5 && hitright.collider.CompareTag("Wall"))
            {
                Debug.Log("오른쪽 충돌!");
                if (horizontalInput > 0)
                {
                    horizontalInput = 0;
                }
            }
        }
    }

    IEnumerator Flip()
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
    }

}
