using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accenture.UIPAth.CodeReviewTool.Infrastructure
{
    public class CopyToClipboard
    {
        public void CopyDataToClipboard(DataGridView dgv)
        {
            
            if (dgv.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(dgv.GetClipboardContent());


                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show(StringConstant.Clipboard, StringConstant.exit, MessageBoxButtons.OKCancel);
                }
            }





        }
    }
}
