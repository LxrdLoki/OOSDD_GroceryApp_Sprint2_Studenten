using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class GroceryListItemsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;
        private readonly IProductService _productService;
        public ObservableCollection<GroceryListItem> MyGroceryListItems { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        GroceryList groceryList = new(0, "None", DateOnly.MinValue, "", 0);

        public GroceryListItemsViewModel(IGroceryListItemsService groceryListItemsService, IProductService productService)
        {
            _groceryListItemsService = groceryListItemsService;
            _productService = productService;
            Load(groceryList.Id);
        }

        private void Load(int id)
        {
            MyGroceryListItems.Clear();
            foreach (var item in _groceryListItemsService.GetAllOnGroceryListId(id)) MyGroceryListItems.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts()
        {
            //Maak de lijst AvailableProducts leeg
            AvailableProducts.Clear();

            //Haal de lijst met producten op
            var allProducts = _productService.GetAll();
            var groceryList = MyGroceryListItems;

            foreach (Product product in allProducts)
            {

                // Controleer of het product al op de boodschappenlijst staat (via ProductId)
                bool isInGroceryList = groceryList.Any(listItem => listItem.ProductId == product.Id);

                // Als het product niet in de lijst is en in stock is, voeg toe aan beschikbare producten
                if (!isInGroceryList && product.Stock > 0) AvailableProducts.Add(product);
            }
        }

        partial void OnGroceryListChanged(GroceryList value)
        {
            Load(value.Id);
        }

        [RelayCommand]
        public async Task ChangeColor()
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), GroceryList } };
            await Shell.Current.GoToAsync($"{nameof(ChangeColorView)}?Name={GroceryList.Name}", true, paramater);
        }
        [RelayCommand]
        public void AddProduct(Product product)
        {

            //Controleerd of het product bestaat en dat de Id > 0
            if(product == null || product.Id <= 0) return; //mogelijk error handling toepassen in de toekomst 

            //Maakt een GroceryListItem met Id 0 en vul de juiste productid en grocerylistid
            var newItem = new GroceryListItem(0, GroceryList.Id, product.Id, 1);

            //Voegt het GroceryListItem toe aan de dataset
            _groceryListItemsService.Add(newItem);


            //Werkt de voorraad (Stock) van het product bij en zorgt dat deze wordt vastgelegd
            product.Stock -= 1;
            _productService.Update(product);

            //Werkt de lijst AvailableProducts bij
            GetAvailableProducts();

            OnGroceryListChanged(GroceryList);
        }
    }
}
