using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionEtudiantsApp.Models;

namespace GestionEtudiantsApp.DAL
{
    public static class MatiereDAL
    {
        public static List<Matiere> ObtenirToutesLesMatieres()
        {
            List<Matiere> liste = new List<Matiere>();

            string query = "SELECT IdMatiere, NomMatiere, Coefficient FROM Matieres";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    liste.Add(new Matiere
                    {
                        IdMatiere = (int)reader["IdMatiere"],
                        NomMatiere = reader["NomMatiere"].ToString(),
                        Coefficient = (int)reader["Coefficient"]
                    });
                }
            }
            return liste;
        }

        public static void AjouterMatiere(Matiere m)
        {
            string query = "INSERT INTO Matieres (NomMatiere, Coefficient) VALUES (@n, @c)";

            SqlParameter[] p =
            {
                new SqlParameter("@n", m.NomMatiere),
                new SqlParameter("@c", m.Coefficient)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }

        public static void ModifierMatiere(Matiere m)
        {
            string query = @"UPDATE Matieres 
                             SET NomMatiere=@n, Coefficient=@c 
                             WHERE IdMatiere=@id";

            SqlParameter[] p =
            {
                new SqlParameter("@n", m.NomMatiere),
                new SqlParameter("@c", m.Coefficient),
                new SqlParameter("@id", m.IdMatiere)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }

        public static void SupprimerMatiere(int id)
        {
            string query = "DELETE FROM Matieres WHERE IdMatiere=@id";

            SqlParameter[] p =
            {
                new SqlParameter("@id", id)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }

        public static List<Matiere> RechercherMatieres(string motCle)
        {
            List<Matiere> liste = new List<Matiere>();

            string query = @"SELECT IdMatiere, NomMatiere, Coefficient 
                             FROM Matieres
                             WHERE NomMatiere LIKE '%' + @mot + '%'";

            SqlParameter[] p =
            {
                new SqlParameter("@mot", motCle)
            };

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(p);
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    liste.Add(new Matiere
                    {
                        IdMatiere = (int)reader["IdMatiere"],
                        NomMatiere = reader["NomMatiere"].ToString(),
                        Coefficient = (int)reader["Coefficient"]
                    });
                }
            }

            return liste;
        }
    }
}
