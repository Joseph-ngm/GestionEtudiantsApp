using System;
using System.Collections.Generic;
using System.Linq;
using GestionEtudiantsApp.BLL.Managers;
using GestionEtudiantsApp.Models;
using GestionEtudiantsApp.DAL;

namespace GestionEtudiantsApp.BLL.Managers
{
    public class EtudiantManager
    {
         
        /// Classe pour représenter un étudiant avec sa moyenne
        ///  
        public class EtudiantAvecMoyenne
        {
            public Etudiant Etudiant { get; set; }
            public decimal Moyenne { get; set; }
            public string Appreciation
            {
                get
                {
                    if (Moyenne >= 16) return "Très Bien";
                    if (Moyenne >= 14) return "Bien";
                    if (Moyenne >= 12) return "Assez Bien";
                    if (Moyenne >= 10) return "Passable";
                    return "Insuffisant";
                }
            }
        }

         
        /// Calculer la moyenne générale d'un étudiant (moyenne pondérée)
        ///  
        public static decimal CalculerMoyenneEtudiant(int idEtudiant)
        {
            var notes = NoteDAL.ObtenirNotesParEtudiant(idEtudiant);

            if (notes == null || notes.Count == 0)
                return 0;

            // Obtenir les coefficients des matières
            var matieres = MatiereDAL.ObtenirToutesLesMatieres();

            // Calculer la moyenne pondérée avec LINQ
            var notesAvecCoeff = from n in notes
                                 join m in matieres on n.IdMatiere equals m.IdMatiere
                                 select new
                                 {
                                     Note = n.Valeur,
                                     Coefficient = m.Coefficient
                                 };

            if (!notesAvecCoeff.Any())
                return 0;

            decimal sommeNotesCoeff = notesAvecCoeff.Sum(nc => nc.Note * nc.Coefficient);
            int sommeCoeff = notesAvecCoeff.Sum(nc => nc.Coefficient);

            return sommeCoeff > 0 ? Math.Round(sommeNotesCoeff / sommeCoeff, 2) : 0;
        }

         
        /// Calculer la moyenne par matière pour un étudiant
        ///  
        public static Dictionary<string, decimal> CalculerMoyennesParMatiere(int idEtudiant)
        {
            var notes = NoteDAL.ObtenirNotesParEtudiant(idEtudiant);

            // Grouper par matière et calculer la moyenne avec LINQ
            var moyennesParMatiere = notes
                .GroupBy(n => n.NomMatiere)
                .ToDictionary(
                    g => g.Key,
                    g => Math.Round(g.Average(n => n.Valeur), 2)
                );

            return moyennesParMatiere;
        }

         
        /// Obtenir le classement général des étudiants (tri décroissant par moyenne)
        ///  
        public static List<EtudiantAvecMoyenne> ObtenirClassementGeneral()
        {
            var etudiants = EtudiantDAL.ObtenirTousLesEtudiants();
            var classement = new List<EtudiantAvecMoyenne>();

            foreach (var etudiant in etudiants)
            {
                classement.Add(new EtudiantAvecMoyenne
                {
                    Etudiant = etudiant,
                    Moyenne = CalculerMoyenneEtudiant(etudiant.IdEtudiant)
                });
            }

            // Tri décroissant par moyenne avec LINQ
            return classement.OrderByDescending(e => e.Moyenne).ToList();
        }

         
        /// Filtrer les étudiants ayant une moyenne >= seuil (exemple LINQ)
        ///  
        public static List<EtudiantAvecMoyenne> ObtenirEtudiantsParSeuil(decimal seuil)
        {
            var classement = ObtenirClassementGeneral();

            // Filtrage LINQ
            return classement.Where(e => e.Moyenne >= seuil).ToList();
        }

         
        /// Obtenir les statistiques globales
        ///  
        public static Dictionary<string, object> ObtenirStatistiques()
        {
            var classement = ObtenirClassementGeneral();

            if (classement.Count == 0)
            {
                return new Dictionary<string, object>
                {
                    { "NombreEtudiants", 0 },
                    { "MoyenneGenerale", 0m },
                    { "MoyennePlusHaute", 0m },
                    { "MoyennePlusBasse", 0m }
                };
            }

            // Calculs avec LINQ
            return new Dictionary<string, object>
            {
                { "NombreEtudiants", classement.Count },
                { "MoyenneGenerale", Math.Round(classement.Average(e => e.Moyenne), 2) },
                { "MoyennePlusHaute", classement.Max(e => e.Moyenne) },
                { "MoyennePlusBasse", classement.Min(e => e.Moyenne) },
                { "NombreReussis", classement.Count(e => e.Moyenne >= 10) },
                { "NombreEchecs", classement.Count(e => e.Moyenne < 10) }
            };
        }

         
        /// Valider les données d'un étudiant avant ajout/modification
        ///  
        public static void ValiderEtudiant(Etudiant etudiant)
        {
            if (string.IsNullOrWhiteSpace(etudiant.Nom))
                throw new ArgumentException("Le nom est obligatoire.");

            if (string.IsNullOrWhiteSpace(etudiant.Prenom))
                throw new ArgumentException("Le prénom est obligatoire.");

            if (etudiant.DateNaissance > DateTime.Now)
                throw new ArgumentException("La date de naissance ne peut pas être dans le futur.");

            if (etudiant.DateNaissance < DateTime.Now.AddYears(-100))
                throw new ArgumentException("La date de naissance n'est pas valide.");
        }

         
        /// Ajouter un étudiant avec validation
        ///  
        public static int AjouterEtudiant(Etudiant etudiant)
        {
            ValiderEtudiant(etudiant);
            return EtudiantDAL.AjouterEtudiant(etudiant);
        }

         
        /// Modifier un étudiant avec validation
        ///  
        public static bool ModifierEtudiant(Etudiant etudiant)
        {
            ValiderEtudiant(etudiant);
            return EtudiantDAL.ModifierEtudiant(etudiant);
        }
    }
}