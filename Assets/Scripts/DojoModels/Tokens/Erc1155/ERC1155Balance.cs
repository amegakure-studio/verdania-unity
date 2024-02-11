using Dojo;
using Dojo.Starknet;
using System.Numerics;

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
}
