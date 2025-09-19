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
            // Clear the list of AvailableProducts
            AvailableProducts.Clear();

            // Get list of al products
            var allProducts = _productService.GetAll();
            var groceryList = MyGroceryListItems;

            foreach (Product product in allProducts)
            {

                // Check if product is already in the groceryList (using productId)
                bool isInGroceryList = groceryList.Any(listItem => listItem.ProductId == product.Id);

                // If the product is not in the list, and is in stock, add to AvailableProducts
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

            // Checks if product exists and if Id is not smaller or equal to 0
            if(product == null || product.Id <= 0) return; // Possible error handling for the future

            // Makes a GroceryListItem with Id 0, and fills in the correct ProductId and GroceryListId
            var newItem = new GroceryListItem(0, GroceryList.Id, product.Id, 1);

            // Adds the item to the GroceryList dataset
            _groceryListItemsService.Add(newItem);


            // Updates the stock of the product
            product.Stock -= 1;
            _productService.Update(product);

            // Updates the AvailableProducts list
            GetAvailableProducts();

            OnGroceryListChanged(GroceryList);
        }
    }
}
