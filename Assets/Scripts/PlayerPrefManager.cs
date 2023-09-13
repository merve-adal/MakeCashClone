using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    // "PlayerCash" adýndaki oyuncu parasý deðiþkenini getirir veya ayarlar
    public static float PlayerCash
    {
        get { return PlayerPrefs.GetFloat("PlayerCash", 0f); }
        set { PlayerPrefs.SetFloat("PlayerCash", value); }
    }

    // "GiftCash" adýndaki hediye parasý deðiþkenini getirir veya ayarlar
    public static float GiftCash
    {
        get { return PlayerPrefs.GetFloat("GiftCash", 0f); }
        set { PlayerPrefs.SetFloat("GiftCash", value); }
    }

    // "SpeedLevel" adýndaki hýz düðmesi seviyesi deðiþkenini getirir veya ayarlar
    public static int SpeedButtonLevel
    {
        get { return PlayerPrefs.GetInt("SpeedLevel", 0); }
        set { PlayerPrefs.SetInt("SpeedLevel", value); }
    }

    // "IncomeLevel" adýndaki gelir düðmesi seviyesi deðiþkenini getirir veya ayarlar
    public static int IncomeButtonLevel
    {
        get { return PlayerPrefs.GetInt("IncomeLevel", 0); }
        set { PlayerPrefs.SetInt("IncomeLevel", value); }
    }

    // "AddPipeLevel" adýndaki boru ekleme düðmesi seviyesi deðiþkenini getirir veya ayarlar
    public static int AddPipeButtonLevel
    {
        get { return PlayerPrefs.GetInt("AddPipeLevel", 0); }
        set { PlayerPrefs.SetInt("AddPipeLevel", value); }
    }

    // "CombineLevel" adýndaki birleþtirme düðmesi seviyesi deðiþkenini getirir veya ayarlar
    public static int CombineButtonLevel
    {
        get { return PlayerPrefs.GetInt("CombineLevel", 0); }
        set { PlayerPrefs.SetInt("CombineLevel", value); }
    }

    // "GPULevel" adýndaki GPU düðmesi seviyesi deðiþkenini getirir veya ayarlar
    public static int GPUButtonLevel
    {
        get { return PlayerPrefs.GetInt("GPULevel", 0); }
        set { PlayerPrefs.SetInt("GPULevel", value); }
    }

    // "TotalLevel" adýndaki toplam düðme seviyesi deðiþkenini getirir veya ayarlar
    public static int TotalButtonLevel
    {
        get { return PlayerPrefs.GetInt("TotalLevel", 0); }
        set { PlayerPrefs.SetInt("TotalLevel", value); }
    }

    // "GiftLevel" adýndaki hediye seviyesi deðiþkenini getirir veya ayarlar
    public static int GiftCashLevel
    {
        get { return PlayerPrefs.GetInt("GiftLevel", 0); }
        set { PlayerPrefs.SetInt("GiftLevel", value); }
    }
}