using HackatonAdminPanel.Services;

namespace HackatonAdminPanel.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;
    private readonly ApiService _apiService;

    [ObservableProperty]
    private string? name;

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";

        GoToSecond = new AsyncRelayCommand(GoToSecondView);
        Refresh = new AsyncRelayCommand(RefreshData);
        Delete = new AsyncRelayCommand(DeleteData);
        AddTokenCommand = new AsyncRelayCommand(AddToken);
        GetDetailsCommand = new AsyncRelayCommand(GoToDetailPage);

        _apiService = new ApiService();

        //UsersList = GenerateRandomUsers();
    }
    public string? Title { get; }

    public ICommand GoToSecond { get; }
    public ICommand Refresh { get; }
    public ICommand Delete { get; }
    public ICommand AddTokenCommand { get; }
    public ICommand GetDetailsCommand { get; }

    private async Task GoToSecondView()
    {
        //await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
        await _navigator.NavigateViewModelAsync<SecondViewModel>(this);
    }
    private async Task RefreshData()
    {
        UsersList = await _apiService.GetAllDistributorsAsync();
    }
    private async Task DeleteData()
    {
        var x = _apiService.SoftDeleteUserAsync(SelectedUser.Id);
        await RefreshData();
    }
    private async Task AddToken()
    {
        var x = SelectedUser.WalletId;
        var y = await _apiService.MintTokensAsync(x, Walleturl);
   
    }

    private async Task GoToDetailPage()
    {
        await _navigator.NavigateViewModelAsync<UserDetailViewModel>(this, data: new Entity(SelectedUser.accountId));
    }
    /*private List<UserDto> usersList;
    public List<UserDto> UsersList
    {
        get => usersList;
        set { usersList = value; OnPropertyChanged(nameof(usersList)); }
    }*/
    [ObservableProperty]
    private List<UserDto> usersList;
    [ObservableProperty]
    private UserDto? selectedUser;

    [ObservableProperty]
    private string walleturl="";
    //public Users SelectedUser { get=>selectedUser; set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }  }

    private List<Users> GenerateRandomUsers()
    {
        List<Users> usersList = new List<Users>();
        Random rand = new Random();
        string[] names = { "John", "Jane", "Jim", "Jill", "Jack", "Jenny", "Joe", "Jess", "Jerry", "Jordan" };
        string[] emails = { "john@example.com", "jane@example.com", "jim@example.com", "jill@example.com", "jack@example.com", "jenny@example.com", "joe@example.com", "jess@example.com", "jerry@example.com", "jordan@example.com" };

        for (int i = 0; i < 10; i++)
        {
            Users user = new Users
            {
                Id = i + 1,
                Name = names[rand.Next(names.Length)],
                Email = emails[rand.Next(emails.Length)],
                isDistributor = rand.Next(2) == 0,
                walletId = Guid.NewGuid().ToString()
            };
            usersList.Add(user);
        }
        return usersList;
    }
}
