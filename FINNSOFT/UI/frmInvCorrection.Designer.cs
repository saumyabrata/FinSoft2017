namespace FINNSOFT.UI
{
    partial class frmInvCorrection
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
            this.lblcreditor = new System.Windows.Forms.Label();
            this.CreditorList = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblinvno = new System.Windows.Forms.Label();
            this.lbljob = new System.Windows.Forms.Label();
            this.lblinvdate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtinv = new System.Windows.Forms.TextBox();
            this.txtjob = new System.Windows.Forms.TextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.txtinvvalue = new System.Windows.Forms.TextBox();
            this.bttnupdate = new System.Windows.Forms.Button();
            this.bttndelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblcreditor
            // 
            this.lblcreditor.AutoSize = true;
            this.lblcreditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcreditor.Location = new System.Drawing.Point(27, 27);
            this.lblcreditor.Name = "lblcreditor";
            this.lblcreditor.Size = new System.Drawing.Size(74, 20);
            this.lblcreditor.TabIndex = 0;
            this.lblcreditor.Text = "Creditor:";
            // 
            // CreditorList
            // 
            this.CreditorList.FormattingEnabled = true;
            this.CreditorList.Location = new System.Drawing.Point(120, 27);
            this.CreditorList.Name = "CreditorList";
            this.CreditorList.Size = new System.Drawing.Size(493, 24);
            this.CreditorList.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(810, 285);
            this.dataGridView1.TabIndex = 2;
            // 
            // lblinvno
            // 
            this.lblinvno.AutoSize = true;
            this.lblinvno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinvno.Location = new System.Drawing.Point(31, 72);
            this.lblinvno.Name = "lblinvno";
            this.lblinvno.Size = new System.Drawing.Size(87, 20);
            this.lblinvno.TabIndex = 3;
            this.lblinvno.Text = "Invoice No";
            // 
            // lbljob
            // 
            this.lbljob.AutoSize = true;
            this.lbljob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbljob.Location = new System.Drawing.Point(188, 72);
            this.lbljob.Name = "lbljob";
            this.lbljob.Size = new System.Drawing.Size(127, 20);
            this.lbljob.TabIndex = 4;
            this.lbljob.Text = "Job Description";
            // 
            // lblinvdate
            // 
            this.lblinvdate.AutoSize = true;
            this.lblinvdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinvdate.Location = new System.Drawing.Point(534, 72);
            this.lblinvdate.Name = "lblinvdate";
            this.lblinvdate.Size = new System.Drawing.Size(102, 20);
            this.lblinvdate.TabIndex = 5;
            this.lblinvdate.Text = "Invoice Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(700, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Invoice Value";
            // 
            // txtinv
            // 
            this.txtinv.Location = new System.Drawing.Point(31, 109);
            this.txtinv.Name = "txtinv";
            this.txtinv.Size = new System.Drawing.Size(137, 22);
            this.txtinv.TabIndex = 7;
            // 
            // txtjob
            // 
            this.txtjob.Location = new System.Drawing.Point(192, 109);
            this.txtjob.Name = "txtjob";
            this.txtjob.Size = new System.Drawing.Size(327, 22);
            this.txtjob.TabIndex = 8;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(538, 109);
            this.maskedTextBox1.Mask = "00/00/0000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(132, 22);
            this.maskedTextBox1.TabIndex = 9;
            this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
            // 
            // txtinvvalue
            // 
            this.txtinvvalue.Location = new System.Drawing.Point(704, 109);
            this.txtinvvalue.Name = "txtinvvalue";
            this.txtinvvalue.Size = new System.Drawing.Size(137, 22);
            this.txtinvvalue.TabIndex = 10;
            // 
            // bttnupdate
            // 
            this.bttnupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnupdate.Location = new System.Drawing.Point(326, 464);
            this.bttnupdate.Name = "bttnupdate";
            this.bttnupdate.Size = new System.Drawing.Size(103, 40);
            this.bttnupdate.TabIndex = 11;
            this.bttnupdate.Text = "Update";
            this.bttnupdate.UseVisualStyleBackColor = true;
            // 
            // bttndelete
            // 
            this.bttndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttndelete.Location = new System.Drawing.Point(463, 464);
            this.bttndelete.Name = "bttndelete";
            this.bttndelete.Size = new System.Drawing.Size(103, 40);
            this.bttndelete.TabIndex = 12;
            this.bttndelete.Text = "Delete";
            this.bttndelete.UseVisualStyleBackColor = true;
            // 
            // frmInvCorrection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 531);
            this.Controls.Add(this.bttndelete);
            this.Controls.Add(this.bttnupdate);
            this.Controls.Add(this.txtinvvalue);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.txtjob);
            this.Controls.Add(this.txtinv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblinvdate);
            this.Controls.Add(this.lbljob);
            this.Controls.Add(this.lblinvno);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.CreditorList);
            this.Controls.Add(this.lblcreditor);
            this.Name = "frmInvCorrection";
            this.Text = "frmInvCorrection";
            this.Load += new System.EventHandler(this.frmInvCorrection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblcreditor;
        private System.Windows.Forms.ComboBox CreditorList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblinvno;
        private System.Windows.Forms.Label lbljob;
        private System.Windows.Forms.Label lblinvdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtinv;
        private System.Windows.Forms.TextBox txtjob;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.TextBox txtinvvalue;
        private System.Windows.Forms.Button bttnupdate;
        private System.Windows.Forms.Button bttndelete;
    }
}