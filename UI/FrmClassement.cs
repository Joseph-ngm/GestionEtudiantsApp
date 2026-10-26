using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GestionEtudiantsApp.BLL.Managers;

namespace UI
{
    public partial class FrmClassement : Form
    {
        public FrmClassement()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.dgvClassement = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMoyenneGenerale = new System.Windows.Forms.Label();
            this.lblMeilleure = new System.Windows.Forms.Label();
            this.lblPlusBasse = new System.Windows.Forms.Label();
            this.lblReussis = new System.Windows.Forms.Label();
            this.lblEchecs = new System.Windows.Forms.Label();
            this.txtSeuil = new System.Windows.Forms.TextBox();
            this.btnFiltrer = new System.Windows.Forms.Button();
            this.btnActualiser = new System.Windows.Forms.Button();
            this.lblSeuil = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassement)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Name = "FrmClassement";
            this.Text = "Classement Général des Étudiants";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmClassement_Load);

            // groupBox1 - Liste
            this.groupBox1.Controls.Add(this.dgvClassement);
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 600);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Classement";

            // dgvClassement
            this.dgvClassement.AllowUserToAddRows = false;
            this.dgvClassement.AllowUserToDeleteRows = false;
            this.dgvClassement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClassement.Location = new System.Drawing.Point(15, 30);
            this.dgvClassement.Name = "dgvClassement";
            this.dgvClassement.ReadOnly = true;
            this.dgvClassement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClassement.Size = new System.Drawing.Size(620, 530);
            this.dgvClassement.TabIndex = 0;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(15, 570);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "Total: 0 étudiant(s)";

            // groupBox2 - Statistiques
            this.groupBox2.Controls.Add(this.lblMoyenneGenerale);
            this.groupBox2.Controls.Add(this.lblMeilleure);
            this.groupBox2.Controls.Add(this.lblPlusBasse);
            this.groupBox2.Controls.Add(this.lblReussis);
            this.groupBox2.Controls.Add(this.lblEchecs);
            this.groupBox2.Location = new System.Drawing.Point(670, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 250);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statistiques";

            // lblMoyenneGenerale
            this.lblMoyenneGenerale.AutoSize = true;
            this.lblMoyenneGenerale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMoyenneGenerale.Location = new System.Drawing.Point(15, 40);
            this.lblMoyenneGenerale.Name = "lblMoyenneGenerale";
            this.lblMoyenneGenerale.Size = new System.Drawing.Size(200, 17);
            this.lblMoyenneGenerale.Text = "Moyenne de classe: --";

            // lblMeilleure
            this.lblMeilleure.AutoSize = true;
            this.lblMeilleure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMeilleure.ForeColor = Color.Green;
            this.lblMeilleure.Location = new System.Drawing.Point(15, 80);
            this.lblMeilleure.Name = "lblMeilleure";
            this.lblMeilleure.Size = new System.Drawing.Size(200, 17);
            this.lblMeilleure.Text = "Meilleure moyenne: --";

            // lblPlusBasse
            this.lblPlusBasse.AutoSize = true;
            this.lblPlusBasse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPlusBasse.ForeColor = Color.Red;
            this.lblPlusBasse.Location = new System.Drawing.Point(15, 120);
            this.lblPlusBasse.Name = "lblPlusBasse";
            this.lblPlusBasse.Size = new System.Drawing.Size(200, 17);
            this.lblPlusBasse.Text = "Plus basse moyenne: --";

            // lblReussis
            this.lblReussis.AutoSize = true;
            this.lblReussis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblReussis.Location = new System.Drawing.Point(15, 160);
            this.lblReussis.Name = "lblReussis";
            this.lblReussis.Size = new System.Drawing.Size(150, 17);
            this.lblReussis.Text = "Réussis (≥10): --";

            // lblEchecs
            this.lblEchecs.AutoSize = true;
            this.lblEchecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblEchecs.Location = new System.Drawing.Point(15, 200);
            this.lblEchecs.Name = "lblEchecs";
            this.lblEchecs.Size = new System.Drawing.Size(150, 17);
            this.lblEchecs.Text = "Échecs (<10): --";

            // groupBox3 - Filtres
            this.groupBox3.Controls.Add(this.lblSeuil);
            this.groupBox3.Controls.Add(this.txtSeuil);
            this.groupBox3.Controls.Add(this.btnFiltrer);
            this.groupBox3.Controls.Add(this.btnActualiser);
            this.groupBox3.Location = new System.Drawing.Point(670, 270);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 180);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtres";

            // lblSeuil
            this.lblSeuil.AutoSize = true;
            this.lblSeuil.Location = new System.Drawing.Point(15, 35);
            this.lblSeuil.Name = "lblSeuil";
            this.lblSeuil.Text = "Seuil de moyenne:";

            // txtSeuil
            this.txtSeuil.Location = new System.Drawing.Point(15, 55);
            this.txtSeuil.Name = "txtSeuil";
            this.txtSeuil.Size = new System.Drawing.Size(280, 20);

            // btnFiltrer
            this.btnFiltrer.BackColor = Color.LightBlue;
            this.btnFiltrer.Location = new System.Drawing.Point(15, 85);
            this.btnFiltrer.Name = "btnFiltrer";
            this.btnFiltrer.Size = new System.Drawing.Size(280, 35);
            this.btnFiltrer.Text = "🔍 Filtrer";
            this.btnFiltrer.UseVisualStyleBackColor = false;
            this.btnFiltrer.Click += new System.EventHandler(this.btnFiltrer_Click);

            // btnActualiser
            this.btnActualiser.BackColor = Color.LightGreen;
            this.btnActualiser.Location = new System.Drawing.Point(15, 130);
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.Size = new System.Drawing.Size(280, 35);
            this.btnActualiser.Text = "🔄 Actualiser";
            this.btnActualiser.UseVisualStyleBackColor = false;
            this.btnActualiser.Click += new System.EventHandler(this.btnActualiser_Click);

            // Controls
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);

            ((System.ComponentModel.ISupportInitialize)(this.dgvClassement)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        private void FrmClassement_Load(object sender, EventArgs e)
        {
            ChargerClassement();
            ChargerStatistiques();
        }

        private void ChargerClassement()
        {
            try
            {
                var classement = EtudiantManager.ObtenirClassementGeneral();

                // Configuration manuelle des colonnes
                dgvClassement.Columns.Clear();
                dgvClassement.AutoGenerateColumns = false;

                dgvClassement.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Rang",
                    HeaderText = "Rang",
                    Width = 60
                });

                dgvClassement.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NomComplet",
                    HeaderText = "Étudiant",
                    Width = 200
                });

                dgvClassement.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Groupe",
                    HeaderText = "Groupe",
                    Width = 100
                });

                dgvClassement.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Moyenne",
                    HeaderText = "Moyenne",
                    Width = 100
                });

                dgvClassement.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Appreciation",
                    HeaderText = "Appréciation",
                    Width = 150
                });

                // Remplir les lignes
                dgvClassement.Rows.Clear();
                int rang = 1;

                foreach (var item in classement)
                {
                    int index = dgvClassement.Rows.Add();
                    DataGridViewRow row = dgvClassement.Rows[index];

                    row.Cells["Rang"].Value = rang++;
                    row.Cells["NomComplet"].Value = item.Etudiant.NomComplet;
                    row.Cells["Groupe"].Value = item.Etudiant.Groupe;
                    row.Cells["Moyenne"].Value = item.Moyenne.ToString("0.00");
                    row.Cells["Appreciation"].Value = item.Appreciation;

                    // Coloration selon la moyenne
                    if (item.Moyenne >= 16)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (item.Moyenne >= 14)
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    else if (item.Moyenne >= 12)
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (item.Moyenne < 10)
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                }

                lblTotal.Text = $"Total: {classement.Count} étudiant(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerStatistiques()
        {
            try
            {
                var stats = EtudiantManager.ObtenirStatistiques();

                lblMoyenneGenerale.Text = $"Moyenne de classe: {stats["MoyenneGenerale"]}";
                lblMeilleure.Text = $"Meilleure moyenne: {stats["MoyennePlusHaute"]}";
                lblPlusBasse.Text = $"Plus basse moyenne: {stats["MoyennePlusBasse"]}";
                lblReussis.Text = $"Réussis (≥10): {stats["NombreReussis"]}";
                lblEchecs.Text = $"Échecs (<10): {stats["NombreEchecs"]}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            ChargerClassement();
            ChargerStatistiques();
        }

        private void btnFiltrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.TryParse(txtSeuil.Text, out decimal seuil))
                {
                    if (seuil < 0 || seuil > 20)
                    {
                        MessageBox.Show("Le seuil doit être entre 0 et 20.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var etudiants = EtudiantManager.ObtenirEtudiantsParSeuil(seuil);

                    MessageBox.Show($"{etudiants.Count} étudiant(s) avec moyenne ≥ {seuil}",
                        "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Afficher les étudiants filtrés
                    dgvClassement.Rows.Clear();
                    int rang = 1;

                    foreach (var item in etudiants)
                    {
                        int index = dgvClassement.Rows.Add();
                        DataGridViewRow row = dgvClassement.Rows[index];

                        row.Cells["Rang"].Value = rang++;
                        row.Cells["NomComplet"].Value = item.Etudiant.NomComplet;
                        row.Cells["Groupe"].Value = item.Etudiant.Groupe;
                        row.Cells["Moyenne"].Value = item.Moyenne.ToString("0.00");
                        row.Cells["Appreciation"].Value = item.Appreciation;
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrer un seuil valide.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
