using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Player Cash
    public TextMeshProUGUI playerCash; // Oyuncu nakit miktar�

    // Income Button
    public TextMeshProUGUI incomeLevel; // Gelir seviyesi
    public TextMeshProUGUI incomeCash; // Gelir nakit miktar�

    // AddPipe
    public TextMeshProUGUI addPipeCash; // Boru ekleme i�lemi i�in gerekli nakit miktar�

    //Speed Button
    public TextMeshProUGUI speedLevel; // H�z seviyesi
    public TextMeshProUGUI speedCash; // H�z i�in gerekli nakit miktar�

    //Combine Button
    public TextMeshProUGUI combineCash; // Birle�tirme i�lemi i�in gerekli nakit miktar�

    public GameObject CombineButton; // Birle�tirme d��mesi

    public Slider gPUBar; // GPU ilerleme �ubu�u
    public Slider giftBar; // Hediye ilerleme �ubu�u

    public List<TextMeshProUGUI> huniMultiplier; // Huni �arpanlar� metin kutular� listesi

    #region Action
    // Aksiyonlar
    public static Action<float> SetPlayerCash; // Oyuncu nakit miktar�n� ayarlamak i�in
    public static Action<float> IncomeCash; // Gelir nakit miktar�n� ayarlamak i�in
    public static Action<int> IncomeLevel; // Gelir seviyesini ayarlamak i�in
    public static Action<float> AddPipeCash; // Boru ekleme i�lemi i�in gerekli nakit miktar�n� ayarlamak i�in
    public static Action<float> SpeedCash; // H�z i�in gerekli nakit miktar�n� ayarlamak i�in
    public static Action<int> SpeedLevel; // H�z seviyesini ayarlamak i�in
    public static Action<float> CombineCash; // Birle�tirme i�lemi i�in gerekli nakit miktar�n� ayarlamak i�in
    public static Action<int, string> HuniMultiplier; // Huni �arpanlar� metin kutular�n� ayarlamak i�in
    #endregion

    private void OnEnable()
    {
        // Aksiyonlar� etkinle�tir
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
        // Aksiyonlar� devre d��� b�rak
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