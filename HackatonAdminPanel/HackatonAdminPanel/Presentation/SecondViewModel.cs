using System.Xml.Linq;
using HackatonAdminPanel.Services;

namespace HackatonAdminPanel.Presentation;

public partial class SecondViewModel : ObservableObject
{

    private INavigator _navigator;
    ApiService _apiService;

    public SecondViewModel(IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator) 
    {
        _apiService = new ApiService();
        _navigator = navigator;
        GoBack = new AsyncRelayCommand(GoBackView);
        AddUserCommand = new AsyncRelayCommand(AddUser);
    }

    public ICommand GoBack  { get; }
    public ICommand AddUserCommand  { get; }
    private string email;
    public string Email {
        get { return email; } set { email = value; OnPropertyChanged(nameof(Email)); } }
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; OnPropertyChanged(nameof(Name)); }
    }
    private bool isDistributor;
    public bool IsDistributor
    {
        get => isDistributor;
        set {  isDistributor = value; OnPropertyChanged(nameof(IsDistributor)); }
    }



    private async Task GoBackView()
    {
        //await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
        await _navigator.NavigateBackAsync(this);
    }
    private async Task AddUser()
    {
        await _apiService.AddUserAsync(new UserRequest() {Name=Name, Email=Email });
        await _navigator.NavigateBackAsync(this);
    }

}
