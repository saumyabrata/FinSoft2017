namespace FINNSOFT.UI
{
    partial class FrmCreditorInvoice
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
            this.CreditorList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CreditorFind = new System.Windows.Forms.TextBox();
            this.TxtJob = new System.Windows.Forms.TextBox();
            this.TxtInv = new System.Windows.Forms.TextBox();
            this.txtinvdate = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.TxtInvValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // CreditorList
            // 
            this.CreditorList.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditorList.FormattingEnabled = true;
            this.CreditorList.ItemHeight = 21;
            this.CreditorList.Location = new System.Drawing.Point(459, 30);
            this.CreditorList.Name = "CreditorList";
            this.CreditorList.Size = new System.Drawing.Size(312, 445);
            this.CreditorList.TabIndex = 0;
            this.CreditorList.SelectedIndexChanged += new System.EventHandler(this.CreditorList_SelectedIndexChanged);
            this.CreditorList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CreditorList_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(200, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Job Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Invoice No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(512, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Invoice Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(637, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Invoice Value:";
            // 
            // CreditorFind
            // 
            this.CreditorFind.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditorFind.Location = new System.Drawing.Point(105, 30);
            this.CreditorFind.Name = "CreditorFind";
            this.CreditorFind.Size = new System.Drawing.Size(360, 28);
            this.CreditorFind.TabIndex = 5;
            this.CreditorFind.TextChanged += new System.EventHandler(this.CreditorFind_TextChanged);
            this.CreditorFind.Enter += new System.EventHandler(this.CreditorFind_Enter);
            this.CreditorFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CreditorFind_KeyUp);
            this.CreditorFind.Leave += new System.EventHandler(this.CreditorFind_Leave);
            // 
            // TxtJob
            // 
            this.TxtJob.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtJob.Location = new System.Drawing.Point(204, 90);
            this.TxtJob.Name = "TxtJob";
            this.TxtJob.Size = new System.Drawing.Size(306, 28);
            this.TxtJob.TabIndex = 6;
            this.TxtJob.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtJob_KeyPress);
            // 
            // TxtInv
            // 
            this.TxtInv.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInv.Location = new System.Drawing.Point(31, 90);
            this.TxtInv.Name = "TxtInv";
            this.TxtInv.Size = new System.Drawing.Size(167, 28);
            this.TxtInv.TabIndex = 7;
            this.TxtInv.TextChanged += new System.EventHandler(this.TxtInv_TextChanged);
            this.TxtInv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInv_KeyPress);
            // 
            // txtinvdate
            // 
            this.txtinvdate.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinvdate.Location = new System.Drawing.Point(516, 90);
            this.txtinvdate.Mask = "00/00/0000";
            this.txtinvdate.Name = "txtinvdate";
            this.txtinvdate.Size = new System.Drawing.Size(115, 28);
            this.txtinvdate.TabIndex = 9;
            this.txtinvdate.ValidatingType = typeof(System.DateTime);
            this.txtinvdate.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtinvdate_MaskInputRejected);
            this.txtinvdate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtinvdate_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Creditor:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceNo,
            this.Job,
            this.InvDt,
            this.InvValue});
            this.dataGridView1.Location = new System.Drawing.Point(31, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(740, 334);
            this.dataGridView1.TabIndex = 12;
            // 
            // InvoiceNo
            // 
            this.InvoiceNo.HeaderText = "Invoice No.";
            this.InvoiceNo.Name = "InvoiceNo";
            this.InvoiceNo.ReadOnly = true;
            // 
            // Job
            // 
            this.Job.HeaderText = "Job Description";
            this.Job.Name = "Job";
            this.Job.ReadOnly = true;
            // 
            // InvDt
            // 
            this.InvDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.InvDt.DefaultCellStyle = dataGridViewCellStyle1;
            this.InvDt.HeaderText = "Inv Date";
            this.InvDt.MaxInputLength = 10;
            this.InvDt.MinimumWidth = 10;
            this.InvDt.Name = "InvDt";
            this.InvDt.ReadOnly = true;
            this.InvDt.Width = 82;
            // 
            // InvValue
            // 
            this.InvValue.HeaderText = "Invoice Value";
            this.InvValue.Name = "InvValue";
            this.InvValue.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(471, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "GLID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(587, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "SLID:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(524, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 21);
            this.label8.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(637, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 21);
            this.label9.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(363, 495);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 18;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TxtInvValue
            // 
            this.TxtInvValue.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvValue.Location = new System.Drawing.Point(641, 90);
            this.TxtInvValue.Name = "TxtInvValue";
            this.TxtInvValue.Size = new System.Drawing.Size(130, 28);
            this.TxtInvValue.TabIndex = 19;
            this.TxtInvValue.TextChanged += new System.EventHandler(this.TxtInvValue_TextChanged);
            this.TxtInvValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInvValue_KeyPress);
            // 
            // FrmCreditorInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 552);
            this.Controls.Add(this.CreditorList);
            this.Controls.Add(this.TxtInvValue);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtinvdate);
            this.Controls.Add(this.TxtInv);
            this.Controls.Add(this.TxtJob);
            this.Controls.Add(this.CreditorFind);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmCreditorInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Creditor invoice Master";
            this.Load += new System.EventHandler(this.FrmCreditorInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox CreditorList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CreditorFind;
        private System.Windows.Forms.TextBox TxtJob;
        private System.Windows.Forms.TextBox TxtInv;
        private System.Windows.Forms.MaskedTextBox txtinvdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TxtInvValue;
    }
}