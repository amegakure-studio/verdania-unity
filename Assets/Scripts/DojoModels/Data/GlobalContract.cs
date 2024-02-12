using Dojo;
using Dojo.Starknet;

public class GlobalContract : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("address")]
    public FieldElement address;
}
