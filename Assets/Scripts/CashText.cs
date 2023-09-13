using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CashText : MonoBehaviour
{
    public float multiplier = 1; // Çarpan deðeri

    void Start()
    {
        // Metin kutusunun içeriði çarpan deðerine ayarla
        gameObject.GetComponent<TextMeshProUGUI>().text = multiplier.ToString();

        // Metin kutusunu yukarý doðru hareket ettir
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z), 1f);

        // Metin kutusunu 1 saniye sonra yok et
        Destroy(gameObject, 1);
    }
}