using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObstacleManager : NetworkBehaviour
{
    //private NetworkVariable<string> _mapType = new NetworkVariable<string>();
    //private const string _initialValue = "Type1";

    //public override void OnNetworkSpawn()
    //{
    //    if (IsServer)
    //    {
    //        _mapType.Value = "Type" + Random.Range(1, 5);
    //        Debug.Log(_mapType.Value);
    //    }
    //}

    private NetworkVariable<ObstacleData> _netState = new NetworkVariable<ObstacleData>(writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            TransmitState();
        }
        
        ConsumeState();

    }

    #region Transmit State

    private void TransmitState()
    {
        var state = new ObstacleData()
        {
            Map = "Type" + Random.Range(1, 5)
        };

        _netState.Value = state;
    }

    #endregion

    #region Get state

    private void ConsumeState()
    {
        DataManager.instance.map = _netState.Value.Map;

    }

    #endregion

    struct ObstacleData : INetworkSerializable
    {
        private string _map;

        internal string Map
        {
            get => _map;
            set => _map = value;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _map);
        }
    }
}
