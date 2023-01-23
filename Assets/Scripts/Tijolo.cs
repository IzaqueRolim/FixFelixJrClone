using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tijolo : MonoBehaviour
{
    
    void Start()
    {
        Destroy(this.gameObject,4f);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "felix"){
            col.gameObject.GetComponent<FelixController>().Dano();
        }
    }
}
