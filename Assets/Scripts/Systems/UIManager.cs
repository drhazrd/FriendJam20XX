using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public CanvasGroup blackOut;

    void Awake(){
         if(UIManager.instance == null){
            instance = this;
        } else if (UIManager.instance != this && UIManager.instance != null){
            Destroy(this);
        }
    }
    void Start()
    {
        blackOut.alpha = 0;
    }

    public void BlackOut(){
        StartCoroutine(BlackOutProcess());

    }
    IEnumerator BlackOutProcess()
    {

        blackOut.alpha = 1;
        yield return new WaitForSeconds(.3f);
        blackOut.alpha = 0;
        
    }
}
