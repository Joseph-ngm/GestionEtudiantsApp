using GestionEtudiantsApp.BLL;
using GestionEtudiantsApp.Models;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmBulletin : Form
    {
        private string cheminLogoEcole = null;

        public FrmBulletin()
        {
            InitializeComponent();
        }

        private void FrmBulletin_Load(object sender, EventArgs e)
        {
            ChargerEtudiants();
            ConfigurerDataGridView();
            ConfigurerComboBoxSession();
        }

        #region Chargement des données

        private void ChargerEtudiants()
        {
            try
            {
                var etudiants = BulletinManager.GetAllEtudiants();

                if (etudiants.Count == 0)
                {
                    MessageBox.Show("Aucun étudiant trouvé dans la base de données.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmbEtudiants.DataSource = etudiants;
                cmbEtudiants.DisplayMember = "NomComplet";
                cmbEtudiants.ValueMember = "IdEtudiant";
                cmbEtudiants.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des étudiants :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurerComboBoxSession()
        {
            cmbSession.Items.Add("Toutes");
            cmbSession.Items.Add("Normale");
            cmbSession.Items.Add("Rattrapage");
            cmbSession.SelectedIndex = 0;
        }

        #endregion

        #region Configuration de l'interface

        private void ConfigurerDataGridView()
        {
            dgvNotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotes.ReadOnly = true;
            dgvNotes.AllowUserToAddRows = false;
            dgvNotes.BackgroundColor = Color.White;
            dgvNotes.BorderStyle = BorderStyle.Fixed3D;

            dgvNotes.EnableHeadersVisualStyles = false;
            dgvNotes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvNotes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNotes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvNotes.ColumnHeadersHeight = 35;
        }

        #endregion

        #region Affichage du bulletin

        private void cmbEtudiants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEtudiants.SelectedIndex >= 0 && cmbEtudiants.SelectedValue != null)
            {
                try
                {
                    AfficherBulletin();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'affichage du bulletin :\n{ex.Message}",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEtudiants.SelectedIndex >= 0)
            {
                AfficherBulletin();
            }
        }

        private void AfficherBulletin()
        {
            if (!(cmbEtudiants.SelectedValue is int))
                return;

            int idEtudiant = (int)cmbEtudiants.SelectedValue;

            // 1. Charger les informations de l'étudiant
            Etudiant etu = BulletinManager.GetEtudiantById(idEtudiant);
            lblNom.Text = etu.NomComplet;
            lblGroupe.Text = !string.IsNullOrEmpty(etu.Groupe) ? etu.Groupe : "Non assigné";
            lblDateNaissance.Text = etu.DateNaissance != DateTime.MinValue
                ? etu.DateNaissance.ToShortDateString()
                : "Non renseignée";

            // 2. Charger les notes
            DataTable notes = BulletinManager.GetBulletinData(idEtudiant);

            // Filtrer par session si nécessaire
            if (cmbSession.SelectedIndex > 0)
            {
                string sessionSelectionnee = cmbSession.SelectedItem.ToString();
                DataView dv = notes.DefaultView;
                dv.RowFilter = $"Session = '{sessionSelectionnee}'";
                notes = dv.ToTable();
            }

            dgvNotes.DataSource = notes;

            PersonnaliserColonnes();

            // 3. Calculer et afficher la moyenne
            if (notes.Rows.Count > 0)
            {
                decimal moyenne = BulletinManager.CalculerMoyenne(notes);
                AfficherMoyenne(moyenne);
                AfficherStatistiques(notes, moyenne);
            }
            else
            {
                lblMoyenne.Text = "Aucune note";
                lblMoyenne.ForeColor = Color.Gray;
                lblStatistiques.Text = "Aucune donnée disponible";
            }
        }

        private void PersonnaliserColonnes()
        {
            if (dgvNotes.Columns.Count == 0) return;

            if (dgvNotes.Columns["Matiere"] != null)
                dgvNotes.Columns["Matiere"].HeaderText = "Matière";

            if (dgvNotes.Columns["Coefficient"] != null)
            {
                dgvNotes.Columns["Coefficient"].HeaderText = "Coef.";
                dgvNotes.Columns["Coefficient"].Width = 60;
            }

            if (dgvNotes.Columns["MeilleureNote"] != null)
            {
                dgvNotes.Columns["MeilleureNote"].HeaderText = "Note";
                dgvNotes.Columns["MeilleureNote"].DefaultCellStyle.Format = "0.00";
                dgvNotes.Columns["MeilleureNote"].Width = 80;
            }

            if (dgvNotes.Columns["NotePonderee"] != null)
            {
                dgvNotes.Columns["NotePonderee"].HeaderText = "Note pondérée";
                dgvNotes.Columns["NotePonderee"].DefaultCellStyle.Format = "0.00";
                dgvNotes.Columns["NotePonderee"].Width = 120;
            }

            if (dgvNotes.Columns["Session"] != null)
            {
                dgvNotes.Columns["Session"].HeaderText = "Session";
                dgvNotes.Columns["Session"].Width = 100;
            }
        }

        private void AfficherMoyenne(decimal moyenne)
        {
            lblMoyenne.Text = $"{moyenne:F2} / 20";

            if (moyenne >= 14)
            {
                lblMoyenne.ForeColor = Color.Green;
                lblMoyenne.Font = new Font(lblMoyenne.Font.FontFamily, 16, FontStyle.Bold);
            }
            else if (moyenne >= 10)
            {
                lblMoyenne.ForeColor = Color.Orange;
                lblMoyenne.Font = new Font(lblMoyenne.Font.FontFamily, 16, FontStyle.Bold);
            }
            else
            {
                lblMoyenne.ForeColor = Color.Red;
                lblMoyenne.Font = new Font(lblMoyenne.Font.FontFamily, 16, FontStyle.Bold);
            }
        }

        private void AfficherStatistiques(DataTable notes, decimal moyenne)
        {
            decimal noteMax = 0;
            decimal noteMin = 20;
            string matiereMax = "";
            string matiereMin = "";

            foreach (DataRow row in notes.Rows)
            {
                decimal note = Convert.ToDecimal(row["MeilleureNote"]);
                string matiere = row["Matiere"].ToString();

                if (note > noteMax)
                {
                    noteMax = note;
                    matiereMax = matiere;
                }

                if (note < noteMin)
                {
                    noteMin = note;
                    matiereMin = matiere;
                }
            }

            lblStatistiques.Text = $"Statistiques:\n" +
                                  $"Nombre de matières: {notes.Rows.Count}\n" +
                                  $"Meilleure note: {noteMax:F2} ({matiereMax})\n" +
                                  $"Note la plus basse: {noteMin:F2} ({matiereMin})\n" +
                                  $"Appréciation: {ObtenirAppreciation(moyenne)}";
        }

        private string ObtenirAppreciation(decimal moyenne)
        {
            if (moyenne >= 16) return "Excellent";
            if (moyenne >= 14) return "Très bien";
            if (moyenne >= 12) return "Bien";
            if (moyenne >= 10) return "Assez bien";
            if (moyenne >= 8) return "Passable";
            return "Insuffisant";
        }

        #endregion

        #region Exports

        private void btnExporterCSV_Click(object sender, EventArgs e)
        {
            if (cmbEtudiants.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant.",
                    "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idEtudiant = (int)cmbEtudiants.SelectedValue;
                Etudiant etu = BulletinManager.GetEtudiantById(idEtudiant);
                DataTable notes = BulletinManager.GetBulletinData(idEtudiant);
                decimal moyenne = BulletinManager.CalculerMoyenne(notes);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Fichiers CSV (*.csv)|*.csv";
                sfd.FileName = $"Bulletin_{etu.Nom}_{etu.Prenom}_{DateTime.Now:yyyyMMdd}.csv";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool succes = ExportManager.ExporterBulletinCSV(etu, notes, moyenne, sfd.FileName);

                    if (succes)
                    {
                        MessageBox.Show($"Bulletin exporté avec succès !\n\n{sfd.FileName}",
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (MessageBox.Show("Voulez-vous ouvrir le fichier ?", "Ouvrir",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export CSV :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExporterPDF_Click(object sender, EventArgs e)
        {
            if (cmbEtudiants.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant.",
                    "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idEtudiant = (int)cmbEtudiants.SelectedValue;
                Etudiant etu = BulletinManager.GetEtudiantById(idEtudiant);
                DataTable notes = BulletinManager.GetBulletinData(idEtudiant);
                decimal moyenne = BulletinManager.CalculerMoyenne(notes);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Fichiers PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Bulletin_{etu.Nom}_{etu.Prenom}_{DateTime.Now:yyyyMMdd}.pdf";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool succes = ExportManager.ExporterBulletinPDF(etu, notes, moyenne,
                        sfd.FileName, cheminLogoEcole);

                    if (succes)
                    {
                        MessageBox.Show($"Bulletin PDF exporté avec succès !\n\n{sfd.FileName}",
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (MessageBox.Show("Voulez-vous ouvrir le PDF ?", "Ouvrir",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export PDF :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChargerLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Sélectionner le logo de l'école";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                cheminLogoEcole = ofd.FileName;
                MessageBox.Show("Logo chargé avec succès !\nIl sera inclus dans les exports PDF.",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRapportClasse_Click(object sender, EventArgs e)
        {
            try
            {
                var etudiants = BulletinManager.GetAllEtudiants();

                if (etudiants.Count == 0)
                {
                    MessageBox.Show("Aucun étudiant trouvé.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable rapport = new DataTable();
                rapport.Columns.Add("NomComplet", typeof(string));
                rapport.Columns.Add("Moyenne", typeof(decimal));

                foreach (var etu in etudiants)
                {
                    decimal moyenne = BulletinManager.GetMoyenneGenerale(etu.IdEtudiant);
                    rapport.Rows.Add(etu.NomComplet, moyenne);
                }

                DataView dv = rapport.DefaultView;
                dv.Sort = "Moyenne DESC";
                DataTable rapportTrie = dv.ToTable();

                string groupe = etudiants.FirstOrDefault()?.Groupe ?? "Tous";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Fichiers PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Rapport_Classe_{groupe}_{DateTime.Now:yyyyMMdd}.pdf";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool succes = ExportManager.ExporterRapportClasse(rapportTrie, groupe, sfd.FileName);

                    if (succes)
                    {
                        MessageBox.Show($"Rapport de classe généré avec succès !\n\n{sfd.FileName}",
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (MessageBox.Show("Voulez-vous ouvrir le PDF ?", "Ouvrir",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la génération du rapport :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Autres actions

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ChargerEtudiants();
            if (cmbEtudiants.SelectedIndex >= 0)
            {
                AfficherBulletin();
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            if (cmbEtudiants.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant.",
                    "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idEtudiant = (int)cmbEtudiants.SelectedValue;
                Etudiant etu = BulletinManager.GetEtudiantById(idEtudiant);
                DataTable notes = BulletinManager.GetBulletinData(idEtudiant);
                decimal moyenne = BulletinManager.CalculerMoyenne(notes);

                string tempPdf = Path.Combine(Path.GetTempPath(), $"Bulletin_Temp_{DateTime.Now.Ticks}.pdf");

                bool succes = ExportManager.ExporterBulletinPDF(etu, notes, moyenne, tempPdf, cheminLogoEcole);

                if (succes)
                {
                    System.Diagnostics.Process.Start(tempPdf);
                    MessageBox.Show("Le bulletin a été ouvert. Utilisez Ctrl+P pour l'imprimer.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'impression :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}