using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb2d.AddForce(new Vector2(moveHorizontal * speed, moveVertical * speed));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag ("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
        if (count == 4)
        {
            transform.position = new Vector2(55.0f, 50.0f);
            lives = 3;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 3);
        }
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You win! Game created by Allyson Pyle!";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            GameObject.Destroy(gameObject);
            winText.text = "You lose, try again.";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
