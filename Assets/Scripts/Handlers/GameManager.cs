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
    
    [SerializeField] private UnityEvent wiresCompleted;
    [SerializeField] private UnityEvent fusesCompleted;
    [SerializeField] private UnityEvent gameStarted;

    [SerializeField] private int minTime, maxTime;

    private void Awake()
    {
        RandomizeWires();
        RandomizeBrokenFuse();

        wiresCompleted.AddListener(CheckBothSystems);
        fusesCompleted.AddListener(CheckBothSystems);
    }

    public void CheckWireCompletion()
    {
        var connectorsConnected = connectors.Count(connector => connector.ConnectedCorrectly);

        if (connectorsConnected == connectors.Length)
        {
            wiresCompleted.Invoke();
            _wiresFixed = true;
        }
    }

    private IEnumerator RandomMalfunction()
    {
        while (true)
        {
            var timeUntilNextMalfunction = Random.Range(minTime, maxTime);
            Debug.Log(timeUntilNextMalfunction);
            yield return new WaitForSeconds(timeUntilNextMalfunction);
            var randEvent = Random.Range(0, 2);
            switch (randEvent)
            {
                case 0:
                {
                    RandomizeWires();
                    break;
                }
                case 1:
                {
                    RandomizeBrokenFuse();
                    break;
                }
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

        foreach (var connector in rightConnectors)
        {
            var randColor = Random.Range(0, availableColors.Count);
            
            connector.ChangeColor(availableColors[randColor]);
            availableColors.Remove(availableColors[randColor]);
        }
    }
    
    public void CheckFuseCompletion()
    {
        //Check for fuse completion here
        var fusesFixed = fuses.Count(fuse => !fuse.IsBroken);

        if (fusesFixed == fuses.Length)
        {
            fusesCompleted.Invoke();
            _fusesFixed = true;
        }
    }

    private void CheckBothSystems()
    {
        if (_fusesFixed && _wiresFixed && !_gameStarted)
        {
            StartCoroutine(RandomMalfunction());
            gameStarted.Invoke();
            _gameStarted = true;
        }
    }

    public void RandomizeBrokenFuse()
    {
        _fusesFixed = false;
        var randFuse = Random.Range(0, fuses.Length);
        fuses[randFuse].BreakFuse();
    }
}
