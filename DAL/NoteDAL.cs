using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionEtudiantsApp.Models;

namespace GestionEtudiantsApp.DAL
{
    public static class NoteDAL
    {
        // Obtenir toutes les notes (jointure)
        public static List<Note> ObtenirToutesLesNotes()
        {
            List<Note> notes = new List<Note>();

            string query = @"
                SELECT n.IdNote, n.IdEtudiant, n.IdMatiere, n.Note,
                       e.Nom + ' ' + e.Prenom AS NomEtudiant,
                       m.NomMatiere
                FROM Notes n
                JOIN Etudiants e ON n.IdEtudiant = e.IdEtudiant
                JOIN Matieres m ON n.IdMatiere = m.IdMatiere";

            using SqlConnection conn = DatabaseHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                notes.Add(new Note
                {
                    IdNote = (int)reader["IdNote"],
                    IdEtudiant = (int)reader["IdEtudiant"],
                    IdMatiere = (int)reader["IdMatiere"],
                    Valeur = Convert.ToDecimal(reader["Note"]),
                    NomEtudiant = reader["NomEtudiant"].ToString(),
                    NomMatiere = reader["NomMatiere"].ToString()
                });
            }

            return notes;
        }

        // Ajouter
        public static void AjouterNote(Note n)
        {
            string query = @"
                INSERT INTO Notes (IdEtudiant, IdMatiere, Note)
                VALUES (@e, @m, @n)";
            
            SqlParameter[] p =
            {
                new SqlParameter("@e", n.IdEtudiant),
                new SqlParameter("@m", n.IdMatiere),
                new SqlParameter("@n", n.Valeur)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }

        // Modifier
        public static void ModifierNote(Note n)
        {
            string query = @"
                UPDATE Notes
                SET Note = @n
                WHERE IdNote = @id";

            SqlParameter[] p =
            {
                new SqlParameter("@n", n.Valeur),
                new SqlParameter("@id", n.IdNote)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }

        // Supprimer
        public static void SupprimerNote(int id)
        {
            string query = "DELETE FROM Notes WHERE IdNote=@id";

            SqlParameter[] p =
            {
                new SqlParameter("@id", id)
            };

            DatabaseHelper.ExecuteNonQuery(query, p);
        }
        /// Obtenir les notes d'un étudiant spécifique
        ///  
        public static List<Note> ObtenirNotesParEtudiant(int idEtudiant)
        {
            List<Note> liste = new List<Note>();
            string query = @"SELECT n.*, m.NomMatiere, m.Coefficient
                           FROM Notes n
                           INNER JOIN Matieres m ON n.IdMatiere = m.IdMatiere
                           WHERE n.IdEtudiant = @IdEtudiant
                           ORDER BY m.NomMatiere";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEtudiant", idEtudiant);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            liste.Add(new Note
                            {
                                IdNote = (int)reader["IdNote"],
                                IdEtudiant = (int)reader["IdEtudiant"],
                                IdMatiere = (int)reader["IdMatiere"],
                                Valeur = (decimal)reader["Note"],
                                Session = reader["Session"].ToString(),
                                NomMatiere = reader["NomMatiere"].ToString()
                            });
                        }
                    }
                }
            }
            return liste;
        }


        // Recherche par étudiant
        public static List<Note> RechercherNotes(string motCle)
        {
            List<Note> notes = new List<Note>();

            string query = @"
                SELECT n.IdNote, n.IdEtudiant, n.IdMatiere, n.Note,
                       e.Nom + ' ' + e.Prenom AS NomEtudiant,
                       m.NomMatiere
                FROM Notes n
                JOIN Etudiants e ON n.IdEtudiant = e.IdEtudiant
                JOIN Matieres m ON n.IdMatiere = m.IdMatiere
                WHERE e.Nom LIKE '%' + @mot + '%'
                   OR e.Prenom LIKE '%' + @mot + '%'
                   OR m.NomMatiere LIKE '%' + @mot + '%'";

            SqlParameter[] p =
            {
                new SqlParameter("@mot", motCle)
            };

            using SqlConnection conn = DatabaseHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddRange(p);
            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                notes.Add(new Note
                {
                    IdNote = (int)reader["IdNote"],
                    IdEtudiant = (int)reader["IdEtudiant"],
                    IdMatiere = (int)reader["IdMatiere"],
                    Valeur = Convert.ToDecimal(reader["Note"]),
                    NomEtudiant = reader["NomEtudiant"].ToString(),
                    NomMatiere = reader["NomMatiere"].ToString()
                });
            }

            return notes;
        }
    }
}
