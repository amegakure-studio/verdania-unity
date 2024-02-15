using Dojo;
using Dojo.Starknet;
using System;

public class ERC20Allowance : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("owner")]
    public FieldElement owner;
    
    [ModelField("spender")]
    public FieldElement spender;

    [ModelField("amount")]
    public UInt64 amount;
}
