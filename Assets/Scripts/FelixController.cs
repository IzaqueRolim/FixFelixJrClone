using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class FelixController : MonoBehaviour
{
    public bool canMove,isCenter;
    public Transform pointAtual;

    public GameObject time,panel;
    public Text pontos;

    public ralph ra;
    public int pontuacao {get;set;}
    int vida,objetivo;

    Animator anim;
    SpriteRenderer sprite;

    void Start(){
        isCenter = false;

        pontuacao = 0;
        vida = 3;
        objetivo = 26;

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public int GetPoint()
    {    
        return pontuacao;
    }

    void Update(){
    Debug.Log(panel.transform.GetChild(0).GetComponent<Text>());
        if(canMove){
            Move();
            InputConsert();
        }
        else{
            // if(transform.position != pointAtual.GetChild(2).transform.position){
            //     transform.position = Vector2.MoveTowards(transform.position,new Vector2(pointAtual.GetChild(2).transform.position.x, pointAtual.GetChild(2).transform.position.y),Time.deltaTime*2);
            // }
        }


        
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
                if(pointAtual.transform.childCount > 1){
                    anim.SetTrigger("conserta");
                    pontuacao++;
                    pontos.text = pontuacao.ToString();
                }
                else{
                    return;
                }  
        }
    }
    void Consert(){
        Destroy(pointAtual.GetChild(0).gameObject);
        if(pontuacao==objetivo){
                anim.SetTrigger("win");
                panel.SetActive(true);
                ra.SetLost(true);
                canMove = false; 
        }
    }

    public void Dano(){
        if(vida>1){
            vida--;
        }
        else{
            anim.SetTrigger("morre");
            canMove = false;
            ra.SetLost(true);
            panel.SetActive(true);
             panel.transform.GetChild(0).GetComponent<Text>().text = "QUE PENA!nVOCÊ PERDEU";
            
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