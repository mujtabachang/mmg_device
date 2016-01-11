using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

namespace Testing_ResizingForm
{
    public partial class frmSerialInterface : Form
    {
        public Boolean Connected = false;
        public frmSerialInterface()
        {
            InitializeComponent();
            Testing_ResizingForm.frmSerialInterface.CheckForIllegalCrossThreadCalls = false;
            //this.txtSend.CheckForIllegalCrossThreadCalls = false;
            
        }

        private void frmSerialInterface_Load(object sender, EventArgs e)
        {
            GetListOfPorts();
            
            
            
        }

        public void GetListOfPorts()
        {
            cboList.Items.Clear();
             // Get a list of serial port names. 
            string[] ports = SerialPort.GetPortNames();
            
            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console. 
            foreach(string port in ports)
            {
                Console.WriteLine(port);
                cboList.Items.Add(port);
            }
            try { cboList.SelectedIndex = 0; }
            catch { }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetListOfPorts();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Connected == false)
            {
                sp1.PortName = (String)cboList.SelectedItem;
                //Console.WriteLine(cboList.SelectedIndex);
                Console.WriteLine(cboList.SelectedItem);
                sp1.Open();
                if (sp1.IsOpen)
                {
                    Connected = true;
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    cboList.Enabled = false;
                    btnRefresh.Enabled = false;
                    txtSend.Enabled = true;
                    btnSend.Enabled = true;

                }
            }
          
        }

        private void sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String a = sp1.ReadExisting();
            txtRec.Text = txtRec.Text + a;
            txtRec.SelectionStart = txtRec.TextLength;
            txtRec.ScrollToCaret();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            sp1.Close();
            Connected = false;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            cboList.Enabled = true;
            btnRefresh.Enabled = true;
            txtSend.Enabled = false;
            btnSend.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //sp1.WriteLine(txtSend.Text);
            sp1.Write(txtSend.Text + "\n" + "\r");
            txtSend.Text = "";
        }

        public void SendLeft()
        {
            if (sp1.IsOpen)
            {
                sp1.Write("l" + "\n" + "\r");
            }
        }
        public void SendRight()
        {
            if (sp1.IsOpen)
            {
                sp1.Write("r" + "\n" + "\r");
            }
        }

        public void SendCenter()
        {
            if (sp1.IsOpen)
            {
                sp1.Write("c" + "\n" + "\r");
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            SendLeft();
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            SendCenter();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            SendRight();
        }
       
       
    }
}
