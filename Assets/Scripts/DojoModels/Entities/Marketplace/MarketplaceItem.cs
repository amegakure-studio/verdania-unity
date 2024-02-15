using Dojo;
using Dojo.Starknet;
using System;

public class MarketplaceItem : ModelInstance
{
    [ModelField("id")]
    public UInt64 id;

    [ModelField("token_id")]
    public UInt64 tokenId;

    [ModelField("seller")]
    public FieldElement seller;

    [ModelField("amount")]
    public UInt64 amount;

    [ModelField("remaining_amount")]
    public UInt64 remainingAmount;

    [ModelField("price")]
    public UInt64 price;
}
