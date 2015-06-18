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
    public class ShipperController : Controller
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["NorthCnn"].ConnectionString;
        public ActionResult AnaSayfa()
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("SELECT ShipperID,CompanyName,Phone FROM Shippers", connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewBag.Nakliyeciler = dataTable.AsEnumerable();

            return View();
        }

        public ActionResult Goruntule(int id)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("select CompanyName,Phone from Shippers where ShipperID=@id", connect);

            command.Parameters.AddWithValue("@id", id);

            connect.Open();
            SqlDataReader reader = command.ExecuteReader();

            NakliyeciModel model = new NakliyeciModel();
            model.NakliyeciId = id;

            while (reader.Read())
            {
                model.SirketAdi = reader["CompanyName"].ToString();
                model.Telefon = reader["Phone"].ToString();           

            }

            connect.Close();

            return View(model);
        }
        public ActionResult Kaydet(string sirketAdi,string telefon)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("INSERT INTO Shippers (CompanyName,Phone) VALUES (@companyname,@phone)", connect);
            command.Parameters.AddWithValue("@companyname",sirketAdi);
            command.Parameters.AddWithValue("@phone",telefon);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");
        }
        public ActionResult Yeni()
        {
            return View();
        }
        public ActionResult Guncelle(NakliyeciModel model)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("update Shippers set CompanyName=@sirketAdi,Phone=@telefon where ShipperID=@id", connect);

            command.Parameters.AddWithValue("@sirketAdi", model.SirketAdi);
            command.Parameters.AddWithValue("@telefon", model.Telefon);
            command.Parameters.AddWithValue("@id", model.NakliyeciId);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");

        }
	}
}