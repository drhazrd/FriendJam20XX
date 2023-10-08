using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public CanvasGroup blackOut;
    public GameObject pickupUI;
    public GameObject interactUI;

    void Awake()
    {
        if (UIManager.instance == null)
        {
            instance = this;
        }
        else if (UIManager.instance != this && UIManager.instance != null)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        ResetUI();
    }

    public void BlackOut()
    {
        StartCoroutine(BlackOutProcess());

    }
    IEnumerator BlackOutProcess()
    {

        blackOut.alpha = Mathf.MoveTowards(1f, 0f, 0.5f);
        blackOut.alpha = 1f;
        yield return new WaitForSeconds(1f);
        blackOut.alpha = Mathf.MoveTowards(0f, 1f, 0.5f);
        blackOut.alpha = 0f;

    }

    public void SetPickupUI(Item item, Vector2 position)
    {
        pickupUI.SetActive(false);
        TextMeshProUGUI textMesh = pickupUI.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = item.itemName;
        textMesh.color = RarityColor(item.rarity);
        Vector2 offset = new Vector2(0, 50);
        pickupUI.transform.position = Vector2.Lerp(pickupUI.transform.position, position + offset, 0.1f);
        pickupUI.SetActive(true);
    }

    public void SetPickupUI(InteractableObject io, Vector2 position)
    {
        pickupUI.SetActive(false);
        TextMeshProUGUI textMesh = pickupUI.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = io.promptText;
        textMesh.color = Color.white;
        Vector2 offset = new Vector2(0, 50);
        pickupUI.transform.position = Vector2.Lerp(pickupUI.transform.position, position + offset, 0.1f);
        pickupUI.SetActive(true);
    }

    private Color RarityColor(Rarity r)
    {
        if (r == Rarity.Common)
        {
            return Color.white;
        }
        else if (r == Rarity.Uncommon)
        {
            return Color.green;
        }
        else if (r == Rarity.Rare)
        {
            return Color.blue;
        }
        else if (r == Rarity.Legendary)
        {
            return Color.yellow;
        }
        else
        {
            return Color.white;
        }
    }
    void ResetUI(){
        blackOut.alpha = 0;
        interactUI.SetActive(false);
        pickupUI.SetActive(false);
    }
}
