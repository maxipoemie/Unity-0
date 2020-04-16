using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private Vector3 OrigTrans;

    public float speed;
    public Text countText;
    public Text winText;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public bool Checkpoint;
    public Vector3 SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        OrigTrans = transform.position;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (count >= 17)
        {
            rb.isKinematic = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Lava"))
        {
            transform.position = OrigTrans;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 17)
        {
            winText.text = "You Win!";
        }
    }
}