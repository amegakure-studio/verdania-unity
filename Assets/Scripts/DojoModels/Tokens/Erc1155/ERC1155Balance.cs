using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;
using System.Numerics;
using UnityEngine;

public class ERC1155Balance : ModelInstance
{
    [ModelField("id_contract")]
    public FieldElement id_contract;

    [ModelField("account")]
    public FieldElement account;

    [ModelField("id")]
    public UInt64 id;

    [ModelField("amount")]
    public UInt64 amount;

    public event Action<ERC1155Balance> balanceChanged;

    public override void OnUpdate(Model model)
    {
        UInt64 oldAmount = amount;

        base.OnUpdate(model);

        if (oldAmount != amount) 
        {
            balanceChanged?.Invoke(this);
        }
    }
}