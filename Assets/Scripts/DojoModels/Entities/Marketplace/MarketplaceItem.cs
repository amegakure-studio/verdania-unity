using Dojo;
using Dojo.Starknet;

public class MarketplaceItem : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("token_id")]
    public FieldElement tokenId;

    [ModelField("seller")]
    public FieldElement seller;

    [ModelField("amount")]
    public FieldElement amount;

    [ModelField("remaining_amount")]
    public FieldElement remainingAmount;

    [ModelField("price")]
    public FieldElement price;
}
