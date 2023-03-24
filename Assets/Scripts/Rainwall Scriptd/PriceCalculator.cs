using System.Collections.Generic;
using UnityEngine;

using TMPro;

using static DataManagement;

public class PriceCalculator : MonoBehaviour
{

    public static PriceCalculator instance;
    string panelName;
    int price;
    float totalCost;
    float xValue;
    float yValue;

    [SerializeField] List<Transform> panelList;

    float amountOfPanels;
    [SerializeField] TMP_Text basePrice;
    [SerializeField] TMP_Text totalPrice;
    

    void Start()
    {
        instance = this;
    }

    void Update()
    {

        if (RoofManager.instance.getPanel != null)
        {
            ListViewer();

            panelName = RoofManager.instance.getPanel.transform.GetChild(0).name;
            if (panelName.Contains("Zonnepaneel (1)")) price = 198;
            if (panelName.Contains("Zonnepaneel (2)")) price = 248;
            if (panelName.Contains("Zonnepaneel (3)")) price = 396;

            xValue = RoofManager.instance.getX;
            yValue = RoofManager.instance.getY;

            basePrice.text = "price: " + price;

            totalCost = 0;

            if (xValue > 1 || yValue> 1) totalCost = amountOfPanels * price;
            totalPrice.text = "total cost: " + totalCost;

          

        }

    }

    public void ListViewer()
    {
        panelList = RoofManager.instance.panelsPrijs;
        amountOfPanels = 0;

        for (int i = 0; i < panelList.Count; i++)
        {
            int Children = 1;
            if (Children ==  panelList[i].childCount)
            {
                amountOfPanels = amountOfPanels + 1;
            }
        }
    }
}
