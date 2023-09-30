using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCGenerator : MonoBehaviour
{
    [Header("References")]
    public Image body;
    public Image hair;
    public Image shirt;
    public Image pants;
    public Image headgear;
    public Image accessory;


    [Space(10)]
    [Header("Sprite Pools")]
    public Sprite[] bodySprites;
    public Sprite[] hairSprites;
    public Sprite[] shirtSprites;
    public Sprite[] pantsSprites;
    public Sprite[] headgearSprites;
    public Sprite[] accessorySprites;

    private void OnEnable()
    {
        GenerateNPC();
    }

    private void Update()
    {
        // if press shift g, generate npc
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G))
            GenerateNPC();
    }

    public void GenerateNPC()
    {
        body.sprite = bodySprites[Random.Range(0, bodySprites.Length)];
        hair.sprite = hairSprites[Random.Range(0, hairSprites.Length)];
        shirt.sprite = shirtSprites[Random.Range(0, shirtSprites.Length)];
        pants.sprite = pantsSprites[Random.Range(0, pantsSprites.Length)];
        headgear.sprite = headgearSprites[Random.Range(0, headgearSprites.Length)];
        accessory.sprite = accessorySprites[Random.Range(0, accessorySprites.Length)];
    }
}
