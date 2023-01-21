using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FelixController : MonoBehaviour
{
    public bool canMove,die;
    public Transform pointAtual;

    public int pontuacao, vida;

    Animator anim;
    SpriteRenderer sprite;

    void Start(){
        die = false;
        pontuacao = 0;
        vida = 3;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(canMove){
            Move();
            InputConsert();
        }
        print(pontuacao);
    }

    async void Move(){
         if(Input.GetKeyDown(KeyCode.D)){
                Vector2 force = pointAtual.GetComponent<Points>().forceRight;
                sprite.flipX = false;
                if(force != new Vector2(0,0)){
                    GetComponent<Rigidbody2D>().AddForce(force);
                    canMove=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.A)){
                Vector2 force = pointAtual.GetComponent<Points>().forceLeft;
                sprite.flipX = true;
                if(force != new Vector2(0,0)){
                    GetComponent<Rigidbody2D>().AddForce(force);
                    canMove=false;
                }
            }
            if(Input.GetKeyDown(KeyCode.W)){
                Vector2 force = pointAtual.GetComponent<Points>().forceUp;
                if(force != new Vector2(0,0)){
                    GetComponent<Rigidbody2D>().AddForce(force);
                    canMove=false;
                }  
            }
            if(Input.GetKeyDown(KeyCode.S)){
                if(pointAtual.GetComponent<Points>().canDown){
                    GetComponent<BoxCollider2D>().isTrigger = true;
                    await Task.Delay(1000);
                    GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
    }
    void InputConsert(){
        if(Input.GetKeyDown(KeyCode.E)){
            print(pointAtual.transform.childCount);
            if(pointAtual.transform.childCount > 1){
                anim.SetTrigger("conserta");
                pontuacao++;
            }
            else{
                return;
            }
        }
    }
    void Consert(){
        Destroy(pointAtual.GetChild(0).gameObject);
    }
    public void Dano(){
        if(vida>1){
            vida--;
        }
        else{
            anim.SetTrigger("morre");
            die = true;
            //animacao pra morrer
        }
    }
    void DiminuirColisor()
    {
        GetComponent<BoxCollider2D>().size =  new Vector2(0.2f,0.04f);
    }
   
    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "point"){
            canMove = true;
            pointAtual = col.gameObject.transform.parent;
        }
    }
    
}