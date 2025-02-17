﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetCESI.Data;
using ProjetCESI.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCESI.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class GestionController : BaseController
    {
        [Route("Gestion")]
        public async Task<IActionResult> Gestion(GestionViewModel model)
        {
            PrepareModel(model);

            if (model.NomVue == "Validation")
            {
                model.Ressources = (await MetierFactory.CreateRessourceMetier().GetRessourcesNonValider()).ToList();
                if (model.Ressources == null)
                {
                    return View();
                }
            }
            else if (model.NomVue == "UserList")
            {
                model.Users = (await MetierFactory.CreateUtilisateurMetier().GetUser()).ToList();
                if (model.Users == null || model == null)
                {
                    return View();
                }
            }
            else if (model.NomVue == "statistique")
            {
                await UpdateStatistique(model);

                return View(model);
            }
            else if (model.NomVue == "Parametre")
            {
                model.categories = (await MetierFactory.CreateCategorieMetier().GetAll()).ToList();
                if (model.categories == null)
                {
                    return View();
                }
            }
            else if (model.NomVue == "suspendu")
            {
                model.Ressources = (await MetierFactory.CreateRessourceMetier().GetRessourcesSuspendu()).ToList();
                if (model.Ressources == null)
                {
                    return View();
                }
            }
            else
                return RedirectToAction("Accueil", "Accueil");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModifParamCategorie(int id, string nomCategorie)
        {
            if (!string.IsNullOrEmpty(nomCategorie))
            {
                var categorie = await MetierFactory.CreateCategorieMetier().GetById(id);
                categorie.Nom = nomCategorie;
                var result = await MetierFactory.CreateCategorieMetier().InsertOrUpdate(categorie);
            }
            return RedirectToAction("Gestion", new { nomVue = "Parametre" });

        }

        [HttpPost]
        public async Task<IActionResult> AddCategorie(string newCategorie)
        {
            if (!string.IsNullOrEmpty(newCategorie))
            {
                Core.Categorie NewCate = new Core.Categorie();
                NewCate.Nom = newCategorie;
                await MetierFactory.CreateCategorieMetier().InsertOrUpdate(NewCate);
            }
            return RedirectToAction("Gestion", new { nomVue = "Parametre" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await MetierFactory.CreateCategorieMetier().GetById(id);
            var result = await MetierFactory.CreateCategorieMetier().DeleteCategorie(categorie);
            return RedirectToAction("Gestion", new { nomVue = "Parametre" });
        }

        [HttpGet]
        public async Task<JsonResult> UpdateTopRechercheDisplay(int selectedRange)
        {
            List<TopObject> json = new List<TopObject>();

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            switch (selectedRange)
            {
                case 1:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                case 2:
                    {
                        var firstDayOfWeek = DateTimeOffset.Now.AddDays(-((int)DateTimeOffset.Now.DayOfWeek - (int)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
                        firstDayOfWeek = new DateTimeOffset(firstDayOfWeek.Date, firstDayOfWeek.Offset);

                        int diff = ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday) == 0 ? 7 : ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday);
                        var lastDayOfWeek = DateTimeOffset.Now.AddDays(7 - diff);
                        lastDayOfWeek = new DateTimeOffset(lastDayOfWeek.Date, lastDayOfWeek.Offset).AddHours(23).AddMinutes(59).AddSeconds(59);

                        json = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, firstDayOfWeek, lastDayOfWeek)).ToList();

                        break;
                    }
                case 3:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                case 4:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, new DateTimeOffset(DateTimeOffset.Now.Year, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, 12, 31, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                default:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
            }

            return Json(new { parametres = json.Select(c => c.Parametre), count = json.Select(c => c.Count) });
        }

        [HttpGet]
        public async Task<JsonResult> UpdateTopConsultationDisplay(int selectedRange)
        {
            List<TopObject> json = new List<TopObject>();

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            switch (selectedRange)
            {
                case 1:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                case 2:
                    {
                        var firstDayOfWeek = DateTimeOffset.Now.AddDays(-((int)DateTimeOffset.Now.DayOfWeek - (int)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
                        firstDayOfWeek = new DateTimeOffset(firstDayOfWeek.Date, firstDayOfWeek.Offset);

                        int diff = ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday) == 0 ? 7 : ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday);
                        var lastDayOfWeek = DateTimeOffset.Now.AddDays(7 - diff);
                        lastDayOfWeek = new DateTimeOffset(lastDayOfWeek.Date, lastDayOfWeek.Offset).AddHours(23).AddMinutes(59).AddSeconds(59);

                        json = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, firstDayOfWeek, lastDayOfWeek)).ToList();

                        break;
                    }
                case 3:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                case 4:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, new DateTimeOffset(DateTimeOffset.Now.Year, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, 12, 31, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
                default:
                    json = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();
                    break;
            }

            return Json(new { parametres = json.Select(c => c.Parametre), count = json.Select(c => c.Count) });
        }

        [HttpGet]
        public async Task<JsonResult> UpdateTopActionsDisplay(int selectedRange)
        {
            object json = new object();
            List<TopObject> result;

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            switch (selectedRange)
            {
                case 1:
                    result = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Day, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

                    json = new { parametres = result.Select(c => c.Parametre), count = result.Select(c => c.Count) };
                    break;
                case 2:
                    {
                        var firstDayOfWeek = DateTimeOffset.Now.AddDays(-((int)DateTimeOffset.Now.DayOfWeek - (int)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
                        firstDayOfWeek = new DateTimeOffset(firstDayOfWeek.Date, firstDayOfWeek.Offset);

                        int diff = ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday) == 0 ? 7 : ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday);
                        var lastDayOfWeek = DateTimeOffset.Now.AddDays(7 - diff);
                        lastDayOfWeek = new DateTimeOffset(lastDayOfWeek.Date, lastDayOfWeek.Offset).AddHours(23).AddMinutes(59).AddSeconds(59);

                        result = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Week, firstDayOfWeek, lastDayOfWeek)).ToList();

                        json = new { parametres = result.Select(c => c.Parametre), count = result.Select(c => c.Count) };

                        break;
                    }
                case 3:
                    result = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Month, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

                    json = new { parametres = result.Select(c => c.Parametre), count = result.Select(c => c.Count) };
                    break;
                case 4:
                    result = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Year, new DateTimeOffset(DateTimeOffset.Now.Year, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, 12, 31, 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

                    json = new { parametres = result.Select(c => c.Parametre), count = result.Select(c => c.Count) };
                    break;
                default:
                    result = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Month, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

                    json = new { parametres = result.Select(c => c.Parametre), count = result.Select(c => c.Count) };
                    break;
            }

            return Json(json);
        }   

        [HttpGet]
        public async Task<IActionResult> ExportCSV(TimestampFilter periode)
        {
            string filename = string.Empty;

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            DateTimeOffset dtBas;
            DateTimeOffset dtHaut;

            switch (periode)
            {
                case TimestampFilter.Day:
                    dtBas = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 0, DateTimeOffset.Now.Offset);
                    dtHaut = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset);
                    filename = $"Export_{dtBas.Date.ToShortDateString()}.csv";
                    break;
                case TimestampFilter.Week:
                    var firstDayOfWeek = DateTimeOffset.Now.AddDays(-((int)DateTimeOffset.Now.DayOfWeek - (int)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
                    dtBas = new DateTimeOffset(firstDayOfWeek.Date, firstDayOfWeek.Offset);

                    int diff = ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday) == 0 ? 7 : ((int)DateTimeOffset.Now.DayOfWeek - (int)DayOfWeek.Sunday);
                    var lastDayOfWeek = DateTimeOffset.Now.AddDays(7 - diff);
                    dtHaut = new DateTimeOffset(lastDayOfWeek.Date, lastDayOfWeek.Offset).AddHours(23).AddMinutes(59).AddSeconds(59);
                    filename = $"Export_{dtBas.Year}_Semaine_{System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dtBas.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday)}.csv";
                    break;
                case TimestampFilter.Year:
                    dtBas = new DateTimeOffset(DateTimeOffset.Now.Year, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset);
                    dtHaut = new DateTimeOffset(DateTimeOffset.Now.Year, 12, 31, 23, 59, 59, DateTimeOffset.Now.Offset);
                    filename = $"Export_Annee_{dtBas.Date.Year}.csv";
                    break;
                case TimestampFilter.Month:
                default:
                    dtBas = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset);
                    dtHaut = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset);
                    filename = $"Export_{System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(dtBas.Month)}.csv";
                    break;
            }

            try
            {
                var result = await MetierFactory.CreateStatistiqueMetier().GenerateCSVData(10, dtBas, dtHaut, periode);

                byte[] bytes = Encoding.Unicode.GetBytes(result);

                return File(bytes, "application/octet-stream", filename);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private async Task UpdateStatistique(GestionViewModel model)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            var recherches = (await MetierFactory.CreateStatistiqueMetier().GetTopRecherche(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

            var consultations = (await MetierFactory.CreateStatistiqueMetier().GetTopConsultation(10, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset))).ToList();

            var actions = (await MetierFactory.CreateStatistiqueMetier().GetNombreActionsMoyenneParUtilisateurs(TimestampFilter.Month, new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTime.DaysInMonth(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month), 23, 59, 59, DateTimeOffset.Now.Offset)));

            var exploites = (await MetierFactory.CreateUtilisateurRessourceMetier().GetTopExploitee(10)).ToList();

            model.TopRecherches = new TopStats
            {
                Count = recherches.Select(c => c.Count).ToList(),
                Parametres = recherches.Select(c => c.Parametre).ToList()
            };

            model.TopConsultations = new TopStats
            {
                Count = consultations.Select(c => c.Count).ToList(),
                Parametres = consultations.Select(c => c.Parametre).ToList()
            };

            model.TopActions = new TopStats
            {
                Count = actions.Select(c => c.Count).ToList(),
                Parametres = actions.Select(c => c.Parametre).ToList()
            };

            model.TopExploites = new TopStats
            {
                Count = exploites.Select(c => c.Count).ToList(),
                Parametres = exploites.Select(c => c.Parametre).ToList()
            };
        }
    }
}
