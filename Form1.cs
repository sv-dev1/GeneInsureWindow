using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http;
using RestSharp;
namespace Gene
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            frmClaimRegister objFrm = new frmClaimRegister();
            objFrm.Show();
            this.Hide();
        }

        private void btnNewQuote_Click(object sender, EventArgs e)
        {
            //frmNewQuote obj = new frmNewQuote();
            //obj.Show();

            frmQuote obj = new frmQuote();
            obj.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {       
        }

        private void btnQuickPrint_Click(object sender, EventArgs e)
        {
            frmLicenceQuote objLic = new frmLicenceQuote();
            objLic.Show();
            this.Hide();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            frmRenewPolicy objRE = new frmRenewPolicy();
            objRE.Show();
            this.Hide();

        }
    }
}
