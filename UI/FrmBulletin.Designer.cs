namespace UI
{
    partial class FrmBulletin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpSelection = new GroupBox();
            btnRefresh = new Button();
            cmbSession = new ComboBox();
            lblSession = new Label();
            cmbEtudiants = new ComboBox();
            lblEtudiant = new Label();
            grpInfos = new GroupBox();
            lblDateNaissance = new Label();
            lblGroupe = new Label();
            lblNom = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            grpNotes = new GroupBox();
            dgvNotes = new DataGridView();
            grpMoyenne = new GroupBox();
            lblStatistiques = new Label();
            lblMoyenne = new Label();
            grpActions = new GroupBox();
            btnImprimer = new Button();
            btnRapportClasse = new Button();
            btnChargerLogo = new Button();
            btnExporterPDF = new Button();
            btnExporterCSV = new Button();
            grpSelection.SuspendLayout();
            grpInfos.SuspendLayout();
            grpNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotes).BeginInit();
            grpMoyenne.SuspendLayout();
            grpActions.SuspendLayout();
            SuspendLayout();
            // 
            // grpSelection
            // 
            grpSelection.Controls.Add(btnRefresh);
            grpSelection.Controls.Add(cmbSession);
            grpSelection.Controls.Add(lblSession);
            grpSelection.Controls.Add(cmbEtudiants);
            grpSelection.Controls.Add(lblEtudiant);
            grpSelection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpSelection.Location = new Point(12, 12);
            grpSelection.Name = "grpSelection";
            grpSelection.Size = new Size(860, 80);
            grpSelection.TabIndex = 0;
            grpSelection.TabStop = false;
            grpSelection.Text = "Sélection";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(52, 152, 219);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(655, 45);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Actualiser";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // cmbSession
            // 
            cmbSession.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSession.Font = new Font("Segoe UI", 9F);
            cmbSession.FormattingEnabled = true;
            cmbSession.Location = new Point(435, 50);
            cmbSession.Name = "cmbSession";
            cmbSession.Size = new Size(200, 23);
            cmbSession.TabIndex = 3;
            cmbSession.SelectedIndexChanged += cmbSession_SelectedIndexChanged;
            // 
            // lblSession
            // 
            lblSession.AutoSize = true;
            lblSession.Font = new Font("Segoe UI", 9F);
            lblSession.Location = new Point(435, 32);
            lblSession.Name = "lblSession";
            lblSession.Size = new Size(52, 15);
            lblSession.TabIndex = 2;
            lblSession.Text = "Session :";
            // 
            // cmbEtudiants
            // 
            cmbEtudiants.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEtudiants.Font = new Font("Segoe UI", 9F);
            cmbEtudiants.FormattingEnabled = true;
            cmbEtudiants.Location = new Point(15, 50);
            cmbEtudiants.Name = "cmbEtudiants";
            cmbEtudiants.Size = new Size(400, 23);
            cmbEtudiants.TabIndex = 1;
            cmbEtudiants.SelectedIndexChanged += cmbEtudiants_SelectedIndexChanged;
            // 
            // lblEtudiant
            // 
            lblEtudiant.AutoSize = true;
            lblEtudiant.Font = new Font("Segoe UI", 9F);
            lblEtudiant.Location = new Point(15, 32);
            lblEtudiant.Name = "lblEtudiant";
            lblEtudiant.Size = new Size(57, 15);
            lblEtudiant.TabIndex = 0;
            lblEtudiant.Text = "Étudiant :";
            // 
            // grpInfos
            // 
            grpInfos.Controls.Add(lblDateNaissance);
            grpInfos.Controls.Add(lblGroupe);
            grpInfos.Controls.Add(lblNom);
            grpInfos.Controls.Add(label3);
            grpInfos.Controls.Add(label2);
            grpInfos.Controls.Add(label1);
            grpInfos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpInfos.Location = new Point(12, 98);
            grpInfos.Name = "grpInfos";
            grpInfos.Size = new Size(420, 150);
            grpInfos.TabIndex = 1;
            grpInfos.TabStop = false;
            grpInfos.Text = "Informations de l'étudiant";
            // 
            // lblDateNaissance
            // 
            lblDateNaissance.AutoSize = true;
            lblDateNaissance.Font = new Font("Segoe UI", 9F);
            lblDateNaissance.Location = new Point(150, 105);
            lblDateNaissance.Name = "lblDateNaissance";
            lblDateNaissance.Size = new Size(12, 15);
            lblDateNaissance.TabIndex = 5;
            lblDateNaissance.Text = "-";
            // 
            // lblGroupe
            // 
            lblGroupe.AutoSize = true;
            lblGroupe.Font = new Font("Segoe UI", 9F);
            lblGroupe.Location = new Point(150, 70);
            lblGroupe.Name = "lblGroupe";
            lblGroupe.Size = new Size(12, 15);
            lblGroupe.TabIndex = 3;
            lblGroupe.Text = "-";
            // 
            // lblNom
            // 
            lblNom.AutoSize = true;
            lblNom.Font = new Font("Segoe UI", 9F);
            lblNom.Location = new Point(150, 35);
            lblNom.Name = "lblNom";
            lblNom.Size = new Size(12, 15);
            lblNom.TabIndex = 1;
            lblNom.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(15, 105);
            label3.Name = "label3";
            label3.Size = new Size(112, 15);
            label3.TabIndex = 4;
            label3.Text = "Date de naissance :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(15, 70);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "Groupe :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(15, 35);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Nom complet :";
            // 
            // grpNotes
            // 
            grpNotes.Controls.Add(dgvNotes);
            grpNotes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpNotes.Location = new Point(12, 350);
            grpNotes.Name = "grpNotes";
            grpNotes.Size = new Size(860, 300);
            grpNotes.TabIndex = 4;
            grpNotes.TabStop = false;
            grpNotes.Text = "Détail des notes";
            // 
            // dgvNotes
            // 
            dgvNotes.AllowUserToAddRows = false;
            dgvNotes.AllowUserToDeleteRows = false;
            dgvNotes.BackgroundColor = Color.White;
            dgvNotes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotes.Dock = DockStyle.Fill;
            dgvNotes.Location = new Point(3, 21);
            dgvNotes.Name = "dgvNotes";
            dgvNotes.ReadOnly = true;
            dgvNotes.RowHeadersWidth = 51;
            dgvNotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotes.Size = new Size(854, 276);
            dgvNotes.TabIndex = 0;
            // 
            // grpMoyenne
            // 
            grpMoyenne.Controls.Add(lblStatistiques);
            grpMoyenne.Controls.Add(lblMoyenne);
            grpMoyenne.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpMoyenne.Location = new Point(438, 98);
            grpMoyenne.Name = "grpMoyenne";
            grpMoyenne.Size = new Size(434, 150);
            grpMoyenne.TabIndex = 2;
            grpMoyenne.TabStop = false;
            grpMoyenne.Text = "Moyenne générale et statistiques";
            // 
            // lblStatistiques
            // 
            lblStatistiques.Font = new Font("Segoe UI", 8.5F);
            lblStatistiques.Location = new Point(15, 80);
            lblStatistiques.Name = "lblStatistiques";
            lblStatistiques.Size = new Size(400, 60);
            lblStatistiques.TabIndex = 1;
            lblStatistiques.Text = "Statistiques non disponibles";
            // 
            // lblMoyenne
            // 
            lblMoyenne.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblMoyenne.Location = new Point(15, 30);
            lblMoyenne.Name = "lblMoyenne";
            lblMoyenne.Size = new Size(400, 45);
            lblMoyenne.TabIndex = 0;
            lblMoyenne.Text = "-";
            lblMoyenne.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpActions
            // 
            grpActions.Controls.Add(btnImprimer);
            grpActions.Controls.Add(btnRapportClasse);
            grpActions.Controls.Add(btnChargerLogo);
            grpActions.Controls.Add(btnExporterPDF);
            grpActions.Controls.Add(btnExporterCSV);
            grpActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpActions.Location = new Point(12, 254);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(860, 90);
            grpActions.TabIndex = 3;
            grpActions.TabStop = false;
            grpActions.Text = "Actions";
            // 
            // btnImprimer
            // 
            btnImprimer.BackColor = Color.FromArgb(52, 73, 94);
            btnImprimer.FlatStyle = FlatStyle.Flat;
            btnImprimer.Font = new Font("Segoe UI", 9F);
            btnImprimer.ForeColor = Color.White;
            btnImprimer.Location = new Point(826, 65);
            btnImprimer.Name = "btnImprimer";
            btnImprimer.Size = new Size(19, 10);
            btnImprimer.TabIndex = 4;
            btnImprimer.Text = "Imprimer";
            btnImprimer.UseVisualStyleBackColor = false;
            btnImprimer.Visible = false;
            btnImprimer.Click += btnImprimer_Click;
            // 
            // btnRapportClasse
            // 
            btnRapportClasse.BackColor = Color.FromArgb(230, 126, 34);
            btnRapportClasse.FlatStyle = FlatStyle.Flat;
            btnRapportClasse.Font = new Font("Segoe UI", 9F);
            btnRapportClasse.ForeColor = Color.White;
            btnRapportClasse.Location = new Point(648, 35);
            btnRapportClasse.Name = "btnRapportClasse";
            btnRapportClasse.Size = new Size(186, 40);
            btnRapportClasse.TabIndex = 3;
            btnRapportClasse.Text = "Rapport Classe";
            btnRapportClasse.UseVisualStyleBackColor = false;
            btnRapportClasse.Click += btnRapportClasse_Click;
            // 
            // btnChargerLogo
            // 
            btnChargerLogo.BackColor = Color.FromArgb(155, 89, 182);
            btnChargerLogo.FlatStyle = FlatStyle.Flat;
            btnChargerLogo.Font = new Font("Segoe UI", 9F);
            btnChargerLogo.ForeColor = Color.White;
            btnChargerLogo.Location = new Point(442, 35);
            btnChargerLogo.Name = "btnChargerLogo";
            btnChargerLogo.Size = new Size(186, 40);
            btnChargerLogo.TabIndex = 2;
            btnChargerLogo.Text = "Charger logo";
            btnChargerLogo.UseVisualStyleBackColor = false;
            btnChargerLogo.Click += btnChargerLogo_Click;
            // 
            // btnExporterPDF
            // 
            btnExporterPDF.BackColor = Color.FromArgb(231, 76, 60);
            btnExporterPDF.FlatStyle = FlatStyle.Flat;
            btnExporterPDF.Font = new Font("Segoe UI", 9F);
            btnExporterPDF.ForeColor = Color.White;
            btnExporterPDF.Location = new Point(236, 35);
            btnExporterPDF.Name = "btnExporterPDF";
            btnExporterPDF.Size = new Size(186, 40);
            btnExporterPDF.TabIndex = 1;
            btnExporterPDF.Text = "Exporter PDF";
            btnExporterPDF.UseVisualStyleBackColor = false;
            btnExporterPDF.Click += btnExporterPDF_Click;
            // 
            // btnExporterCSV
            // 
            btnExporterCSV.BackColor = Color.FromArgb(46, 204, 113);
            btnExporterCSV.FlatStyle = FlatStyle.Flat;
            btnExporterCSV.Font = new Font("Segoe UI", 9F);
            btnExporterCSV.ForeColor = Color.White;
            btnExporterCSV.Location = new Point(30, 35);
            btnExporterCSV.Name = "btnExporterCSV";
            btnExporterCSV.Size = new Size(186, 40);
            btnExporterCSV.TabIndex = 0;
            btnExporterCSV.Text = "Exporter CSV";
            btnExporterCSV.UseVisualStyleBackColor = false;
            btnExporterCSV.Click += btnExporterCSV_Click;
            // 
            // FrmBulletin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 661);
            Controls.Add(grpNotes);
            Controls.Add(grpActions);
            Controls.Add(grpMoyenne);
            Controls.Add(grpInfos);
            Controls.Add(grpSelection);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FrmBulletin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bulletin de Notes - Gestion Étudiants";
            Load += FrmBulletin_Load;
            grpSelection.ResumeLayout(false);
            grpSelection.PerformLayout();
            grpInfos.ResumeLayout(false);
            grpInfos.PerformLayout();
            grpNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNotes).EndInit();
            grpMoyenne.ResumeLayout(false);
            grpActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelection;
        private System.Windows.Forms.ComboBox cmbEtudiants;
        private System.Windows.Forms.Label lblEtudiant;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.GroupBox grpInfos;
        private System.Windows.Forms.Label lblDateNaissance;
        private System.Windows.Forms.Label lblGroupe;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.GroupBox grpNotes;
        private System.Windows.Forms.DataGridView dgvNotes;

        private System.Windows.Forms.GroupBox grpMoyenne;
        private System.Windows.Forms.Label lblMoyenne;
        private System.Windows.Forms.Label lblStatistiques;

        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnExporterCSV;
        private System.Windows.Forms.Button btnExporterPDF;
        private System.Windows.Forms.Button btnChargerLogo;
        private System.Windows.Forms.Button btnRapportClasse;
        private System.Windows.Forms.Button btnImprimer;
    }
}