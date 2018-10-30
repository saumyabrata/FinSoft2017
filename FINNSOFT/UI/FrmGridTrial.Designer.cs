namespace FINNSOFT.UI
{
    partial class FrmGridTrial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgTrial = new System.Windows.Forms.DataGridView();
            this.BrCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GLID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_Ledger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op_Bal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dr_Bal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cr_Amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cl_Bal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrial)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTrial
            // 
            this.dgTrial.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgTrial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTrial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTrial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgTrial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTrial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BrCode,
            this.GLID,
            this.GenLedger,
            this.SLID,
            this.Sub_Ledger,
            this.Op_Bal,
            this.Dr_Bal,
            this.Cr_Amt,
            this.Cl_Bal});
            this.dgTrial.Location = new System.Drawing.Point(12, 63);
            this.dgTrial.Name = "dgTrial";
            this.dgTrial.ReadOnly = true;
            this.dgTrial.Size = new System.Drawing.Size(1195, 577);
            this.dgTrial.TabIndex = 0;
            // 
            // BrCode
            // 
            this.BrCode.Frozen = true;
            this.BrCode.HeaderText = "BrCode";
            this.BrCode.Name = "BrCode";
            this.BrCode.ReadOnly = true;
            // 
            // GLID
            // 
            this.GLID.HeaderText = "GLID";
            this.GLID.Name = "GLID";
            this.GLID.ReadOnly = true;
            this.GLID.Visible = false;
            // 
            // GenLedger
            // 
            this.GenLedger.HeaderText = "General Ledger";
            this.GenLedger.Name = "GenLedger";
            this.GenLedger.ReadOnly = true;
            this.GenLedger.Width = 250;
            // 
            // SLID
            // 
            this.SLID.HeaderText = "SLID";
            this.SLID.Name = "SLID";
            this.SLID.ReadOnly = true;
            this.SLID.Visible = false;
            // 
            // Sub_Ledger
            // 
            this.Sub_Ledger.HeaderText = "Sub Ledger";
            this.Sub_Ledger.Name = "Sub_Ledger";
            this.Sub_Ledger.ReadOnly = true;
            this.Sub_Ledger.Width = 300;
            // 
            // Op_Bal
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Op_Bal.DefaultCellStyle = dataGridViewCellStyle3;
            this.Op_Bal.HeaderText = "Opening Balance";
            this.Op_Bal.Name = "Op_Bal";
            this.Op_Bal.ReadOnly = true;
            this.Op_Bal.Width = 150;
            // 
            // Dr_Bal
            // 
            dataGridViewCellStyle4.Format = "N2";
            this.Dr_Bal.DefaultCellStyle = dataGridViewCellStyle4;
            this.Dr_Bal.HeaderText = "Debit";
            this.Dr_Bal.Name = "Dr_Bal";
            this.Dr_Bal.ReadOnly = true;
            // 
            // Cr_Amt
            // 
            dataGridViewCellStyle5.Format = "N2";
            this.Cr_Amt.DefaultCellStyle = dataGridViewCellStyle5;
            this.Cr_Amt.HeaderText = "Credit";
            this.Cr_Amt.Name = "Cr_Amt";
            this.Cr_Amt.ReadOnly = true;
            // 
            // Cl_Bal
            // 
            dataGridViewCellStyle6.Format = "N2";
            this.Cl_Bal.DefaultCellStyle = dataGridViewCellStyle6;
            this.Cl_Bal.HeaderText = "Closing Balance";
            this.Cl_Bal.Name = "Cl_Bal";
            this.Cl_Bal.ReadOnly = true;
            this.Cl_Bal.Width = 150;
            // 
            // FrmGridTrial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 642);
            this.Controls.Add(this.dgTrial);
            this.Name = "FrmGridTrial";
            this.Text = "Trial Balance";
            this.Load += new System.EventHandler(this.FrmGridTrial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTrial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTrial;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GLID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_Ledger;
        private System.Windows.Forms.DataGridViewTextBoxColumn Op_Bal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dr_Bal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cr_Amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cl_Bal;
    }
}