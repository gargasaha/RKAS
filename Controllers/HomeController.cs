using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FeedBackMC.Models;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
namespace FeedBackMC.Controllers;
class Feedback{
    public int FID { get; set; }
    public string ?Name { get; set; }
    public string ?FB { get; set; }
    public string ?EmojiValue { get; set; }
 }
class Profile{
    public string ?Eid { get; set; } 
    public int ?Count { get; set; }
}
public class HomeController : Controller
{
    public SqlConnection con = new SqlConnection("workstation id=GargaSaha.mssql.somee.com;packet size=4096;user id=ChatApplication_SQLLogin_1;pwd=udg4fy1ak5;data source=GargaSaha.mssql.somee.com;persist security info=False;initial catalog=GargaSaha;TrustServerCertificate=True");
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["F1"] = "0";
        ViewData["F2"] = "0";
        ViewData["F3"] = "0";

        return View();
    }
    [HttpPost]
    public IActionResult Index(string Name, string ?Email, string FeedBack, string EmojiValue)
    {
        //checking if any field is empty
        int f = 0;
        bool mail=true;
        try
        {
            if (Name == null)
            {
                ViewData["F1"] = "1";
                f++;
            }
            else
            {
                ViewData["F1"] = "0";
            }
            if(Email==null){
                mail=false;

            }
            if (FeedBack == null)
            {
                ViewData["F3"] = "1";
                f++;
            }
            else
            {
                ViewData["F3"] = "0";
            }
            if (f > 0)
            {
                return View("Index");
            }
        }
        catch { 

        }
        con.Open();
        SqlCommand cmd = new SqlCommand("InsertFeedBack", con);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name", Name.ToString());
        if(mail){
            cmd.Parameters.AddWithValue("@Email", Email.ToString());
            try {
                using (MailMessage m = new MailMessage())
                {
                    m.From = new MailAddress("chatapp659@gmail.com");
                    m.To.Add(Email.ToString());
                    m.Subject = "Thanks for your feedback";
                    m.Body = "Dear " + Nachar Incoming_value = 0;
                    void setup()
                    {
                        // put your setup code  here, to run once:
                        Serial.begin(9600);
                        pinMode(13, OUTPUT);
                        pinMode(11, OUTPUT);

                    }

                    void loop()
                    {
                        // put your main code here, to run repeatedly:
                        if (Serial.available() > 0)
                        {
                            Incoming_value = Serial.read();
                            Serial.print(Incoming_value);
                            analogWrite(11, atoi(&Incoming_value));

                        }

                    }
                    me + ",\n\nThank you for your feedback. We appreciate your time and effort in providing us with your valuable feedback. We will use your feedback to improve our services.\n" +
                        "We hope to see you again soon.\n";
                    m.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("chatapp659@gmail.com", "mjwn gmtx eekj vkdv");
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        else{
            cmd.Parameters.AddWithValue("@Email", "No Email");
        }
        cmd.Parameters.AddWithValue("@FB", FeedBack.ToString());
        cmd.Parameters.AddWithValue("@EmojiValue",EmojiValue.ToString());
        cmd.ExecuteNonQuery();
        con.Close();
        return View("Index");
    }
    public IActionResult Show()
    {
        return View("Show");
    }
    [HttpGet]
    public IActionResult GetTopFeedbacks()
    {
        //Console.WriteLine("GetTopFeedbacks");
        try{
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from FeedBack order by FID Desc", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Feedback> feedbacks = new List<Feedback>();
            
            while (dr.Read())
            {
                Feedback f = new Feedback();
                f.FID = Convert.ToInt32(dr[0]);
                f.Name = dr[1].ToString();
                f.FB = dr["FB"].ToString();
                f.EmojiValue = dr[4].ToString();
                feedbacks.Add(f);
            }
            
            con.Close();
            return Json(feedbacks);
        }
        catch(Exception e){
            Console.WriteLine(e);
            return Json("Error");
        }
        
        
    }
    [HttpPost]
    public void DeleteFeedback(int id){
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from FeedBack where FID = @FID", con);
        cmd.Parameters.AddWithValue("@FID", id);
        cmd.ExecuteNonQuery();
        con.Close();
        
    }
    [HttpGet]
    public IActionResult GetCount()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select count(*) from FeedBack where EmojiValue = '1'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        Profile p1,p2,p3,p4;
        p1=new Profile();
        p2=new Profile();
        p3=new Profile();
        p4=new Profile();
        while (dr.Read())
        {
            p1.Eid = "1";
            p1.Count = Convert.ToInt32(dr[0]);
        }
        con.Close();
        con.Open();
        cmd = new SqlCommand("select count(*) from FeedBack where EmojiValue = '2'", con);
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            p2.Eid = "2";
            p2.Count = Convert.ToInt32(dr[0]);
        }
        con.Close();
        con.Open();
        cmd = new SqlCommand("select count(*) from FeedBack where EmojiValue = '3'", con);
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            p3.Eid = "3";
            p3.Count = Convert.ToInt32(dr[0]);
        }
        con.Close();
        con.Open();
        cmd = new SqlCommand("select count(*) from FeedBack where EmojiValue = '4'", con);
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            p4.Eid = "4";
            p4.Count = Convert.ToInt32(dr[0]);
        }
        con.Close();
        List<Profile> c = [p1, p2, p3, p4];
        c = c.OrderByDescending(x => x.Count).ToList();
        return Json(c);
    }
    public IActionResult Privacy()
    {
        con.Open();

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
