using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour
{
    public Vector3 rotation; // Fan�n d�nme h�z� ve y�n�
    public float speed; // Fan�n d�nme h�z� (derece/sn)

    // Her frame'de �a�r�l�r, fan�n d�n�� hareketini ger�ekle�tirir
    void Update()
    {
        // Fan� belirtilen h�zda ve belirtilen y�nde d�nd�r�r. Time.deltaTime, her frame aras�ndaki zaman fark�n� hesaplamak i�in kullan�l�r.
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
}