using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {
            var listFormation = new List<Formation>();
            var ListeFormationViewModel = new List<ListePersonnaliséeFormationViewModel>();
            using (var context = new AvisEntities())  // AvisEntities : la chaine de connexion "lien avec la BD class"; using stop le travail BDD après context c'est un variable{
            { 
                 listFormation = context.Formation.ToList();
                foreach (var f in listFormation) 
                {
                    var vm = new ListePersonnaliséeFormationViewModel();
                    vm.Id = f.Id;
                    vm.Nom = f.Nom;
                    vm.Description = f.Description;
                    vm.Url = f.Url;
                    vm.NomSeo = f.NomSeo;
                    if (f.Avis.Count > 0)
                    {

                        vm.Note = Math.Round(f.Avis.Average(n => n.Note),2);
                        vm.NombreAvis = f.Avis.Count;
                    }
                    else
                    {
                        vm.NombreAvis = 0;
                        vm.Note = 0;
                    }
                    ListeFormationViewModel.Add(vm);
                }
            }
            return View(ListeFormationViewModel);
            }
        public ActionResult DetailsFormation(string nomSeo = null) //creation d'une action (la formation demandée par l'utilisation)
        {
            var formation = new Formation(); // instancier un obj de la BDD
            var dformation = new Detailsviewmodel(); //instancier un obj su nouveau model
            using (var context = new AvisEntities()) // instancier un obj Context pour se connecter à BDD
            {
                formation = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo); // Lambda expression
                if (formation == null)
                {
                        return RedirectToAction("ToutesLesFormations");
                }
                dformation.Nom = formation.Nom;
                dformation.NomSeo = formation.NomSeo;
                dformation.Url = formation.Url;
                dformation.Description = formation.Description;
                if (formation.Avis.Count > 0)
                {
                    dformation.Note = Math.Round(formation.Avis.Average(n => n.Note), 2);
                    dformation.NombreAvis = formation.Avis.Count;
                    dformation.Avis = formation.Avis.ToList();
                }
                else
                {
                    dformation.NombreAvis = 0;
                    dformation.Note = 0;
                }
            }
            return View(dformation); //ajouter l'objet formation instancier dans le vue dformation
        }
    }
}