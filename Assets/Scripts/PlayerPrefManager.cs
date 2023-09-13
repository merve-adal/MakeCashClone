using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    // "PlayerCash" ad�ndaki oyuncu paras� de�i�kenini getirir veya ayarlar
    public static float PlayerCash
    {
        get { return PlayerPrefs.GetFloat("PlayerCash", 0f); }
        set { PlayerPrefs.SetFloat("PlayerCash", value); }
    }

    // "GiftCash" ad�ndaki hediye paras� de�i�kenini getirir veya ayarlar
    public static float GiftCash
    {
        get { return PlayerPrefs.GetFloat("GiftCash", 0f); }
        set { PlayerPrefs.SetFloat("GiftCash", value); }
    }

    // "SpeedLevel" ad�ndaki h�z d��mesi seviyesi de�i�kenini getirir veya ayarlar
    public static int SpeedButtonLevel
    {
        get { return PlayerPrefs.GetInt("SpeedLevel", 0); }
        set { PlayerPrefs.SetInt("SpeedLevel", value); }
    }

    // "IncomeLevel" ad�ndaki gelir d��mesi seviyesi de�i�kenini getirir veya ayarlar
    public static int IncomeButtonLevel
    {
        get { return PlayerPrefs.GetInt("IncomeLevel", 0); }
        set { PlayerPrefs.SetInt("IncomeLevel", value); }
    }

    // "AddPipeLevel" ad�ndaki boru ekleme d��mesi seviyesi de�i�kenini getirir veya ayarlar
    public static int AddPipeButtonLevel
    {
        get { return PlayerPrefs.GetInt("AddPipeLevel", 0); }
        set { PlayerPrefs.SetInt("AddPipeLevel", value); }
    }

    // "CombineLevel" ad�ndaki birle�tirme d��mesi seviyesi de�i�kenini getirir veya ayarlar
    public static int CombineButtonLevel
    {
        get { return PlayerPrefs.GetInt("CombineLevel", 0); }
        set { PlayerPrefs.SetInt("CombineLevel", value); }
    }

    // "GPULevel" ad�ndaki GPU d��mesi seviyesi de�i�kenini getirir veya ayarlar
    public static int GPUButtonLevel
    {
        get { return PlayerPrefs.GetInt("GPULevel", 0); }
        set { PlayerPrefs.SetInt("GPULevel", value); }
    }

    // "TotalLevel" ad�ndaki toplam d��me seviyesi de�i�kenini getirir veya ayarlar
    public static int TotalButtonLevel
    {
        get { return PlayerPrefs.GetInt("TotalLevel", 0); }
        set { PlayerPrefs.SetInt("TotalLevel", value); }
    }

    // "GiftLevel" ad�ndaki hediye seviyesi de�i�kenini getirir veya ayarlar
    public static int GiftCashLevel
    {
        get { return PlayerPrefs.GetInt("GiftLevel", 0); }
        set { PlayerPrefs.SetInt("GiftLevel", value); }
    }
}