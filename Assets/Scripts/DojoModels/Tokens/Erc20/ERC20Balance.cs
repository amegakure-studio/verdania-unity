using Dojo;
using Dojo.Starknet;
using System;

public class ERC20Balance : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("account")]
    public FieldElement account;

    [ModelField("amount")]
    public UInt64 amount;
}
