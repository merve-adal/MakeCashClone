using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickedScript : MonoBehaviour
{
    public Transform huni; // Huni nesnesinin pozisyonunu belirlemek i�in kullan�lan Transform nesnesi.
    public GameObject[] spherify; // K�re nesnelerinin dizisi.
    public Renderer renderer; // Bu nesnenin Renderer bile�enine eri�mek i�in kullan�lan de�i�ken.

    public int PipeMultiplier; // Bu de�i�ken, borunun �arpan de�erini belirlemek i�in kullan�l�r.
    float Defaultcash = 5; // Bu de�i�ken, varsay�lan para miktar�n� tutar.
    public float multiplierCash; // Bu de�i�ken, �arpan hesaplamalar�ndan sonra elde edilen para miktar�n� tutar.

    GameManager gameManager; // GameManager nesnesine eri�mek i�in kullan�lan de�i�ken.

    // Bu metot, nesne etkinle�tirildi�inde GameManager.click ve GameManager.DefaultCash olaylar�na abone olur.
    private void OnEnable()
    {
        GameManager.click += Click;
        GameManager.DefaultCash += CheckDefaultCash;
    }

    // Bu metot, nesne olu�turuldu�unda GameManager.Instance �rne�ine eri�ilir ve CheckDefaultCash() metodu �a�r�l�r.
    private void Start()
    {
        gameManager = GameManager.Instance;
        CheckDefaultCash();
    }

    // Bu metot, GameManager.click olay� tetiklendi�inde �a�r�l�r.
    public void Click()
    {
        // �arpan hesaplamas� yap�l�r.
        multiplierCash = CashCalculation(Defaultcash);

        // Se�ilen para miktar� belirlenir.
        int selectCash = -1;
        switch (multiplierCash)
        {
            case < 100:
                selectCash = 0;
                break;
            case < 200:
                selectCash = 1;
                break;
            case < 1000:
                selectCash = 2;
                break;
            case < 1500:
                selectCash = 3;
                break;
            case < 2000:
                selectCash = 4;
                break;
            case < 3000:
                selectCash = 5;
                break;
            case < 70000:
                selectCash = 6;
                break;
            default:
                selectCash = -1;
                break;
        }

        // Havuzdan nesne al�n�r ve huni nesnesinin alt�na yerle�tirilir.
        var obj = GameManager.Instance.poolManager.GetPoolObject(selectCash);
        obj.transform.position = new Vector3(huni.transform.position.x, huni.transform.position.y - 0.2f, huni.transform.position.z);

        // Metin nesnesi olu�turulur ve huni nesnesinin �st�nde g�sterilir.
        var textObj = Instantiate(GameManager.Instance.text, transform);
        textObj.gameObject.GetComponentInChildren<CashText>().multiplier = multiplierCash;
        textObj.transform.position = new Vector3(huni.transform.position.x, huni.transform.position.y + 0.3f, huni.transform.position.z + 0.2f);

        // Oyuncu para miktar� art�r�l�r.
        gameManager.AddPlayerCash(multiplierCash);
    }

    // Bu metot, GameManager.DefaultCash olay� tetiklendi�inde �a�r�l�r ve Defaultcash de�eri g�ncellenir.
    void CheckDefaultCash()
    {
        Defaultcash = gameManager.StartCash;
    }

    // Bu metot, girilen para miktar�n� boru �arpan�yla �arparak hesaplay�p sonucu d�nd�r�r.
    float CashCalculation(float _cash)
    {
        _cash = _cash * PipeMultiplier;
        return _cash;
    }

    // Bu metot, nesne devre d��� b�rak�ld���nda GameManager.click ve GameManager.DefaultCash olaylar�ndan abonelikten ��kar.
    private void OnDisable()
    {
        GameManager.click -= Click;
        GameManager.DefaultCash -= CheckDefaultCash;
    }

    // Bu metot, nesnenin rengini de�i�tirmek i�in kullan�l�r.
    public void ChangeColor(Color _color)
    {
        renderer.material.color = _color;
    }
}