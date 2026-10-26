using GestionEtudiantsApp.DAL;
using GestionEtudiantsApp.Models;
using System.Collections.Generic;

namespace GestionEtudiantsApp.BLL.Managers
{
    public static class MatiereManager
    {
        public static int AjouterMatiere(Matiere m)
        {
            MatiereDAL.AjouterMatiere(m);
            return 1;
        }

        public static void ModifierMatiere(Matiere m)
        {
            MatiereDAL.ModifierMatiere(m);
        }

        public static void SupprimerMatiere(int id)
        {
            MatiereDAL.SupprimerMatiere(id);
        }

        public static List<Matiere> ObtenirToutes()
        {
            return MatiereDAL.ObtenirToutesLesMatieres();
        }
    }
}
