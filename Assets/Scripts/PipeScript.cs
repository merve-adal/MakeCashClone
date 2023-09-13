using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PipeScript : MonoBehaviour
{
    public List<GameObject> pipeList = new List<GameObject>();
    public List<Color> DefaultPipeColor = new List<Color>();
    

    public int openedPipe;

    public int[] MultiplierPipes;

    #region CombinePipe
    public int[] activedPipe;
    public int activedPipeSize;
    public int startPipeNumber;
    public int endPipeNumber;
    public bool CombinedPipe = false;
    #endregion

    bool switchColored = false;
    bool pipeNumberActived = false;
    private void OnEnable()
    {
        GameManager.AddPipe += AddPipe;
        GameManager.CombinePipe += CombinePipe;
        
    }
    private void Awake()
    {
        
        SearchPipe();
    }
    private void Start()
    {
        MultiplierPipes = new int[gameObject.GetComponentsInChildren<ClickedScript>().Length];
        activedPipe = new int[MultiplierPipes.Length];
        openedPipe = GameManager.Instance.ActivePipeSize(GameManager.Instance.AddPipeButtonLevel, GameManager.Instance.CombineButtonLevel);
        PipeMultiplierCheck(GameManager.Instance.CombineButtonLevel,openedPipe);
        ActivitionPipe(openedPipe,MultiplierPipes);
        GameManager.Instance.uIManager.CombineButtonActived(false);
        CheckPipe();
    }
    private void Update()
    {
        if (switchColored)
        {

            for (int i = 0; i < activedPipe.Length; i++)
            {
                Color _color = Color.yellow;
                switch (MultiplierPipes[i])
                {
                    case 4:
                        pipeList[i].GetComponent<ClickedScript>().ChangeColor(DefaultPipeColor[1]);
                        break;
                    case 16:
                        pipeList[i].GetComponent<ClickedScript>().ChangeColor(DefaultPipeColor[2]);
                        break;
                    case 64:
                        pipeList[i].GetComponent<ClickedScript>().ChangeColor(DefaultPipeColor[3]);
                        break;
                    default:
                        pipeList[i].GetComponent<ClickedScript>().ChangeColor(DefaultPipeColor[0]);
                        break;
                }
               
            }
            switchColored = false;
            CheckPipe();
        }
    }
    void SearchPipe()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Pipe")
            {
                pipeList.Add(child.gameObject);
            }
        }
    }

    void PipeMultiplierCheck(int _combinePipeLevel,int _activedPipe)
    {
        int sixteenMultiplier, sixtyTwoMultiplier;
        sixteenMultiplier = _combinePipeLevel / 4;
        sixtyTwoMultiplier = _combinePipeLevel / 12;
        Debug.Log("OnAltý:"+sixteenMultiplier  + " AtmýþÝki"+sixtyTwoMultiplier);
        for (int i = 0; i < MultiplierPipes.Length; i++)
        {
            if (sixtyTwoMultiplier > 0 && sixtyTwoMultiplier > i)
            {
                MultiplierPipes[i] = 64;
                pipeList[i].GetComponent<ClickedScript>().ChangeColor(Color.blue);
                UIManager.HuniMultiplier(i,MultiplierPipes[i].ToString());
                _combinePipeLevel -= 12;
            }
            else if (sixteenMultiplier > 0 && sixteenMultiplier + sixtyTwoMultiplier >i)
            {
                MultiplierPipes[i] = 16;
                pipeList[i].GetComponent<ClickedScript>().ChangeColor(Color.red);
                UIManager.HuniMultiplier(i, MultiplierPipes[i].ToString());
                _combinePipeLevel -= 4;
            }
            else if (i < _combinePipeLevel) 
            {
                MultiplierPipes[i] = 4;
                pipeList[i].GetComponent<ClickedScript>().ChangeColor(Color.black);
                UIManager.HuniMultiplier(i, MultiplierPipes[i].ToString());
            }
            else { 
                MultiplierPipes[i] = 1;
                UIManager.HuniMultiplier(i, MultiplierPipes[i].ToString());
            }
            
        }
    }

    public void ActivitionPipe(int _activedPipeSize, int[] _multiplierPipes)
    {
        
        for (int i = 0; i < pipeList.Count; i++)
        {
            if (_activedPipeSize > i)
            {
                pipeList[i].SetActive(true);

                activedPipe[i] = _multiplierPipes[i];
            }
            else 
            {
                pipeList[i].SetActive(false);
                activedPipe[i] = 0;
            }
            
            pipeList[i].GetComponent<ClickedScript>().PipeMultiplier = _multiplierPipes[i];
        }
    }

    public void AddPipe()
    {
        openedPipe += 1;
        ActivitionPipe(openedPipe,MultiplierPipes);
        CheckPipe();
    }
    public void CombinePipe()
    {
        if (CombinedPipe)
        {
            
            MultiplierPipes[startPipeNumber] *= 4;
            UIManager.HuniMultiplier(startPipeNumber, MultiplierPipes[startPipeNumber].ToString());
            int i = activedPipeSize - endPipeNumber;
            if (i > 0)
            {
                openedPipe = startPipeNumber + i;
            }
            else
            {
                openedPipe = startPipeNumber + 1;
            }
            for (int j = 0; j < activedPipe.Length; j++)
            {
                if (startPipeNumber < j && endPipeNumber >=j)
                {
                    MultiplierPipes[j] = 1;
                    activedPipe[j] = 0;
                }
            }
            ActivitionPipe(openedPipe,MultiplierPipes);
            CombinedPipe = false;
            startPipeNumber = 0;
            endPipeNumber = 0;
            
            switchColored = true;
            CheckPipe();
        }
        
    }
    public void CheckPipe()
    {
        int _pipe=0;
        int _activedPipe=0;
        for (int i = 0; i < pipeList.Count-1; i++)
        {
            for (int j = i + 1; j < pipeList.Count; j++)
            {
                if (j <= pipeList.Count && !pipeNumberActived)
                {
                    
                    if (activedPipe[i] == activedPipe[j] && _pipe < 4 && activedPipe[i] != 0  && activedPipe[j] != 0)
                    {
                        _pipe += 1;
                        endPipeNumber = j;
                    }
                    if (_pipe == 2)
                    {
                        pipeNumberActived = true;
                        CombinedPipe = true;
                        GameManager.Instance.uIManager.CombineButtonActived(true);
                        startPipeNumber = i;
                        break;
                    }
                    
                }
                

            }
            if (activedPipe[i] != 0)
            {
                _activedPipe += 1;
            }
            _pipe = 0;
        }
        
        activedPipeSize=_activedPipe;
        pipeNumberActived = false;
    }
    
    private void OnDisable()
    {
        GameManager.AddPipe -= AddPipe;
        GameManager.CombinePipe -= CombinePipe;
        
    }
}
