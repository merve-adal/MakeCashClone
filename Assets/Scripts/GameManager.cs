using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using Dreamteck.Splines;
public class GameManager : MonoBehaviour
{
    #region Managers
    public static GameManager Instance;

    public PoolManager poolManager;
    public UIManager uIManager;

    #endregion
    #region Script
    public SpawnScript spawnScript;
    public GPUScript gPUScript;
    public PipeScript pipeScript;
    #endregion

    #region Property
    [SerializeField]private float _startCash;
    public float StartCash
    {
        get
        {
            return _startCash;
        }
        set
        {
            _startCash = value;
        }
    }

    [SerializeField] private float _playerCash;
    public float PlayerCash
    {
        get
        {
            return _playerCash;
        }
        set
        {
            _playerCash = value;
        }
    }
    [SerializeField] private float _waitCash;
    public float WaitCash
    {
        get
        {
            return _waitCash;
        }
        set
        {
            _waitCash = value;
        }
    }
    [SerializeField] private float _giftCash;
    public float GiftCash
    {
        get
        {
            return _giftCash;
        }
        set
        {
            _giftCash = value;
        }
    }

    #endregion

    #region Events
    public static Action click;
    public static Action AddPipe;
    public static Action DefaultCash;
    public static Action CombinePipe;
    public static Action DroppedHotBar;
    public static Action ChangeGPU;
    public static Action IncreaseHotBar;
    public static Action<bool> GPUColorChange;
    public static Action<float> SetGiftCash;
    public static Action<float> SetGiftCashMax;
    public static Action GPUColorClear;
    #endregion

    public GameObject Hotbar;
    public GameObject PlayerCashBar;
    public GameObject GPUButton;

    public Button SpeedButton;
    public Button IncomeButton;
    public Button AddPipeButton;

    bool endCash = false;
    public bool GPUActived = false;

    public int TotalButtonLevel = 1;
    public int SpeedButtonLevel = 0;
    public int IncomeButtonLevel = 0;
    public int AddPipeButtonLevel = 0;
    public int CombineButtonLevel =0;
    public int GPUButtonLevel = 0;
    public int GiftCashLevel = 0;

    public float SpeedButtonCash;
    public float AddPipeButtonCash;
    public float IncomeButtonCash;
    public float CombineButtonCash;
    public double _followDuration;
    private RaycastHit hitObject;
    float giftmax = 0;

    public GameObject burnParticle;
    public GameObject electricityParticle;
    public bool touched = true;
    public bool firstTouched = false;

    public GameObject text;

    float waitCash = 0;
    private void OnEnable()
    {
        Instance = this;
        PlayerCash = PlayerPrefManager.PlayerCash;
        SpeedButtonLevel = PlayerPrefManager.SpeedButtonLevel;
        IncomeButtonLevel = PlayerPrefManager.IncomeButtonLevel;
        AddPipeButtonLevel = PlayerPrefManager.AddPipeButtonLevel;
        CombineButtonLevel = PlayerPrefManager.CombineButtonLevel;
        GPUButtonLevel = PlayerPrefManager.GPUButtonLevel;
        TotalButtonLevel = PlayerPrefManager.TotalButtonLevel;
        GiftCash = PlayerPrefManager.GiftCash;
        GiftCashLevel = PlayerPrefManager.GiftCashLevel;
        GPUButton.GetComponent<Button>().enabled = false;
    }
    private void Start()
    {
        if (TotalButtonLevel == 0)
        {
            TotalButtonLevel++;
        }
        GPUActived = false;
        uIManager.GPUBarMax(20);
        uIManager.GPUBar(TotalButtonLevel);
        StartCash += IncomeButtonLevel * (IncomeButtonLevel * 0.5f);
        WaitCash -= (SpeedButtonLevel * 0.05f);
        //pipeScript.activedPipeSize = ActivePipeSize(AddPipeButtonLevel,CombineButtonLevel);
        GiftCashMax(GiftCashLevel);
        UIManager.SetPlayerCash(PlayerCash);
        UIManager.IncomeLevel(IncomeButtonLevel);
        SpeedButtonCash = ButtonCash(SpeedButtonLevel);
        AddPipeButtonCash = ButtonCash(AddPipeButtonLevel);
        IncomeButtonCash = ButtonCash(IncomeButtonLevel);
        CombineButtonCash = ButtonCash(CombineButtonLevel);
        UIManager.IncomeCash(IncomeButtonCash);
        UIManager.AddPipeCash(AddPipeButtonCash);
        UIManager.SpeedCash(SpeedButtonCash);
        UIManager.SpeedLevel(SpeedButtonLevel);
        UIManager.CombineCash(CombineButtonCash);
        SplineFollower.WaitCash(WaitCash);
    }
    public void GiftCashMax(int _level)
    {
        giftmax = 10000 * (_level + 1);
        SetGiftCashMax(giftmax);
    }
    public float ButtonCash(int _level)
    {
        _level++;
        float _cash = ((_level - 1)*5.25f) + (_level * 5.25f);

        return _cash;
    }
    public int ActivePipeSize(int _addPipe, int _combinePipe)
    {
        _addPipe += 1;
        if (_addPipe == 0 && _combinePipe == 0)
        {
            return 1;
        }
        else
        {
            
            int _activePipe,_total;
            int a, b;
            _total = _addPipe + _combinePipe;
            a = _total % 3;
            b = _total / 3;
            if (b != _combinePipe)
            {
                int i = (b - _combinePipe) * 3;
                _activePipe = i + a;

            }
            else { _activePipe = a; }
            return _activePipe;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && touched)
        {
               
            if (!firstTouched)
            {
                firstTouched = true;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitObject, 20f))
            {
                waitCash = WaitCash;   
                if (!IsPointOverUIObject() )
                {


                    Deform.Deformer.Play(true);
                   
                    if (endCash)
                    {
                        IncreaseHotBar();

                    }
                    else
                    {
                        StartCoroutine(SpawnCash());
                        SizeChange(Hotbar, new Vector3(1, 1, 1), 1);

                    }
                }
            }
        }
        else if (!endCash)
        {
            Deform.Deformer.Play(false);
            //SplineFollower.CashAnim(false);
            DroppedHotBar();
            SizeChange(Hotbar, new Vector3(0.75f, 0.75f, 0.75f), 1);
        }
        if (GiftCash >= giftmax)
        {
            GiftCash = 0;
            GiftCashLevel++;
            PlayerPrefManager.GiftCashLevel = GiftCashLevel;
            PlayerPrefManager.GiftCash = GiftCash;
            SetGiftCash(GiftCash);
            GiftCashMax(GiftCashLevel);
        }

        if (PlayerCash >= IncomeButtonCash)
        {
            IncomeButton.interactable = true;
        }
        else
        {
            IncomeButton.interactable = false;
        }
        if (PlayerCash >= SpeedButtonCash)
        {
            SpeedButton.interactable = true;
        }
        else
        {
            SpeedButton.interactable = false;
        }
        if (PlayerCash >= AddPipeButtonCash)
        {
            AddPipeButton.interactable = true;
        }
        else
        {
            AddPipeButton.interactable = false;
        }


        if (TotalButtonLevel % 20 == 0)
        {
            GPUActived = true;
            
        }
        else
        {
            GPUActived = false;
        }
        if (GPUActived)
        {
            GPUButton.GetComponent<Button>().enabled = true;
        }
    }
    private bool IsPointOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition,result);
        return result.Count > 0;

    }

   IEnumerator SpawnCash()
    {
        SplineFollower.CashAnim();
        endCash = true;
        yield return new WaitForSeconds(waitCash);
        endCash = false;
        click();
        SplineFollower.CashAnim();
        
    }
    public void SizeChange(GameObject _obje,Vector3 _size, float _speed)
    {
        _obje.transform.DOScale(_size, _speed);
    }

    public void AddPlayerCash(float _cash)
    {
        PlayerCash += _cash;
        GiftCash += _cash;
        SizeChange(PlayerCashBar, new Vector3(1,1,1),0.1f);
        PlayerCashBar.transform.DOScale(new Vector3(1,1,1),0.1f).OnComplete(() => { PlayerCashBar.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.1f); });
        UIManager.SetPlayerCash(PlayerCash);
        SetGiftCash(GiftCash);
        PlayerPrefManager.PlayerCash = PlayerCash;
        PlayerPrefManager.GiftCash = GiftCash;
    }

    public void SpeedButtonClicked()
    {
        WaitCash -= 0.1f;
        SpeedButtonLevel++;
        PlayerPrefManager.SpeedButtonLevel = SpeedButtonLevel;
        TotalButtonLevel++;
        PlayerPrefManager.TotalButtonLevel = TotalButtonLevel;
        PlayerCash -= SpeedButtonCash;
        SpeedButtonCash = ButtonCash(SpeedButtonLevel);
        UIManager.SpeedCash(SpeedButtonCash);
        UIManager.SpeedLevel(SpeedButtonLevel);
        uIManager.GPUBar(TotalButtonLevel);
        UIManager.SetPlayerCash(PlayerCash);
        PlayerPrefManager.PlayerCash = PlayerCash;
        SplineFollower.WaitCash(WaitCash);
    }
    public void IncomeButtonClicked()
    {
        float _cash = StartCash;
        StartCash = _cash + (IncomeButtonLevel + 0.5f);
        IncomeButtonLevel++;
        DefaultCash();
        PlayerPrefManager.IncomeButtonLevel = IncomeButtonLevel;
        TotalButtonLevel++;
        PlayerPrefManager.TotalButtonLevel = TotalButtonLevel;
        UIManager.IncomeLevel(IncomeButtonLevel);
        PlayerCash -= IncomeButtonCash;
        IncomeButtonCash = ButtonCash(IncomeButtonLevel);
        UIManager.IncomeCash(IncomeButtonCash);
        uIManager.GPUBar(TotalButtonLevel);
        UIManager.SetPlayerCash(PlayerCash);
        PlayerPrefManager.PlayerCash = PlayerCash;
    }
    public void AddPipeButtonClicked()
    {
        AddPipeButtonLevel++;
        AddPipe();
        PlayerPrefManager.AddPipeButtonLevel = AddPipeButtonLevel;
        PlayerCash -= AddPipeButtonCash;
        AddPipeButtonCash = ButtonCash(AddPipeButtonLevel);
        WaitCash += 0.5f;
        UIManager.AddPipeCash(AddPipeButtonCash);
        UIManager.SetPlayerCash(PlayerCash);
        PlayerPrefManager.PlayerCash = PlayerCash;
        SplineFollower.WaitCash(WaitCash);
    }
    public void CombineButtonClicked()
    {
        CombinePipe();
        CombineButtonLevel++;
        PlayerPrefManager.CombineButtonLevel = CombineButtonLevel;
        CombineButtonCash = ButtonCash(CombineButtonLevel);
        UIManager.CombineCash(CombineButtonCash);
        PlayerCash -= CombineButtonCash;
        UIManager.SetPlayerCash(PlayerCash);
        PlayerPrefManager.PlayerCash = PlayerCash;
        uIManager.CombineButtonActived(false);

    }
    public void NewGPUButton()
    {
        ChangeGPU();
        GPUButtonLevel++;
        PlayerPrefManager.GPUButtonLevel = GPUButtonLevel;
        GPUButton.GetComponent<Button>().enabled = false;
        GPUActived = false;
        TotalButtonLevel = 1;
        uIManager.GPUBar(0);
        GPUColorClear();
    }
    public void BurnGPU(bool actived)
    {
        burnParticle.SetActive(actived);
    }
    public void LightningGpu(bool actived)
    {
        electricityParticle.SetActive(actived);
    }
    public void Touched(bool actived)
    {
        touched = actived;
    }
}


