using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarScripts : MonoBehaviour
{
    public Slider hotBar; // Slider nesnesi referans�

    public int hotBarValue = 0; // S�cakl�k �ubu�u de�eri
    public float speed; // �ubu�un de�erinin de�i�me h�z�
    int increaseInt = 10; // S�cakl�k de�erinin artt�r�lacak miktar�
    bool hotValueGPU = false; // S�cakl�k de�erinin GPU'yu etkileyip etkilemedi�ini belirten bayrak

    // Abone olunacak GameManager olaylar�
    private void OnEnable()
    {
        GameManager.IncreaseHotBar += IncreaseHotBar;
        GameManager.DroppedHotBar += DroppingHotBar;
    }

    // Ba�lang��ta �al��acak kod blo�u
    void Start()
    {
        hotBar = gameObject.GetComponent<Slider>(); // Slider nesnesine referans al�nacak oyun nesnesi atan�yor
        hotBar.maxValue = 100; // S�cakl�k �ubu�unun maksimum de�eri
        hotBar.value = hotBarValue; // Ba�lang��ta s�cakl�k �ubu�unun de�eri
    }

    // Her karede �al��acak kod blo�u
    private void Update()
    {
        // E�er s�cakl�k �ubu�u de�eri de�i�tiyse g�ncelleme yap�lacak
        if (hotBarValue != hotBar.value)
        {
            SetHotBar();
        }

        // S�cakl�k �ubu�u de�erine g�re GameManager'deki olaylar tetiklenecek
        switch (hotBar.value)
        {
            case < 31:
                GameManager.Instance.Touched(true);
                break;
            case < 71:
                GameManager.GPUColorChange(false);
                break;
            case < 81:
                GameManager.Instance.BurnGPU(false);
                GameManager.GPUColorChange(true);
                break;
            case < 91:
                GameManager.Instance.BurnGPU(true);
                break;
            case < 99:
                GameManager.Instance.LightningGpu(false);
                break;
            case < 110:
                GameManager.Instance.BurnGPU(false);
                GameManager.Instance.Touched(false);
                GameManager.Instance.LightningGpu(true);
                break;
        }
    }

    // S�cakl�k �ubu�u de�erinin artt�r�lmas� i�in �a�r�lan metot
    public void IncreaseHotBar()
    {
        if (hotBarValue <= hotBar.maxValue)
        {
            hotBarValue += 10;
        }
    }

    // S�cakl�k �ubu�u de�erinin azalt�lmas� i�in �a�r�lan metot
    public void DroppingHotBar()
    {
        if (hotBarValue >= 0)
        {
            hotBarValue -= 10;
        }
    }

    // S�cakl�k �ubu�u de�erinin g�rsel olarak g�ncellenmesi
    void SetHotBar()
    {
        float _speed = speed;
        float _change = hotBarValue - hotBar.value;

        // De�er art�yorsa h�z� normal hale getir
        if (_change > 0)
        {
            _speed *= 1f;
        }
        // De�er azal�yorsa h�z� art�r
        else
        {
            _speed *= 5f;
        }

        // Slider de�eri, hedef de�ere do�ru zamanla de�i�ecek
        hotBar.value += _change * Time.deltaTime * _speed;
    }

    // Bu nesne devre d��� b�rak�ld���nda, GameManager olaylar�na abonelikten ��k�lacak
    private void OnDisable()
    {
        GameManager.IncreaseHotBar -= IncreaseHotBar;
        GameManager.DroppedHotBar -= DroppingHotBar;
    }
}