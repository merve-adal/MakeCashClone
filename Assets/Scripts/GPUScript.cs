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
        public GameObject GPUPrefab; // GPU için kullanılacak prefab nesnesi.
        public Vector3 GPUPosition; // GPU'nun konumu.
    }

    [SerializeField] public GPU[] gPU; // GPU dizisi.

    public Color[] hotColor; // GPU'nun sıcaklığına göre belirlenmiş renklerin dizisi.
    public int activedGPU; // Aktif GPU'nun indeksi.
    public GameObject GPUPrefab; // GPU için kullanılacak prefab nesnesi.
    public Renderer rendererGPU; // GPU için kullanılan Renderer bileşeni.

    public List<Color> gPUDefaultColor; // GPU'nun varsayılan renkleri için kullanılan liste.

    public bool ChangedColor = false; // GPU renginin değişip değişmediği.
    public bool HotValueBar = false; // GPU sıcaklığına göre çubukun hareket edip etmediği.
    bool opened = false;

    // Bu metot, GPUScript etkinleştirildiğinde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylarına abone olur.
    public void OnEnable()
    {
        GameManager.ChangeGPU += IncreaseActivedGPU;
        GameManager.GPUColorChange += ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }

    // Bu metot, nesne oluşturulduğunda GameManager.Instance örneğine erişilir ve SetGPU() metodu çağrılır.
    private void Start()
    {
        activedGPU = GameManager.Instance.GPUButtonLevel;
        SetGPU();
        SetGPURenderer();
    }

    // Bu metot her karede çağrılır, ChangedColor değişkeni true olduğunda GPU'nun rengini değiştirir ve HotValueBar değişkenini true yapar.
    // ChangedColor değişkeni false olduğunda ise GPU'nun rengini varsayılan renklerine döndürür ve HotValueBar değişkenini false yapar.
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

    // Bu metot, seçilen GPU prefabını oluşturur ve gPUDefaultColor listesine varsayılan renkleri ekler.
    public void SetGPU()
    {
        Destroy(GPUPrefab);
        GPUPrefab = GameManager.Instance.spawnScript.Spawn(gPU[activedGPU].GPUPrefab, this.gameObject, gPU[activedGPU].GPUPosition);
        StartCoroutine(CheckGPU());
    }

    // Bu metot, GPU prefabını oluşturulduktan sonra 1 saniye bekleyip GPU'nun varsayılan renklerini gPUDefaultColor listesine ekler.
    IEnumerator CheckGPU()
    {
        yield return new WaitForSeconds(1f);
        foreach (var _gpu in GPUPrefab.GetComponentsInChildren<Renderer>())
        {
            gPUDefaultColor.Add(_gpu.material.color);
        }
    }

    // Bu metot, aktif GPU'nun indeksini artırır ve SetGPU() metodu çağrılır.
    public void IncreaseActivedGPU()
    {
        activedGPU++;
        SetGPU();
    }

    // Bu metot, GPU renginin değişip değişmediğini belirleyen ChangedColor değişkenini günceller.
    public void ChangeColor(bool _actived)
    {
        ChangedColor = _actived;
    }

    // Bu metot, gPUDefaultColor listesini temizler.
    public void GPUDefaultColorClear()
    {
        gPUDefaultColor.Clear();
    }

    // Bu metot, GPUPrefab nesnesinin Renderer bileşenini rendererGPU değişkenine atar.
    public void SetGPURenderer()
    {
        rendererGPU = GPUPrefab.GetComponent<Renderer>();
    }

    // Bu metot, GPUScript etkisizleştirildiğinde GameManager.ChangeGPU, GameManager.GPUColorChange ve GameManager.GPUColorClear olaylarından abonelikleri kaldırır.
    private void OnDisable()
    {
        GameManager.ChangeGPU -= IncreaseActivedGPU;
        GameManager.GPUColorChange -= ChangeColor;
        GameManager.GPUColorClear += GPUDefaultColorClear;
    }
}