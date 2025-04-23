using TMPro;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public int collectedMoney = 0;
    public TextMeshProUGUI moneyText;
    public int TimeSpend;
    public TextMeshProUGUI TimeText;
    public float DelayInSeconds = 1f;
    public int collectedLifes = 0;
    public TextMeshProUGUI LifesText;

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }
    public void ActMoney()
    {
        collectedMoney++;
        moneyText.text = collectedMoney.ToString() + "";
    }
    public void ActLifes()
    {
        collectedLifes++;
        LifesText.text = collectedLifes.ToString() + "";
    }
    IEnumerator UpdateTime()
    {
        while (true)
        {
            TimeSpend++;
            TimeText.text = TimeSpend.ToString();
            yield return new WaitForSeconds(DelayInSeconds);
        }
    }
}
