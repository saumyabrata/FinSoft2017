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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.TxtInvValue = new System.Windows.Forms.TextBox();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.txtsgst = new System.Windows.Forms.TextBox();
            this.txtcgst = new System.Windows.Forms.TextBox();
            this.txtigst = new System.Windows.Forms.TextBox();
            this.txtothers = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textnet = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Others = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // CreditorList
            // 
            this.CreditorList.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditorList.FormattingEnabled = true;
            this.CreditorList.ItemHeight = 21;
            this.CreditorList.Location = new System.Drawing.Point(392, 30);
            this.CreditorList.Name = "CreditorList";
            this.CreditorList.Size = new System.Drawing.Size(313, 445);
            this.CreditorList.TabIndex = 0;
            this.CreditorList.SelectedIndexChanged += new System.EventHandler(this.CreditorList_SelectedIndexChanged);
            this.CreditorList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CreditorList_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 66);
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
            this.label3.Location = new System.Drawing.Point(435, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Invoice Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(571, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Gross Value:";
            // 
            // CreditorFind
            // 
            this.CreditorFind.BackColor = System.Drawing.Color.PapayaWhip;
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
            this.TxtJob.BackColor = System.Drawing.Color.PapayaWhip;
            this.TxtJob.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtJob.Location = new System.Drawing.Point(167, 90);
            this.TxtJob.Name = "TxtJob";
            this.TxtJob.Size = new System.Drawing.Size(266, 28);
            this.TxtJob.TabIndex = 6;
            this.TxtJob.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtJob_KeyPress);
            // 
            // TxtInv
            // 
            this.TxtInv.BackColor = System.Drawing.Color.PapayaWhip;
            this.TxtInv.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInv.Location = new System.Drawing.Point(31, 90);
            this.TxtInv.Name = "TxtInv";
            this.TxtInv.Size = new System.Drawing.Size(130, 28);
            this.TxtInv.TabIndex = 7;
            this.TxtInv.TextChanged += new System.EventHandler(this.TxtInv_TextChanged);
            this.TxtInv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInv_KeyPress);
            // 
            // txtinvdate
            // 
            this.txtinvdate.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtinvdate.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinvdate.Location = new System.Drawing.Point(439, 90);
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
            this.InvValue,
            this.Others,
            this.Discount,
            this.SGST,
            this.CGST,
            this.IGST,
            this.NetValue});
            this.dataGridView1.Location = new System.Drawing.Point(31, 204);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(673, 285);
            this.dataGridView1.TabIndex = 12;
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
            this.button2.Location = new System.Drawing.Point(314, 525);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 44);
            this.button2.TabIndex = 18;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TxtInvValue
            // 
            this.TxtInvValue.BackColor = System.Drawing.Color.PapayaWhip;
            this.TxtInvValue.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvValue.Location = new System.Drawing.Point(575, 90);
            this.TxtInvValue.Name = "TxtInvValue";
            this.TxtInvValue.Size = new System.Drawing.Size(130, 28);
            this.TxtInvValue.TabIndex = 19;
            this.TxtInvValue.TextChanged += new System.EventHandler(this.TxtInvValue_TextChanged);
            this.TxtInvValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInvValue_KeyPress);
            // 
            // txtdiscount
            // 
            this.txtdiscount.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdiscount.Location = new System.Drawing.Point(167, 153);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(130, 28);
            this.txtdiscount.TabIndex = 20;
            this.txtdiscount.TextChanged += new System.EventHandler(this.txtdiscount_TextChanged);
            this.txtdiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdiscount_KeyPress);
            // 
            // txtsgst
            // 
            this.txtsgst.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtsgst.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsgst.Location = new System.Drawing.Point(303, 153);
            this.txtsgst.Name = "txtsgst";
            this.txtsgst.Size = new System.Drawing.Size(130, 28);
            this.txtsgst.TabIndex = 21;
            this.txtsgst.TextChanged += new System.EventHandler(this.txtsgst_TextChanged);
            this.txtsgst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsgst_KeyDown);
            this.txtsgst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsgst_KeyPress);
            // 
            // txtcgst
            // 
            this.txtcgst.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtcgst.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcgst.Location = new System.Drawing.Point(439, 153);
            this.txtcgst.Name = "txtcgst";
            this.txtcgst.Size = new System.Drawing.Size(130, 28);
            this.txtcgst.TabIndex = 22;
            this.txtcgst.TextChanged += new System.EventHandler(this.txtcgst_TextChanged);
            this.txtcgst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcgst_KeyPress);
            // 
            // txtigst
            // 
            this.txtigst.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtigst.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtigst.Location = new System.Drawing.Point(575, 153);
            this.txtigst.Name = "txtigst";
            this.txtigst.Size = new System.Drawing.Size(130, 28);
            this.txtigst.TabIndex = 23;
            this.txtigst.TextChanged += new System.EventHandler(this.txtigst_TextChanged);
            this.txtigst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtigst_KeyPress);
            // 
            // txtothers
            // 
            this.txtothers.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtothers.Location = new System.Drawing.Point(31, 153);
            this.txtothers.Name = "txtothers";
            this.txtothers.Size = new System.Drawing.Size(130, 28);
            this.txtothers.TabIndex = 24;
            this.txtothers.TextChanged += new System.EventHandler(this.txtothers_TextChanged);
            this.txtothers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtothers_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(163, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 21);
            this.label10.TabIndex = 25;
            this.label10.Text = "Discount:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(299, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 21);
            this.label11.TabIndex = 26;
            this.label11.Text = "SGST:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(435, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 21);
            this.label12.TabIndex = 27;
            this.label12.Text = "CGST:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(571, 129);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 21);
            this.label13.TabIndex = 28;
            this.label13.Text = "IGST:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(27, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 21);
            this.label14.TabIndex = 29;
            this.label14.Text = "Other Charges:";
            // 
            // textnet
            // 
            this.textnet.BackColor = System.Drawing.Color.DarkBlue;
            this.textnet.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textnet.ForeColor = System.Drawing.Color.White;
            this.textnet.Location = new System.Drawing.Point(574, 505);
            this.textnet.Name = "textnet";
            this.textnet.Size = new System.Drawing.Size(130, 28);
            this.textnet.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(471, 508);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 21);
            this.label15.TabIndex = 31;
            this.label15.Text = "Net Value:";
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.InvDt.DefaultCellStyle = dataGridViewCellStyle2;
            this.InvDt.HeaderText = "Inv Date";
            this.InvDt.MaxInputLength = 10;
            this.InvDt.MinimumWidth = 10;
            this.InvDt.Name = "InvDt";
            this.InvDt.ReadOnly = true;
            this.InvDt.Width = 89;
            // 
            // InvValue
            // 
            this.InvValue.HeaderText = "Invoice Value";
            this.InvValue.Name = "InvValue";
            this.InvValue.ReadOnly = true;
            // 
            // Others
            // 
            this.Others.HeaderText = "Other Charges";
            this.Others.Name = "Others";
            this.Others.ReadOnly = true;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            // 
            // SGST
            // 
            this.SGST.HeaderText = "SGST";
            this.SGST.Name = "SGST";
            this.SGST.ReadOnly = true;
            // 
            // CGST
            // 
            this.CGST.HeaderText = "CGST";
            this.CGST.Name = "CGST";
            this.CGST.ReadOnly = true;
            // 
            // IGST
            // 
            this.IGST.HeaderText = "IGST";
            this.IGST.Name = "IGST";
            this.IGST.ReadOnly = true;
            // 
            // NetValue
            // 
            this.NetValue.HeaderText = "Net Value";
            this.NetValue.Name = "NetValue";
            this.NetValue.ReadOnly = true;
            // 
            // FrmCreditorInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 585);
            this.Controls.Add(this.CreditorList);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textnet);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtothers);
            this.Controls.Add(this.txtigst);
            this.Controls.Add(this.txtcgst);
            this.Controls.Add(this.txtsgst);
            this.Controls.Add(this.txtdiscount);
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TxtInvValue;
        private System.Windows.Forms.TextBox txtdiscount;
        private System.Windows.Forms.TextBox txtsgst;
        private System.Windows.Forms.TextBox txtcgst;
        private System.Windows.Forms.TextBox txtigst;
        private System.Windows.Forms.TextBox txtothers;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textnet;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Others;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SGST;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGST;
        private System.Windows.Forms.DataGridViewTextBoxColumn IGST;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetValue;
    }
}