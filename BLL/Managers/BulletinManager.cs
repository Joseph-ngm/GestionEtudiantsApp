using GestionEtudiantsApp.DAL;
using GestionEtudiantsApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GestionEtudiantsApp.BLL
{
    public static class BulletinManager
    {
        /// <summary>
        /// Obtenir tous les étudiants sous forme de liste d'objets Etudiant
        /// </summary>
        public static List<Etudiant> GetAllEtudiants()
        {
            DataTable dt = BulletinDAL.GetAllEtudiants();
            List<Etudiant> etudiants = new List<Etudiant>();

            foreach (DataRow row in dt.Rows)
            {
                etudiants.Add(new Etudiant
                {
                    IdEtudiant = Convert.ToInt32(row["IdEtudiant"]),
                    Nom = row["Nom"].ToString(),
                    Prenom = row["Prenom"].ToString(),
                    DateNaissance = row["DateNaissance"] != DBNull.Value
                        ? Convert.ToDateTime(row["DateNaissance"])
                        : DateTime.MinValue,
                    Groupe = row["Groupe"]?.ToString() ?? ""
                });
            }

            return etudiants;
        }

        /// <summary>
        /// Obtenir un étudiant par son ID
        /// </summary>
        public static Etudiant GetEtudiantById(int idEtudiant)
        {
            DataRow row = BulletinDAL.GetEtudiantInfo(idEtudiant);

            if (row == null)
                throw new Exception($"Étudiant avec l'ID {idEtudiant} introuvable.");

            return new Etudiant
            {
                IdEtudiant = Convert.ToInt32(row["IdEtudiant"]),
                Nom = row["Nom"].ToString(),
                Prenom = row["Prenom"].ToString(),
                DateNaissance = row["DateNaissance"] != DBNull.Value
                    ? Convert.ToDateTime(row["DateNaissance"])
                    : DateTime.MinValue,
                Groupe = row["Groupe"]?.ToString() ?? ""
            };
        }

        /// <summary>
        /// Obtenir le bulletin d'un étudiant (matières avec meilleures notes)
        /// </summary>
        public static DataTable GetBulletinData(int idEtudiant)
        {
            return BulletinDAL.GetDetailsBulletin(idEtudiant);
        }

        /// <summary>
        /// Calculer la moyenne pondérée à partir d'un DataTable de notes
        /// (utilisé pour affichage après chargement du bulletin)
        /// </summary>
        public static decimal CalculerMoyenne(DataTable notes)
        {
            if (notes == null || notes.Rows.Count == 0)
                return 0;

            decimal totalPondere = 0;
            decimal totalCoef = 0;

            foreach (DataRow row in notes.Rows)
            {
                if (row["MeilleureNote"] == DBNull.Value || row["Coefficient"] == DBNull.Value)
                    continue;

                decimal note = Convert.ToDecimal(row["MeilleureNote"]);
                int coef = Convert.ToInt32(row["Coefficient"]);

                totalPondere += note * coef;
                totalCoef += coef;
            }

            if (totalCoef == 0)
                return 0;

            return Math.Round(totalPondere / totalCoef, 2);
        }

        /// <summary>
        /// Obtenir la moyenne générale calculée directement en base
        /// </summary>
        public static decimal GetMoyenneGenerale(int idEtudiant)
        {
            return BulletinDAL.GetMoyenneGenerale(idEtudiant);
        }
    }
}