using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickedScript : MonoBehaviour
{
    public Transform huni; // Huni nesnesinin pozisyonunu belirlemek için kullanýlan Transform nesnesi.
    public GameObject[] spherify; // Küre nesnelerinin dizisi.
    public Renderer renderer; // Bu nesnenin Renderer bileþenine eriþmek için kullanýlan deðiþken.

    public int PipeMultiplier; // Bu deðiþken, borunun çarpan deðerini belirlemek için kullanýlýr.
    float Defaultcash = 5; // Bu deðiþken, varsayýlan para miktarýný tutar.
    public float multiplierCash; // Bu deðiþken, çarpan hesaplamalarýndan sonra elde edilen para miktarýný tutar.

    GameManager gameManager; // GameManager nesnesine eriþmek için kullanýlan deðiþken.

    // Bu metot, nesne etkinleþtirildiðinde GameManager.click ve GameManager.DefaultCash olaylarýna abone olur.
    private void OnEnable()
    {
        GameManager.click += Click;
        GameManager.DefaultCash += CheckDefaultCash;
    }

    // Bu metot, nesne oluþturulduðunda GameManager.Instance örneðine eriþilir ve CheckDefaultCash() metodu çaðrýlýr.
    private void Start()
    {
        gameManager = GameManager.Instance;
        CheckDefaultCash();
    }

    // Bu metot, GameManager.click olayý tetiklendiðinde çaðrýlýr.
    public void Click()
    {
        // Çarpan hesaplamasý yapýlýr.
        multiplierCash = CashCalculation(Defaultcash);

        // Seçilen para miktarý belirlenir.
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

        // Havuzdan nesne alýnýr ve huni nesnesinin altýna yerleþtirilir.
        var obj = GameManager.Instance.poolManager.GetPoolObject(selectCash);
        obj.transform.position = new Vector3(huni.transform.position.x, huni.transform.position.y - 0.2f, huni.transform.position.z);

        // Metin nesnesi oluþturulur ve huni nesnesinin üstünde gösterilir.
        var textObj = Instantiate(GameManager.Instance.text, transform);
        textObj.gameObject.GetComponentInChildren<CashText>().multiplier = multiplierCash;
        textObj.transform.position = new Vector3(huni.transform.position.x, huni.transform.position.y + 0.3f, huni.transform.position.z + 0.2f);

        // Oyuncu para miktarý artýrýlýr.
        gameManager.AddPlayerCash(multiplierCash);
    }

    // Bu metot, GameManager.DefaultCash olayý tetiklendiðinde çaðrýlýr ve Defaultcash deðeri güncellenir.
    void CheckDefaultCash()
    {
        Defaultcash = gameManager.StartCash;
    }

    // Bu metot, girilen para miktarýný boru çarpanýyla çarparak hesaplayýp sonucu döndürür.
    float CashCalculation(float _cash)
    {
        _cash = _cash * PipeMultiplier;
        return _cash;
    }

    // Bu metot, nesne devre dýþý býrakýldýðýnda GameManager.click ve GameManager.DefaultCash olaylarýndan abonelikten çýkar.
    private void OnDisable()
    {
        GameManager.click -= Click;
        GameManager.DefaultCash -= CheckDefaultCash;
    }

    // Bu metot, nesnenin rengini deðiþtirmek için kullanýlýr.
    public void ChangeColor(Color _color)
    {
        renderer.material.color = _color;
    }
}