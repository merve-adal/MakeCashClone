using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GPUScript : MonoBehaviour
{
    [Serializable]
    public struct GPU
    {
        public GameObject GPUPrefab; // GPU i�in kullan�lacak prefab nesnesi.
        public Vector3 GPUPosition; // GPU'nun konumu.
    }

    [SerializeField] public GPU[] gPU; // GPU dizisi.

    public Color[] hotColor; // GPU'nun s�cakl���na g�re belirlenmi� renklerin dizisi.
    public int activedGPU; // Aktif GPU'nun indeksi.
    public GameObject GPUPrefab; // GPU i�in kullan�lacak prefab nesnesi.
    public Renderer rendererGPU; // GPU i�in kullan�lan Renderer bile�eni.

    public List<Color> gPUDefaultColor; // GPU'nun varsay�lan renkleri i�in kullan�lan liste.

    public bool ChangedColor = false; // GPU renginin de�i�ip de�i�medi�i.
    public bool HotValueBar = false; // GPU s�cakl���na g�re �ubukun hareket edip etmedi�i.
    bool opened = false;

    // Bu metot, GPUScript etkinle�tirildi�inde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylar�na abone olur.
    public void OnEnable()
    {
        GameManager.ChangeGPU += IncreaseActivedGPU;
        GameManager.GPUColorChange += ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }

    // Bu metot, nesne olu�turuldu�unda GameManager.Instance �rne�ine eri�ilir ve SetGPU() metodu �a�r�l�r.
    private void Start()
    {
        activedGPU = GameManager.Instance.GPUButtonLevel;
        SetGPU();
        SetGPURenderer();
    }

    // Bu metot her karede �a�r�l�r, ChangedColor de�i�keni true oldu�unda GPU'nun rengini de�i�tirir ve HotValueBar de�i�kenini true yapar.
    // ChangedColor de�i�keni false oldu�unda ise GPU'nun rengini varsay�lan renklerine d�nd�r�r ve HotValueBar de�i�kenini false yapar.
    private void Update()
    {
        if (ChangedColor)
        {
            foreach (var _gpu in GPUPrefab.GetComponentsInChildren<Renderer>())
            {
                _gpu.material.color = Color.Lerp(_gpu.material.color, Color.red, Time.deltaTime);
            }
            HotValueBar = true;
        }
        else if (!ChangedColor && HotValueBar)
        {
            int i = 0;
            foreach (var _gpu in GPUPrefab.GetComponentsInChildren<Renderer>())
            {
                Color _color = gPUDefaultColor[i];
                _gpu.material.color = Color.Lerp(_gpu.material.color, _color, Time.deltaTime);
                i++;
            }
        }
    }

    // Bu metot, se�ilen GPU prefab�n� olu�turur ve gPUDefaultColor listesine varsay�lan renkleri ekler.
    public void SetGPU()
    {
        Destroy(GPUPrefab);
        GPUPrefab = GameManager.Instance.spawnScript.Spawn(gPU[activedGPU].GPUPrefab, this.gameObject, gPU[activedGPU].GPUPosition);
        StartCoroutine(CheckGPU());
    }

    // Bu metot, GPU prefab�n� olu�turulduktan sonra 1 saniye bekleyip GPU'nun varsay�lan renklerini gPUDefaultColor listesine ekler.
    IEnumerator CheckGPU()
    {
        yield return new WaitForSeconds(1f);
        foreach (var _gpu in GPUPrefab.GetComponentsInChildren<Renderer>())
        {
            gPUDefaultColor.Add(_gpu.material.color);
        }
    }

    // Bu metot, aktif GPU'nun indeksini art�r�r ve SetGPU() metodu �a�r�l�r.
    public void IncreaseActivedGPU()
    {
        activedGPU++;
        SetGPU();
    }

    // Bu metot, GPU renginin de�i�ip de�i�medi�ini belirleyen ChangedColor de�i�kenini g�nceller.
    public void ChangeColor(bool _actived)
    {
        ChangedColor = _actived;
    }

    // Bu metot, gPUDefaultColor listesini temizler.
    public void GPUDefaultColorClear()
    {
        gPUDefaultColor.Clear();
    }

    // Bu metot, GPUPrefab nesnesinin Renderer bile�enini rendererGPU de�i�kenine atar.
    public void SetGPURenderer()
    {
        rendererGPU = GPUPrefab.GetComponent<Renderer>();
    }

    // Bu metot, GPUScript etkisizle�tirildi�inde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylar�ndan abonelikleri kald�r�r.
    private void OnDisable()
    {
        GameManager.ChangeGPU -= IncreaseActivedGPU;
        GameManager.GPUColorChange -= ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }
}