using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ralph : MonoBehaviour
{
    public Transform[] dests;
    public GameObject fab;
    public FelixController controller;

    Animator animator;

    public bool lost;

    int index,numAnterior;

    void Start(){
        ChangePos();
        animator = GetComponent<Animator>();
        lost = false;
    }
    void Update()
    {
        if(lost == false){
            if(transform.position == dests[index].position){
                animator.SetBool("soco",true);
            }
            else{
                animator.SetBool("soco",false);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(dests[index].position.x,dests[index].position.y),Time.deltaTime);
            }
        }
        else{
            animator.SetBool("soco",false);
        }

    }

    void ChangePos()
    {
        if(lost == false){
            numAnterior = index;
            index = Random.Range(0, dests.Length);
            if(index == numAnterior){
                ChangePos();
            }
            else{
                return;
            }
        }
    }

    
    void InsTijolo(){
        Instantiate(fab,transform.position,Quaternion.identity);
    }

    public void SetLost(bool value){
        lost = true;
    }

}
