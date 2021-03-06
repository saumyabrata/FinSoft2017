﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FINNSOFT.UI;
using System.Security.Cryptography;
using System.IO;

namespace FINNSOFT
{
    public partial class DashBoardMenu : MetroFramework.Forms.MetroForm
    {
        public DashBoardMenu()
        {
            InitializeComponent();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGLMast f1 = new frmGLMast();
            f1.Show();
        }

        private void cashBankBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCashbook csh = new frmCashbook();
            csh.Show();
        }

        private void booksOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Details_Ledger fdel = new frm_Details_Ledger();
            fdel.comboBox1.Text = "Only This Ledger";
            fdel.Show();
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.Show();
        }

        private void currentBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCollectionView clv = new frmCollectionView();
            clv.Show();
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDaybook fdb = new frmDaybook();

            fdb.Show();
        }

        private void ledgerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLedgerList ldgst = new frmLedgerList();
            ldgst.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void proffitAndLossStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPL pl = new frmPL();
            pl.Show();
        }

        private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBalan bl = new frmBalan();
            bl.Show();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVoucherAlt FrmVoucherAltMDIChild = new FrmVoucherAlt();
            // Set the Parent Form of the Child window.  
            FrmVoucherAltMDIChild.MdiParent = this;
            // Display the new form.  
            FrmVoucherAltMDIChild.Show();
            FrmVoucherAltMDIChild.BringToFront();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVoucherPrint vchp = new FrmVoucherPrint();
            vchp.Show();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSLMast SL = new frmSLMast();
            SL.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFinyr fyr = new frmFinyr();
            fyr.Show();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackupRestore br = new frmBackupRestore();
            br.Show();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVoucherReport vr = new frmVoucherReport();
            vr.Show();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateUser usr = new frmCreateUser();
            usr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void DashBoardMenu_Load(object sender, EventArgs e)
        {
            FrmGraphs FrmGraphsMDIChild = new FrmGraphs();
            // Set the Parent Form of the Child window.  
            FrmGraphsMDIChild.MdiParent = this;
            // Display the new form.  
            FrmGraphsMDIChild.Show();
            FrmGraphsMDIChild.BringToFront();
            toolStrip1.Visible = false;

        }

        

        private void DashBoardMenu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                frmCollectionView clv = new frmCollectionView();
                clv.Show();
            }
        }

        private void creditorInvoiceMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCreditorInvoice crdinv = new FrmCreditorInvoice();
            crdinv.Show();
        }

        private void creditorInvoiceCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInvCorrection invCorrection = new frmInvCorrection();
            invCorrection.Show();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void reconcilliateBankStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStmntImport stmnt = new FrmStmntImport();
            stmnt.Show();
        }

        private void displayChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGraphs FrmGraphsMDIChild = new FrmGraphs();
            // Set the Parent Form of the Child window.  
            FrmGraphsMDIChild.MdiParent = this;
            // Display the new form.  
            FrmGraphsMDIChild.Show();
            FrmGraphsMDIChild.BringToFront();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmDaybook fdb = new frmDaybook();

            fdb.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //frmVoucher vch = new frmVoucher();
            //vch.Show();
            FrmVoucherAlt vch2 = new FrmVoucherAlt();
            vch2.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frm_Details_Ledger fdel = new frm_Details_Ledger();
            fdel.comboBox1.Text = "Only This Ledger";
            fdel.Show();
        }

        private void DashBoardMenu_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolStrip1.Bounds.Contains(e.Location) == true)
            {
                toolStrip1.Visible = true;
            }
            else
            {
                toolStrip1.Visible = false;
            }
        }

        private void toolStripContainer3_ContentPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
