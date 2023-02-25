using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult AjouterUnAvis(string nomSeo)
        {
            var formation = new Formation();
            var vm = new AjouterUnAvisViewModel();
            using (var context = new AvisEntities())  //se connecter à la BDD//
            {
                formation = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo); //pour importer toute les information mettre Toliste au lieu FirstOrDefault//
                vm.FormationNomSeo = formation.NomSeo;
                vm.FormationNom = formation.Nom;
            }
                return View(vm);
        }
        [HttpPost]   //ne s'execute qu'après que l'action précédente soit exécuté//
        public ActionResult AjouterUnAvis(string NomSeo,string Nom, string Description,int Note) 
        {
            var formation = new Formation(); //creer un obj vide//
            var avis = new Avis();
            using (var context = new AvisEntities())  //se connecter à la BDD//
            {
                formation = context.Formation.FirstOrDefault(f => f.NomSeo == NomSeo); //pour importer toute les information mettre Toliste au lieu FirstOrDefault//
                avis.IdFormation = formation.Id;
                avis.Nom = Nom; 
                avis.Description = Description;
                avis.Note = Note;
                avis.DateAvis = DateTime.Now;
                avis.UserId = "";
                context.Avis.Add(avis);
                context.SaveChanges();

            }

            return RedirectToAction("DetailsFormation", "Formation", new {nomSeo=NomSeo});
        }
    }
}