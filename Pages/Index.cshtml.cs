using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorSamples.DatabaseSpecific;
using RazorSamples.EntityClasses;
using RazorSamples.FactoryClasses;
using RazorSamples.HelperClasses;
using RazorSamples.Linq;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public List<UserEntity> Users { get; set; }
    [BindProperty]
    public string NewUserName { get; set; }
    public void OnGet()
    {
        LoadUsers();
    }
    public void OnPostAddUser()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        // Ensure the connection string is correctly set
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string 'DefaultConnection' is not configured.");
        }
        using (var adapter = new DataAccessAdapter(connectionString))
        {
            // Create a new user entity
            var newUser = new UserEntity
            {
                Name = NewUserName
            };
            // Save the new user to the database
            adapter.SaveEntity(newUser);

            // Reload users to reflect the changes
            LoadUsers();
        }
    }
    private void LoadUsers()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (var adapter = new DataAccessAdapter(connectionString))
        {
            var userCollection = new EntityCollection<UserEntity>(new UserEntityFactory());
            var query = new LinqMetaData(adapter).User;
            Users = query.ToList();
        }
    }
}
