using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace FINNSOFT
{
    public partial class voucherGridview : DataGridView
    {
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                try
                {
                    int col = this.CurrentCell.ColumnIndex;
                    int row = this.CurrentCell.RowIndex;

                    if (col == 0)
                    {
                        this.CurrentCell = this[3, row];
                        this.BeginEdit(true);
                    }
                    else if (col == 3)
                    {
                        this.CurrentCell = this[4, row];
                        this.BeginEdit(true);
                    }
                    else if (col == 4)
                    {
                        this.Rows.Add();
                        this.CurrentCell = this[0, this.Rows.Count - 1];
                        this.Rows[this.Rows.Count - 1].Cells[0].Value = "By";
                        this.BeginEdit(true);
                    }
                    else if (col == 5)
                    {
                        this.Rows.Add();
                        this.CurrentCell = this[0, this.Rows.Count - 1];
                        this.Rows[this.Rows.Count - 1].Cells[0].Value = "By";
                        this.BeginEdit(true);
                    }

                    return true;
                }
                catch (Exception)
                {
                }
                
            }
            

            return base.ProcessDialogKey(keyData);
        }

    }
}
