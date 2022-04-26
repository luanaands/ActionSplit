using CartShop;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CartShopTest
{
    public class SplitTest
    {
        [Fact]
        public void CaseOfSucesssSimple()
        {
            //arrange 
            List<ShoppingList> shoppingLists = new List<ShoppingList>
            {
                  new ShoppingList{  Amount = 5.20M,
                        Item = new  Item { Name ="Caneta" },
                      Quantity = 8},
                  new ShoppingList{  Amount = 33.60M,
                        Item = new  Item { Name ="Caderno" },
                      Quantity = 1},
                  new ShoppingList{  Amount = 7.40M,
                        Item = new  Item { Name ="Marcador" },
                      Quantity = 2},

            };

            List<string> emails = new List<string>
            {
                "luanaands@gmail.com", "teste@teste.com", "teste@teste.com.br"
            };

            var usecase = new SplitUseCase();

            //act 
            var result = usecase.Split(shoppingLists, emails);

            //assert 
            Assert.Equal(30, result["luanaands@gmail.com"]);
            Assert.Equal(90, result["teste@teste.com.br"] + result["luanaands@gmail.com"] + result["teste@teste.com"]);
        }

        [Fact]
        public void CaseOfSucesssWithTithe()
        {
            //arrange 
            List<ShoppingList> shoppingLists = new List<ShoppingList>
            {
                  new ShoppingList{  Amount = 5.20M,
                        Item = new  Item { Name ="Caneta" },
                      Quantity = 8},
                  new ShoppingList{  Amount = 43.60M,
                        Item = new  Item { Name ="Caderno" },
                      Quantity = 1},
                  new ShoppingList{  Amount = 7.40M,
                        Item = new  Item { Name ="Marcador" },
                      Quantity = 2},

            };

            List<string> emails = new List<string>
            {
                "luanaands@gmail.com", "teste@teste.com", "teste@teste.com.br"
            };

            var usecase = new SplitUseCase();

            //act 
            var result = usecase.Split(shoppingLists, emails);

            //assert 
            Assert.Equal(33, result["luanaands@gmail.com"]);
            Assert.Equal(33.50M, result["teste@teste.com"]);
            Assert.Equal(33.50M, result["teste@teste.com.br"]);
            Assert.Equal(100, result["teste@teste.com.br"] + result["luanaands@gmail.com"] + result["teste@teste.com"]);
        }

        [Fact]
        public void CaseOfExceptionWithShopListEmpty()
        {
            //arrange 
            List<ShoppingList> shoppingLists = new List<ShoppingList>();

            List<string> emails = new List<string>
            {
                "luanaands@gmail.com", "teste@teste.com", "teste@teste.com.br"
            };

            var usecase = new SplitUseCase();

            //act 

            var caughtException = Assert.Throws<SplitValidationException>(() => usecase.Split(shoppingLists, emails));

            //assert 
            Assert.Equal("Lista de compras está vazia", caughtException.Message);

        }
        [Fact]
        public void CaseOfExceptionWithEmailsEmpty()
        {
            //arrange 
            List<ShoppingList> shoppingLists = new List<ShoppingList>
            {
                  new ShoppingList{  Amount = 5.20M,
                        Item = new  Item { Name ="Caneta" },
                      Quantity = 8},
                  new ShoppingList{  Amount = 43.60M,
                        Item = new  Item { Name ="Caderno" },
                      Quantity = 1},
                  new ShoppingList{  Amount = 7.40M,
                        Item = new  Item { Name ="Marcador" },
                      Quantity = 2},

            };
            List<string> emails = new List<string>();
            var usecase = new SplitUseCase();

            //act 
            var caughtException = Assert.Throws<SplitValidationException>(() => usecase.Split(shoppingLists, emails));

            //assert 
            Assert.Equal("Lista de e-mails está vazia", caughtException.Message);

            // Act
            Action action = () => usecase.Split(shoppingLists, emails);

            // Assert
            action.Should()
              .Throw<SplitValidationException>()
              .WithMessage("Lista de e-mails está vazia");
        }
    }
}