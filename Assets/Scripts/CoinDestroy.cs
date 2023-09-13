using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour
{
    public int ObjectType; // Bu de�i�ken, objenin hangi tip havuz objesi oldu�unu belirlemek i�in kullan�l�r.

    // Bu metot, nesne etkinle�tirildi�inde �al��t�r�l�r.
    private void OnEnable()
    {
        // CoinDestroyer adl� bir IEnumerator (yinelemeli) i�lem ba�lat�l�r ve bu i�lem bitene kadar beklenir.
        StartCoroutine(CoinDestroyer());
    }

    // Bu IEnumerator (yinelemeli) i�lem, �nce 2 saniye bekler ve daha sonra GameManager.Instance.poolManager.SetPoolObject() metodunu �a��rarak nesneyi havuza geri g�nderir.
    IEnumerator CoinDestroyer()
    {
        yield return new WaitForSeconds(2);

        GameManager.Instance.poolManager.SetPoolObject(gameObject, ObjectType);
    }
}