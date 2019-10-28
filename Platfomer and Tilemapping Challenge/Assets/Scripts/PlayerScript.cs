using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite_rn;
    private CircleCollider2D player_collide;
    private Animator anim;

    public float speed;
    public Text score;
    public Text lives;
    public Text winText;
    public Text loseText;

    private int scoreValue;
    private int livesValue;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite_rn = GetComponent<SpriteRenderer>();
        player_collide = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        score.text = "";
        lives.text = "";
        winText.text = "";
        loseText.text = "";
        scoreValue = 0;
        livesValue = 3;
        SetCountText();
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMoment = Input.GetAxis("Horizontal");
        float verMoment = Input.GetAxis("Vertical");

        rb2d.AddForce(new Vector2(hozMoment * speed, verMoment * speed));

        if (facingRight == false && hozMoment > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMoment < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }




        if (livesValue <= 0 | scoreValue >= 8)
        {
            Destroy(sprite_rn);
            Destroy(player_collide);
            rb2d.isKinematic = true;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))

        {
            collision.gameObject.SetActive(false);
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            SetCountText();
        }

        if (collision.gameObject.CompareTag("Enemy"))

        {
            collision.gameObject.SetActive(false);
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            SetCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);

                anim.SetInteger("State", 0);

            }

         
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetCountText()
    {
        if (scoreValue == 4)
        {
            transform.position = new Vector2(50.0f, 50.0f);
        }

        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();

        if (scoreValue >= 8)
        {
            winText.text = "You Win!" +
                "\n" +
                "Game created by Jonathan Gonzalez!";
        }
        else if (livesValue <= 0)
        {
            loseText.text = "You Lose!" +
                "\n" +
                "Game created by Jonathan Gonzalez!";
        }
    }
}