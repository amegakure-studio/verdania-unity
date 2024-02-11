using Dojo;
using Dojo.Starknet;
using System.Numerics;

public class ERC20Balance : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("account")]
    public FieldElement account;

    [ModelField("amount")]
    public BigInteger amount;
}
