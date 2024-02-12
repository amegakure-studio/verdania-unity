using Dojo;
using Dojo.Starknet;

public class ERC1155OperatorApproval : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("owner")]
    public FieldElement owner;

    [ModelField("operator")]
    public FieldElement _operator;

    [ModelField("approved")]
    public bool approved;
}
