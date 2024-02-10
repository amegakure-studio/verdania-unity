using Dojo;
using Dojo.Starknet;

public class ERC20Meta : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("name")]
    public FieldElement erc20Name;
    
    [ModelField("symbol")]
    public FieldElement symbol;

    [ModelField("total_supply")]
    public FieldElement total_supply;
    
    [ModelField("owner")]
    public FieldElement owner;
}
