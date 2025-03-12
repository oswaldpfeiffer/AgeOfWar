using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeatManager : MonoBehaviour
{
    [SerializeField] TMP_Text _meatAmountText;
    [SerializeField] Image _meatAmountFill;

    private int _meatCount = 0;
    private float _counter = 0;

    // DEBUG
    private float MEATDELAY = 4f;

    void Start()
    {
        UpdateText();
        _meatAmountFill.fillAmount = 0f;
    }

    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > MEATDELAY)
        {
            _meatCount++;
            UpdateText();
            _counter = 0;
        }
        _meatAmountFill.fillAmount = Mathf.Clamp01(_counter / MEATDELAY);
    }

    private void UpdateText ()
    {
        _meatAmountText.text = _meatCount.ToString();
    }

    public int GetMeatAmount ()
    {
        return _meatCount;
    }

    public void PayMeat (int amount)
    {
        _meatCount -= amount;
        UpdateText();
    }
}
