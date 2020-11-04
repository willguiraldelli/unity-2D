using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleInimigo : MonoBehaviour
{
    private Rigidbody2D rig;
    private float mov = 1F;
    public GameObject sons;
    public Text vida;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (mov > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (mov < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        rig.velocity = new Vector2(mov, rig.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.transform.position.y > gameObject.transform.position.y + 1)
            {
                Destroy(gameObject);
                sons.GetComponents<AudioSource>()[2].Play();
            }
            else
            {
                sons.GetComponents<AudioSource>()[1].Play();

                int numero;
                int.TryParse(vida.text, out numero);
                numero--;
                vida.text = "" + numero;
                if (numero == 0)
                {
                    //abrir cena de game over  
                    Destroy(col.gameObject);
                }

            }
        }
        else if (col.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
            sons.GetComponents<AudioSource>()[2].Play();
        }
        else
        {
            mov = mov * -1;
        }
    }
}

