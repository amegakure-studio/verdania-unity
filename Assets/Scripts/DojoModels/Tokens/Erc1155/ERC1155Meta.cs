using Dojo;
using Dojo.Starknet;

public class ERC1155Meta : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("owner")]
    public FieldElement owner;
}
