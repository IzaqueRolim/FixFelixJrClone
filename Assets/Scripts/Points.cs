using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;

public class Points : MonoBehaviour
{
    public Vector2 forceUp,forceLeft,forceRight;

    public Transform felix;
    public Transform piso;
    

    public bool canDown;


    void Start()
    {
        
        piso = this.gameObject.transform.GetChild(2);
    }

    void Update(){
        if(felix.position.y < piso.position.y){
            ChangeTrigger(true);
        }
        else{
            ChangeTrigger(false);
        }
    }

    public void ChangeTrigger(bool trigger){
        piso.GetComponent<EdgeCollider2D>().isTrigger = trigger;        
    }

}
