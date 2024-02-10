using Dojo;
using Dojo.Starknet;

public class ERC1155Balance : ModelInstance
{
    [ModelField("id_contract")]
    public FieldElement id_contract;

    [ModelField("account")]
    public FieldElement account;

    [ModelField("id")]
    public FieldElement id;

    [ModelField("amount")]
    public FieldElement amount;
}
