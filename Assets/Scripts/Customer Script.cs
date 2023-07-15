using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerScript : MonoBehaviour
{
    [Header("Order vars")]
    OrderManScr ordMan;
    TextMeshProUGUI orderTmp;
    Canvas selfCanvasComp;
    [SerializeField] float speechDelay = 1f;

    [Header("Random Sprite vars")]
    [SerializeField] Sprite[] customerVariants = new Sprite[4];

    void Start()
    {
        varReferences();
        getRandomSprite();
    }

    void Update()
    {
        
    }

    private void varReferences()
    {
        //Order vars
        ordMan = FindObjectOfType<OrderManScr>();
        orderTmp = GetComponentInChildren<TextMeshProUGUI>();
        selfCanvasComp = GetComponentInChildren<Canvas>();
    }

    void acquireOrder()
    {
        selfCanvasComp.enabled = true;   
        orderTmp.text = ordMan.GenerateOrder();
    }

    void getRandomSprite()
    {
        int spriteIdx = Random.Range(0,customerVariants.Length);
        SpriteRenderer selfSprite = GetComponent<SpriteRenderer>();
        selfSprite.sprite = customerVariants[spriteIdx];
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "OrderManager")
        {
            Invoke("acquireOrder",speechDelay);
        }
    }
}
