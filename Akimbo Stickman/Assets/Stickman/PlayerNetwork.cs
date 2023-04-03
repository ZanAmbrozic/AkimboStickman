using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<PlayerNetworkData> _netState = new NetworkVariable<PlayerNetworkData>(writePerm: NetworkVariableWritePermission.Owner);

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            TransmitState();
        }
        else
        {
            ConsumeState();
        }
    }

    #region Transmit State

    private void TransmitState()
    {
        var state = new PlayerNetworkData()
        {
            Hp = GetComponent<HealthComponent>().health
        };

        _netState.Value = state;
    }

    #endregion

    #region Get state

    private void ConsumeState()
    {
        GetComponent<HealthComponent>().health = _netState.Value.Hp;

    }

    #endregion

    struct PlayerNetworkData : INetworkSerializable
    {
        private int _hp;

        internal int Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _hp);
        }
    }
}
