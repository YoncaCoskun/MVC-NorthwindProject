using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHomework.Models;

namespace MVCHomework.Controllers
{
    public class EmployeeController : Controller
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["NorthCnn"].ConnectionString;
        public ActionResult AnaSayfa()
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("SELECT EmployeeID,LastName,FirstName,Title,City FROM Employees", connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewBag.Calisanlar = dataTable.AsEnumerable();

            return View();
        }

        public ActionResult Goruntule(int id)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("select LastName,FirstName,Title,City from Employees where EmployeeID=@id", connect);

            command.Parameters.AddWithValue("@id", id);

            connect.Open();
            SqlDataReader reader = command.ExecuteReader();

            CalisanModel model = new CalisanModel();
            model.CalisanId = id;

            while (reader.Read())
            {
                model.Soyad = reader["LastName"].ToString();
                model.Adı = reader["FirstName"].ToString();
                model.Unvan = reader["Title"].ToString();
                model.Sehir = reader["City"].ToString();

            }

            connect.Close();

            return View(model);
        }

        public ActionResult Kaydet(string soyad, string ad,string unvan,string sehir)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("INSERT INTO Employees (LastName,FirstName,Title,City) VALUES (@lastname, @firstname,@title,@city)", connect);
            command.Parameters.AddWithValue("@lastname", soyad);
            command.Parameters.AddWithValue("@firstname", ad);
            command.Parameters.AddWithValue("@title", unvan);
            command.Parameters.AddWithValue("@city",sehir);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");
        }
        public ActionResult Yeni()
        {
            return View();
        }
        public ActionResult Guncelle(CalisanModel model)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("update Employees set LastName=@soyad,FirstName=@ad,Title=@title,City=@city where EmployeeID=@id", connect);

            command.Parameters.AddWithValue("@soyad", model.Soyad);
            command.Parameters.AddWithValue("@ad", model.Adı);
            command.Parameters.AddWithValue("@id", model.CalisanId);
            command.Parameters.AddWithValue("@title", model.Unvan);
            command.Parameters.AddWithValue("@city", model.Sehir);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");

        }
	}
}