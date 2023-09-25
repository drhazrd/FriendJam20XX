using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : Item
{ 
    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            Collect();
        }
    }
    void Collect(){
        value *= Random.Range(1,5);

    }
}
