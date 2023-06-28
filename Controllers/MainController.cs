using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp_Lab.DataAbstractionLayer;
using Asp_Lab.Models;

namespace Asp_Lab.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View("Login");
        }
        public string Test()
        {
            return "It's working";
        }

        public ActionResult GetDocuments()
        {
            DAL dal = new DAL();
            List<Document> doc_list = dal.GetAllDocuments();
            ViewData["documentList"] = doc_list;
            return View("GetDocuments");
        }


        public ActionResult AddDocument()
        {
            
            return View("AddDocument");
        }

        public ActionResult Login()
        {

            return View("Login");
        }

        public ActionResult ErrorLogin()
        {

            return View("ErrorLogin");
        }

       

        public String Check(string username,string password)
        {
            DAL dal = new DAL();
            
            bool ok= dal.Login(username, password);

            if (ok)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        public ActionResult DeleteDocument(int id)
        {

            DAL dal = new DAL();
            dal.DeleteDocument(id);
            return RedirectToAction("GetDocuments");
            //List<Document> doc_list = dal.GetAllDocuments();
            //ViewData["documentList"] = doc_list;
            //return View("GetDocuments");
        }

        public ActionResult SaveDocument()
        {
            Document document = new Document();
            document.Author = Request.Params["author"];
            document.Title = Request.Params["title"];
            document.NrOfPages = int.Parse(Request.Params["nr_of_pages"]);
            document.Type = Request.Params["type"];
            document.Format = Request.Params["format"];
            
            DAL dal = new DAL();
            dal.AddDocument(document);

            //return "don";
            return RedirectToAction("GetDocuments");
            //return View("GetDocuments");
        }



        public ActionResult UpdateDocument(int id, string author, string title, int pages, string type, string format)
        {
           
            
            ViewData["id"] = int.Parse(Request.Params["id"]); ;
            ViewData["auth"] = Request.Params["author"]; ;
            ViewData["title"] = Request.Params["title"];
            ViewData["pages"] = int.Parse(Request.Params["pages"]);
            ViewData["type"] = Request.Params["type"];
            ViewData["format"] = Request.Params["format"];
            return View("UpdateDocument");
        }


        public ActionResult SaveUpdateDocument(int id, string author,string title,int nr_of_pages,string type,string format)
        {
            
            
            DAL dal = new DAL();
            dal.UpdateDocument(id,author,title,nr_of_pages,type,format);
            List<Document> doc_list = dal.GetAllDocuments();
            ViewData["documentList"] = doc_list;
            return View("GetDocuments");
        }

        public String FilterDocuments(string type, string format)
        {


            DAL dal = new DAL();
            List<Document> doc_list = dal.GetAllDocuments(type,format);
            ViewData["documentList"] = doc_list;

            string result = "<table border='1'>" +
                "<tr>" +
                "<th>Id</th>" +
                "<th>Author</th>" +
                "<th>Title</th>" +
                "<th>NrOfPages</th>" +
                "<th>Type</th>" +
                "<th>Format</th>" +
                "<th>Operation</th>" +
                "</tr>";


            foreach (Document d in doc_list)
            {
                result += "<tr><td>" + d.Id + "</td><td>" + d.Author + "</td><td>" + d.Title + "</td><td>" + d.NrOfPages + "</td><td>"+ d.Type + "</td><td>"+ d.Format + "</td><td>"+ "<button class='edit-btn' data-id='" + d.Id + "' data-author='" + d.Author + "' data-title='" + d.Title + "' data-pages='" + d.NrOfPages + "' data-type='" + d.Type + "' data-format='" + d.Format + "'>Edit</button>" +
"<button class='delete-btn' data-id='" + d.Id + "'>Delete</button>" + "</td>"+"</tr>";
            }

            result += "</table>";
            return result;
        }

        

    }
}