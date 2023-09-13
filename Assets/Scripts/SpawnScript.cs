using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    // Bu fonksiyon prefab�, parent nesnesini ve pozisyonunu parametre olarak al�r ve bu parametrelere g�re bir nesne yarat�r. Yarat�lan nesneyi d�nd�r�r.
    public GameObject Spawn(GameObject _prefab, GameObject _parentObject, Vector3 _position)
    {
        var _object = Instantiate(_prefab, _parentObject.transform);
        _object.transform.position = _position;
        return _object;
    }

    // Bu fonksiyon prefab� ve parent nesnesini parametre olarak al�r ve bu parametrelere g�re bir nesne yarat�r. Yarat�lan nesneyi d�nd�r�r.
    public GameObject Spawn(GameObject _prefab, GameObject _parentObject)
    {
        var _object = Instantiate(_prefab, _parentObject.transform);
        return _object;
    }

    // Bu fonksiyon sadece prefab� parametre olarak al�r ve bu parametreye g�re bir nesne yarat�r. Yarat�lan nesneyi d�nd�r�r.
    public GameObject Spawn(GameObject _prefab)
    {
        var _object = Instantiate(_prefab);
        return _object;
    }
}