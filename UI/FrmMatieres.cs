using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionEtudiantsApp.Models;
using GestionEtudiantsApp.BLL.Managers;
using GestionEtudiantsApp.DAL;

namespace UI
{
    public partial class FrmMatieres : Form
    {
        private int idMatiereSelectionnee = 0;

        public FrmMatieres()
        {
            InitializeComponent();
        }

        private void FrmMatieres_Load(object sender, EventArgs e)
        {
            ChargerMatieres();
        }

        private void ChargerMatieres()
        {
            try
            {
                var matieres = MatiereDAL.ObtenirToutesLesMatieres();

                dgvMatieres.DataSource = null;
                dgvMatieres.DataSource = matieres;

                if (dgvMatieres.Columns["IdMatiere"] != null)
                    dgvMatieres.Columns["IdMatiere"].Visible = false;

                lblTotalMatieres.Text = $"Total : {matieres.Count} matière(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMatieres_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMatieres.CurrentRow == null) return;

            var row = dgvMatieres.CurrentRow;
            idMatiereSelectionnee = Convert.ToInt32(row.Cells["IdMatiere"].Value);

            txtNomMatiere.Text = row.Cells["NomMatiere"].Value.ToString();
            numCoefficient.Value = Convert.ToDecimal(row.Cells["Coefficient"].Value);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (!Valider()) return;

            var matiere = new Matiere
            {
                NomMatiere = txtNomMatiere.Text.Trim(),
                Coefficient = (int)numCoefficient.Value
            };

            MatiereManager.AjouterMatiere(matiere);

            MessageBox.Show("Matière ajoutée avec succès!", "Succès");
            ChargerMatieres();
            ViderChamps();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (idMatiereSelectionnee == 0)
            {
                MessageBox.Show("Sélectionnez une matière.", "Attention");
                return;
            }

            if (!Valider()) return;

            var matiere = new Matiere
            {
                IdMatiere = idMatiereSelectionnee,
                NomMatiere = txtNomMatiere.Text.Trim(),
                Coefficient = (int)numCoefficient.Value
            };

            MatiereManager.ModifierMatiere(matiere);

            MessageBox.Show("Modifié avec succès!", "Succès");
            ChargerMatieres();
            ViderChamps();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (idMatiereSelectionnee == 0)
            {
                MessageBox.Show("Sélectionnez une matière.", "Attention");
                return;
            }

            var confirm = MessageBox.Show("Supprimer cette matière ?", "Confirmation",
                MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                MatiereDAL.SupprimerMatiere(idMatiereSelectionnee);
                MessageBox.Show("Supprimée!", "Succès");
                ChargerMatieres();
                ViderChamps();
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            string motCle = txtRecherche.Text.Trim();

            if (string.IsNullOrWhiteSpace(motCle))
            {
                ChargerMatieres();
                return;
            }

            var results = MatiereDAL.RechercherMatieres(motCle);
            dgvMatieres.DataSource = results;

            lblTotalMatieres.Text = $"Résultats : {results.Count} matière(s)";
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
        }

        private bool Valider()
        {
            if (string.IsNullOrWhiteSpace(txtNomMatiere.Text))
            {
                MessageBox.Show("Le nom de la matière est obligatoire.");
                return false;
            }

            if (numCoefficient.Value < 1)
            {
                MessageBox.Show("Le coefficient doit être ≥ 1.");
                return false;
            }

            return true;
        }

        private void ViderChamps()
        {
            idMatiereSelectionnee = 0;
            txtNomMatiere.Clear();
            numCoefficient.Value = 1;
            txtRecherche.Clear();
        }
    }
}
