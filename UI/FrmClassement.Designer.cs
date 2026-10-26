namespace UI
{
    partial class FrmClassement
    {
         
        /// Required designer variable.
        ///  
        private System.ComponentModel.IContainer components = null;

         
        /// Clean up any resources being used.
        ///  
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.DataGridView dgvClassement;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMoyenneGenerale;
        private System.Windows.Forms.Label lblMeilleure;
        private System.Windows.Forms.Label lblPlusBasse;
        private System.Windows.Forms.Label lblReussis;
        private System.Windows.Forms.Label lblEchecs;
        private System.Windows.Forms.TextBox txtSeuil;
        private System.Windows.Forms.Button btnFiltrer;
        private System.Windows.Forms.Button btnActualiser;
        private System.Windows.Forms.Label lblSeuil;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}