using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing_ResizingForm
{
    public partial class frmArrowing : Form
    {
        public frmArrowing()
        {
            InitializeComponent();
        }

        public void ShowLeft()
        {
            picLeft.Visible = true;
            picMiddle.Visible = false;
            picRight.Visible = false;
        }

        public void ShowCenter()
        {
            picLeft.Visible = false;
            picMiddle.Visible = true;
            picRight.Visible = false;
        }

        public void ShowRight()
        {
            picLeft.Visible = false;
            picMiddle.Visible = false;
            picRight.Visible = true;
        }

        public void ShowNone()
        {
            picLeft.Visible = false;
            picMiddle.Visible = false;
            picRight.Visible = false;
        }
    }
}
