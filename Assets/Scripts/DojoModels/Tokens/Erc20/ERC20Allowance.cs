using Dojo;
using Dojo.Starknet;

public class ERC20Allowance : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("owner")]
    public FieldElement owner;
    
    [ModelField("spender")]
    public FieldElement spender;

    [ModelField("amount")]
    public FieldElement amount;
}
