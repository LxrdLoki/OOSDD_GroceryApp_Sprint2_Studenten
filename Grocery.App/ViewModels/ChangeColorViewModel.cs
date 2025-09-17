using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class ChangeColorViewModel : BaseViewModel
    {
        private readonly IGroceryListService _groceryListService;

        [ObservableProperty]
        GroceryList groceryList = new(0, "", DateOnly.MinValue, "", 0);


        public ChangeColorViewModel(IGroceryListService groceryListService)
        {
            _groceryListService = groceryListService;
        }

        partial void OnGroceryListChanged(GroceryList value)
        {
            var UpdatedList = _groceryListService.Update(value);

            if (UpdatedList != null)
            {
                GroceryList = UpdatedList;
            }
            else
            {
                // throw error when update fails (prevent warnings in IDE)
                throw new ArgumentNullException(nameof(UpdatedList)); 
            }
        }

        [RelayCommand]
        private async Task ChangeColor(string color)
        {
            GroceryList.Color = color;
            OnGroceryListChanged(GroceryList);
            await Shell.Current.GoToAsync("..");
        }
    }
}
