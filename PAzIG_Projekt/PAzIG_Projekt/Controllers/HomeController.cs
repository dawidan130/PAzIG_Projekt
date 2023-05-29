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
                    ViewBag.animacja = "pulsate";
                    ViewBag.czasanimacji = "1.5s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ oddechowy":
                    ViewBag.kolor = "#5CE5D5";
                    ViewBag.animacja = "breath";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ nerwowy":
                    ViewBag.kolor = "#7FFF00";
                    ViewBag.animacja = "translate";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ kostny":
                    ViewBag.kolor = "#B76CFD";
                    ViewBag.animacja = "rotate";
                    ViewBag.czasanimacji = "10s";
                    ViewBag.timing = "linear";
                    break;
                case "Całe ciało":
                    ViewBag.kolor = "#FFFF00";
                    ViewBag.animacja = "walk";
                    ViewBag.czasanimacji = "3s";
                    ViewBag.timing = "unset";
                    break;
            }
            return this.View();
        }

        public ActionResult ScorePage(string nazwa, int wynik, double czas)
        {
            if(czas > 150000)
            {
                czas = 150000;
            }

            ViewBag.nazwa = nazwa;
            ViewBag.wynik = wynik;
            ViewBag.czas = czas/1000;
            switch (nazwa)
            {
                case "Układ krwionośny":
                    ViewBag.kolor = "#F21A1D";
                    ViewBag.animacja = "pulsate";
                    ViewBag.czasanimacji = "1.5s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ oddechowy":
                    ViewBag.kolor = "#5CE5D5";
                    ViewBag.animacja = "breath";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ nerwowy":
                    ViewBag.kolor = "#7FFF00";
                    ViewBag.animacja = "translate";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ kostny":
                    ViewBag.kolor = "#B76CFD";
                    ViewBag.animacja = "rotate";
                    ViewBag.czasanimacji = "10s";
                    ViewBag.timing = "linear";
                    break;
                case "Całe ciało":
                    ViewBag.kolor = "#FFFF00";
                    ViewBag.animacja = "walk";
                    ViewBag.czasanimacji = "3s";
                    ViewBag.timing = "unset";
                    break;
            }
            return this.View();
        }

        public ActionResult RankingPick()
        {
            return View();
        }

        public ActionResult Ranking(string nazwa)
        {
            ViewBag.nazwa = nazwa;
            switch (nazwa)
            {
                case "Układ krwionośny":
                    ViewBag.kolor = "#F21A1D";
                    ViewBag.animacja = "pulsate";
                    ViewBag.czasanimacji = "1.5s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ oddechowy":
                    ViewBag.kolor = "#5CE5D5";
                    ViewBag.animacja = "breath";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ nerwowy":
                    ViewBag.kolor = "#7FFF00";
                    ViewBag.animacja = "translate";
                    ViewBag.czasanimacji = "4s";
                    ViewBag.timing = "unset";
                    break;
                case "Układ kostny":
                    ViewBag.kolor = "#B76CFD";
                    ViewBag.animacja = "rotate";
                    ViewBag.czasanimacji = "10s";
                    ViewBag.timing = "linear";
                    break;
                case "Całe ciało":
                    ViewBag.kolor = "#FFFF00";
                    ViewBag.animacja = "walk";
                    ViewBag.czasanimacji = "3s";
                    ViewBag.timing = "unset";
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

        public ActionResult PobierzWyniki(string nazwa)
        {
            client = new FireSharp.FirebaseClient(data);

            List<Wyniki> lista_wynikow = new List<Wyniki>();

            var get = client.Get("Wyniki/" + nazwa + "/");
            lista_wynikow = get.ResultAs<List<Wyniki>>();

            lista_wynikow = lista_wynikow.OrderByDescending(w => w.Wynik).ThenBy(w => w.Czas).ToList();

            return Json(lista_wynikow, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        
        public ActionResult WyslijWyniki(string nazwa, string pseudonim, int wynik, string czas)
        {
            double c = double.Parse(czas);

            client = new FireSharp.FirebaseClient(data);

            List<Wyniki> lista_wynikow = new List<Wyniki>();

            var get = client.Get("Wyniki/"+nazwa +"/");
            lista_wynikow = get.ResultAs<List<Wyniki>>();

            Wyniki w = new Wyniki();
            w.Pseudonim = pseudonim;
            w.Wynik = wynik;
            w.Czas = c;

            lista_wynikow.Add(w);

            var set = client.Set("Wyniki/" + nazwa + "/", lista_wynikow);

            return Json(new { success = true, responseText = "Results saved!" }, JsonRequestBehavior.AllowGet);
        }
    }
}