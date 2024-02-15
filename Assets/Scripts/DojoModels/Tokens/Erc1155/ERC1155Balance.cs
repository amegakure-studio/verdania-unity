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
    public BigInteger id;

    [ModelField("amount")]
    public BigInteger amount;

    public event Action<ERC1155Balance> balanceChanged;

    public override void OnUpdate(Model model)
    {
        BigInteger oldAmount = amount;

        base.OnUpdate(model);

        if (!oldAmount.Equals(amount)) 
        {
            balanceChanged?.Invoke(this);
        }
    }
}
