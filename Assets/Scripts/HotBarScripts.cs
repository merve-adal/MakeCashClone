using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarScripts : MonoBehaviour
{
    public Slider hotBar; // Slider nesnesi referansý

    public int hotBarValue = 0; // Sýcaklýk çubuðu deðeri
    public float speed; // Çubuðun deðerinin deðiþme hýzý
    int increaseInt = 10; // Sýcaklýk deðerinin arttýrýlacak miktarý
    bool hotValueGPU = false; // Sýcaklýk deðerinin GPU'yu etkileyip etkilemediðini belirten bayrak

    // Abone olunacak GameManager olaylarý
    private void OnEnable()
    {
        GameManager.IncreaseHotBar += IncreaseHotBar;
        GameManager.DroppedHotBar += DroppingHotBar;
    }

    // Baþlangýçta çalýþacak kod bloðu
    void Start()
    {
        hotBar = gameObject.GetComponent<Slider>(); // Slider nesnesine referans alýnacak oyun nesnesi atanýyor
        hotBar.maxValue = 100; // Sýcaklýk çubuðunun maksimum deðeri
        hotBar.value = hotBarValue; // Baþlangýçta sýcaklýk çubuðunun deðeri
    }

    // Her karede çalýþacak kod bloðu
    private void Update()
    {
        // Eðer sýcaklýk çubuðu deðeri deðiþtiyse güncelleme yapýlacak
        if (hotBarValue != hotBar.value)
        {
            SetHotBar();
        }

        // Sýcaklýk çubuðu deðerine göre GameManager'deki olaylar tetiklenecek
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

    // Sýcaklýk çubuðu deðerinin arttýrýlmasý için çaðrýlan metot
    public void IncreaseHotBar()
    {
        if (hotBarValue <= hotBar.maxValue)
        {
            hotBarValue += 10;
        }
    }

    // Sýcaklýk çubuðu deðerinin azaltýlmasý için çaðrýlan metot
    public void DroppingHotBar()
    {
        if (hotBarValue >= 0)
        {
            hotBarValue -= 10;
        }
    }

    // Sýcaklýk çubuðu deðerinin görsel olarak güncellenmesi
    void SetHotBar()
    {
        float _speed = speed;
        float _change = hotBarValue - hotBar.value;

        // Deðer artýyorsa hýzý normal hale getir
        if (_change > 0)
        {
            _speed *= 1f;
        }
        // Deðer azalýyorsa hýzý artýr
        else
        {
            _speed *= 5f;
        }

        // Slider deðeri, hedef deðere doðru zamanla deðiþecek
        hotBar.value += _change * Time.deltaTime * _speed;
    }

    // Bu nesne devre dýþý býrakýldýðýnda, GameManager olaylarýna abonelikten çýkýlacak
    private void OnDisable()
    {
        GameManager.IncreaseHotBar -= IncreaseHotBar;
        GameManager.DroppedHotBar -= DroppingHotBar;
    }
}