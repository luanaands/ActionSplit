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
            int count = emails.Count;
            var emailsArray = emails.ToArray();
            decimal part = amount/quantity; 
            decimal valueRound = Math.Round(part);
            decimal rest = amount - (valueRound * quantity);
            decimal restDiv = rest;
            int quantityStop = 1;
            while (count > 0)
            {
                var valor = (rest * 100) % count;
                if ((rest*100) % count == 0)
                {
                    restDiv = rest/count;
                    quantityStop = count;
                    break;
                }
                count--;
            }
            for (int i = 0; i < quantity; i++)
            {
                var email = emailsArray[i];
                result[email] = valueRound;
            }
            for (int i = (quantityStop - 1); i < quantity; i++)
            {
                var email = emailsArray[i];
                result[email] = result[email] + restDiv;
            }
            return result;
        }
    }
}
