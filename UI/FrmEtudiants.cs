using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GestionEtudiantsApp.BLL.Managers;
using GestionEtudiantsApp.Models;
using GestionEtudiantsApp.DAL;

namespace UI
{
    public partial class FrmEtudiants : Form

    {
        private int idEtudiantSelectionne = 0;
        public FrmEtudiants()
        {
            InitializeComponent();
        }

        private void FrmEtudiants_Load(object sender, EventArgs e)
        {
            ChargerEtudiants();
        }

        private void ChargerEtudiants()
        {
            try
            {
                var etudiants = EtudiantDAL.ObtenirTousLesEtudiants();
                dgvEtudiants.DataSource = null;
                dgvEtudiants.DataSource = etudiants;

                // Masquer la colonne ID
                if (dgvEtudiants.Columns["IdEtudiant"] != null)
                    dgvEtudiants.Columns["IdEtudiant"].Visible = false;

                lblTotal.Text = $"Total: {etudiants.Count} étudiant(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderChamps()) return;

                var etudiant = new Etudiant
                {
                    Nom = txtNom.Text.Trim(),
                    Prenom = txtPrenom.Text.Trim(),
                    DateNaissance = dtpDateNaissance.Value,
                    Groupe = txtGroupe.Text.Trim()
                };

                int id = EtudiantManager.AjouterEtudiant(etudiant);
                MessageBox.Show("Étudiant ajouté avec succès!", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChargerEtudiants();
                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEtudiantSelectionne == 0)
                {
                    MessageBox.Show("Sélectionnez un étudiant à modifier.", "Attention",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValiderChamps()) return;

                var etudiant = new Etudiant
                {
                    IdEtudiant = idEtudiantSelectionne,
                    Nom = txtNom.Text.Trim(),
                    Prenom = txtPrenom.Text.Trim(),
                    DateNaissance = dtpDateNaissance.Value,
                    Groupe = txtGroupe.Text.Trim()
                };

                EtudiantManager.ModifierEtudiant(etudiant);
                MessageBox.Show("Étudiant modifié avec succès!", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChargerEtudiants();
                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEtudiantSelectionne == 0)
                {
                    MessageBox.Show("Sélectionnez un étudiant à supprimer.", "Attention",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmation = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet étudiant?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmation == DialogResult.Yes)
                {
                    EtudiantDAL.SupprimerEtudiant(idEtudiantSelectionne);
                    MessageBox.Show("Étudiant supprimé avec succès!", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ChargerEtudiants();
                    ViderChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEtudiants_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEtudiants.CurrentRow != null && dgvEtudiants.CurrentRow.Index >= 0)
            {
                var row = dgvEtudiants.CurrentRow;
                idEtudiantSelectionne = Convert.ToInt32(row.Cells["IdEtudiant"].Value);
                txtNom.Text = row.Cells["Nom"].Value.ToString();
                txtPrenom.Text = row.Cells["Prenom"].Value.ToString();
                dtpDateNaissance.Value = Convert.ToDateTime(row.Cells["DateNaissance"].Value);
                txtGroupe.Text = row.Cells["Groupe"].Value?.ToString() ?? "";
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRecherche.Text))
                {
                    ChargerEtudiants();
                    return;
                }

                var etudiants = EtudiantDAL.RechercherEtudiants(txtRecherche.Text.Trim());
                dgvEtudiants.DataSource = null;
                dgvEtudiants.DataSource = etudiants;

                lblTotal.Text = $"Résultats: {etudiants.Count} étudiant(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
        }

        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Le prénom est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrenom.Focus();
                return false;
            }

            if (dtpDateNaissance.Value > DateTime.Now)
            {
                MessageBox.Show("La date de naissance ne peut pas être dans le futur.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDateNaissance.Focus();
                return false;
            }

            return true;
        }

        private void ViderChamps()
        {
            idEtudiantSelectionne = 0;
            txtNom.Clear();
            txtPrenom.Clear();
            dtpDateNaissance.Value = DateTime.Now.AddYears(-18);
            txtGroupe.Clear();
            txtRecherche.Clear();
        }
        private void InitializeComponent()
        {
            dgvEtudiants = new DataGridView();
            txtNom = new TextBox();
            txtPrenom = new TextBox();
            txtGroupe = new TextBox();
            txtRecherche = new TextBox();
            dtpDateNaissance = new DateTimePicker();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnNouveau = new Button();
            btnRechercher = new Button();
            lblNom = new Label();
            lblPrenom = new Label();
            lblDateNaissance = new Label();
            lblGroupe = new Label();
            lblRecherche = new Label();
            lblTotal = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((ISupportInitialize)dgvEtudiants).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvEtudiants
            // 
            dgvEtudiants.AllowUserToAddRows = false;
            dgvEtudiants.AllowUserToDeleteRows = false;
            dgvEtudiants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEtudiants.Location = new Point(20, 65);
            dgvEtudiants.Name = "dgvEtudiants";
            dgvEtudiants.ReadOnly = true;
            dgvEtudiants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEtudiants.Size = new Size(620, 440);
            dgvEtudiants.TabIndex = 0;
            dgvEtudiants.SelectionChanged += dgvEtudiants_SelectionChanged;
            // 
            // txtNom
            // 
            txtNom.Location = new Point(20, 60);
            txtNom.Name = "txtNom";
            txtNom.Size = new Size(260, 23);
            txtNom.TabIndex = 1;
            // 
            // txtPrenom
            // 
            txtPrenom.Location = new Point(20, 120);
            txtPrenom.Name = "txtPrenom";
            txtPrenom.Size = new Size(260, 23);
            txtPrenom.TabIndex = 3;
            // 
            // txtGroupe
            // 
            txtGroupe.Location = new Point(20, 240);
            txtGroupe.Name = "txtGroupe";
            txtGroupe.Size = new Size(260, 23);
            txtGroupe.TabIndex = 7;
            // 
            // txtRecherche
            // 
            txtRecherche.Location = new Point(100, 27);
            txtRecherche.Name = "txtRecherche";
            txtRecherche.Size = new Size(400, 23);
            txtRecherche.TabIndex = 1;
            // 
            // dtpDateNaissance
            // 
            dtpDateNaissance.Format = DateTimePickerFormat.Short;
            dtpDateNaissance.Location = new Point(20, 180);
            dtpDateNaissance.Name = "dtpDateNaissance";
            dtpDateNaissance.Size = new Size(260, 23);
            dtpDateNaissance.TabIndex = 5;
            // 
            // btnAjouter
            // 
            btnAjouter.BackColor = Color.LightGreen;
            btnAjouter.Location = new Point(20, 290);
            btnAjouter.Name = "btnAjouter";
            btnAjouter.Size = new Size(260, 40);
            btnAjouter.TabIndex = 8;
            btnAjouter.Text = "➕ Ajouter";
            btnAjouter.UseVisualStyleBackColor = false;
            btnAjouter.Click += btnAjouter_Click;
            // 
            // btnModifier
            // 
            btnModifier.BackColor = Color.LightBlue;
            btnModifier.Location = new Point(20, 340);
            btnModifier.Name = "btnModifier";
            btnModifier.Size = new Size(260, 40);
            btnModifier.TabIndex = 9;
            btnModifier.Text = "✏️ Modifier";
            btnModifier.UseVisualStyleBackColor = false;
            btnModifier.Click += btnModifier_Click;
            // 
            // btnSupprimer
            // 
            btnSupprimer.BackColor = Color.LightCoral;
            btnSupprimer.Location = new Point(20, 390);
            btnSupprimer.Name = "btnSupprimer";
            btnSupprimer.Size = new Size(260, 40);
            btnSupprimer.TabIndex = 10;
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.UseVisualStyleBackColor = false;
            btnSupprimer.Click += btnSupprimer_Click;
            // 
            // btnNouveau
            // 
            btnNouveau.Location = new Point(20, 440);
            btnNouveau.Name = "btnNouveau";
            btnNouveau.Size = new Size(260, 40);
            btnNouveau.TabIndex = 11;
            btnNouveau.Text = "🆕 Nouveau";
            btnNouveau.UseVisualStyleBackColor = true;
            btnNouveau.Click += btnNouveau_Click;
            // 
            // btnRechercher
            // 
            btnRechercher.Location = new Point(510, 25);
            btnRechercher.Name = "btnRechercher";
            btnRechercher.Size = new Size(130, 25);
            btnRechercher.TabIndex = 2;
            btnRechercher.Text = "🔍 Rechercher";
            btnRechercher.UseVisualStyleBackColor = true;
            btnRechercher.Click += btnRechercher_Click;
            // 
            // lblNom
            // 
            lblNom.AutoSize = true;
            lblNom.Location = new Point(20, 40);
            lblNom.Name = "lblNom";
            lblNom.Size = new Size(37, 15);
            lblNom.TabIndex = 0;
            lblNom.Text = "Nom:";
            // 
            // lblPrenom
            // 
            lblPrenom.AutoSize = true;
            lblPrenom.Location = new Point(20, 100);
            lblPrenom.Name = "lblPrenom";
            lblPrenom.Size = new Size(52, 15);
            lblPrenom.TabIndex = 2;
            lblPrenom.Text = "Prénom:";
            // 
            // lblDateNaissance
            // 
            lblDateNaissance.AutoSize = true;
            lblDateNaissance.Location = new Point(20, 160);
            lblDateNaissance.Name = "lblDateNaissance";
            lblDateNaissance.Size = new Size(104, 15);
            lblDateNaissance.TabIndex = 4;
            lblDateNaissance.Text = "Date de naissance:";
            // 
            // lblGroupe
            // 
            lblGroupe.AutoSize = true;
            lblGroupe.Location = new Point(20, 220);
            lblGroupe.Name = "lblGroupe";
            lblGroupe.Size = new Size(49, 15);
            lblGroupe.TabIndex = 6;
            lblGroupe.Text = "Groupe:";
            // 
            // lblRecherche
            // 
            lblRecherche.AutoSize = true;
            lblRecherche.Location = new Point(20, 30);
            lblRecherche.Name = "lblRecherche";
            lblRecherche.Size = new Size(65, 15);
            lblRecherche.TabIndex = 0;
            lblRecherche.Text = "Recherche:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(20, 515);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(128, 15);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "Total: 0 étudiant(s)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblNom);
            groupBox1.Controls.Add(txtNom);
            groupBox1.Controls.Add(lblPrenom);
            groupBox1.Controls.Add(txtPrenom);
            groupBox1.Controls.Add(lblDateNaissance);
            groupBox1.Controls.Add(dtpDateNaissance);
            groupBox1.Controls.Add(lblGroupe);
            groupBox1.Controls.Add(txtGroupe);
            groupBox1.Controls.Add(btnAjouter);
            groupBox1.Controls.Add(btnModifier);
            groupBox1.Controls.Add(btnSupprimer);
            groupBox1.Controls.Add(btnNouveau);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 550);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Informations Étudiant";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblRecherche);
            groupBox2.Controls.Add(txtRecherche);
            groupBox2.Controls.Add(btnRechercher);
            groupBox2.Controls.Add(dgvEtudiants);
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Location = new Point(320, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(660, 550);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Liste des Étudiants";
            // 
            // FrmEtudiants
            // 
            ClientSize = new Size(1000, 600);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "FrmEtudiants";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestion des Étudiants";
            Load += FrmEtudiants_Load;
            ((ISupportInitialize)dgvEtudiants).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);

        }
    }
}
