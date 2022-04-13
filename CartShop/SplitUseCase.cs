namespace CartShop
{
    public class SplitUseCase : ISplitUseCase
    {
        public Dictionary<string, decimal> Split(List<ShoppingList> shopList, List<string> emails)
        {
            Validation(shopList, emails);
            decimal total = CalcTotal(shopList);
            return SplitByQuantity(total, emails);
        }
        private static void Validation(List<ShoppingList> shoppingLists, List<string> emails)
        {
            if (shoppingLists == null || shoppingLists.Count < 1)
            {
                throw new SplitValidationException("Lista de compras está vazia");
            }

            if (emails == null || emails.Count < 1)
            {
                throw new SplitValidationException("Lista de e-mails está vazia");
            }
        }
        private static decimal CalcTotal(List<ShoppingList> shoppingLists)
        {
            decimal total = 0;
            foreach(ShoppingList shop in shoppingLists)
            {
                var valueParcial = shop.Amount * shop.Quantity;
                total += valueParcial;
            }
            return total;
        }
        private static Dictionary<string, decimal> SplitByQuantity(decimal amount, List<string> emails)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            int quantity = emails.Count;
            int indexLast = quantity - 1;
            var emailsArray = emails.ToArray();
            decimal part = amount/quantity; 
            decimal valueRound = Math.Round(part);
            decimal rest = amount - (valueRound * quantity);
            decimal valueLast = valueRound + rest;

            for (int i = 0; i < quantity; i++)
            {
                var email = emailsArray[i];
                if (i != indexLast)
                {
                    result[email] = valueRound;
                }
                else
                {
                    result[email] = valueLast;
                }
            }
            return result;
        }
    }
}
