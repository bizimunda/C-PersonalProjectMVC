using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalProject.Models;
using System.Data.SqlClient;
using System.Data;

namespace PersonalProject.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Message = "Edit";
            List<Registration> registration = new List<Registration>();

            SqlConnection con;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            String connectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
            con = new SqlConnection(connectionString);
            String sql = "SELECT * FROM Users";
            da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                registration.Add(new Registration()
                {
                    UserName = dr[0].ToString(),
                    Password = dr[1].ToString(),
                    FirstName = dr[2].ToString(),
                    LastName = dr[3].ToString(),
                    Email = dr[4].ToString(),
                    Adress = dr[5].ToString(),
                    Phone = dr[6].ToString(),
                    Phone1 = dr[7].ToString(),
                    Status = (int)dr[8],
                    Id = (int)dr[9]
                });


            }

            return View(registration);
        }

        public PartialViewResult DisplayUser(int id = 0)
        {
            //get User From database

            /* Sampla! */
            SqlConnection con;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            String connectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
            con = new SqlConnection(connectionString);

            String sql = "SELECT UserName, Password, Id FROM Users where id=" + id;

            da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            Registration r = new Registration();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                r.UserName = dr[0].ToString();
                r.Password = dr[1].ToString();
                r.Id = (int)dr[2];
            }
            return PartialView("_DisplayOneUserData", r);
        }

        [HttpPost]
        public ActionResult ValidateEdit(int id, string username, string password)
        {
            SqlConnection con;
            String connectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
            con = new SqlConnection(connectionString);

            String sql = "Update users set password = '" + password + "', username = '" + username + "' where id = " + id;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Edit", "Admin", null);
        }

        public PartialViewResult DisplayProfile(int id = 0)
        {
            //get User From database

            /* Sampla! */
            SqlConnection con;
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            String connectionString = @"Data Source=PC503\SQL2012;Initial Catalog=PersonalProject;User ID=sa;Password=test1234=";
            con = new SqlConnection(connectionString);

            String sql = "SELECT * FROM Users where id=" + id;

            da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            Registration r = new Registration();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                r.UserName = dr["UserName"].ToString();
                r.Password = dr["Password"].ToString();
                r.FirstName = dr["FirstName"].ToString();
                r.LastName = dr["LastName"].ToString();
                r.Email = dr["Email"].ToString();
                r.Phone = dr["Phone1"].ToString();
                r.Phone1 = dr["Phone2"].ToString();
                r.Status = (int)dr["Status"];
                r.Id = (int)dr["id"];
            }
            return PartialView("_DisplayOneUserData", r);
        }
    }
}