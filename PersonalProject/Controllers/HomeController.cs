using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalProject.Models;
using System.Data.SqlClient;
using System.Data;

namespace PersonalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Register: Signup";

            return View();
        }

        [HttpPost]
        public ActionResult Register(Registration registration)
        {
            

            string ConnectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
            if (ModelState.IsValid)
            {
                string userName = registration.UserName;
                string password = registration.Password;
                string firstName = registration.FirstName;
                string lastName = registration.LastName;
                string email = registration.Email;
                string adress = registration.Adress;
                string phone = registration.Phone;
                string phone1 = registration.Phone1;


                try
                {
                    string query = "INSERT INTO Users (UserName, Password, FirstName, LastName, Email, Adress, Phone1, Phone2, Status) VALUES('" + userName + "','" + password + "','" + firstName + "','" + lastName + "','" + email + "','" + adress + "','" + phone + "','" + phone1 + "', 1)";
                    SqlConnection db = new SqlConnection();
                    db.ConnectionString = ConnectionString;

                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = query;
                    db.Open();
                    int rows=cmd.ExecuteNonQuery();
                    db.Close();
                    return RedirectToAction("Connection");

                }
                catch (Exception ex)
                {
                    return View(registration);

                }
                
            }
            else {
                return View(registration);
            }

            
        }

        public ActionResult Connection()
        {
            ViewBag.Message = "Connection";

            return View();
        }

        [HttpPost]
        public ActionResult Connection(LogInVerification user)
        {

            if(ModelState.IsValid)
            {
                SqlConnection con;
                SqlDataAdapter da;
                DataSet ds = new DataSet();
                String connectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
                con = new SqlConnection(connectionString);

                String sql = "SELECT * FROM users where username='"+user.UserName + "' and password='" + user.Password + "'";

                da = new SqlDataAdapter(sql, con);
                da.Fill(ds);

                Registration r = new Registration();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    r.UserName = dr[0].ToString();
                    r.Password = dr[1].ToString();
                    r.FirstName = dr["firstname"].ToString();
                    r.LastName = dr["LastName"].ToString();
                    r.Email = dr["Email"].ToString();
                    r.Adress = dr["Adress"].ToString();
                    r.Phone = dr["Phone1"].ToString();
                    r.Phone1 = dr["Phone2"].ToString();
                    r.Id = (int)dr["ID"];

                }

                Session["user"] = r;

                return RedirectToAction("Profile", "Home", new { area = "" });

            } else

            return View();
        }

        public ActionResult Welcome()
        {
            ViewBag.Message = "Welcome";

            return View();
        }

        public ActionResult Profile()
        {
            ViewBag.Message = "Welcome";
            Registration r = Session["user"] as Registration;
            return View(r);
        }

        

        
    }
}