using Dojo;
using Dojo.Starknet;

public class ERC20Balance : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("account")]
    public FieldElement account;

    [ModelField("amount")]
    public FieldElement amount;
}
