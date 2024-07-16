using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;


    public GameObject player;

    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager.Speed = 10.0f;
        StartCoroutine("Flip");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Block();

        baseSkill();

        

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * dataManager.Speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * dataManager.Speed);

        //RayTrace2D를 이용하여 이동 멈추기
    }

    public void baseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dataManager.Speed =  20f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            dataManager.Speed = 10.0f;
        }
    }

    public void Block()
    {
        RaycastHit2D[] hitdown = Physics2D.RaycastAll(player.transform.position, Vector2.down);

        for (int i = 0; i < hitdown.Length; i++)
        {
            if (hitdown[i].transform != null)
            {
                if (hitdown[i].distance < 1 && hitdown[i].collider.CompareTag("Wall"))
                {
                    Debug.Log("아래 충돌!");
                    if (verticalInput < 0)
                    {
                        verticalInput = 0;
                    }
                }

            }
        }

        RaycastHit2D[] hitup = Physics2D.RaycastAll (player.transform.position, Vector2.up );
        for(int i = 0;i < hitup.Length; i++)
        {
            if (hitup[i].transform != null)
            {
                if (hitup[i].distance < 1 && hitup[i].collider.CompareTag("Wall"))
                {
                    Debug.Log("위 충돌!");
                    if (verticalInput > 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        RaycastHit2D[] hitleft = Physics2D.RaycastAll(player.transform.position, Vector2.left);
        for(int i = 0; i < hitleft.Length; i++)
        {
            if (hitleft[i].transform != null)
            {
                if (hitleft[i].distance < 0.5 && hitleft[i].collider.CompareTag("Wall"))
                {
                    Debug.Log("옆 충돌!");
                    if (horizontalInput < 0)
                    {
                        horizontalInput = 0;
                    }
                }

            }
        }

        RaycastHit2D[] hitright = Physics2D.RaycastAll(player.transform.position, Vector2.right);
        for(int i = 0; i< hitright.Length; i++)
        {
            if (hitright[i].transform != null)
            {
                if (hitright[i].distance < 0.5 && hitright[i].collider.CompareTag("Wall"))
                {
                    Debug.Log("오른쪽 충돌!");
                    if (horizontalInput > 0)
                    {
                        horizontalInput = 0;
                    }
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
