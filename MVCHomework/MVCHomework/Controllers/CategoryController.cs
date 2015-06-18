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
    public class CategoryController : Controller
    {
        string cnnStr = ConfigurationManager.ConnectionStrings["NorthCnn"].ConnectionString;
        public ActionResult AnaSayfa()
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command= new SqlCommand("SELECT CategoryID, CategoryName,Description FROM Categories", connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewBag.Kategoriler = dataTable.AsEnumerable();

            return View();
        }

        public ActionResult Goruntule(int id)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("select CategoryName,Description from Categories where CategoryID=@id", connect);

            command.Parameters.AddWithValue("@id", id);

            connect.Open();
            SqlDataReader reader = command.ExecuteReader();

            KategoriModel model = new KategoriModel();
            model.KategoriId= id;

            while (reader.Read())
            {
                model.KategoriAdi= reader["CategoryName"].ToString();
                model.Aciklama= reader["Description"].ToString();

            }

            connect.Close();

            return View(model);
        }

        public ActionResult Kaydet(string kategoriAdi,string aciklama)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("INSERT INTO Categories (CategoryName, Description) VALUES (@name, @description)", connect);
            command.Parameters.AddWithValue("@name", kategoriAdi);
            command.Parameters.AddWithValue("@description",aciklama);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");  
        }
        public ActionResult Yeni()
        {
            return View();
        }
        public ActionResult Guncelle(KategoriModel model)
        {
            SqlConnection connect = new SqlConnection(cnnStr);
            SqlCommand command = new SqlCommand("update Categories set CategoryName=@ad,Description=@aciklama where CategoryID=@id", connect);

            command.Parameters.AddWithValue("@ad", model.KategoriAdi);
            command.Parameters.AddWithValue("@aciklama", model.Aciklama);
            command.Parameters.AddWithValue("@id", model.KategoriId);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            return RedirectToAction("AnaSayfa");

        }
	}
}