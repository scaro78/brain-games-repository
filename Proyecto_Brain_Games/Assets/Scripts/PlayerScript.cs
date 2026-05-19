using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameManagerScript gmScr;

    [SerializeField] int jumpoForce;
    [SerializeField] int speed;

    bool hasGameStarted = false;
    bool hasGameOver = false;   


    //Behaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gmScr = Object.FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hasGameStarted && !hasGameOver)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpoForce);
           
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hasGameOver = true;
            gmScr.GameOver();
        }
    }
    //Función para saber la posición del jugador
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void StartGame()
    {
        hasGameStarted = true;
        rb.simulated = true;
    }

}
