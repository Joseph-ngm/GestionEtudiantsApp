using System;
using System.Data;
using System.Data.SqlClient;

namespace GestionEtudiantsApp.DAL
{
    public static class BulletinDAL
    {
        /// <summary>
        /// Obtenir le détail des matières avec les MEILLEURES notes et coefficients
        /// </summary>
        public static DataTable GetDetailsBulletin(int idEtudiant)
        {
            string query = @"
                SELECT 
                    m.NomMatiere AS Matiere,
                    m.Coefficient,
                    MAX(n.Note) AS MeilleureNote,
                    MAX(n.Note) * m.Coefficient AS NotePonderee
                FROM Notes n
                INNER JOIN Matieres m ON n.IdMatiere = m.IdMatiere
                WHERE n.IdEtudiant = @id
                GROUP BY m.NomMatiere, m.Coefficient
                ORDER BY m.NomMatiere";

            return DatabaseHelper.ExecuteDataTable(query,
                new SqlParameter("@id", idEtudiant));
        }

        /// <summary>
        /// Calculer la moyenne pondérée générale d'un étudiant
        /// </summary>
        public static decimal GetMoyenneGenerale(int idEtudiant)
        {
            string query = @"
                SELECT 
                    SUM(best.MeilleureNote * m.Coefficient) AS TotalPondere,
                    SUM(m.Coefficient) AS TotalCoef
                FROM Matieres m
                INNER JOIN (
                    SELECT IdMatiere, MAX(Note) AS MeilleureNote
                    FROM Notes
                    WHERE IdEtudiant = @id
                    GROUP BY IdMatiere
                ) best ON best.IdMatiere = m.IdMatiere";

            DataTable dt = DatabaseHelper.ExecuteDataTable(query,
                new SqlParameter("@id", idEtudiant));

            if (dt.Rows.Count == 0 || dt.Rows[0]["TotalPondere"] == DBNull.Value)
                return 0;

            decimal total = Convert.ToDecimal(dt.Rows[0]["TotalPondere"]);
            decimal coef = Convert.ToDecimal(dt.Rows[0]["TotalCoef"]);

            if (coef == 0) return 0;

            return Math.Round(total / coef, 2);
        }

        /// <summary>
        /// Récupérer les informations complètes d'un étudiant
        /// </summary>
        public static DataRow GetEtudiantInfo(int idEtudiant)
        {
            string query = @"
                SELECT IdEtudiant, Nom, Prenom, Groupe, DateNaissance
                FROM Etudiants
                WHERE IdEtudiant = @id";

            DataTable dt = DatabaseHelper.ExecuteDataTable(query,
                new SqlParameter("@id", idEtudiant));

            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }

        /// <summary>
        /// Obtenir la liste de tous les étudiants
        /// </summary>
        public static DataTable GetAllEtudiants()
        {
            string query = @"
                SELECT IdEtudiant, Nom, Prenom, Groupe, DateNaissance
                FROM Etudiants
                ORDER BY Nom, Prenom";

            return DatabaseHelper.ExecuteDataTable(query);
        }
    }
}