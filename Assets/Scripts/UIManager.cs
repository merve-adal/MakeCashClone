using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Player Cash
    public TextMeshProUGUI playerCash; // Oyuncu nakit miktarý

    // Income Button
    public TextMeshProUGUI incomeLevel; // Gelir seviyesi
    public TextMeshProUGUI incomeCash; // Gelir nakit miktarý

    // AddPipe
    public TextMeshProUGUI addPipeCash; // Boru ekleme iþlemi için gerekli nakit miktarý

    //Speed Button
    public TextMeshProUGUI speedLevel; // Hýz seviyesi
    public TextMeshProUGUI speedCash; // Hýz için gerekli nakit miktarý

    //Combine Button
    public TextMeshProUGUI combineCash; // Birleþtirme iþlemi için gerekli nakit miktarý

    public GameObject CombineButton; // Birleþtirme düðmesi

    public Slider gPUBar; // GPU ilerleme çubuðu
    public Slider giftBar; // Hediye ilerleme çubuðu

    public List<TextMeshProUGUI> huniMultiplier; // Huni çarpanlarý metin kutularý listesi

    #region Action
    // Aksiyonlar
    public static Action<float> SetPlayerCash; // Oyuncu nakit miktarýný ayarlamak için
    public static Action<float> IncomeCash; // Gelir nakit miktarýný ayarlamak için
    public static Action<int> IncomeLevel; // Gelir seviyesini ayarlamak için
    public static Action<float> AddPipeCash; // Boru ekleme iþlemi için gerekli nakit miktarýný ayarlamak için
    public static Action<float> SpeedCash; // Hýz için gerekli nakit miktarýný ayarlamak için
    public static Action<int> SpeedLevel; // Hýz seviyesini ayarlamak için
    public static Action<float> CombineCash; // Birleþtirme iþlemi için gerekli nakit miktarýný ayarlamak için
    public static Action<int, string> HuniMultiplier; // Huni çarpanlarý metin kutularýný ayarlamak için
    #endregion

    private void OnEnable()
    {
        // Aksiyonlarý etkinleþtir
        SetPlayerCash += SetCash;
        IncomeCash += SetIncomeCash;
        IncomeLevel += SetIncomeLevel;
        AddPipeCash += SetAddPipeCash;
        SpeedCash += SetSpeedCash;
        SpeedLevel += SetSpeedLevel;
        CombineCash += SetCombineCash;
        GameManager.SetGiftCash += GiftBar;
        GameManager.SetGiftCashMax += GiftBarMax;
        HuniMultiplier += HuniMultiplierText;
    }

    void SetCash(float _cash)
    {
        playerCash.text = _cash.ToString();
    }

    void SetIncomeCash(float _cash)
    {
        incomeCash.text = _cash.ToString();
    }

    void SetIncomeLevel(int _level)
    {
        incomeLevel.text = _level.ToString();
    }

    void SetAddPipeCash(float _cash)
    {
        addPipeCash.text = _cash.ToString();
    }

    void SetSpeedCash(float _cash)
    {
        speedCash.text = _cash.ToString();
    }

    void SetSpeedLevel(int _level)
    {
        speedLevel.text = _level.ToString();
    }

    void SetCombineCash(float _cash)
    {
        combineCash.text = _cash.ToString();
    }

    public void CombineButtonActived(bool actived)
    {
        CombineButton.SetActive(actived);
    }

    public void GPUBar(int _value)
    {
        gPUBar.value = _value;
    }

    public void GPUBarMax(int _value)
    {
        gPUBar.maxValue = _value;
    }

    public void GiftBar(float _value)
    {
        giftBar.value = _value;
    }

    public void GiftBarMax(float _value)
    {
        giftBar.maxValue = _value;
    }

    public void HuniMultiplierText(int _count, string _text)
    {
        huniMultiplier[_count].text = _text;
    }

    private void OnDisable()
    {
        // Aksiyonlarý devre dýþý býrak
        SetPlayerCash -= SetCash;
        IncomeCash -= SetIncomeCash;
        IncomeLevel -= SetIncomeLevel;
        AddPipeCash -= SetAddPipeCash;
        SpeedCash -= SetSpeedCash;
        SpeedLevel -= SetSpeedLevel;
        CombineCash -= SetCombineCash;
        GameManager.SetGiftCash -= GiftBar;
        GameManager.SetGiftCashMax -= GiftBarMax;
        HuniMultiplier += HuniMultiplierText;
    }
}