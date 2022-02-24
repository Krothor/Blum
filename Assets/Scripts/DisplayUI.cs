using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    int currentGold = 0;   
    [SerializeField] TextMeshProUGUI goldText;
    void Update()
    {
        goldText.text = GoldToText();
    }
    public void AddGold(int goldAmount)
    {
        currentGold += goldAmount;
    }
    string GoldToText()
    {
        string returnString="";
        string goldToText = currentGold.ToString();
        for (int i = 4; i > goldToText.Length; i--)
        {
            returnString += "0";
        }
        return returnString += goldToText;
    }
}
