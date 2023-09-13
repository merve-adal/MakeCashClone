using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    // Bu fonksiyon prefabý, parent nesnesini ve pozisyonunu parametre olarak alýr ve bu parametrelere göre bir nesne yaratýr. Yaratýlan nesneyi döndürür.
    public GameObject Spawn(GameObject _prefab, GameObject _parentObject, Vector3 _position)
    {
        var _object = Instantiate(_prefab, _parentObject.transform);
        _object.transform.position = _position;
        return _object;
    }

    // Bu fonksiyon prefabý ve parent nesnesini parametre olarak alýr ve bu parametrelere göre bir nesne yaratýr. Yaratýlan nesneyi döndürür.
    public GameObject Spawn(GameObject _prefab, GameObject _parentObject)
    {
        var _object = Instantiate(_prefab, _parentObject.transform);
        return _object;
    }

    // Bu fonksiyon sadece prefabý parametre olarak alýr ve bu parametreye göre bir nesne yaratýr. Yaratýlan nesneyi döndürür.
    public GameObject Spawn(GameObject _prefab)
    {
        var _object = Instantiate(_prefab);
        return _object;
    }
}