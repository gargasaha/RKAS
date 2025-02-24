using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RKAS.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace RKAS.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AdmRegister()
    {
        string connectionString = _configuration.GetConnectionString("MySql") ?? throw new InvalidOperationException("MySql connection string is not configured.");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MySql connection string is not configured.");
        }
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        mySqlConnection.Open();
        MySqlCommand mySqlCommand = new MySqlCommand("select * from State", mySqlConnection);
        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
        List<State> states = new List<State>();
        while (mySqlDataReader.Read())
        {
            State state = new State();
            state.Sid = mySqlDataReader["Sid"].ToString();
            state.Sname = mySqlDataReader["Sname"].ToString();
            states.Add(state);
        }
        ViewBag.st=states;
        return View();
    }
    [HttpPost]
    public IActionResult UpdateDist(string Sid){
        // Console.WriteLine(Sid);
        string connectionString = _configuration.GetConnectionString("MySql") ?? throw new InvalidOperationException("MySql connection string is not configured.");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MySql connection string is not configured.");
        }
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        mySqlConnection.Open();
        MySqlCommand mySqlCommand = new MySqlCommand("select * from Dist where Sid='"+Sid+"';", mySqlConnection);
        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
        List<Dist> dists = new List<Dist>();
        while (mySqlDataReader.Read())
        {
            Dist dist=new Dist();
            dist.Did = mySqlDataReader["Did"].ToString();
            dist.DName = mySqlDataReader["DName"].ToString();
            dist.Sid = mySqlDataReader["Sid"].ToString();
            dists.Add(dist);
        }
        var jsonResult = Json(dists);
        mySqlConnection.Close();
        return jsonResult;
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
