using NeniscaAntoniaLab7;
using  NeniscaAntoniaLab7.Models;
public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList) this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
      
        var shopList = (ShopList)BindingContext;


        var products = await App.Database.GetListProductsAsync(shopList.ID);
        if (products == null || !products.Any())
        {
            await DisplayAlert("Error", "No items to delete.", "OK");
            return;
        }

        var productToDelete = products.Last();
        await App.Database.DeleteProductAsync(productToDelete);


        listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);


        await DisplayAlert("Success", "Item deleted successfully.", "OK");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}
