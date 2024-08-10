using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WireConnector[] connectors;

    public bool AllWiresConnected()
    {
        var connectorsConnected = connectors.Count(connector => connector.ConnectedCorrectly);

        return connectorsConnected == connectors.Length;
    }
}
