using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
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

    //public override void Initialize(Model model)
    //{
    //    base.Initialize(model);
    //    Debug.Log("Item_id: " + id + "\n" + "Amm: " + amount.ToString());
    //}
}
