using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CashText : MonoBehaviour
{
    public float multiplier = 1; // �arpan de�eri

    void Start()
    {
        // Metin kutusunun i�eri�i �arpan de�erine ayarla
        gameObject.GetComponent<TextMeshProUGUI>().text = multiplier.ToString();

        // Metin kutusunu yukar� do�ru hareket ettir
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z), 1f);

        // Metin kutusunu 1 saniye sonra yok et
        Destroy(gameObject, 1);
    }
}