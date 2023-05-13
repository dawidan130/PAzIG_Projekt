using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using PAzIG_Projekt.Models;
using PAzIG_Projekt.Attributes;

namespace PAzIG_Projekt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult QuizPage(string nazwa)
        {
            ViewBag.nazwa = nazwa;
            switch (nazwa)
            {
                case "Układ krwionośny":
                    ViewBag.kolor = "#F21A1D";
                    break;
                case "Układ oddechowy":
                    ViewBag.kolor = "#5CE5D5";
                    break;
                case "Układ nerwowy":
                    ViewBag.kolor = "#7FFF00";
                    break;
                case "Układ kostny":
                    ViewBag.kolor = "#B76CFD";
                    break;
                case "Całe ciało":
                    ViewBag.kolor = "#FFFF00";
                    break;
            }
            return this.View();
        }

        IFirebaseConfig data = new FirebaseConfig
        {
            BasePath = "https://pazig-78445-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        [JsonNetFilter]
        public ActionResult GetPytania(string nazwa)
        {
            client = new FireSharp.FirebaseClient(data);

            List<Pytanie> lista_pytan = new List<Pytanie>();

            var get = client.Get("Pytania/" + nazwa + "/");
            lista_pytan = get.ResultAs<List<Pytanie>>();

            return Json(lista_pytan, JsonRequestBehavior.AllowGet);
        }
    }
}