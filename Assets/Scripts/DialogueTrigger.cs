using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSequence dialog;
    Transform player;
    public bool canTalk, playerHere;
    float coolDown = 1.5f;
    public GameObject interactIcon;
    
    
    void Update()
    {
        if (playerHere)
        {
            interactIcon.SetActive(!DialogueSystem.instance.dialogueActive);
        }
        else if (!playerHere)
        {
            interactIcon.SetActive(false);
        }
    }

    void OnTriggerStay(Collider col){
        if(col.tag == "Player"){
            playerHere = true;
            player = col.transform;
        }
    }
    void OnTriggerExit(Collider col){
        if(col.tag == "Player"){
            playerHere = false;
            player = null;
        }
    }
}
