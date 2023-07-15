using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManScr : MonoBehaviour
{
    [Header("Order Variables")]
    string[] menu = {"pizza","hamburger","pasta","french fries","salad","chocalate cake","steak","waffle","ice cream","donut"};
    int orderLength;
    List<int> currentOrderIndexes = new List<int>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public string GenerateOrder()
    {
        string orderText = " ";
        orderLength = Random.Range(1,4);
        int currentFoodIndex;

        currentOrderIndexes.Clear();

        for (int i = 0; i < orderLength; i++)
        {
            currentFoodIndex = Random.Range(0,10);

            while(currentOrderIndexes.Contains(currentFoodIndex))
            {
                currentFoodIndex = Random.Range(0,10);;
            }

            currentOrderIndexes.Add(currentFoodIndex);
        }

        for (int i = 0; i<currentOrderIndexes.Count; i++)
        {
            orderText += menu[currentOrderIndexes[i]] + " ";
        }

        return orderText;
    }
}
