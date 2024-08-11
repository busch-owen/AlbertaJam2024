using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WireConnector[] connectors;
    [SerializeField] private WireConnector[] rightConnectors;

    [SerializeField] private Fuse[] fuses;

    private List<CorrespondingColor> availableColors = new();

    private bool _fusesFixed = false;
    private bool _wiresFixed = false;

    private bool _gameStarted;

    private Coroutine _timeoutCoroutine;
    
    private PlayerEventManager _eventManager;
    
    [SerializeField] private UnityEvent wiresCompleted;
    [SerializeField] private UnityEvent fusesCompleted;
    [SerializeField] public UnityEvent gameStarted;

    private WiresHandler _wiresHandler;
    private FusesHandler _fusesHandler;

    [SerializeField] private float timeoutTime = 10f;
    private bool _timeoutStarted;

    private InputHandler _inputHandler;
    
    [SerializeField] private int minTime, maxTime;

    private void Start()
    {
        _wiresHandler = FindObjectOfType<WiresHandler>();
        _fusesHandler = FindObjectOfType<FusesHandler>();
        _inputHandler = FindObjectOfType<InputHandler>();
        
        RandomizeWires();
        RandomizeBrokenFuse();
        wiresCompleted.AddListener(CheckBothSystems);
        fusesCompleted.AddListener(CheckBothSystems);
        _eventManager = FindObjectOfType<PlayerEventManager>();
    }

    public void CheckWireCompletion()
    {
        if (_wiresFixed) return;
        
        var connectorsConnected = connectors.Count(connector => connector.ConnectedCorrectly);

        if (connectorsConnected == connectors.Length)
        {
            _wiresFixed = true;
            wiresCompleted.Invoke();
            _wiresHandler.Sparks.Stop();
        }
    }

    private IEnumerator RandomMalfunction()
    {
        var timeUntilNextMalfunction = Random.Range(minTime, maxTime);
        Debug.Log(timeUntilNextMalfunction);
        yield return new WaitForSeconds(timeUntilNextMalfunction);
        if (_gameStarted)
        {
            if (!_timeoutStarted)
                _timeoutCoroutine = StartCoroutine(Timeout());
        }

        var randEvent = Random.Range(0, 2);
        switch (randEvent)
        {
            case 0:
            {
                RandomizeWires();
                _wiresHandler.Sparks.Play();
                _eventManager.RunRandom();
                Debug.Log("rand");
                AudioManager.Instance.PlaySFX("sSnip");
                AudioManager.Instance.PlayMusic("musSesame");
                break;
            }
            case 1:
            {
                RandomizeBrokenFuse();
                _fusesHandler.Sparks.Play();
                _eventManager.RunRandom();
                Debug.Log("rand");
                AudioManager.Instance.PlaySFX("sSnip");
                AudioManager.Instance.PlayMusic("musSesame");
                break;
            }

        }
    }

    private void ResetColorList()
    {
        availableColors.Add(CorrespondingColor.Red);
        availableColors.Add(CorrespondingColor.Blue);
        availableColors.Add(CorrespondingColor.Yellow);
    }
    
    public void RandomizeWires()
    {
        ResetColorList();
        _wiresFixed = false;
        foreach (var connector in connectors)
        {
            connector.ResetConnections();
        }
        int whichLight = 0;
        foreach (var connector in rightConnectors)
        {
            
            var randColor = Random.Range(0, availableColors.Count);
            
            connector.ChangeColor(availableColors[randColor]);
            _wiresHandler.LightRenderers[whichLight].material = _wiresHandler.LightMaterials[availableColors[randColor].GetHashCode()];
            availableColors.Remove(availableColors[randColor]);
            whichLight++;
        }
        _wiresHandler.ResetWirePositions();
    }
    
    public void CheckFuseCompletion()
    {
        if (_fusesFixed) return;
        //Check for fuse completion here
        _fusesHandler.Sparks.Stop();
        _fusesFixed = true;
        fusesCompleted.Invoke();
    }

    private void CheckBothSystems()
    {
        if (_fusesFixed && _wiresFixed)
        {
            StartCoroutine(RandomMalfunction());
            _timeoutStarted = false;
            if(_timeoutCoroutine != null)
            {
                StopCoroutine(_timeoutCoroutine);
                _timeoutCoroutine = null;
            }
            gameStarted.Invoke();
            _gameStarted = true;
            AudioManager.Instance.PlayMusic("musLoop");
        }
    }

    private IEnumerator Timeout()
    {
        _timeoutStarted = true;
        yield return new WaitForSeconds(timeoutTime);
        _eventManager.RunPlayerDeath();
        _inputHandler.DisableInput();
        _timeoutStarted = false;
    }

    public void RandomizeBrokenFuse()
    {
        _fusesFixed = false;
        var randFuse = Random.Range(0, fuses.Length);
        fuses[randFuse].BreakFuse();
    }
}
