using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;
    public float jumpForce;
    private bool pulando = false;
    private Animator animator;
    private bool abaixando = false;
    public Transform camera;
    public float minimoCameraX;
    public float maximoCameraX;
    public float minimoCameraY;
    public float maximoCameraY;
    public GameObject fireball;
    public GameObject sons;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {
        // minimo 
        // maximo 
        float camx = rig.transform.position.x + 3;

        if (camx < minimoCameraX){
            camx = minimoCameraX;
        }

        if (camx > maximoCameraX){
            camx = maximoCameraX;
        }


        float camy = rig.transform.position.y + 3;
        if (camy < minimoCameraY){
            camy = minimoCameraY;
        }

        if (camy > maximoCameraY){
            camy = maximoCameraY;
        }

        camera.position = new Vector3(camx, camy, -10);

        // pega o valor da seta do teclado (1-direita / -1=esquerda)
        float mov = Input.GetAxisRaw("Horizontal");

        // faz o flip do sprite do jogador de acordo com a sua direção 
        if (mov == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (mov == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        // move o jogador para a direita e esequerda se não estiver pulando
        if (pulando == false)
        {
            rig.velocity = new Vector2(mov * speed, rig.velocity.y);
        }
        animator.SetFloat("Velocidade", Mathf.Abs(mov));
        //pula se não estiver pulando 
        if (Input.GetKeyDown(KeyCode.Space) && pulando == false)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pulando = true;
            animator.SetBool("Pulando", true);
            sons.GetComponents<AudioSource>()[3].Play();
        }

        // deslizando
        if (Input.GetKeyDown(KeyCode.DownArrow) && (abaixando == false))
        {
            abaixando = true;
            animator.SetBool("Abaixando", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            abaixando = false;
            animator.SetBool("Abaixando", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {             sons.GetComponents<AudioSource>()[4].Play();
             float fx;             float movFire;             bool flipFire;             if (GetComponent<SpriteRenderer>().flipX)
            {                 movFire = -3F;                 fx = rig.transform.position.x - 2;
                flipFire = false;             }
            else
            {                 movFire = 3F;                 fx = rig.transform.position.x + 2;
                flipFire = true;             }
            float fy = rig.transform.position.y + 0.5F;             float fz = rig.transform.position.z;             GameObject novo = Instantiate(fireball, new Vector3(fx, fy, fz), Quaternion.identity);             novo.GetComponent<ControleFireball>().mov = movFire;             novo.GetComponent<SpriteRenderer>().flipX = flipFire;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        pulando = false;
        animator.SetBool("Pulando", false);
        animator.SetBool("Abaixando", false);
    }
}
