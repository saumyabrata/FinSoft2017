namespace FINNSOFT.UI
{
    partial class FrmVoucherAlt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblvchno = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bttnsave = new FINNSOFT.RoundButton();
            this.bttnreceipt = new FINNSOFT.RoundButton();
            this.bttnpayment = new FINNSOFT.RoundButton();
            this.bttncontra = new FINNSOFT.RoundButton();
            this.bttnjournal = new FINNSOFT.RoundButton();
            this.txtvchtype = new System.Windows.Forms.Label();
            this.txtvchno = new System.Windows.Forms.TextBox();
            this.lblvchdt = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pmttype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblbalance = new System.Windows.Forms.Label();
            this.lblchqno = new System.Windows.Forms.Label();
            this.lblissuedt = new System.Windows.Forms.Label();
            this.instrno = new System.Windows.Forms.TextBox();
            this.issuedt = new System.Windows.Forms.DateTimePicker();
            this.lbltotdebit = new System.Windows.Forms.Label();
            this.lbltotcredit = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblglslid = new System.Windows.Forms.Label();
            this.txtnarration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtamttype = new System.Windows.Forms.TextBox();
            this.bttnadd = new System.Windows.Forms.Button();
            this.bttnsearch = new FINNSOFT.RoundButton();
            this.lblbillnum = new System.Windows.Forms.Label();
            this.txtbillnum = new System.Windows.Forms.TextBox();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.checkprintToggle = new MetroFramework.Controls.MetroToggle();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblvchno
            // 
            this.lblvchno.AutoSize = true;
            this.lblvchno.Location = new System.Drawing.Point(223, 13);
            this.lblvchno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblvchno.Name = "lblvchno";
            this.lblvchno.Size = new System.Drawing.Size(67, 13);
            this.lblvchno.TabIndex = 1;
            this.lblvchno.Text = "Voucher No:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bttnsave);
            this.panel2.Controls.Add(this.bttnreceipt);
            this.panel2.Controls.Add(this.bttnpayment);
            this.panel2.Controls.Add(this.bttncontra);
            this.panel2.Controls.Add(this.bttnjournal);
            this.panel2.Location = new System.Drawing.Point(47, 401);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(638, 90);
            this.panel2.TabIndex = 5;
            // 
            // bttnsave
            // 
            this.bttnsave.BackColor = System.Drawing.Color.Gold;
            this.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnsave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnsave.ForeColor = System.Drawing.Color.Maroon;
            this.bttnsave.Location = new System.Drawing.Point(564, 11);
            this.bttnsave.Margin = new System.Windows.Forms.Padding(2);
            this.bttnsave.Name = "bttnsave";
            this.bttnsave.Size = new System.Drawing.Size(72, 72);
            this.bttnsave.TabIndex = 31;
            this.bttnsave.Text = "Save";
            this.bttnsave.UseVisualStyleBackColor = false;
            this.bttnsave.Click += new System.EventHandler(this.bttnsave_Click);
            // 
            // bttnreceipt
            // 
            this.bttnreceipt.BackColor = System.Drawing.Color.Gold;
            this.bttnreceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnreceipt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnreceipt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnreceipt.ForeColor = System.Drawing.Color.Maroon;
            this.bttnreceipt.Location = new System.Drawing.Point(196, 23);
            this.bttnreceipt.Margin = new System.Windows.Forms.Padding(2);
            this.bttnreceipt.Name = "bttnreceipt";
            this.bttnreceipt.Size = new System.Drawing.Size(82, 45);
            this.bttnreceipt.TabIndex = 31;
            this.bttnreceipt.Text = "Receipt";
            this.bttnreceipt.UseVisualStyleBackColor = false;
            this.bttnreceipt.Click += new System.EventHandler(this.bttnreceipt_Click);
            // 
            // bttnpayment
            // 
            this.bttnpayment.BackColor = System.Drawing.Color.Gold;
            this.bttnpayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnpayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnpayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnpayment.ForeColor = System.Drawing.Color.Maroon;
            this.bttnpayment.Location = new System.Drawing.Point(296, 23);
            this.bttnpayment.Margin = new System.Windows.Forms.Padding(2);
            this.bttnpayment.Name = "bttnpayment";
            this.bttnpayment.Size = new System.Drawing.Size(83, 45);
            this.bttnpayment.TabIndex = 30;
            this.bttnpayment.Text = "Payment";
            this.bttnpayment.UseVisualStyleBackColor = false;
            this.bttnpayment.Click += new System.EventHandler(this.bttnpayment_Click);
            // 
            // bttncontra
            // 
            this.bttncontra.BackColor = System.Drawing.Color.Gold;
            this.bttncontra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttncontra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttncontra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttncontra.ForeColor = System.Drawing.Color.Maroon;
            this.bttncontra.Location = new System.Drawing.Point(101, 23);
            this.bttncontra.Margin = new System.Windows.Forms.Padding(2);
            this.bttncontra.Name = "bttncontra";
            this.bttncontra.Size = new System.Drawing.Size(81, 45);
            this.bttncontra.TabIndex = 29;
            this.bttncontra.Text = "Contra";
            this.bttncontra.UseVisualStyleBackColor = false;
            this.bttncontra.Click += new System.EventHandler(this.bttncontra_Click);
            // 
            // bttnjournal
            // 
            this.bttnjournal.BackColor = System.Drawing.Color.Gold;
            this.bttnjournal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnjournal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnjournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnjournal.ForeColor = System.Drawing.Color.DarkRed;
            this.bttnjournal.Location = new System.Drawing.Point(4, 23);
            this.bttnjournal.Margin = new System.Windows.Forms.Padding(2);
            this.bttnjournal.Name = "bttnjournal";
            this.bttnjournal.Size = new System.Drawing.Size(81, 47);
            this.bttnjournal.TabIndex = 28;
            this.bttnjournal.Text = "Journal";
            this.bttnjournal.UseVisualStyleBackColor = false;
            this.bttnjournal.Click += new System.EventHandler(this.bttnjournal_Click);
            // 
            // txtvchtype
            // 
            this.txtvchtype.BackColor = System.Drawing.Color.DarkRed;
            this.txtvchtype.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtvchtype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtvchtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvchtype.ForeColor = System.Drawing.Color.White;
            this.txtvchtype.Location = new System.Drawing.Point(292, 38);
            this.txtvchtype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtvchtype.Name = "txtvchtype";
            this.txtvchtype.Size = new System.Drawing.Size(104, 27);
            this.txtvchtype.TabIndex = 6;
            this.txtvchtype.Text = "Payment";
            this.txtvchtype.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtvchno
            // 
            this.txtvchno.BackColor = System.Drawing.Color.Gold;
            this.txtvchno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvchno.ForeColor = System.Drawing.Color.Maroon;
            this.txtvchno.Location = new System.Drawing.Point(292, 9);
            this.txtvchno.Margin = new System.Windows.Forms.Padding(2);
            this.txtvchno.Name = "txtvchno";
            this.txtvchno.Size = new System.Drawing.Size(104, 23);
            this.txtvchno.TabIndex = 1;
            this.txtvchno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvchno.Click += new System.EventHandler(this.txtvchno_Click);
            this.txtvchno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtvchno_KeyDown);
            // 
            // lblvchdt
            // 
            this.lblvchdt.AutoSize = true;
            this.lblvchdt.Location = new System.Drawing.Point(442, 13);
            this.lblvchdt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblvchdt.Name = "lblvchdt";
            this.lblvchdt.Size = new System.Drawing.Size(76, 13);
            this.lblvchdt.TabIndex = 8;
            this.lblvchdt.Text = "Voucher Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(521, 9);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(104, 23);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pmttype,
            this.glid,
            this.slid,
            this.Particulars,
            this.Debit,
            this.Credit,
            this.Delete});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(48, 124);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(640, 164);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // pmttype
            // 
            this.pmttype.HeaderText = "By/To";
            this.pmttype.MaxInputLength = 5;
            this.pmttype.Name = "pmttype";
            this.pmttype.ReadOnly = true;
            this.pmttype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pmttype.Width = 50;
            // 
            // glid
            // 
            this.glid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.glid.HeaderText = "glid";
            this.glid.Name = "glid";
            this.glid.ReadOnly = true;
            this.glid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.glid.Visible = false;
            this.glid.Width = 5;
            // 
            // slid
            // 
            this.slid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.slid.HeaderText = "slid";
            this.slid.Name = "slid";
            this.slid.ReadOnly = true;
            this.slid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.slid.Visible = false;
            this.slid.Width = 5;
            // 
            // Particulars
            // 
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            this.Particulars.ReadOnly = true;
            this.Particulars.Width = 280;
            // 
            // Debit
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Debit.DefaultCellStyle = dataGridViewCellStyle7;
            this.Debit.HeaderText = "Debit";
            this.Debit.Name = "Debit";
            this.Debit.ReadOnly = true;
            this.Debit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Credit
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.Credit.DefaultCellStyle = dataGridViewCellStyle8;
            this.Credit.HeaderText = "Credit";
            this.Credit.Name = "Credit";
            this.Credit.ReadOnly = true;
            this.Credit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Delete
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Blue;
            this.Delete.DefaultCellStyle = dataGridViewCellStyle9;
            this.Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Delete.HeaderText = "Del";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Text = "X";
            this.Delete.Width = 50;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(520, 71);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 23);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // lblbalance
            // 
            this.lblbalance.AutoSize = true;
            this.lblbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbalance.ForeColor = System.Drawing.Color.Maroon;
            this.lblbalance.Location = new System.Drawing.Point(74, 99);
            this.lblbalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblbalance.Name = "lblbalance";
            this.lblbalance.Size = new System.Drawing.Size(52, 17);
            this.lblbalance.TabIndex = 14;
            this.lblbalance.Text = "label1";
            // 
            // lblchqno
            // 
            this.lblchqno.AutoSize = true;
            this.lblchqno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblchqno.Location = new System.Drawing.Point(44, 329);
            this.lblchqno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblchqno.Name = "lblchqno";
            this.lblchqno.Size = new System.Drawing.Size(100, 17);
            this.lblchqno.TabIndex = 17;
            this.lblchqno.Text = "Instrument No:";
            // 
            // lblissuedt
            // 
            this.lblissuedt.AutoSize = true;
            this.lblissuedt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblissuedt.Location = new System.Drawing.Point(44, 362);
            this.lblissuedt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblissuedt.Name = "lblissuedt";
            this.lblissuedt.Size = new System.Drawing.Size(112, 17);
            this.lblissuedt.TabIndex = 18;
            this.lblissuedt.Text = "Instrument Date:";
            // 
            // instrno
            // 
            this.instrno.BackColor = System.Drawing.Color.Linen;
            this.instrno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instrno.ForeColor = System.Drawing.Color.Maroon;
            this.instrno.Location = new System.Drawing.Point(149, 329);
            this.instrno.Margin = new System.Windows.Forms.Padding(2);
            this.instrno.Name = "instrno";
            this.instrno.Size = new System.Drawing.Size(101, 23);
            this.instrno.TabIndex = 6;
            this.instrno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.instrno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.instrno_KeyUp);
            // 
            // issuedt
            // 
            this.issuedt.CustomFormat = "dd/MM/yyyy";
            this.issuedt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.issuedt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.issuedt.Location = new System.Drawing.Point(149, 362);
            this.issuedt.Margin = new System.Windows.Forms.Padding(2);
            this.issuedt.Name = "issuedt";
            this.issuedt.Size = new System.Drawing.Size(101, 23);
            this.issuedt.TabIndex = 7;
            this.issuedt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.issuedt_KeyUp);
            // 
            // lbltotdebit
            // 
            this.lbltotdebit.AutoSize = true;
            this.lbltotdebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotdebit.ForeColor = System.Drawing.Color.Maroon;
            this.lbltotdebit.Location = new System.Drawing.Point(434, 290);
            this.lbltotdebit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltotdebit.Name = "lbltotdebit";
            this.lbltotdebit.Size = new System.Drawing.Size(70, 17);
            this.lbltotdebit.TabIndex = 21;
            this.lbltotdebit.Text = "TotDebit";
            // 
            // lbltotcredit
            // 
            this.lbltotcredit.AutoSize = true;
            this.lbltotcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotcredit.ForeColor = System.Drawing.Color.Maroon;
            this.lbltotcredit.Location = new System.Drawing.Point(558, 290);
            this.lbltotcredit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltotcredit.Name = "lbltotcredit";
            this.lbltotcredit.Size = new System.Drawing.Size(70, 17);
            this.lbltotcredit.TabIndex = 22;
            this.lbltotcredit.Text = "totCredit";
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(76, 71);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(430, 25);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox2.Enter += new System.EventHandler(this.comboBox2_Enter);
            this.comboBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox2_KeyDown);
            this.comboBox2.Leave += new System.EventHandler(this.comboBox2_Leave);
            // 
            // lblglslid
            // 
            this.lblglslid.AutoSize = true;
            this.lblglslid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblglslid.Location = new System.Drawing.Point(471, 99);
            this.lblglslid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblglslid.Name = "lblglslid";
            this.lblglslid.Size = new System.Drawing.Size(36, 15);
            this.lblglslid.TabIndex = 23;
            this.lblglslid.Text = "glslid";
            // 
            // txtnarration
            // 
            this.txtnarration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnarration.ForeColor = System.Drawing.Color.Blue;
            this.txtnarration.Location = new System.Drawing.Point(262, 329);
            this.txtnarration.Margin = new System.Windows.Forms.Padding(2);
            this.txtnarration.Multiline = true;
            this.txtnarration.Name = "txtnarration";
            this.txtnarration.Size = new System.Drawing.Size(425, 54);
            this.txtnarration.TabIndex = 24;
            this.txtnarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnarration_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(260, 311);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Narration -";
            // 
            // txtamttype
            // 
            this.txtamttype.AutoCompleteCustomSource.AddRange(new string[] {
            "Dr.",
            "Cr."});
            this.txtamttype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtamttype.BackColor = System.Drawing.Color.White;
            this.txtamttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamttype.Location = new System.Drawing.Point(50, 72);
            this.txtamttype.Margin = new System.Windows.Forms.Padding(2);
            this.txtamttype.MaxLength = 3;
            this.txtamttype.Name = "txtamttype";
            this.txtamttype.Size = new System.Drawing.Size(24, 23);
            this.txtamttype.TabIndex = 27;
            this.txtamttype.Text = "Dr.";
            this.txtamttype.Click += new System.EventHandler(this.txtamttype_Click);
            this.txtamttype.Enter += new System.EventHandler(this.txtamttype_Enter);
            this.txtamttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtamttype_KeyDown);
            // 
            // bttnadd
            // 
            this.bttnadd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bttnadd.BackColor = System.Drawing.SystemColors.Control;
            this.bttnadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bttnadd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnadd.FlatAppearance.BorderSize = 0;
            this.bttnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttnadd.Image = global::FINNSOFT.Properties.Resources.icons8_plus_50;
            this.bttnadd.Location = new System.Drawing.Point(599, 55);
            this.bttnadd.Margin = new System.Windows.Forms.Padding(2);
            this.bttnadd.Name = "bttnadd";
            this.bttnadd.Size = new System.Drawing.Size(61, 51);
            this.bttnadd.TabIndex = 15;
            this.bttnadd.UseVisualStyleBackColor = false;
            this.bttnadd.Click += new System.EventHandler(this.bttnadd_Click);
            // 
            // bttnsearch
            // 
            this.bttnsearch.BackColor = System.Drawing.SystemColors.Control;
            this.bttnsearch.BackgroundImage = global::FINNSOFT.Properties.Resources.search;
            this.bttnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bttnsearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnsearch.ForeColor = System.Drawing.Color.DarkRed;
            this.bttnsearch.Location = new System.Drawing.Point(399, 7);
            this.bttnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.bttnsearch.Name = "bttnsearch";
            this.bttnsearch.Size = new System.Drawing.Size(30, 30);
            this.bttnsearch.TabIndex = 30;
            this.bttnsearch.UseVisualStyleBackColor = false;
            this.bttnsearch.Click += new System.EventHandler(this.bttnsearch_Click);
            // 
            // lblbillnum
            // 
            this.lblbillnum.AutoSize = true;
            this.lblbillnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbillnum.Location = new System.Drawing.Point(45, 303);
            this.lblbillnum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblbillnum.Name = "lblbillnum";
            this.lblbillnum.Size = new System.Drawing.Size(56, 17);
            this.lblbillnum.TabIndex = 31;
            this.lblbillnum.Text = "Bill No.:";
            // 
            // txtbillnum
            // 
            this.txtbillnum.BackColor = System.Drawing.Color.Linen;
            this.txtbillnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillnum.ForeColor = System.Drawing.Color.Maroon;
            this.txtbillnum.Location = new System.Drawing.Point(149, 301);
            this.txtbillnum.Margin = new System.Windows.Forms.Padding(2);
            this.txtbillnum.Name = "txtbillnum";
            this.txtbillnum.Size = new System.Drawing.Size(101, 23);
            this.txtbillnum.TabIndex = 32;
            this.txtbillnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbillnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbillnum_KeyDown);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.checkprintToggle);
            this.metroPanel1.Controls.Add(this.txtbillnum);
            this.metroPanel1.Controls.Add(this.lblbillnum);
            this.metroPanel1.Controls.Add(this.dataGridView1);
            this.metroPanel1.Controls.Add(this.bttnsearch);
            this.metroPanel1.Controls.Add(this.lblvchno);
            this.metroPanel1.Controls.Add(this.panel2);
            this.metroPanel1.Controls.Add(this.txtamttype);
            this.metroPanel1.Controls.Add(this.txtvchtype);
            this.metroPanel1.Controls.Add(this.label1);
            this.metroPanel1.Controls.Add(this.txtvchno);
            this.metroPanel1.Controls.Add(this.txtnarration);
            this.metroPanel1.Controls.Add(this.lblvchdt);
            this.metroPanel1.Controls.Add(this.lblglslid);
            this.metroPanel1.Controls.Add(this.dateTimePicker1);
            this.metroPanel1.Controls.Add(this.comboBox2);
            this.metroPanel1.Controls.Add(this.textBox2);
            this.metroPanel1.Controls.Add(this.lbltotcredit);
            this.metroPanel1.Controls.Add(this.lblbalance);
            this.metroPanel1.Controls.Add(this.lbltotdebit);
            this.metroPanel1.Controls.Add(this.bttnadd);
            this.metroPanel1.Controls.Add(this.issuedt);
            this.metroPanel1.Controls.Add(this.lblchqno);
            this.metroPanel1.Controls.Add(this.instrno);
            this.metroPanel1.Controls.Add(this.lblissuedt);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(30, 73);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(725, 508);
            this.metroPanel1.TabIndex = 33;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // checkprintToggle
            // 
            this.checkprintToggle.AutoSize = true;
            this.checkprintToggle.Location = new System.Drawing.Point(52, 38);
            this.checkprintToggle.Name = "checkprintToggle";
            this.checkprintToggle.Size = new System.Drawing.Size(80, 17);
            this.checkprintToggle.TabIndex = 33;
            this.checkprintToggle.Text = "Off";
            this.checkprintToggle.UseVisualStyleBackColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(52, 13);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(94, 19);
            this.metroLabel1.TabIndex = 34;
            this.metroLabel1.Text = "Print with Save";
            // 
            // FrmVoucherAlt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(786, 608);
            this.Controls.Add(this.metroPanel1);
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmVoucherAlt";
            this.Padding = new System.Windows.Forms.Padding(15, 60, 15, 16);
            this.Text = "Voucher Entry";
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Activated += new System.EventHandler(this.FrmVoucherAlt_Activated);
            this.Load += new System.EventHandler(this.FrmVoucherAlt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmVoucherAlt_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblvchno;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtvchtype;
        private System.Windows.Forms.TextBox txtvchno;
        private System.Windows.Forms.Label lblvchdt;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblbalance;
        private System.Windows.Forms.Button bttnadd;
        private System.Windows.Forms.Label lblchqno;
        private System.Windows.Forms.Label lblissuedt;
        private System.Windows.Forms.TextBox instrno;
        private System.Windows.Forms.DateTimePicker issuedt;
        private System.Windows.Forms.Label lbltotdebit;
        private System.Windows.Forms.Label lbltotcredit;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblglslid;
        private System.Windows.Forms.TextBox txtnarration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtamttype;
        private System.Windows.Forms.DataGridViewTextBoxColumn pmttype;
        private System.Windows.Forms.DataGridViewTextBoxColumn glid;
        private System.Windows.Forms.DataGridViewTextBoxColumn slid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private RoundButton bttnsave;
        private RoundButton bttnreceipt;
        private RoundButton bttnpayment;
        private RoundButton bttncontra;
        private RoundButton bttnjournal;
        private RoundButton bttnsearch;
        private System.Windows.Forms.Label lblbillnum;
        private System.Windows.Forms.TextBox txtbillnum;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroToggle checkprintToggle;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}