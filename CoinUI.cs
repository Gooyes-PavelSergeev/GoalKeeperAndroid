using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _coinText;

    private int _currentCoinAmount;

    private void Start()
    {
        _coinText.text = "0";
    }

    public void IncrementCoinAmount(int amount)
    {
        _currentCoinAmount += amount;
        _coinText.text = _currentCoinAmount.ToString();
    }
}
