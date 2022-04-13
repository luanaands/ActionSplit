namespace CartShop
{
    public interface ISplitUseCase
    {
        Dictionary<string, decimal> Split(List<ShoppingList> shopList, List<string> emails);
    }
}
