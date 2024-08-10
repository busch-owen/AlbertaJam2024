using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WireConnector[] connectors;
    [SerializeField] private WireConnector[] rightConnectors;

    private List<CorrespondingColor> availableColors = new();
    
    [SerializeField] private UnityEvent wiresCompleted;

    private void Awake()
    {
        RandomizeWires();
    }

    public void CheckWireCompletion()
    {
        var connectorsConnected = connectors.Count(connector => connector.ConnectedCorrectly);

        if (connectorsConnected == connectors.Length)
        {
            wiresCompleted.Invoke();
            Debug.Log("wires completed");
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
}
