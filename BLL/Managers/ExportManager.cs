using GestionEtudiantsApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Text;

// Alias pour éviter les conflits de noms
using iTextFont = iTextSharp.text.Font;
using iTextDocument = iTextSharp.text.Document;
using iTextImage = iTextSharp.text.Image;
using iTextRectangle = iTextSharp.text.Rectangle;

namespace GestionEtudiantsApp.BLL
{
    public static class ExportManager
    {
        #region Export CSV

 
        /// Exporter le bulletin d'un étudiant en CSV
 
        public static bool ExporterBulletinCSV(Etudiant etudiant, DataTable notes, decimal moyenne, string cheminFichier = null)
        {
            try
            {
                // Définir le chemin par défaut sur le bureau
                if (string.IsNullOrEmpty(cheminFichier))
                {
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string nomFichier = $"Bulletin_{etudiant.Nom}_{etudiant.Prenom}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    cheminFichier = Path.Combine(desktop, nomFichier);
                }

                StringBuilder csv = new StringBuilder();

                // En-tête du bulletin
                csv.AppendLine("BULLETIN DE NOTES");
                csv.AppendLine($"Étudiant;{etudiant.NomComplet}");
                csv.AppendLine($"Groupe;{etudiant.Groupe}");
                csv.AppendLine($"Date de naissance;{etudiant.DateNaissance:dd/MM/yyyy}");
                csv.AppendLine($"Date d'édition;{DateTime.Now:dd/MM/yyyy HH:mm}");
                csv.AppendLine();

                // En-têtes des colonnes
                csv.AppendLine("Matière;Coefficient;Note;Note Pondérée");

                // Lignes de notes
                foreach (DataRow row in notes.Rows)
                {
                    string matiere = row["Matiere"].ToString();
                    int coefficient = Convert.ToInt32(row["Coefficient"]);
                    decimal note = Convert.ToDecimal(row["MeilleureNote"]);
                    decimal notePonderee = Convert.ToDecimal(row["NotePonderee"]);

                    csv.AppendLine($"{matiere};{coefficient};{note:F2};{notePonderee:F2}");
                }

                // Moyenne générale
                csv.AppendLine();
                csv.AppendLine($"MOYENNE GÉNÉRALE;{moyenne:F2}/20");

                // Écriture du fichier
                File.WriteAllText(cheminFichier, csv.ToString(), Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'export CSV : {ex.Message}");
            }
        }

        #endregion

        #region Export PDF

 
        /// Exporter le bulletin d'un étudiant en PDF avec iTextSharp
 
        public static bool ExporterBulletinPDF(Etudiant etudiant, DataTable notes, decimal moyenne,
            string cheminFichier = null, string cheminLogo = null)
        {
            try
            {
                // Définir le chemin par défaut sur le bureau
                if (string.IsNullOrEmpty(cheminFichier))
                {
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string nomFichier = $"Bulletin_{etudiant.Nom}_{etudiant.Prenom}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    cheminFichier = Path.Combine(desktop, nomFichier);
                }

                // Création du document PDF
                iTextDocument document = new iTextDocument(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(cheminFichier, FileMode.Create));

                document.Open();

                // Polices
                iTextFont fontTitre = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                iTextFont fontSousTitre = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                iTextFont fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                iTextFont fontTableHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Logo de l'école (si fourni)
                if (!string.IsNullOrEmpty(cheminLogo) && File.Exists(cheminLogo))
                {
                    iTextImage logo = iTextImage.GetInstance(cheminLogo);
                    logo.ScaleToFit(100f, 100f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                    document.Add(new Paragraph("\n"));
                }

                // Titre principal
                Paragraph titre = new Paragraph("BULLETIN DE NOTES", fontTitre);
                titre.Alignment = Element.ALIGN_CENTER;
                document.Add(titre);
                document.Add(new Paragraph("\n"));

                // Informations de l'étudiant
                PdfPTable tableInfo = new PdfPTable(2);
                tableInfo.WidthPercentage = 100;
                tableInfo.SetWidths(new float[] { 1, 2 });

                AjouterCelluleInfo(tableInfo, "Nom complet :", etudiant.NomComplet, fontSousTitre, fontNormal);
                AjouterCelluleInfo(tableInfo, "Groupe :", etudiant.Groupe ?? "Non assigné", fontSousTitre, fontNormal);
                AjouterCelluleInfo(tableInfo, "Date de naissance :", etudiant.DateNaissance.ToShortDateString(), fontSousTitre, fontNormal);
                AjouterCelluleInfo(tableInfo, "Date d'édition :", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontSousTitre, fontNormal);

                document.Add(tableInfo);
                document.Add(new Paragraph("\n"));

                // Titre section notes
                Paragraph titreNotes = new Paragraph("Détail des notes", fontSousTitre);
                document.Add(titreNotes);
                document.Add(new Paragraph("\n"));

                // Table des notes
                PdfPTable tableNotes = new PdfPTable(4);
                tableNotes.WidthPercentage = 100;
                tableNotes.SetWidths(new float[] { 3, 1, 1.5f, 1.5f });

                // En-têtes
                AjouterCelluleEntete(tableNotes, "Matière", fontTableHeader);
                AjouterCelluleEntete(tableNotes, "Coef.", fontTableHeader);
                AjouterCelluleEntete(tableNotes, "Note", fontTableHeader);
                AjouterCelluleEntete(tableNotes, "Note Pond.", fontTableHeader);

                // Lignes de notes
                decimal totalPondere = 0;
                int totalCoef = 0;

                foreach (DataRow row in notes.Rows)
                {
                    string matiere = row["Matiere"].ToString();
                    int coefficient = Convert.ToInt32(row["Coefficient"]);
                    decimal note = Convert.ToDecimal(row["MeilleureNote"]);
                    decimal notePonderee = Convert.ToDecimal(row["NotePonderee"]);

                    totalCoef += coefficient;
                    totalPondere += notePonderee;

                    AjouterCelluleNormale(tableNotes, matiere, fontNormal, Element.ALIGN_LEFT);
                    AjouterCelluleNormale(tableNotes, coefficient.ToString(), fontNormal, Element.ALIGN_CENTER);
                    AjouterCelluleNormale(tableNotes, $"{note:F2}", fontNormal, Element.ALIGN_CENTER);
                    AjouterCelluleNormale(tableNotes, $"{notePonderee:F2}", fontNormal, Element.ALIGN_CENTER);
                }

                document.Add(tableNotes);
                document.Add(new Paragraph("\n"));

                // Moyenne générale avec mise en forme
                PdfPTable tableMoyenne = new PdfPTable(2);
                tableMoyenne.WidthPercentage = 50;
                tableMoyenne.HorizontalAlignment = Element.ALIGN_RIGHT;

                BaseColor couleurMoyenne = moyenne >= 14 ? BaseColor.GREEN :
                                          moyenne >= 10 ? new BaseColor(255, 165, 0) :
                                          BaseColor.RED;

                PdfPCell cellMoyenneLabel = new PdfPCell(new Phrase("MOYENNE GÉNÉRALE", fontSousTitre));
                cellMoyenneLabel.Border = iTextRectangle.NO_BORDER;
                cellMoyenneLabel.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellMoyenneLabel.Padding = 8;
                tableMoyenne.AddCell(cellMoyenneLabel);

                iTextFont fontMoyenne = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, couleurMoyenne);
                PdfPCell cellMoyenneValeur = new PdfPCell(new Phrase($"{moyenne:F2} / 20", fontMoyenne));
                cellMoyenneValeur.Border = iTextRectangle.BOX;
                cellMoyenneValeur.BorderWidth = 2;
                cellMoyenneValeur.BorderColor = couleurMoyenne;
                cellMoyenneValeur.HorizontalAlignment = Element.ALIGN_CENTER;
                cellMoyenneValeur.Padding = 10;
                tableMoyenne.AddCell(cellMoyenneValeur);

                document.Add(tableMoyenne);

                // Appréciation
                document.Add(new Paragraph("\n"));
                string appreciation = ObtenirAppreciation(moyenne);
                Paragraph para = new Paragraph($"Appréciation : {appreciation}", fontSousTitre);
                para.Alignment = Element.ALIGN_CENTER;
                document.Add(para);

                // Pied de page
                document.Add(new Paragraph("\n\n"));
                Paragraph piedPage = new Paragraph("Document généré automatiquement - Ne nécessite pas de signature",
                    FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, BaseColor.GRAY));
                piedPage.Alignment = Element.ALIGN_CENTER;
                document.Add(piedPage);

                document.Close();
                writer.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'export PDF : {ex.Message}");
            }
        }

 
        /// Générer un rapport PDF pour toute une classe
 
        public static bool ExporterRapportClasse(DataTable bulletins, string groupe, string cheminFichier = null)
        {
            try
            {
                if (string.IsNullOrEmpty(cheminFichier))
                {
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string nomFichier = $"Rapport_Classe_{groupe}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    cheminFichier = Path.Combine(desktop, nomFichier);
                }

                iTextDocument document = new iTextDocument(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(document, new FileStream(cheminFichier, FileMode.Create));
                document.Open();

                iTextFont fontTitre = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.DARK_GRAY);
                iTextFont fontTableHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                iTextFont fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                Paragraph titre = new Paragraph($"RAPPORT DE CLASSE - {groupe}", fontTitre);
                titre.Alignment = Element.ALIGN_CENTER;
                document.Add(titre);
                document.Add(new Paragraph($"Date : {DateTime.Now:dd/MM/yyyy}\n\n", fontNormal));

                // Table récapitulative
                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 3, 1.5f, 2 });

                AjouterCelluleEntete(table, "Étudiant", fontTableHeader);
                AjouterCelluleEntete(table, "Moyenne", fontTableHeader);
                AjouterCelluleEntete(table, "Appréciation", fontTableHeader);

                foreach (DataRow row in bulletins.Rows)
                {
                    string nomComplet = row["NomComplet"].ToString();
                    decimal moyenne = Convert.ToDecimal(row["Moyenne"]);
                    string appreciation = ObtenirAppreciation(moyenne);

                    AjouterCelluleNormale(table, nomComplet, fontNormal, Element.ALIGN_LEFT);
                    AjouterCelluleNormale(table, $"{moyenne:F2}", fontNormal, Element.ALIGN_CENTER);
                    AjouterCelluleNormale(table, appreciation, fontNormal, Element.ALIGN_LEFT);
                }

                document.Add(table);
                document.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'export du rapport de classe : {ex.Message}");
            }
        }

        #endregion

        #region Méthodes auxiliaires

        private static void AjouterCelluleInfo(PdfPTable table, string label, string valeur, iTextFont fontLabel, iTextFont fontValeur)
        {
            PdfPCell cellLabel = new PdfPCell(new Phrase(label, fontLabel));
            cellLabel.Border = iTextRectangle.NO_BORDER;
            cellLabel.Padding = 5;
            table.AddCell(cellLabel);

            PdfPCell cellValeur = new PdfPCell(new Phrase(valeur, fontValeur));
            cellValeur.Border = iTextRectangle.NO_BORDER;
            cellValeur.Padding = 5;
            table.AddCell(cellValeur);
        }

        private static void AjouterCelluleEntete(PdfPTable table, string texte, iTextFont font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texte, font));
            cell.BackgroundColor = new BaseColor(41, 128, 185);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        private static void AjouterCelluleNormale(PdfPTable table, string texte, iTextFont font, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texte, font));
            cell.HorizontalAlignment = alignment;
            cell.Padding = 5;
            table.AddCell(cell);
        }

        private static string ObtenirAppreciation(decimal moyenne)
        {
            if (moyenne >= 16) return "Excellent";
            if (moyenne >= 14) return "Très bien";
            if (moyenne >= 12) return "Bien";
            if (moyenne >= 10) return "Assez bien";
            if (moyenne >= 8) return "Passable";
            return "Insuffisant";
        }

        #endregion
    }
}