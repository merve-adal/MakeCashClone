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
        public GameObject GPUPrefab; // GPU için kullanýlacak prefab nesnesi.
        public Vector3 GPUPosition; // GPU'nun konumu.
    }

    [SerializeField] public GPU[] gPU; // GPU dizisi.

    public Color[] hotColor; // GPU'nun sýcaklýðýna göre belirlenmiþ renklerin dizisi.
    public int activedGPU; // Aktif GPU'nun indeksi.
    public GameObject GPUPrefab; // GPU için kullanýlacak prefab nesnesi.
    public Renderer rendererGPU; // GPU için kullanýlan Renderer bileþeni.

    public List<Color> gPUDefaultColor; // GPU'nun varsayýlan renkleri için kullanýlan liste.

    public bool ChangedColor = false; // GPU renginin deðiþip deðiþmediði.
    public bool HotValueBar = false; // GPU sýcaklýðýna göre çubukun hareket edip etmediði.
    bool opened = false;

    // Bu metot, GPUScript etkinleþtirildiðinde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylarýna abone olur.
    public void OnEnable()
    {
        GameManager.ChangeGPU += IncreaseActivedGPU;
        GameManager.GPUColorChange += ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }

    // Bu metot, nesne oluþturulduðunda GameManager.Instance örneðine eriþilir ve SetGPU() metodu çaðrýlýr.
    private void Start()
    {
        activedGPU = GameManager.Instance.GPUButtonLevel;
        SetGPU();
        SetGPURenderer();
    }

    // Bu metot her karede çaðrýlýr, ChangedColor deðiþkeni true olduðunda GPU'nun rengini deðiþtirir ve HotValueBar deðiþkenini true yapar.
    // ChangedColor deðiþkeni false olduðunda ise GPU'nun rengini varsayýlan renklerine döndürür ve HotValueBar deðiþkenini false yapar.
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

    // Bu metot, seçilen GPU prefabýný oluþturur ve gPUDefaultColor listesine varsayýlan renkleri ekler.
    public void SetGPU()
    {
        Destroy(GPUPrefab);
        GPUPrefab = GameManager.Instance.spawnScript.Spawn(gPU[activedGPU].GPUPrefab, this.gameObject, gPU[activedGPU].GPUPosition);
        StartCoroutine(CheckGPU());
    }

    // Bu metot, GPU prefabýný oluþturulduktan sonra 1 saniye bekleyip GPU'nun varsayýlan renklerini gPUDefaultColor listesine ekler.
    IEnumerator CheckGPU()
    {
        yield return new WaitForSeconds(1f);
        foreach (var _gpu in GPUPrefab.GetComponentsInChildren<Renderer>())
        {
            gPUDefaultColor.Add(_gpu.material.color);
        }
    }

    // Bu metot, aktif GPU'nun indeksini artýrýr ve SetGPU() metodu çaðrýlýr.
    public void IncreaseActivedGPU()
    {
        activedGPU++;
        SetGPU();
    }

    // Bu metot, GPU renginin deðiþip deðiþmediðini belirleyen ChangedColor deðiþkenini günceller.
    public void ChangeColor(bool _actived)
    {
        ChangedColor = _actived;
    }

    // Bu metot, gPUDefaultColor listesini temizler.
    public void GPUDefaultColorClear()
    {
        gPUDefaultColor.Clear();
    }

    // Bu metot, GPUPrefab nesnesinin Renderer bileþenini rendererGPU deðiþkenine atar.
    public void SetGPURenderer()
    {
        rendererGPU = GPUPrefab.GetComponent<Renderer>();
    }

    // Bu metot, GPUScript etkisizleþtirildiðinde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylarýndan abonelikleri kaldýrýr.
    private void OnDisable()
    {
        GameManager.ChangeGPU -= IncreaseActivedGPU;
        GameManager.GPUColorChange -= ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }
}