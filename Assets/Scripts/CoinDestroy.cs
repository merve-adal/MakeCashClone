using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour
{
    public int ObjectType; // Bu deðiþken, objenin hangi tip havuz objesi olduðunu belirlemek için kullanýlýr.

    // Bu metot, nesne etkinleþtirildiðinde çalýþtýrýlýr.
    private void OnEnable()
    {
        // CoinDestroyer adlý bir IEnumerator (yinelemeli) iþlem baþlatýlýr ve bu iþlem bitene kadar beklenir.
        StartCoroutine(CoinDestroyer());
    }

    // Bu IEnumerator (yinelemeli) iþlem, önce 2 saniye bekler ve daha sonra GameManager.Instance.poolManager.SetPoolObject() metodunu çaðýrarak nesneyi havuza geri gönderir.
    IEnumerator CoinDestroyer()
    {
        yield return new WaitForSeconds(2);

        GameManager.Instance.poolManager.SetPoolObject(gameObject, ObjectType);
    }
}