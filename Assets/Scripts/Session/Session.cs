using Dojo.Starknet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    [SerializeField] FieldElement m_PlayerId;
    [SerializeField] UInt64 m_MapId;
    [SerializeField] UInt64 m_FarmId;

    public FieldElement PlayerId { get => m_PlayerId; set => m_PlayerId = value; }
    public ulong MapId { get => m_MapId; set => m_MapId = value; }
    public ulong FarmId { get => m_FarmId; set => m_FarmId = value; }
}
