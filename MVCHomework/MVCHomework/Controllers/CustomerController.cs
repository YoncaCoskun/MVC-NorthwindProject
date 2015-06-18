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
    public class CustomerController : Controller
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["NorthCnn"].ConnectionString;
        public ActionResult AnaSayfa()
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("SELECT CustomerID, CompanyName,ContactName,City FROM Customers", connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewBag.Müsteriler = dataTable.AsEnumerable();

            return View();
        }

        public ActionResult Goruntule(string id)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("select CompanyName,ContactName,City from Customers where CustomerID=@id", connect);

            command.Parameters.AddWithValue("@id", id);

            connect.Open();
            SqlDataReader reader = command.ExecuteReader();

            MusteriModel model = new MusteriModel();
            model.MusteriId = id;

            while (reader.Read())
            {
                model.SirketAdi = reader["CompanyName"].ToString();
                model.BaglantiAdi = reader["ContactName"].ToString();
                model.Sehir = reader["City"].ToString();
            }

            connect.Close();

            return View(model);
        }

        public ActionResult Kaydet(string musteriAdi, string baglantiAdi,string sehir,string id)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("INSERT INTO Customers(CustomerID,CompanyName,ContactName,City) VALUES(@id,@name,@contact,@city)", connect);
            command.Parameters.AddWithValue("@name",musteriAdi);
            command.Parameters.AddWithValue("@contact",baglantiAdi);
            command.Parameters.AddWithValue("@city",sehir);
            command.Parameters.AddWithValue("@id", id);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");
        }
        public ActionResult Yeni()
        {
            return View();
        }
        public ActionResult Guncelle(MusteriModel model)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("update Customers set CompanyName=@ad,ContactName=@baglanti,City=@city where CustomerID=@id", connect);

            command.Parameters.AddWithValue("@ad", model.SirketAdi);
            command.Parameters.AddWithValue("@baglanti", model.BaglantiAdi);
            command.Parameters.AddWithValue("@id", model.MusteriId);
            command.Parameters.AddWithValue("@city", model.Sehir);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");

        }

	}
}