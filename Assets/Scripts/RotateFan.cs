using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour
{
    public Vector3 rotation; // Fanýn dönme hýzý ve yönü
    public float speed; // Fanýn dönme hýzý (derece/sn)

    // Her frame'de çaðrýlýr, fanýn dönüþ hareketini gerçekleþtirir
    void Update()
    {
        // Faný belirtilen hýzda ve belirtilen yönde döndürür. Time.deltaTime, her frame arasýndaki zaman farkýný hesaplamak için kullanýlýr.
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
}