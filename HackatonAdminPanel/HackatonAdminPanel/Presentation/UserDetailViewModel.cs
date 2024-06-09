using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackatonAdminPanel.Services;

namespace HackatonAdminPanel.Presentation;
public partial class UserDetailViewModel : ObservableObject
{

    private INavigator _navigator;
    ApiService _apiService;


    [ObservableProperty]
    private string userId;

    public string Test { get; }
    public UserDetailViewModel(Entity entity, IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _apiService = new ApiService();
        _navigator = navigator;
        GoBack = new AsyncRelayCommand(GoBackView);
        UserId = entity.Name;
        Test = "ss";
       GetData();

        AddAmmountCommand = new AsyncRelayCommand(AddAmmount);
    }

    private async void GetData()
    {
        Roots = await _apiService.GetTokensAsync(UserId);
    }

    [ObservableProperty]
    private AccountNft? selectedAccountNFT;

    [ObservableProperty]
    private Root roots;

    public ICommand GoBack { get; }
    private async Task GoBackView()
    {
        //await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
        await _navigator.NavigateBackAsync(this);
    }


    public ICommand AddAmmountCommand { get; }

    [ObservableProperty]
    private string ammount;

    private async Task AddAmmount()
    {
        /*   var x = SelectedAccountNFT.nfTokenID;
           var y = ammount;

           ContentDialog noWifiDialog = new ContentDialog
           {
               Title = "No wifi connection",
               Content = "Check your connection and try again.",
               CloseButtonText = "Ok"
           */
        // Make sure to set the XamlRoot!

        await Task.Delay(5000);
        await _navigator.NavigateBackAsync(this);
      //  ContentDialogResult result = await noWifiDialog.ShowAsync();


    }
}
