using GensureAPIv2.Models;
using Insurance.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Globalization;
using InsuranceClaim.Models;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace Gene
{
    public partial class frmQuote : Form
    {
        ResultRootObject quoteresponse;

        //ResultRootObjects _quoteresponse;

        static String ApiURL = "http://geneinsureclaim2.kindlebit.com/api/Account/";
        static String IceCashRequestUrl = "http://geneinsureclaim2.kindlebit.com/api/ICEcash/";

        //static String ApiURL = "http://localhost:6220/api/Account/";
        //static String IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";


        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";


        ICEcashTokenResponse ObjToken;
        List<UserInput> objListUserInput;
        List<RiskDetailModel> objlistRisk;

        RiskDetailModel objRiskModel;
        ICEcashService IcServiceobj;
        CustomerModel customerInfo;
        ResultResponse resObject;
        SummaryDetailModel summaryModel;

        List<VehicleLicenseModel> objVehicleLicense;

        List<VehicalModel> objlistVehicalModel;
        string parternToken = "";
        bool isVehicalDeleted = false;
        bool isbackclicked = false;

        int VehicalIndex = -1;
        long TransactionId = 0;
        string responseMessage = "";

        //private static frmQuote _mf;
        public frmQuote()
        {

            
            //Load += new EventHandler(frmQuote_Load);
            objListUserInput = new List<UserInput>();
            customerInfo = new CustomerModel();
            ObjToken = new ICEcashTokenResponse();
            objlistRisk = new List<RiskDetailModel>();
            IcServiceobj = new ICEcashService();
            //  objRiskModel = new RiskDetailModel();

            resObject = new ResultResponse();

            //objlistRisk.Add(objRiskModel);

            InitializeComponent();


            this.Size = new System.Drawing.Size(1300, 720);
            pnlRiskDetails.Visible = false;
            pnlPersonalDetails.Visible = false;
            pnlsumary.Visible = false;
            pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            PnlVrn.Visible = true;


            // pnlSummery.
            //pnlSum.Location = new Point(350, 100);
            //pnlSum.Size = new System.Drawing.Size(1390, 638);

            pnlSum.Location = new Point(335, 100);
            pnlSum.Size = new System.Drawing.Size(1160, 1200);


            pnlConfirm.Visible = false;
            pnlOptionalCover.Visible = false;

            //PnlVrn.Location = new Point(335, 100);
            //PnlVrn.Size = new System.Drawing.Size(739, 238);

            PnlVrn.Location = new Point(335, 100);
            PnlVrn.Size = new System.Drawing.Size(1350, 638);

            pnlLogo.Location = new Point(this.Width - 320, this.Height - 220);

            pnlLogo.Size = new System.Drawing.Size(300, 220);

            //pnlRiskDetails.Location = new Point(120, 33);
            //pnlRiskDetails.Size = new System.Drawing.Size(900, 700);


            pnlRiskDetails.Location = new Point(335, 100);
            pnlRiskDetails.Size = new System.Drawing.Size(1350, 750);


            //pnlOptionalCover.Location = new Point(200, 33);
            //pnlOptionalCover.Size = new System.Drawing.Size(800, 1040);


            pnlOptionalCover.Location = new Point(335, 100);
            pnlOptionalCover.Size = new System.Drawing.Size(1390, 750);

            //pnlAddMoreVehicle.Location = new Point(994, 398);
            //pnlAddMoreVehicle.Size = new System.Drawing.Size(263, 99);

            pnlAddMoreVehicle.Location = new Point(1300, 398);
            pnlAddMoreVehicle.Size = new System.Drawing.Size(690, 200);
            //testdd

            //12Feb 

            pnlRadioZinara.Visible = false;
            pnlRadio.Visible = false;
            pnlZinara.Visible = false;


            pnlRadioZinara.Location = new Point(335, 100);
            pnlRadioZinara.Size = new System.Drawing.Size(1390, 750);



            pnlRadio.Location = new Point(222, 217);
            pnlRadio.Size = new System.Drawing.Size(824, 334);


            pnlZinara.Location = new Point(222, 217);
            pnlZinara.Size = new System.Drawing.Size(824, 334);



            pnlPersonalDetails.Location = new Point(355, 100);
            pnlPersonalDetails.Size = new System.Drawing.Size(1450, 750);

            pnlPersonalDetails2.Location = new Point(355, 100);
            pnlPersonalDetails2.Size = new System.Drawing.Size(1450, 750);

            pnlsumary.Location = new Point(355, 100);
            pnlsumary.Size = new System.Drawing.Size(1390, 1040);
            //pnlSummery.Size = new System.Drawing.Size(800, 700);

            //pnlConfirm.Location = new Point(200, 33);
            //pnlConfirm.Size = new System.Drawing.Size(800, 1040);


            pnlConfirm.Location = new Point(335, 100);
            pnlConfirm.Size = new System.Drawing.Size(1350, 750);

            pnlThankyou.Location = new Point(300, 33);
            pnlThankyou.Size = new System.Drawing.Size(1180, 1040);

            //pnlPaymentStatus.Location = new Point(200, 33);
            //pnlPaymentStatus.Size = new System.Drawing.Size(800, 1040);


            txtVrn.Text = "Car Registration Number";

            //txtVrn.Text = "AAD333";
            txtVrn.ForeColor = SystemColors.GrayText;

            txtZipCode.Text = "00263";
            txtZipCode.ForeColor = SystemColors.GrayText;



            bindMake();
            bindVehicleUsage();
            bindCoverType();
            bindPaymentType();
            bindAllCities();
            // Checkobject();
            // loadVRNPanel();
            //GenerateTransactionId();
            //checkdummydata();
            //  SavePaymentinformation("100000");

        }

        public void loadVRNPanel()
        {
            lblpayment.Text = "tessdddddddd";

            pnlSummery.Controls.Clear(); //to remove all controls
            // Int32 counter = 2;
            Int32 counter = objlistRisk.Count();

            for (int i = 0; i < counter; i++)
            {
                if (counter == 1)
                {
                    Panel pnl = new Panel();
                    // pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
                    pnl.BackgroundImage = Gene.Properties.Resources.top_bar;
                    pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Panel Bottompnl = new Panel();
                    //  Bottompnl.BackColor = Color.White;
                    Bottompnl.BackgroundImage = Gene.Properties.Resources.bottom_bar;
                    Bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;
                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);

                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(i + 5, 3);
                    lblvehicle.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblvehicle.BackColor = Color.Transparent;

                    pnl.Controls.Add(lblvehicle);


                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;

                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);


                    lblTermly.Text = paymentTermName.ToString();
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(i + 5, 50);
                    lblTermly.BackColor = Color.Transparent;
                    lblTermly.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :       ";
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured);

                    lblSumInsured.AutoSize = true;
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.Location = new Point(10, 140);
                    lblSumInsured.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    //Label lblSumInsuredAmount = new System.Windows.Forms.Label();
                    //lblSumInsuredAmount.Name = lblSumInsuredAmount + i.ToString();
                    //lblSumInsuredAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblSumInsuredAmount.Text = objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured); // sum insured amount
                    //lblSumInsuredAmount.AutoSize = true;
                    //lblSumInsuredAmount.BackColor = Color.Transparent;
                    //lblSumInsuredAmount.Location = new Point(160, 140);
                    //lblSumInsuredAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblSumInsuredAmount);



                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Total premium :     ";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.BackColor = Color.Transparent;
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.Location = new Point(10, 190);
                    lblTotalpremium.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);


                    //Label lblTotalpremiumAmount = new System.Windows.Forms.Label();
                    //lblTotalpremiumAmount.Name = lblTotalpremiumAmount + i.ToString();
                    //lblTotalpremiumAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblTotalpremiumAmount.Text = objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    //lblTotalpremiumAmount.BackColor = Color.Transparent;
                    //lblTotalpremiumAmount.AutoSize = true;
                    //lblTotalpremiumAmount.Location = new Point(170, 190);
                    //lblTotalpremiumAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblTotalpremiumAmount);



                    Label lblDiscount = new System.Windows.Forms.Label();
                    lblDiscount.Name = lblDiscount + i.ToString();
                    lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblDiscount.Text = "Discount :             ";
                    lblDiscount.Text += objlistRisk[i].Discount == null ? "0" : Convert.ToString(objlistRisk[i].Discount);
                    lblDiscount.BackColor = Color.Transparent;
                    lblDiscount.AutoSize = true;
                    lblDiscount.Location = new Point(10, 240);
                    lblDiscount.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblDiscount);


                    //Label lblDiscountAmount = new System.Windows.Forms.Label();
                    //lblDiscountAmount.Name = lblDiscountAmount + i.ToString();
                    //lblDiscountAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblDiscountAmount.Text = objlistRisk[i].Discount == null ? "0" : Convert.ToString(objlistRisk[i].Discount);
                    //lblDiscountAmount.BackColor = Color.Transparent;
                    //lblDiscountAmount.AutoSize = true;
                    //lblDiscountAmount.Location = new Point(160, 240);
                    //lblDiscountAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblDiscountAmount);



                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :                  ";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    //lblZTSC.Width = 100;

                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(10, 290);
                    lblZTSC.AutoSize = true;
                    lblZTSC.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);

                    //Label lblZTSCAmount = new System.Windows.Forms.Label();
                    //lblZTSCAmount.Name = lblZTSCAmount + i.ToString();
                    //lblZTSCAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblZTSCAmount.Text = objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    //lblZTSCAmount.Width = 100;

                    //lblZTSCAmount.BackColor = Color.Transparent;
                    //lblZTSCAmount.Location = new Point(160, 290);
                    //lblZTSCAmount.AutoSize = true;
                    //lblZTSCAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblZTSCAmount);


                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :         ";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    lblStampDuty.Width = 100;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.BackColor = Color.Transparent;
                    lblStampDuty.Location = new Point(10, 340);
                    lblStampDuty.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    //Label lblStampDutyAmount = new System.Windows.Forms.Label();
                    //lblStampDutyAmount.Name = lblStampDutyAmount + i.ToString();
                    //lblStampDutyAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblStampDutyAmount.Text = objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    //lblStampDutyAmount.Width = 100;
                    //lblStampDutyAmount.AutoSize = true;
                    //lblStampDutyAmount.BackColor = Color.Transparent;
                    //lblStampDutyAmount.Location = new Point(160, 340);
                    //lblStampDutyAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblStampDutyAmount);

                    string res = "";
                    string paddedParam = res.PadRight(50);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 100;
                    btnEdit.Height = 40;

                    btnEdit.Location = new Point(60, 395);
                    btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.BackColor = Color.Transparent;
                    btnEdit.FlatAppearance.BorderSize = 0;
                    Bottompnl.Controls.Add(btnEdit);




                    Button btnDelete = new System.Windows.Forms.Button();
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);

                    btnDelete.Width = 100;
                    btnDelete.Height = 40;
                    btnDelete.BackColor = Color.Transparent;
                    btnDelete.Location = new Point(480, 395);

                    btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;




                    Bottompnl.Controls.Add(btnDelete);


                    pnl.Size = new System.Drawing.Size(650, 100);
                    pnl.Location = new Point(240, (i * 140));

                    //pnl.Size = new System.Drawing.Size(650, 100);
                    //pnl.Location = new Point(140, (i * 140));

                    Bottompnl.Size = new System.Drawing.Size(650, 460);
                    Bottompnl.Location = new Point(240, (i * 140));

                    pnlSummery.Size = new System.Drawing.Size(900, 470);
                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }

                if (counter == 2)
                {
                    Panel pnl = new Panel();
                    // pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
                    pnl.BackgroundImage = Gene.Properties.Resources.top_bar;
                    pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                    Panel Bottompnl = new Panel();
                    //Bottompnl.BackColor = Color.White;
                    Bottompnl.BackgroundImage = Gene.Properties.Resources.bottom_bar;
                    Bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;
                    lblvehicle.BackColor = Color.Transparent;
                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "0" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(5, 3);
                    lblvehicle.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblvehicle);


                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);


                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;
                    lblTermly.BackColor = Color.Transparent;
                    lblTermly.Text = paymentTermName;
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(5, 50);
                    lblTermly.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :"; // sum insured amount
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "" : Convert.ToString(objlistRisk[i].SumInsured);
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.AutoSize = true;
                    lblSumInsured.Location = new Point(5, 110);
                    lblSumInsured.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    //Label lblSumInsuredAmount = new System.Windows.Forms.Label();
                    //lblSumInsuredAmount.Name = lblSumInsuredAmount + i.ToString();
                    //lblSumInsuredAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblSumInsuredAmount.Text = objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured); // sum insured amount
                    //lblSumInsuredAmount.BackColor = Color.Transparent;
                    //lblSumInsuredAmount.AutoSize = true;
                    //lblSumInsuredAmount.Location = new Point(150, 110);
                    //lblSumInsuredAmount.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblSumInsuredAmount);


                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Total premium :";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.BackColor = Color.Transparent;
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.Location = new Point(5, 155);
                    lblTotalpremium.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);


                    Label lblDiscount = new System.Windows.Forms.Label();
                    lblDiscount.Name = lblDiscount + i.ToString();
                    lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblDiscount.Text = "Discount :";
                    lblDiscount.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblDiscount.BackColor = Color.Transparent;
                    lblDiscount.AutoSize = true;
                    lblDiscount.Location = new Point(5, 210);
                    lblDiscount.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblDiscount);


                    //Label lblTotalpremiumAmount = new System.Windows.Forms.Label();
                    //lblTotalpremiumAmount.Name = lblTotalpremiumAmount + i.ToString();
                    //lblTotalpremiumAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblTotalpremiumAmount.Text = objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    ////lblTotalpremiumAmount.Text = "0";
                    //lblTotalpremiumAmount.BackColor = Color.Transparent;
                    //lblTotalpremiumAmount.AutoSize = true;
                    //lblTotalpremiumAmount.Location = new Point(150, 155);
                    //lblTotalpremiumAmount.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblTotalpremiumAmount);




                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    //lblZTSC.Width = 100;
                    lblZTSC.AutoSize = true;
                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(5, 260);
                    lblZTSC.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);


                    //Label lblZTSCAmount = new System.Windows.Forms.Label();
                    //lblZTSCAmount.Name = lblZTSC + i.ToString();
                    //lblZTSCAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblZTSCAmount.Text = objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    ////lblZTSCAmount.Text = "0";
                    //lblZTSCAmount.Width = 100;
                    //lblZTSCAmount.BackColor = Color.Transparent;
                    //lblZTSCAmount.Location = new Point(150, 210);
                    //lblZTSCAmount.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblZTSCAmount);



                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    lblStampDuty.BackColor = Color.Transparent;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.Width = 100;
                    lblStampDuty.Location = new Point(5, 310);
                    lblStampDuty.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    // Label lblStampDutyAmount = new System.Windows.Forms.Label();
                    // lblStampDutyAmount.Name = lblStampDutyAmount + i.ToString();
                    // lblStampDutyAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    // lblStampDutyAmount.Text = objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    //// lblStampDutyAmount.Text = "0";
                    // lblStampDutyAmount.BackColor = Color.Transparent;
                    // lblStampDutyAmount.AutoSize = true;
                    // lblStampDutyAmount.Width = 100;
                    // lblStampDutyAmount.Location = new Point(150, 260);
                    // lblStampDutyAmount.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    // Bottompnl.Controls.Add(lblStampDutyAmount);

                    string res = "";
                    string paddedParam = res.PadRight(50);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 90;
                    btnEdit.Height = 40;
                    btnEdit.BackColor = Color.Transparent;
                    btnEdit.Location = new Point(25, 340);
                    btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.FlatAppearance.BorderSize = 0;

                    Bottompnl.Controls.Add(btnEdit);


                    Button btnDelete = new System.Windows.Forms.Button();
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    //btnDelete.Text = "";
                    btnDelete.Width = 90;
                    btnDelete.Height = 40;
                    btnDelete.Location = new Point(270, 340);
                    btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackColor = Color.Transparent;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                    Bottompnl.Controls.Add(btnDelete);


                    pnl.Size = new System.Drawing.Size(370, 100);
                    pnl.Location = new Point((i * 420), 50);


                    Bottompnl.Size = new System.Drawing.Size(370, 400);
                    Bottompnl.Location = new Point((i * 420), 50);

                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }
                else if (counter > 2)
                {

                    Panel pnl = new Panel();
                    pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));

                    Panel Bottompnl = new Panel();
                    //  Bottompnl.BackgroundImage = Gene.Properties.Resources.bottom_bar;
                    // Bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    Bottompnl.BackColor = Color.White;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;

                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "0" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(i, 3);
                    lblvehicle.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblvehicle);


                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);

                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;
                    lblTermly.Text = paymentTermName;
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(i + 400, 3);
                    lblTermly.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :";
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured);
                    lblSumInsured.AutoSize = true;
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.Location = new Point(i, 55);
                    lblSumInsured.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Total premium :";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.BackColor = Color.Transparent;
                    //lblTotalpremium.Location = new Point(i + 150, 55);
                    lblTotalpremium.Location = new Point(i + 170, 55);
                    lblTotalpremium.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);

                    //Label lblDiscount = new System.Windows.Forms.Label();
                    //lblDiscount.Name = lblDiscount + i.ToString();
                    //lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblDiscount.Text = "Discount :";
                    //lblDiscount.Text += objlistRisk[i].Discount == 0 ? "" : Convert.ToString(objlistRisk[i].Discount);
                    //lblDiscount.AutoSize = true;
                    //lblDiscount.BackColor = Color.Transparent;
                    //lblDiscount.Location = new Point(i + 300, 55);
                    //lblDiscount.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblDiscount);


                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    // lblZTSC.Width = 70;
                    lblZTSC.AutoSize = true;
                    lblZTSC.BackColor = Color.Transparent;
                    //lblZTSC.Location = new Point(i + 300, 55);
                    lblZTSC.Location = new Point(i + 340, 55);
                    lblZTSC.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);

                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    //lblStampDuty.Width = 100;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.BackColor = Color.Transparent;
                    //lblStampDuty.Location = new Point(i + 420, 55);
                    lblStampDuty.Location = new Point(i + 450, 55);
                    lblStampDuty.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    string res = "";
                    string paddedParam = res.PadRight(50);
                    //string paddedParam = res.PadRight(80);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 70;
                    //btnEdit.Location = new Point(i + 540, 55);

                    btnEdit.Location = new Point(i + 600, 55);

                    btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.FlatAppearance.BorderSize = 0;




                    Bottompnl.Controls.Add(btnEdit);


                    Button btnDelete = new System.Windows.Forms.Button();
                    //btnDelete.Text = "Delete";
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnDelete.Width = 70;

                    //btnDelete.Location = new Point(i + 630, 55);
                    btnDelete.Location = new Point(i + 680, 55);
                    btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Bottompnl.Controls.Add(btnDelete);


                    pnl.Size = new System.Drawing.Size(750, 40);
                    pnl.Location = new Point(i, (i * 110));


                    Bottompnl.Size = new System.Drawing.Size(750, 90);
                    Bottompnl.Location = new Point(i, (i * 110));

                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }

            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string s = (sender as Button).Text;
            var vehicalDetails = objlistRisk.Find(c => c.RegistrationNo == s.Trim());
            objlistRisk.Remove(vehicalDetails);

            objRiskModel = new RiskDetailModel();

            loadVRNPanel();

            MessageBox.Show(s.Trim() + " Vehicle Successfully Deleted.");

            isVehicalDeleted = true;

            //PnlVrn.Visible = true;
            //pnlSum.Visible = false;
            NewVRN();

            //throw new NotImplementedException();
        }

        public string GetPaymentTerm(string paymentTerm)
        {
            var paymentTermName = "";
            if (paymentTerm == "1")
                paymentTermName = "Yearly";
            else
                paymentTermName = paymentTerm + " Months";

            return paymentTermName;
        }

        //private void ClearControl()
        //{
        //    txtVrn.Text = string.Empty;
        //    txtSumInsured.Text = string.Empty;
        //    cmbVehicleUsage.SelectedIndex = 0;
        //    cmbPaymentTerm.SelectedIndex = 0;
        //    cmbCoverType.SelectedIndex = 0;
        //    cmbMake.SelectedIndex = 0;
        //    cmbModel.SelectedIndex = 0;
        //    txtYear.Text = string.Empty;
        //    txtChasis.Text = string.Empty;
        //    txtEngine.Text = string.Empty;
        //    pnlAddMoreVehicle.Visible = false;
        //}

        private void RequestQuote()
        {
            throw new NotImplementedException();
        }

        public void CheckToken()
        {
            if (ObjToken == null)
            {
                ObjToken = IcServiceobj.getToken();
                ProcessICECashrequest(txtVrn.Text, txtSumInsured.Text, cmbMake.SelectedText, cmbModel.SelectedText, Convert.ToString(cmbPaymentTerm.SelectedValue), txtYear.Text, Convert.ToString(cmbCoverType.SelectedValue), Convert.ToString(cmbVehicleUsage.SelectedValue), "1");

            }

        }
        public void ProcessICECashrequest(String VRN, String SumINsured, String Make, String Model, String Paymentterm, String VehYear, String CoverType, String VehicleUsage, String TaxClass)
        {
            ResultRootObject quoteresponse = IcServiceobj.RequestQuote(ObjToken.Response.PartnerToken, VRN, SumINsured, Make, Model, Convert.ToInt32(Paymentterm), Convert.ToInt32(VehYear), Convert.ToInt32(CoverType), Convert.ToInt32(VehicleUsage), ObjToken.PartnerReference, customerInfo);
            if (quoteresponse != null)
            {
                ResultResponse resObject = quoteresponse.Response;
                if (resObject.Message == "Partner Token has expired")
                {
                    ObjToken = null;
                    CheckToken();
                }
                else if (resObject.Message == "Insufficient Fund to process financial transaction")
                {
                    MessageBox.Show(resObject.Message.ToString());
                }
                else
                {
                    MessageBox.Show(resObject.Message.ToString());
                }
            }
        }
        public void bindCoverType()
        {

            var client = new RestClient(ApiURL + "CoverTypes");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            cmbCoverType.DataSource = result;
            cmbCoverType.DisplayMember = "name";
            cmbCoverType.ValueMember = "ID";
            //cmbCoverType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        public void bindMake()
        {

            //var LocaCashRequestUrl = "http://localhost:6220/api/Account/";

            //var client = new RestClient(LocaCashRequestUrl + "Makes");

            var client = new RestClient(ApiURL + "Makes");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<MakeObject>>(response.Content);
            cmbMake.DataSource = result;
            cmbMake.DisplayMember = "MakeDescription";
            cmbMake.ValueMember = "makeCode";

            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                bindModel(Convert.ToString(cmbMake.SelectedValue));
            }
            else
            {
                this.Invoke(new Action(() => bindModel(Convert.ToString(cmbMake.SelectedValue))));
            }
        }

        public void bindModel(String MaKECode)
        {

            var client = new RestClient(ApiURL + "Models?makeCode=" + MaKECode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<ModelObject>>(response.Content);
            cmbModel.DataSource = result;
            cmbModel.DisplayMember = "modeldescription";
            cmbModel.ValueMember = "ModelCode";
            cmbPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }

        public decimal GetDiscount(decimal premiumAmount, int paymentTermId)
        {

            decimal discount = 0;
            var client = new RestClient(IceCashRequestUrl + "CalculateDiscount?premiumAmount=" + premiumAmount + "&PaymentTermId=" + paymentTermId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<RiskDetailModel>(response.Content);

            return result.Discount == null ? 0 : Math.Round(result.Discount.Value, 2);
        }

        public void bindPaymentType()
        {

            var client = new RestClient(ApiURL + "AllPayment");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            if (result != null)
            {
                cmbPaymentTerm.DataSource = result;
                cmbPaymentTerm.DisplayMember = "name";
                cmbPaymentTerm.ValueMember = "ID";

                //Ds 13 Feb

                ZinPaymentDetail.DataSource = result;
                ZinPaymentDetail.DisplayMember = "name";
                ZinPaymentDetail.ValueMember = "ID";


                RadioPaymnetTerm.DataSource = result;
                RadioPaymnetTerm.DisplayMember = "name";
                RadioPaymnetTerm.ValueMember = "ID";
            }
        }

        public void bindAllCities()
        {
            var client = new RestClient(ApiURL + "AllCities");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);
            if (result != null)
            {
                cmdCity.DataSource = result;
                cmdCity.DisplayMember = "name";
                cmdCity.ValueMember = "CityName";
            }
        }

        public void bindVehicleUsage()
        {

            var client = new RestClient(ApiURL + "VehicleUsage");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<VehUsageObject>>(response.Content);
            if (result != null)
            {
                cmbVehicleUsage.DataSource = result;
                cmbVehicleUsage.DisplayMember = "vehUsage";
                cmbVehicleUsage.ValueMember = "id";
                //cmbVehicleUsage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

        }

        public int bindradioamout(int ProductId, int PaymentTermId)
        {

            vehicledetailModel obj = new vehicledetailModel { ProductId = ProductId, PaymentTermId = PaymentTermId };


            //var client = new RestClient(ApiURL + "Models?makeCode=" + MaKECode);
            //var LoApiURL = "http://localhost:6220/api/Account/";
            //var client = new RestClient(LoApiURL + "getRadioAmount");
            var client = new RestClient(ApiURL + "VehicleUsage");
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            //request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<RadioLicenceAmount>(response.Content);


            return result.RadioLicenceAmounts == null ? 0 : result.RadioLicenceAmounts;

            //return result.RadioLicenceAmounts == null ? 0 : result.RadioLicenceAmounts;


            //objRiskModel.RadioLicenseCost = result.RadioLicenceAmounts;



        }

        private void frmQuote_Load(object sender, EventArgs e)
        {
            //txtZipCode.Text = "00263";
        }

        private void txtVrn_Enter(object sender, EventArgs e)
        {

        }

        private void txtVrn_Leave(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            isbackclicked = false;
            this.Invoke(new Action(() => pictureBox1.Visible = true));
            // first screen where enter vrn number
            if (txtVrn.Text == string.Empty || txtVrn.Text == "Car Registration Number" || txtVrn.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Registration Number");
                return;
            }
            else
            {
                if (VehicalIndex == -1)
                {
                    var vehicalDetails = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtVrn.Text.Trim());
                    if (vehicalDetails != null)
                    {
                        MessageBox.Show("You have already added this registration number.");
                        VrnAlredyExist();
                        return;
                    }
                }
                else
                {
                    objlistRisk[VehicalIndex].RegistrationNo = txtVrn.Text;
                    var vehicalList = objlistRisk.Where(c => c.RegistrationNo == txtVrn.Text.Trim()).ToList();
                    if (vehicalList != null)
                    {
                        if (vehicalList.Count > 1)
                        {
                            MessageBox.Show("You have already added this registration number.");
                            VrnAlredyExist();
                            return;
                        }
                    }
                }

                var success = 0;
                success = ProcessQuote();
                objRiskModel.RegistrationNo = txtVrn.Text;

                if (success == 1)
                {

                    //pictureBox1.Visible = false;
                    this.Invoke(new Action(() => pictureBox1.Visible = false));
                    this.Invoke(new Action(() => pnlConfirm.Visible = true));
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    this.Invoke(new Action(() => PnlVrn.Visible = false));
                    this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
                }
                if (success == 2)
                {
                    //pictureBox1.Visible = false;
                    //this.Invoke(new Action(() => pictureBox1.Visible = false));


                    this.Invoke(new Action(() => pictureBox1.Visible = false));
                    this.Invoke(new Action(() => pnlConfirm.Visible = true));
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    this.Invoke(new Action(() => PnlVrn.Visible = false));
                    this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));

                    //MessageBox.Show("Unable to retrieve vehicle info from Zimlic.");
                    MessageBox.Show("Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.");
                }
                if (success == 3)
                {
                    this.Invoke(new Action(() => pictureBox1.Visible = false));
                    this.Invoke(new Action(() => pnlConfirm.Visible = true));
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    this.Invoke(new Action(() => PnlVrn.Visible = false));
                    this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            // throw new NotImplementedException();
        }

        private void VrnAlredyExist()
        {
            this.Invoke(new Action(() => pnlConfirm.Visible = false));
            //this.Invoke(new Action(() => pnlRiskDetails.Visible = false));
            this.Invoke(new Action(() => PnlVrn.Visible = true));
            this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            PnlVrn.Visible = true;
            pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            pnlPersonalDetails.Visible = false;
            pnlConfirm.Visible = false;
            pnlRiskDetails.Visible = false;

            string s = (sender as Button).Text;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == s.Trim());

            if (VehicalIndex != -1)
            {
                txtVrn.Text = s.Trim();
            }
            //bindVehicleUsage();
            //bindPaymentType();
            //bindCoverType();
        }

        //public void ProcessQuote()
        public int ProcessQuote()
        {
            int result = 0;
            objRiskModel = new RiskDetailModel();
            ICEcashService IcServiceobj = new ICEcashService();

            //9Jan2019
            ObjToken = CheckParterTokenExpire();

            if (ObjToken != null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }

            quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, ""); // comment for testing

            // parternToken = ObjToken.Response.PartnerToken;


            //quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, "d3238e6e-65fd-4b55-b7aa-5206ad79"); // comment for testing

            if (quoteresponse != null)
            {
                resObject = quoteresponse.Response;
                //if token expire
                if (resObject != null && resObject.Message == "Partner Token has expired. ")
                {
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                    {
                        parternToken = ObjToken.Response.PartnerToken;
                        quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, "");
                        resObject = quoteresponse.Response;

                    }
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber);
                    //    var _resObject = quoteresponse.Response;
                    //}
                }


                if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                {
                    if (resObject != null && resObject.Message != "ICEcash System Error [O]")
                    {
                        result = 1;

                        objRiskModel.isVehicleRegisteredonICEcash = true;

                        //17-jan-19

                        if (resObject.Quotes != null && resObject.Quotes[0].Vehicle != null)
                        {
                            string make = resObject.Quotes[0].Vehicle.Make;
                            string model = resObject.Quotes[0].Vehicle.Model;
                            if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model))
                            {
                                SaveVehicalMakeAndModel(make, model);
                                bindMake();
                            }
                            else
                            {
                                // set make and model if IceCash does not retrun
                                resObject.Quotes[0].Vehicle.Make = "0";
                                resObject.Quotes[0].Vehicle.Model = "0";
                            }
                        }
                        //End

                        //15 feb

                        //var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber);
                        //var _resObjects = _quoteresponse.Response;
                        //if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                        //{
                        //    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                        //    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                        //    this.Invoke(new Action(() => txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                        //    this.Invoke(new Action(() => txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                        //    this.Invoke(new Action(() => txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                        //}


                        //end

                        //Policy Details 
                        if (VehicalIndex != -1)
                        {
                            if (resObject.Quotes[0].InsuranceID != null)
                            {
                                objlistRisk[VehicalIndex].InsuranceId = resObject.Quotes[0].InsuranceID;
                            }
                            if (resObject.Quotes[0].Policy != null)
                            {
                                this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                                if (resObject.Quotes[0].Policy.DurationMonths != null)
                                {
                                    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = 1));
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                    }


                                    // Ds 13 Feb

                                    this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = 1));
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                    }


                                    this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = 1));
                                        //bindradioamout();
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                        //bindradioamout();
                                    }


                                }


                                //Bind premium amount
                                objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                var discount = 0.00m;
                                this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));
                                objlistRisk[VehicalIndex].Discount = discount;
                            }

                            if (resObject.Quotes[0].Vehicle != null)
                            {
                                this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));
                                this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));
                                Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                                this.Invoke(new Action(() => cmbMake.SelectedIndex = index));
                                this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));
                                Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);
                                this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));
                                SetValueDuringEdit();
                            }
                            int cmVehicleValue = 0;

                            if (!this.IsHandleCreated)
                            {
                                this.CreateHandle();
                                if (cmbVehicleUsage.SelectedValue != null)
                                {
                                    cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                                }
                            }

                            else
                            {
                                this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                            }



                            //if (cmbVehicleUsage.SelectedValue != null)
                            //{
                            //    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                            //}
                            if (cmVehicleValue != 0)
                            {
                                //this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));

                                //29-Jan-2019

                                var ProductId = this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                                if (ProductId != null)
                                {
                                    objlistRisk[VehicalIndex].ProductId = Convert.ToInt32(ProductId);
                                }

                                //End


                            }
                            if (resObject.Quotes[0].Client != null)
                            {
                                this.Invoke(new Action(() => txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName));
                                this.Invoke(new Action(() => txtPhone.Text = ""));
                                this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                                this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                                //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                                this.Invoke(new Action(() => cmdCity.Text = resObject.Quotes[0].Client.Town));
                                this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
                            }




                            //this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                            //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));

                            //int cmVehicleValue = 0;
                            //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                            //if (cmVehicleValue != 0)
                            //{
                            //    this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                            //}
                            //this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));


                            //if (resObject.Quotes[0].Policy.DurationMonths == "12")
                            //{
                            //    cmbPaymentTerm.SelectedValue = 1;
                            //}
                            //else
                            //{
                            //    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                            //}

                            //txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName;
                            //txtPhone.Text = "";
                            //this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                            //this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                            //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                            //this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
                            // this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

                            // Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);

                            //this.Invoke(new Action(() => cmbMake.SelectedIndex = index));

                            //this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));


                            //Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

                            //this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

                            //Bind premium amount
                            //objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount);
                            //objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy);
                            //objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty);
                            //objlistRisk[VehicalIndex].InsuranceId = resObject.Quotes[0].InsuranceID;

                            //var discount = 0.00m;
                            //this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

                            //objlistRisk[VehicalIndex].Discount = discount;

                        }
                        else
                        {
                            objRiskModel.isVehicleRegisteredonICEcash = true;
                            if (resObject.Quotes != null)
                            {
                                if (resObject.Quotes[0].InsuranceID != null)
                                {
                                    this.Invoke(new Action(() => objRiskModel.InsuranceId = resObject.Quotes[0].InsuranceID));
                                }

                                if (resObject.Quotes[0].Policy != null)
                                {
                                    if (resObject.Quotes[0].Policy.InsuranceType != null)
                                    {
                                        this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                                    }

                                    if (resObject.Quotes[0].Policy.DurationMonths != null)
                                    {
                                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));

                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = 1));
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                        }
                                        //Ds 13 Feb

                                        this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = 1));
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                        }


                                        this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = 1));
                                            //bindradioamout();
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                                            //bindradioamout();
                                        }



                                    }

                                    //this.Invoke(new Action(() => objRiskModel.Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount)));

                                    this.Invoke(new Action(() => objRiskModel.Premium = resObject.Quotes[0].Policy.CoverAmount == null ? 0 : Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture)));

                                    this.Invoke(new Action(() => objRiskModel.ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture)));
                                    this.Invoke(new Action(() => objRiskModel.StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture)));

                                    var discount = 0.00m;
                                    this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));
                                    objRiskModel.Discount = discount;
                                }
                                if (resObject.Quotes[0].Vehicle != null)
                                {
                                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));
                                    this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));
                                    Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                                    this.Invoke(new Action(() => cmbMake.SelectedIndex = index));

                                    int indexMake = 0;
                                    this.Invoke(new Action(() => indexMake = cmbMake.SelectedIndex));
                                    if (indexMake == -1)
                                    {
                                        bindModel("0");
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));
                                    }

                                    Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);
                                    this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

                                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));

                                }
                                int cmVehicleValue = 0;

                                //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                                if (!this.IsHandleCreated)
                                {
                                    this.CreateHandle();
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                                    }
                                }

                                else
                                {
                                    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                                }



                                if (cmVehicleValue != 0)
                                {
                                    this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                                }

                                if (resObject.Quotes[0].Client != null)
                                {
                                    this.Invoke(new Action(() => txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName));
                                    this.Invoke(new Action(() => txtPhone.Text = ""));
                                    this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                                    this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                                    //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                                    this.Invoke(new Action(() => cmdCity.Text = resObject.Quotes[0].Client.Town));
                                    this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));

                                }
                            }

                            //this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                            //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));
                            //int cmVehicleValue = 0;

                            //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));


                            //if (cmVehicleValue != 0)
                            //{
                            //    this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                            //}



                            //this.Invoke(new Action(() => txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName));
                            //this.Invoke(new Action(() => txtPhone.Text = ""));
                            //this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                            //this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                            //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                            //this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
                            //this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

                            //Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                            //this.Invoke(new Action(() => cmbMake.SelectedIndex = index));
                            //int indexMake = 0;
                            //this.Invoke(new Action(() => indexMake = cmbMake.SelectedIndex));
                            //if (indexMake == -1)
                            //{
                            //    bindModel("0");
                            //}
                            //else
                            //{
                            //    this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));
                            //}

                            //this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));

                            //Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

                            //this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

                            //this.Invoke(new Action(() => objRiskModel.Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount)));
                            //this.Invoke(new Action(() => objRiskModel.ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy)));
                            //this.Invoke(new Action(() => objRiskModel.StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty)));


                            //var discount = 0.00m;
                            //this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

                            //objRiskModel.Discount = discount;
                        }
                    }
                }
                else
                {
                    result = 2;

                    SetValueDuringEdit();
                }
            }

            else
            {

                SetValueDuringEdit();
                int cmVehicleValue = 0;
                if (!this.IsHandleCreated)
                {
                    this.CreateHandle();
                    if (cmbVehicleUsage.SelectedValue != null)
                    {
                        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    }
                }
                else
                {
                    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                }
                result = 3;
            }

            return result;
        }


        private void SetValueDuringEdit()
        {
            var SumInsured = 0.00m;
            var ZTSCLevies = 0.00m;
            var StampDuty = 0.00m;
            var Discount = 0.00m;

            if (VehicalIndex != -1)
            {
                if (this.InvokeRequired)
                {
                    // this.CreateHandle();

                    this.Invoke(new Action(() => cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId));
                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage));
                    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId));




                    // SumInsured= objlistRisk[VehicalIndex].SumInsured == null ? "" : objlistRisk[VehicalIndex].SumInsured.ToString();
                    SumInsured = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].SumInsured == null ? "0" : objlistRisk[VehicalIndex].SumInsured.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    //txtSumInsured.Text = Convert.ToString(SumInsured);
                    if (objlistRisk[VehicalIndex].CoverTypeId == 4)
                    {
                        this.Invoke(new Action(() => label2.Visible = true));
                        this.Invoke(new Action(() => txtSumInsured.Visible = true));
                        txtSumInsured.Text = Convert.ToString(SumInsured);
                    }
                    else
                    {
                        this.Invoke(new Action(() => label2.Visible = false));
                        this.Invoke(new Action(() => txtSumInsured.Visible = false));
                    }


                    ZTSCLevies = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].ZTSCLevy == null ? "" : objlistRisk[VehicalIndex].ZTSCLevy.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    // txtZTSCLevies.Text = Convert.ToString(ZTSCLevies);

                    this.Invoke(new Action(() => txtZTSCLevies.Text = Convert.ToString(ZTSCLevies)));


                    StampDuty = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].StampDuty == null ? "" : objlistRisk[VehicalIndex].StampDuty.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);

                    this.Invoke(new Action(() => txtStampDuty.Text = Convert.ToString(StampDuty)));

                    Discount = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].Discount == null ? "" : objlistRisk[VehicalIndex].Discount.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);


                    this.Invoke(new Action(() => txtDiscount.Text = Convert.ToString(Discount)));
                }
                else
                {

                    SumInsured = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].SumInsured == null ? "0.00" : objlistRisk[VehicalIndex].SumInsured.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    ZTSCLevies = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].ZTSCLevy == null ? "0.00" : objlistRisk[VehicalIndex].ZTSCLevy.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    StampDuty = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].StampDuty == null ? "0.00" : objlistRisk[VehicalIndex].StampDuty.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    Discount = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].Discount == null ? "0.00" : objlistRisk[VehicalIndex].Discount.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);


                    //this.Invoke(new Action(() => txtSumInsured.Text = Convert.ToString(SumInsured)));


                    this.Invoke(new Action(() => cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId == null ? "" : objlistRisk[VehicalIndex].CoverTypeId.ToString()));
                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage == null ? "" : objlistRisk[VehicalIndex].VehicleUsage.ToString()));
                    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId == null ? "" : objlistRisk[VehicalIndex].vehicleindex.ToString()));




                    //this.Invoke(new Action(() => cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId)); 
                    //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage));
                    //this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId));

                    if (objlistRisk[VehicalIndex].CoverTypeId == 4)
                    {
                        this.Invoke(new Action(() => label2.Visible = true));
                        this.Invoke(new Action(() => txtSumInsured.Visible = true));
                        this.Invoke(new Action(() => txtSumInsured.Text = Convert.ToString(SumInsured)));
                    }
                    else
                    {
                        this.Invoke(new Action(() => label2.Visible = false));
                        this.Invoke(new Action(() => txtSumInsured.Visible = false));
                    }

                    this.Invoke(new Action(() => txtZTSCLevies.Text = Convert.ToString(ZTSCLevies)));
                    this.Invoke(new Action(() => txtStampDuty.Text = Convert.ToString(StampDuty)));
                    this.Invoke(new Action(() => txtDiscount.Text = Convert.ToString(Discount)));
                }
            }
        }


        public ICEcashTokenResponse CheckParterTokenExpire()
        {
            if (ObjToken != null && ObjToken.PartnerReference != null)
            {
                var icevalue = ObjToken;
                string format = "yyyyMMddHHmmss";
                var IceDateNowtime = DateTime.Now;
                var IceExpery = DateTime.ParseExact(icevalue.Response.ExpireDate, format, CultureInfo.InvariantCulture);
                if (IceDateNowtime > IceExpery)
                {
                    ObjToken = IcServiceobj.getToken();
                }
                icevalue = ObjToken;
            }
            else
            {
                ObjToken = IcServiceobj.getToken();
            }

            return ObjToken;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPernalBack_Click(object sender, EventArgs e)
        {
            pnlSum.Visible = true;
            // pnlAddMoreVehicle.Visible = true;
            pnlPersonalDetails.Visible = false;
            pnlAddMoreVehicle.Visible = true;

            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);

        }

        private void btnRiskCont_Click(object sender, EventArgs e)
        {
            int CoverId = Convert.ToInt32(cmbCoverType.SelectedValue);
            if (CoverId == 4)
            {
                if (txtSumInsured.Text == string.Empty || txtSumInsured.Text == "0")
                {
                    MessageBox.Show("Please Enter Sum Insured");
                    return;
                }
            }
            // need to do   

            btnRiskCont.Text = "Processing..";
            GetPremiumAmount_ChangeOfCoverType();
            btnRiskCont.Text = "Continue";

            if (VehicalIndex == -1)
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    //objRiskModel.SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);

                    objRiskModel.SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    objRiskModel.PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objRiskModel.CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);

                    //29-jan-2019
                    var ProductId = bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
                    if (ProductId != null)
                    {
                        objRiskModel.ProductId = Convert.ToInt32(ProductId);
                    }
                    //12-Feb-2019
                    //var radioamount = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId);
                    //if (radioamount != null)
                    //{
                    //    objRiskModel.RadioLicenseCost = radioamount;
                    //}
                }
            }
            else
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    //objlistRisk[VehicalIndex].SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);

                    objlistRisk[VehicalIndex].SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objlistRisk[VehicalIndex].VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    objlistRisk[VehicalIndex].PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objlistRisk[VehicalIndex].CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);

                    //29-jan-2019
                    var ProductId = bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
                    if (ProductId != null)
                    {
                        objlistRisk[VehicalIndex].ProductId = Convert.ToInt32(ProductId);
                    }
                    //12-Feb-2019

                    //var radioamount = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId);
                    //if (radioamount != null)
                    //{
                    //    objlistRisk[VehicalIndex].RadioLicenseCost = radioamount;
                    //}
                }
            }
            //pnlConfirm.Visible = true;
            pnlOptionalCover.Visible = true;
            pnlRiskDetails.Visible = false;


        }

        private void btnRiskBack_Click(object sender, EventArgs e)
        {
            //PnlVrn.Visible = true;
            pnlConfirm.Visible = true;
            pnlRiskDetails.Visible = false;

        }

        private void txtCoverStartDate_MaskInputRejected(object sender, System.Windows.Forms.MaskInputRejectedEventArgs e)
        {

        }

        private void btnPr2Back_Click(object sender, EventArgs e)
        {

        }

        private void btnPersoanlContinue_Click(object sender, EventArgs e)
        {

            if (txtName.Text == string.Empty || txtEmailAddress.Text == string.Empty || txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Please Enter the required fields");
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtEmailAddress.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(txtEmailAddress.Text))
                {
                    MessageBox.Show("Please Enter a valid email");
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                //Regex regs = new Regex(@"^\(?([0-9]{4})\)?[ ]?([0-9]{3})[ ]?([0-9]{3})$");



                Regex regs = new Regex(@"^([0-9]{4}[0-9]{3}[0-9]{3})$");
                if (!regs.IsMatch(txtPhone.Text))
                {
                    MessageBox.Show("Please Enter a valid Phone Number");
                    return;
                }
            }
            if (!(rdbMale.Checked || rdbFemale.Checked))
            {
                MessageBox.Show("Please Select Gender");
                return;
            }
            if (txtPhone.Text.Length < 10)
            {
                MessageBox.Show("Please Enter a Valid Phone number");
                txtPhone.Focus();
                return;
            }
            //if (txtPhone.Text != string.Empty)
            //{
            //    string phone = txtPhone.Text;
            //    if (phone != string.Empty)
            //    {
            //        Regex re = new Regex("^9[0-9]{9}");

            //        if (re.IsMatch(txtPhone.Text.Trim()) == false || txtPhone.Text.Length > 10)
            //        {
            //            MessageBox.Show("Invalid Mobile Number!!");
            //            txtPhone.Focus();
            //        }

            //        //var a = (Regex.Match(phone, @"^(\+[0-9]{9})$").Success);
            //        //var a = (Regex.Match(phone, @"[0-9]{3}-[0-9]{3}-[0-9]{4}").Success);           
            //        //if (a==false)
            //        //{
            //        //    MessageBox.Show("Format: 123-456-7890");
            //        //    txtPhone.Focus();
            //        //    return;
            //        //}        
            //    }
            //}

            if (txtName.Text != string.Empty && txtEmailAddress.Text != string.Empty && txtPhone.Text != string.Empty)
            {
                int result = checkEmailExist();
                if (result == 1)
                {
                    MessageBox.Show("Email already Exist");
                    return;
                }
                else
                {
                    pnlPersonalDetails2.Visible = true;
                    pnlPersonalDetails.Visible = false;
                }
            }

            string theDate = txtDOB.Value.ToString("dd/MM/yyyy");
            //if (!rdbMale.Checked)
            //{
            //    MessageBox.Show("Please select gender");
            //    return;
            //}
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnConfContinue_Click(object sender, EventArgs e)
        {
            // third screen confirm vehical details

            if (VehicalIndex == -1)
            {
                if (txtYear.Text == string.Empty || cmbModel.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter the required fields");
                    return;
                }
                if (cmbCoverType.SelectedValue != null)
                {
                    var CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                    if (CoverType == 4)
                    {
                        label2.Visible = true;
                        txtSumInsured.Visible = true;
                    }
                }


                else
                {
                    objRiskModel.VehicleYear = txtYear.Text == "" ? 1900 : Convert.ToInt32(txtYear.Text);
                    objRiskModel.MakeId = Convert.ToString(cmbMake.SelectedValue);
                    objRiskModel.ModelId = Convert.ToString(cmbModel.SelectedValue);
                    objRiskModel.EngineNumber = Convert.ToString(txtEngine.Text);
                    objRiskModel.ChasisNumber = Convert.ToString(txtChasis.Text);
                }



            }
            else
            {
                if (txtYear.Text == string.Empty || cmbModel.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter the required fields");
                    return;
                }

                else
                {
                    objlistRisk[VehicalIndex].VehicleYear = txtYear.Text == "" ? 1900 : Convert.ToInt32(txtYear.Text);
                    objlistRisk[VehicalIndex].MakeId = cmbMake.SelectedValue == null ? "0" : Convert.ToString(cmbMake.SelectedValue);
                    objlistRisk[VehicalIndex].ModelId = cmbModel.SelectedValue == null ? "0" : Convert.ToString(cmbModel.SelectedValue);
                    objlistRisk[VehicalIndex].EngineNumber = Convert.ToString(txtEngine.Text);
                    objlistRisk[VehicalIndex].ChasisNumber = Convert.ToString(txtChasis.Text);

                    //objlistRisk[VehicalIndex].MakeId = Convert.ToString(cmbMake.SelectedValue);
                    //objlistRisk[VehicalIndex].ModelId = Convert.ToString(cmbModel.SelectedValue);

                }
            }

            //pnlOptionalCover.Visible = true;
            //pnlConfirm.Visible = true;
            pnlRiskDetails.Visible = true;
            pnlConfirm.Visible = false;

        }

        private void btnConfBack_Click(object sender, EventArgs e)
        {
            //pnlRiskDetails.Visible = true;
            PnlVrn.Visible = true;
            pnlConfirm.Visible = false;

        }

        private void pnlVRNtextbox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlSummery_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlPersonalDetails_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlsumary_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnPerBack2_Click(object sender, EventArgs e)
        {
            pnlPersonalDetails.Visible = true;
            pnlPersonalDetails2.Visible = false;
        }

        public void btnPer2Con_Click(object sender, EventArgs e)
        {
            if (txtAdd1.Text == string.Empty || txtAdd2.Text == string.Empty || cmdCity.SelectedIndex == -1 || txtIDNumber.Text == string.Empty || txtZipCode.Text == string.Empty)
            {
                MessageBox.Show("Please Enter the required fields.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtIDNumber.Text))
            {
                Regex reg = new Regex(@"^([0-9]{2}-[0-9]{6,7}[a-zA-Z]{1}[0-9]{2})$");
                if (!reg.IsMatch(txtIDNumber.Text))
                {
                    MessageBox.Show("Please Enter a valid National Identification Number");
                    return;
                }
            }

            if (txtAdd1.Text != string.Empty && txtAdd2.Text != string.Empty && cmdCity.SelectedIndex != -1 && txtIDNumber.Text != string.Empty && txtZipCode.Text != string.Empty)
            {

                pnlsumary.Visible = true;
                pnlPersonalDetails2.Visible = false;

                customerInfo.FirstName = txtName.Text;
                customerInfo.LastName = "";
                customerInfo.EmailAddress = txtEmailAddress.Text;
                customerInfo.AddressLine2 = txtAdd2.Text;
                customerInfo.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
                //customerInfo.City = txtCity.Text;
                customerInfo.City = cmdCity.Text;
                customerInfo.PhoneNumber = txtPhone.Text;
                customerInfo.CountryCode = "+263";
                customerInfo.AddressLine1 = txtAdd1.Text;
                customerInfo.NationalIdentificationNumber = txtIDNumber.Text;
                // customerInfo.Gender = rdbFemale

                if (rdbMale.Checked)
                    customerInfo.Gender = "Male";
                else if (rdbFemale.Checked)
                    customerInfo.Gender = "Female";

                customerInfo.Zipcode = txtZipCode.Text;


                if (objListUserInput.Count > 0)
                {
                    UserInput objExistingInput = objListUserInput.Find(x => x.VRN == txtVrn.Text);
                    if (objExistingInput != null)
                    {
                        objExistingInput.VRN = txtVrn.Text;
                        objExistingInput.SumInsured = txtSumInsured.Text;

                        objExistingInput.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
                        objExistingInput.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
                        //objExistingInput.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                        objExistingInput.CoverType = cmbCoverType.SelectedValue == null ? 0 : Convert.ToInt32(cmbCoverType.SelectedValue);

                        //Vehicle Type
                        objExistingInput.Make = cmbMake.SelectedText;
                        objExistingInput.MakeID = Convert.ToString(cmbMake.SelectedValue);
                        objExistingInput.Model = cmbModel.SelectedText;
                        objExistingInput.ModelID = Convert.ToString(cmbModel.SelectedValue);
                        objExistingInput.Year = txtYear.Text;
                        objExistingInput.ChasisNumber = txtChasis.Text;
                        objExistingInput.EngineNumber = txtEngine.Text;


                        //PersonalDetails1
                        objExistingInput.Name = txtName.Text;
                        objExistingInput.EmailAddress = txtEmailAddress.Text;
                        objExistingInput.Phone = txtPhone.Text;
                        objExistingInput.Gender = "";
                        objExistingInput.DOB = txtDOB.Value.ToString("MM/dd/yyyy");
                        //objExistingInput.DOB = txtDOB.Text;



                        //PersonalDetails2
                        objExistingInput.Address1 = txtAdd1.Text;
                        objExistingInput.Address2 = txtAdd2.Text;
                        //objExistingInput.City = txtCity.Text;
                        objExistingInput.City = Convert.ToString(cmdCity.Text);
                        objExistingInput.Zip = txtZipCode.Text;
                        objExistingInput.IDNumber = txtIDNumber.Text;


                        //optionalCover
                        objExistingInput.ExcessBuyback = Convert.ToInt32(chkExcessBuyback.Checked);
                        objExistingInput.RoadsideAssistance = Convert.ToInt32(chkRoadsideAssistance.Checked);
                        objExistingInput.MedicalExpenses = Convert.ToInt32(chkMedicalExpenses.Checked);
                        objExistingInput.PassengerAccidentalCover = Convert.ToInt32(chkPassengerAccidentalCover.Checked);
                        objExistingInput.NumberOfPerson = Convert.ToInt32(cmbNoofPerson.Value);
                    }
                    else
                    {
                        SetUserInput();
                    }
                }
                else
                {
                    SetUserInput();
                }

            }

            // calculate summary
            CaclulateSummary(objlistRisk);

            //CheckToken();
        }
        public void SetUserInput()
        {
            UserInput objU = new UserInput();
            objU.VRN = txtVrn.Text;
            objU.SumInsured = txtSumInsured.Text;



            if (cmbVehicleUsage.SelectedValue != null)
            {
                objU.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
            }
            if (cmbPaymentTerm.SelectedValue != null)
            {
                objU.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
            }
            if (cmbCoverType.SelectedValue != null)
            {
                objU.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
            }

            //objU.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
            //objU.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
            //objU.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);



            //Vehicle Type
            objU.Make = cmbMake.SelectedText;
            objU.Model = cmbModel.SelectedText;
            //objU.MakeID = Convert.ToString(cmbMake.SelectedValue);

            if (cmbMake.SelectedValue != null)
            {
                objU.MakeID = Convert.ToString(cmbMake.SelectedValue);
            }
            if (cmbModel.SelectedValue != null)
            {
                objU.ModelID = Convert.ToString(cmbModel.SelectedValue);
            }


            //objU.ModelID = Convert.ToString(cmbModel.SelectedValue);
            objU.Year = txtYear.Text;
            objU.ChasisNumber = txtChasis.Text;
            objU.EngineNumber = txtEngine.Text;


            //PersonalDetails1
            objU.Name = txtName.Text;
            objU.EmailAddress = txtEmailAddress.Text;
            objU.Phone = txtPhone.Text;
            objU.Gender = "";
            //objU.DOB = txtDOB.Text;
            objU.DOB = txtDOB.Value.ToString("MM/dd/yyyy");



            //PersonalDetails2
            objU.Address1 = txtAdd1.Text;
            objU.Address2 = txtAdd2.Text;
            //objU.City = txtCity.Text;
            objU.City = Convert.ToString(cmdCity.Text);
            objU.Zip = txtZipCode.Text;
            objU.IDNumber = txtIDNumber.Text;


            //optionalCover
            objU.ExcessBuyback = Convert.ToInt32(chkExcessBuyback.Checked);
            objU.RoadsideAssistance = Convert.ToInt32(chkRoadsideAssistance.Checked);
            objU.MedicalExpenses = Convert.ToInt32(chkMedicalExpenses.Checked);
            objU.PassengerAccidentalCover = Convert.ToInt32(chkPassengerAccidentalCover.Checked);
            objU.NumberOfPerson = Convert.ToInt32(cmbNoofPerson.Value);
            objListUserInput.Add(objU);
            //NewVRN();
        }

        private void btnSumBack_Click(object sender, EventArgs e)
        {
            pnlPersonalDetails2.Visible = true;
            pnlsumary.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
        }

        private void frmQuote_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Pen p = new Pen(Color.Red);
            //Graphics g = e.Graphics;
            //int variance = 3;
            //g.DrawRectangle(p, new Rectangle(txtVrn.Location.X, txtVrn.Location.Y, txtVrn.Width , txtVrn.Height ));
        }

        private void txtVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void txtVrn_Leave_1(object sender, EventArgs e)
        {

            if (txtVrn.Text.Length == 0)
            {
                txtVrn.Text = "Car Registration Number";
                txtVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtVrn_Enter_1(object sender, EventArgs e)
        {
            if (txtVrn.Text == "Car Registration Number")
            {
                txtVrn.Text = "";
                txtVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void PnlVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnContionOptionalCover_Click(object sender, EventArgs e)
        {

            pnlRadioZinara.Visible = true;
            if (RadiobtnRadioLicence.Checked)
            {
                pnlRadio.Visible = true;
                pnlZinara.Visible = false;

            }
            else if (RadiobtnZinara.Checked)
            {
                pnlZinara.Visible = true;
                pnlRadio.Visible = false;
            }
            else
            {
                pnlRadio.Visible = false;
                pnlRadio.Visible = false;

            }
            pnlOptionalCover.Visible = false;
            //pnlSum.Visible = true;
            //pnlAddMoreVehicle.Visible = true;

            //pnlOptionalCover.Visible = false;
            //CalculatePremium();


            var productid = objRiskModel.ProductId;

            // add vehical list

            //if (isbackclicked == false)
            //{
            //    CalculatePremium();
            //}




            //if (VehicalIndex != -1)
            //{
            //    //Update vehical list
            //    SetValueForUpdate();
            //    VehicalIndex = -1;
            //}
            //else
            //{
            //    if (isbackclicked == false)
            //    {
            //        // add vehical list
            //        objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
            //        objlistRisk.Add(objRiskModel);
            //    }

            //    //if (isbackclicked ==true)
            //    //{
            //    //    if (objlistRisk.Count== 0)
            //    //    {
            //    //        objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
            //    //        objlistRisk.Add(objRiskModel);
            //    //    }
            //    //}


            //}
            //isbackclicked = false;
            //loadVRNPanel();

        }
        public void SetValueForUpdate()
        {
            // VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == objRiskModel.RegistrationNo);

            objlistRisk[VehicalIndex].RegistrationNo = txtVrn.Text;
            //objlistRisk[VehicalIndex].SumInsured = sum
        }

        private void btnOptionCoverBack_Click(object sender, EventArgs e)
        {
            btnAddMoreVehicle.Visible = true;
            pnlRiskDetails.Visible = true;
            //pnlConfirm.Visible = true;
            pnlOptionalCover.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
        }

        private void pnl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Graphics v = e.Graphics;
            //DrawRoundRect(v, Pens.Black, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 10);
            ////Without rounded corners
            ////e.Graphics.DrawRectangle(Pens.Blue, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //base.OnPaint(e);
        }
        public void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }

        private void btnAddvehicle_Click(object sender, EventArgs e)
        {

            //   if (MessageBox.Show("Do you want to add more vehicle", "GENE", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                PnlVrn.Visible = true;
                pnlSum.Visible = false;
                NewVRN();
                pnlAddMoreVehicle.Visible = false;
                //btnAddMoreVehicle.Visible = false;
            }
        }

        private void cmbMake_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindModel(Convert.ToString(cmbMake.SelectedValue));
        }

        public void NewVRN()
        {
            txtVrn.Text = "Car Registration Number";
            txtVrn.ForeColor = SystemColors.GrayText;
            txtSumInsured.Text = string.Empty;
            cmbVehicleUsage.SelectedIndex = 0;
            cmbPaymentTerm.SelectedIndex = 0;
            cmbCoverType.SelectedIndex = 0;

            cmbMake.SelectedIndex = 0;
            txtYear.Text = string.Empty;
            txtChasis.Text = string.Empty;
            txtEngine.Text = string.Empty;

            //optionalCover
            chkExcessBuyback.Checked = false;
            chkRoadsideAssistance.Checked = false;
            chkMedicalExpenses.Checked = false;
            chkPassengerAccidentalCover.Checked = false;
            cmbNoofPerson.Value = 0;
            //Optional
            txtradioAmount.Text = string.Empty;
            RadiobtnRadioLicence.Checked = false;
            RadiobtnZinara.Checked = false;
            txtAccessAmount.Text = string.Empty;
            txtpenalty.Text = string.Empty;
            txtZinTotalAmount.Text = string.Empty;


            btnAddMoreVehicle.Visible = true;
        }

        private void btnSDContinue_Click(object sender, EventArgs e)
        {
            // 5 th screen vehical summary detals 

            if (objlistRisk.Count > 0)
            {
                pnlPersonalDetails.Visible = true;
                pnlSum.Visible = false;
                pnlAddMoreVehicle.Visible = false;
                txtDOB.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtDOB.MaxDate = DateTime.Today;
                txtDOB.CalendarForeColor = Color.LightGray;
            }
        }

        private void BtnSDback_Click(object sender, EventArgs e)
        {
            //pnlOptionalCover.Visible = true;

            pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            pnlRadioZinara.Visible = true;
            if (RadiobtnRadioLicence.Checked)
            {
                pnlRadio.Visible = true;
            }
            if (RadiobtnZinara.Checked)
            {
                pnlZinara.Visible = true;
            }

            if (isVehicalDeleted)
            {
                PnlVrn.Visible = true;
                pnlSum.Visible = false;
                pnlOptionalCover.Visible = false;
                NewVRN();

                isVehicalDeleted = false;
            }
            else
            {
                VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
            }


            isbackclicked = true;
        }

        private void cmbCoverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCoverType.SelectedValue != null)
            {
                int CoverType = CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                if (CoverType == 4)
                {
                    label2.Visible = true;
                    txtSumInsured.Visible = true;
                }
                else
                {
                    label2.Visible = false;
                    txtSumInsured.Visible = false;
                }


            }

        }


        private void GetPremiumAmount_ChangeOfCoverType()
        {
            int CoverType = 0;
     
            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            //picbxCoverType.Visible = true;
            try
            {
                //if (objRiskModel == null)
                //    return;

                //if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                //    return;

                if (cmbPaymentTerm.SelectedValue == null && cmbCoverType.SelectedValue == null)
                    return;



                #region get ICE cash token
                //ICEcashTokenResponse ObjToken = IcServiceobj.getToken(); // uncomment this line 

                // ICEcashTokenResponse ObjToken = null;
                #endregion
                List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();

                objVehicles.Add(new RiskDetailModel { RegistrationNo = txtVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });
                if (parternToken != "")
                {

                    if (String.IsNullOrEmpty(txtYear.Text))
                    {
                        txtYear.Text = "1900";
                    }
                    if (String.IsNullOrEmpty(txtSumInsured.Text))
                    {
                        txtSumInsured.Text = "0";
                    }
                   
                    int PaymentTermId = 0;
                    int CoverTypeId = 0;
                    int VehicleUsage = 0;
                    string RegistrationNo = txtVrn.Text;
                    string suminsured = txtSumInsured.Text;
                    string make = Convert.ToString(cmbMake.Text);
                    string model = Convert.ToString(cmbModel.Text);


                    //int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);

                    if (cmbPaymentTerm.SelectedValue != null)
                    {
                        PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    }
                    if (cmbCoverType.SelectedValue != null)
                    {
                        CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    }
                    if (cmbVehicleUsage.SelectedValue != null)
                    {
                        VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    }

                    //int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    //int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);


                    int VehicleYear = Convert.ToInt32(txtYear.Text);
                    string PartnerReference = ObjToken.PartnerReference;

                    ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 


                    resObject = quoteresponse.Response;
                    //if token expire
                    if (resObject != null && resObject.Message == "Partner Token has expired. ")
                    {
                        ObjToken = IcServiceobj.getToken();
                        if (ObjToken != null)
                        {
                            parternToken = ObjToken.Response.PartnerToken;
                            quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 

                        }

                    }





                    // picbxCoverType.Visible = false;
                    //picbxRiskDetail.Visible = false;
                    //ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, txtVrn.Text, txtSumInsured.Text, Convert.ToString(cmbMake.SelectedValue), Convert.ToString(cmbModel.SelectedValue), Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(txtYear), Convert.ToInt32(cmbCoverType.SelectedValue), Convert.ToInt32(cmbVehicleUsage.SelectedValue), "", customerInfo);
                    if (quoteresponse != null)
                    {
                        response.result = quoteresponse.Response.Result;
                        if (response.result == 0)
                        {
                            response.message = quoteresponse.Response.Quotes[0].Message;
                        }
                        else
                        {

                            response.Data = quoteresponse;
                            if (response.result != 0)
                            {
                                if (quoteresponse.Response.Quotes[0] != null)
                                {
                                    ////9Jan
                                    if (quoteresponse.Response.Quotes[0].Policy != null)
                                    {
                                        cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.InsuranceType);
                                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths); // ask from sir

                                        if (quoteresponse.Response.Quotes[0].Policy.DurationMonths != null)
                                        {
                                            if (quoteresponse.Response.Quotes[0].Policy.DurationMonths == "12")
                                            {
                                                cmbPaymentTerm.SelectedValue = 1;
                                            }
                                            else
                                            {
                                                //cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                                cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
                                            }
                                        }


                                        if (VehicalIndex == -1)
                                        {
                                            objRiskModel.isVehicleRegisteredonICEcash = true;
                                            objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                            var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                            objRiskModel.Discount = discount;
                                        }
                                        else
                                        {
                                        
                                           objlistRisk[VehicalIndex].isVehicleRegisteredonICEcash = true;
                                            objlistRisk[VehicalIndex].BasicPremiumICEcash = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                            var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                            objlistRisk[VehicalIndex].Discount = discount;

                                        }


                                    }

                                    if (quoteresponse.Response.Quotes[0].Vehicle != null)
                                    {
                                        cmbVehicleUsage.SelectedValue = quoteresponse.Response.Quotes[0].Vehicle.VehicleType;
                                        txtYear.Text = quoteresponse.Response.Quotes[0].Vehicle.YearManufacture;
                                        Int32 index = cmbMake.FindStringExact(quoteresponse.Response.Quotes[0].Vehicle.Make);
                                        cmbMake.SelectedIndex = index;
                                        bindModel(cmbMake.SelectedValue.ToString());
                                        Int32 indexModel = cmbModel.FindString(quoteresponse.Response.Quotes[0].Vehicle.Model);
                                        cmbModel.SelectedIndex = indexModel;
                                        //if (cmbVehicleUsage.SelectedValue != null)
                                        //{
                                        //    bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));

                                        //}
                                    }
                                    int cmVehicleValue = 0;
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                                        if (cmVehicleValue != 0)
                                        {
                                            this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                                        }
                                    }

                                    if (quoteresponse.Response.Quotes[0].Client != null)
                                    {
                                        txtName.Text = quoteresponse.Response.Quotes[0].Client.FirstName + " " + quoteresponse.Response.Quotes[0].Client.LastName;
                                        txtPhone.Text = "";
                                        txtAdd1.Text = quoteresponse.Response.Quotes[0].Client.Address1;
                                        txtAdd2.Text = quoteresponse.Response.Quotes[0].Client.Address2;
                                        //txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
                                        cmdCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
                                        txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;
                                    }
                                    /////End

                                    // Session["InsuranceId"] = quoteresponse.Response.Quotes[0].InsuranceID;

                                }


                                // for zinara license 
                                var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber);
                                var _resObjects = _quoteresponse.Response;
                                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                                {
                                    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                                    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                                    this.Invoke(new Action(() => txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                                    this.Invoke(new Action(() => txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                                    this.Invoke(new Action(() => txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                                }




                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = "Error occured.";
            }
        }


        public string getmessageresponse(int data)
        {
            string Message = "";

            switch (data)
            {

                case 00:
                    Message = "Approved or completed successfully";
                    break;

                case 01:
                    Message = "Refer to card issuer";
                    break;


                case 02:
                    Message = " Refer to card issuer, special condition";
                    break;


                case 03:
                    Message = "Invalid merchant";
                    break;


                case 04:
                    Message = "Pick - up card";
                    break;


                case 05:
                    Message = "Do not honor";
                    break;


                case 06:
                    Message = "Error";
                    break;


                case 07:
                    Message = "Pick - up card, special condition";
                    break;

                case 08:
                    Message = "Honor with identification";
                    break;


                case 09:
                    Message = "Request in progress";
                    break;


                case 10:
                    Message = "Approved, partial";
                    break;

                case 11:
                    Message = "Approved, VIP";
                    break;

                case 12:
                    Message = "Invalid transaction";
                    break;


                case 13:
                    Message = "Invalid amount";
                    break;


                case 14:
                    Message = "Invalid card number";
                    break;


                case 15:
                    Message = "No such issuer";
                    break;


                case 16:
                    Message = "Approved, update track 3";
                    break;


                case 17:
                    Message = "Customer cancellation";
                    break;


                case 18:
                    Message = "Customer dispute";
                    break;

                case 19:
                    Message = "Re - enter transaction";
                    break;


                case 20:
                    Message = "Invalid response";
                    break;


                case 21:
                    Message = "No action taken";
                    break;



                case 22:
                    Message = "Suspected malfunction";
                    break;


                case 23:
                    Message = "Unacceptable transaction fee";
                    break;


                case 24:
                    Message = "File update not supported";
                    break;


                case 25:
                    Message = "Unable to locate record";
                    break;


                case 26:
                    Message = "Duplicate record";
                    break;


                case 27:
                    Message = "File update edit error";
                    break;


                case 28:
                    Message = "File update file locked";
                    break;


                case 29:
                    Message = "File update failed";
                    break;

                case 30:
                    Message = "Format error";
                    break;


                case 31:
                    Message = "Bank not supported";
                    break;


                case 32:
                    Message = "Completed partially";
                    break;


                case 33:
                    Message = "Expired card, pick-up";
                    break;


                case 34:
                    Message = "Suspected fraud, pick-up";
                    break;


                case 35:
                    Message = "Contact acquirer, pick-up";
                    break;


                case 36:
                    Message = "Restricted card, pick-up";
                    break;

                case 37:
                    Message = "Call acquirer security, pick - up";
                    break;


                case 38:
                    Message = "PIN tries exceeded, pick - up";
                    break;



                case 39:
                    Message = "No credit account";
                    break;


                case 40:
                    Message = "Function not supported";
                    break;


                case 41:
                    Message = "Lost card";
                    break;


                case 42:
                    Message = "No universal account";
                    break;


                case 43:
                    Message = "Stolen card";
                    break;


                case 44:
                    Message = "No investment account";
                    break;

                case 51:
                    Message = "Not sufficient funds";
                    break;
                case 52:
                    Message = "No check account";
                    break;


                case 53:
                    Message = "No savings account";
                    break;


                case 54:
                    Message = "Card expired or not yet effective";
                    break;


                case 55:
                    Message = "Incorrect PIN";
                    break;


                case 56:
                    Message = "No card record";
                    break;


                case 57:
                    Message = "Transaction not permitted to cardholder";
                    break;


                case 58:
                    Message = "Transaction not permitted on terminal";
                    break;


                case 59:
                    Message = "Suspected fraud";
                    break;


                case 60:
                    Message = "Contact acquirer";
                    break;


                case 61:
                    Message = "Exceeds withdrawal limit";
                    break;


                case 62:
                    Message = "Restricted card";
                    break;


                case 63:
                    Message = "Security violation";
                    break;


                case 64:
                    Message = "Original amount incorrect";
                    break;


                case 65:
                    Message = "Exceeds withdrawal frequency";
                    break;


                case 66:
                    Message = "Call acquirer security";
                    break;


                case 67:
                    Message = "Hard capture";
                    break;


                case 68:
                    Message = "Response received too late";
                    break;


                case 75:
                    Message = "PIN tries exceeded";
                    break;


                case 77:
                    Message = "Intervene, bank approval required";
                    break;


                case 78:
                    Message = "Intervene, bank approval required for partial amount";
                    break;


                case 90:
                    Message = "Cut - off in progress";
                    break;


                case 91:
                    Message = "Issuer or switch inoperative";
                    break;


                case 92:
                    Message = "Routing error";
                    break;


                case 93:
                    Message = "Violation of law";
                    break;


                case 94:
                    Message = "Duplicate transaction";
                    break;


                case 95:
                    Message = "Reconcile error";
                    break;


                case 96:
                    Message = "System malfunction";
                    break;


                case 98:
                    Message = "Exceeds cash limit";
                    break;



            }
            return Message;
        }



        private void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (objRiskModel == null)
            ////    return;

            ////if (objRiskModel != null && objRiskModel.RegistrationNo == null)
            ////    return;

            ////if (objRiskModel != null && objRiskModel.RegistrationNo == null)
            ////    return;

            //if (cmbPaymentTerm.SelectedValue == null)
            //    return;


            //var paymenttermval = cmbPaymentTerm.SelectedValue;
            //var getvrntextval = txtVrn.Text;

            //checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();

            //try
            //{
            //    #region get ICE cash token

            //    //  ICEcashTokenResponse ObjToken = IcServiceobj.getToken();
            //    #endregion

            //    List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();
            //    objVehicles.Add(new RiskDetailModel { RegistrationNo = txtVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });

            //    if (parternToken != "")
            //    {
            //        if (String.IsNullOrEmpty(txtYear.Text))
            //        {
            //            txtYear.Text = "1900";
            //        }
            //        if (String.IsNullOrEmpty(txtSumInsured.Text))
            //        {
            //            txtSumInsured.Text = "0";
            //        }
            //        int PaymentTermId = 0;
            //        int CoverTypeId = 0;
            //        int VehicleUsage = 0;
            //        int VehicleYear = 0;
            //        string RegistrationNo = txtVrn.Text;
            //        string suminsured = txtSumInsured.Text;
            //        string make = Convert.ToString(cmbMake.Text);
            //        string model = Convert.ToString(cmbModel.Text);

            //        if (cmbPaymentTerm.SelectedValue != null)
            //        {
            //            PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            //        }
            //        if (cmbCoverType.SelectedValue != null)
            //        {
            //            CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
            //        }
            //        if (cmbVehicleUsage.SelectedValue != null)
            //        {
            //            VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //        }
            //        if (txtYear.Text != string.Empty)
            //        {
            //            VehicleYear = Convert.ToInt32(txtYear.Text);
            //        }


            //        //int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            //        //int VehicleYear = Convert.ToInt32(txtYear.Text);
            //        //int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
            //        //int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //        //string PartnerReference = ObjToken.PartnerReference;
            //        //   string PartnerReference = ObjToken.PartnerReference;

            //        ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo);


            //        resObject = quoteresponse.Response;
            //        //if token expire
            //        if (resObject != null && resObject.Message == "Partner Token has expired. ")
            //        {
            //            ObjToken = IcServiceobj.getToken();
            //            if (ObjToken != null)
            //            {
            //                parternToken = ObjToken.Response.PartnerToken;
            //                quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo);

            //                resObject = quoteresponse.Response;

            //            }

            //        }







            //        if (quoteresponse != null)
            //        {
            //            response.result = quoteresponse.Response.Result;
            //            if (response.result == 0)
            //            {
            //                response.message = quoteresponse.Response.Quotes[0].Message;
            //            }
            //            else
            //            {
            //                response.Data = quoteresponse;

            //                if (quoteresponse.Response.Quotes != null && quoteresponse.Response.Quotes[0] != null)
            //                {

            //                    //15 Feb  commented on 20_feb

            //                    //var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, quoteresponse.Response.Quotes[0].Client.IDNumber);
            //                    //var _resObjects = _quoteresponse.Response;
            //                    //if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
            //                    //{
            //                    //    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
            //                    //    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
            //                    //    txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt);
            //                    //    txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt);
            //                    //    txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);
            //                    //}

            //                    //End

            //                    ////9Jan
            //                    if (quoteresponse.Response.Quotes[0].Policy != null)
            //                    {
            //                        cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.InsuranceType);
            //                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
            //                        if (quoteresponse.Response.Quotes[0].Policy.DurationMonths == "12")
            //                        {
            //                            cmbPaymentTerm.SelectedValue = 1;
            //                        }
            //                        else
            //                        {
            //                            cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
            //                        }


            //                        objRiskModel.isVehicleRegisteredonICEcash = true;
            //                        objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);

            //                        objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
            //                        objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
            //                        objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);
            //                        var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
            //                        objRiskModel.Discount = discount;

            //                    }
            //                    if (quoteresponse.Response.Quotes[0].Vehicle != null)
            //                    {
            //                        cmbVehicleUsage.SelectedValue = quoteresponse.Response.Quotes[0].Vehicle.VehicleType;
            //                        txtYear.Text = quoteresponse.Response.Quotes[0].Vehicle.YearManufacture;
            //                        Int32 index = cmbMake.FindStringExact(quoteresponse.Response.Quotes[0].Vehicle.Make);
            //                        cmbMake.SelectedIndex = index;
            //                        bindModel(cmbMake.SelectedValue.ToString());
            //                        Int32 indexModel = cmbModel.FindString(quoteresponse.Response.Quotes[0].Vehicle.Model);
            //                        cmbModel.SelectedIndex = indexModel;
            //                    }

            //                    if (cmbVehicleUsage.SelectedValue != null)
            //                    {
            //                        bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
            //                    }
            //                    if (quoteresponse.Response.Quotes[0].Client != null)
            //                    {
            //                        txtName.Text = quoteresponse.Response.Quotes[0].Client.FirstName + " " + quoteresponse.Response.Quotes[0].Client.LastName;
            //                        txtPhone.Text = "";
            //                        txtAdd1.Text = quoteresponse.Response.Quotes[0].Client.Address1;
            //                        txtAdd2.Text = quoteresponse.Response.Quotes[0].Client.Address2;
            //                        //txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
            //                        cmdCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
            //                        txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;
            //                    }

            //                    /////End           
            //                }
            //            }

            //        }

            //    }

            //    //  ICEcashService.LICQuote(regNo, tokenObject.Response.PartnerToken);
            //    //  json.Data = response;
            //}
            //catch (Exception ex)
            //{
            //    response.message = "Error occured.";
            //}
        }

        //public void bindProductid(int VehicleUsageId)
        public string bindProductid(int VehicleUsageId)
        {
            string ProductId = "";
            var client = new RestClient(ApiURL + "GetProductId?VehicleUsageId=" + VehicleUsageId);
            var request = new RestRequest(Method.GET);

            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<ProductIdModel>(response.Content);
            objRiskModel.ProductId = result.ProductId;

            ProductId = Convert.ToString(result.ProductId);
            return ProductId;
        }

        public void CalculatePremium()
        {
            VehicleDetails obj = new VehicleDetails();


            if (VehicalIndex != -1)
             {
                objRiskModel = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtVrn.Text);
            }

            


            if (cmbVehicleUsage.SelectedValue != null)
            {
                obj.vehicleUsageId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            }
            if (cmbCoverType.SelectedValue != null)
            {
                obj.coverType = Convert.ToInt32(cmbCoverType.SelectedValue);
            }
            if (cmbPaymentTerm.SelectedValue != null)
            {
                obj.PaymentTermid = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            }

            //obj.vehicleUsageId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //obj.sumInsured =Convert.ToDecimal(txtSumInsured.Text);
            //obj.coverType = Convert.ToInt32(cmbCoverType.SelectedValue);
            //obj.PaymentTermid = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            obj.sumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture);
            obj.NumberofPersons = Convert.ToInt32(cmbNoofPerson.Value);
            obj.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;
            obj.ExcessBuyBack = chkExcessBuyback.Checked;
            obj.RoadsideAssistance = chkRoadsideAssistance.Checked;
            obj.MedicalExpenses = chkMedicalExpenses.Checked;
            obj.IncludeRadioLicenseCost = objRiskModel.IncludeRadioLicenseCost;
            obj.AddThirdPartyAmount = 0.00m;



            obj.ProductId = objRiskModel.ProductId;
            obj.RadioLicenseCost = objRiskModel.RadioLicenseCost;
            obj.isVehicleRegisteredonICEcash = objRiskModel.isVehicleRegisteredonICEcash;
            obj.BasicPremiumICEcash = objRiskModel.Premium == null ? "0" : Convert.ToString(objRiskModel.Premium);
            obj.StampDutyICEcash = objRiskModel.StampDuty == null ? "0" : Convert.ToString(objRiskModel.StampDuty);
            obj.ZTSCLevyICEcash = objRiskModel.ZTSCLevy == null ? "0" : Convert.ToString(objRiskModel.ZTSCLevy);
            obj.Addthirdparty = false;
            //obj.AddThirdPartyAmount = 00;

            //var client = new RestClient(ApiURL + "CalculateTotalPremium");


           // IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";

            //var client = new RestClient(IceCashRequestUrl + "CalculateTotalPremium");
           // var localCashRequestUrl = "http://localhost:6220/api/ICEcash/";

            var client = new RestClient(IceCashRequestUrl + "CalculateTotalPremium");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<QuoteLogic>(response.Content);
            if (VehicalIndex != -1)
            {
                if (result != null)
                {
                    objlistRisk[VehicalIndex].Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium, System.Globalization.CultureInfo.InvariantCulture);


                   


                    objlistRisk[VehicalIndex].Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount, System.Globalization.CultureInfo.InvariantCulture);
                    objlistRisk[VehicalIndex].ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy, System.Globalization.CultureInfo.InvariantCulture);
                    objlistRisk[VehicalIndex].StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty, System.Globalization.CultureInfo.InvariantCulture);
                    //9Jan
                    objlistRisk[VehicalIndex].AnnualRiskPremium = result.AnnualRiskPremium == 0 ? 0 : result.AnnualRiskPremium;
                    objlistRisk[VehicalIndex].TermlyRiskPremium = result.TermlyRiskPremium == 0 ? 0 : result.TermlyRiskPremium;
                    objlistRisk[VehicalIndex].QuaterlyRiskPremium = result.QuaterlyRiskPremium == 0 ? 0 : result.QuaterlyRiskPremium;

                    //10Jan

                    objlistRisk[VehicalIndex].PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                    objlistRisk[VehicalIndex].ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                    objlistRisk[VehicalIndex].RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                    objlistRisk[VehicalIndex].MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                    objlistRisk[VehicalIndex].PassengerAccidentCoverAmount = result.PassengerAccidentCoverAmount == 0 ? 0 : result.PassengerAccidentCoverAmount;
                    objlistRisk[VehicalIndex].ExcessBuyBackAmount = result.ExcessBuyBackAmount == 0 ? 0 : result.ExcessBuyBackAmount;
                    objlistRisk[VehicalIndex].RoadsideAssistanceAmount = result.RoadsideAssistanceAmount == 0 ? 0 : result.RoadsideAssistanceAmount;
                    objlistRisk[VehicalIndex].MedicalExpensesAmount = result.MedicalExpensesAmount == 0 ? 0 : result.MedicalExpensesAmount;

                    objlistRisk[VehicalIndex].ExcessAmount = result.ExcessAmount == 0 ? 0 : result.ExcessAmount;
                }
            }
            else if (result != null)
            {
                objRiskModel.Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty, System.Globalization.CultureInfo.InvariantCulture);
                //9Jan
                objRiskModel.AnnualRiskPremium = result.AnnualRiskPremium == 0 ? 0 : result.AnnualRiskPremium;
                objRiskModel.TermlyRiskPremium = result.TermlyRiskPremium == 0 ? 0 : result.TermlyRiskPremium;
                objRiskModel.QuaterlyRiskPremium = result.QuaterlyRiskPremium == 0 ? 0 : result.QuaterlyRiskPremium;

                //10Jan
                //objRiskModel.PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                //objRiskModel.ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                //objRiskModel.RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                //objRiskModel.MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                objRiskModel.PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                objRiskModel.ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                objRiskModel.RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                objRiskModel.MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                objRiskModel.PassengerAccidentCoverAmount = result.PassengerAccidentCoverAmount == 0 ? 0 : result.PassengerAccidentCoverAmount;
                objRiskModel.ExcessBuyBackAmount = result.ExcessBuyBackAmount == 0 ? 0 : result.ExcessBuyBackAmount;
                objRiskModel.RoadsideAssistanceAmount = result.RoadsideAssistanceAmount == 0 ? 0 : result.RoadsideAssistanceAmount;
                objRiskModel.MedicalExpensesAmount = result.MedicalExpensesAmount == 0 ? 0 : result.MedicalExpensesAmount;

                objRiskModel.ExcessAmount = result.ExcessAmount == 0 ? 0 : result.ExcessAmount;

            }
        }

        public void Checkobject()
        {
            // String IceCashRequestUrl = "http://localhost:6219/api/ICEcash/";

            CoverObject test = new CoverObject();

            test.Id = 1;
            test.name = "test";

            var client = new RestClient(IceCashRequestUrl + "test1");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            //request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(test);
            IRestResponse response = client.Execute(request);


        }

        //public decimal CalculateDiscount(string coverAmount, int PaymentTermId)
        //{
        //    decimal LoyaltyDiscount = 0;

        //    var Setting = InsuranceContext.Settings.All();
        //    var DiscountOnRenewalSettings = Setting.Where(x => x.keyname == "Discount On Renewal").FirstOrDefault();
        //    var premium = premiumAmount;
        //    switch (PaymentTermId)
        //    {
        //        case 1:
        //            var AnnualRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((AnnualRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //        case 3:
        //            var QuaterlyRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((QuaterlyRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //        case 4:
        //            var TermlyRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((TermlyRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //    }
        //}

        public enum eSettingValueType
        {
            percentage = 1,
            amount = 2
        }

        private void cmbVehicleUsage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVehicleUsage.SelectedIndex > 0)
            {
                bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
            }
        }

        private void chkPassengerAccidentalCover_CheckedChanged(object sender, EventArgs e)
        {

            objRiskModel.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;

            // CalculatePremium();
        }

        private void chkMedicalExpenses_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.MedicalExpenses = chkMedicalExpenses.Checked;
            // CalculatePremium();
        }

        private void chkRoadsideAssistance_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.RoadsideAssistance = chkRoadsideAssistance.Checked;
            //  CalculatePremium();
        }

        private void chkExcessBuyback_CheckedChanged(object sender, EventArgs e)
        {
            //   CalculatePremium();
        }

        public SummaryDetailModel SaveCustomerVehical()
        {
            CustomerVehicalModel objPlanModel = new CustomerVehicalModel();
            SummaryDetailModel summaryDetialsModel = new SummaryDetailModel();

            PolicyDetail policyDetial = new PolicyDetail();
            objlistVehicalModel = new List<VehicalModel>();

            objPlanModel.CustomerModel = customerInfo;
            objPlanModel.RiskDetailModel = objlistRisk;
            objPlanModel.PolicyDetail = policyDetial;
            objPlanModel.SummaryDetailModel = summaryModel;

            //var LocalIceCashReqUrl = "http://localhost:6220/api/ICEcash/";
            //   IceCashRequestUrl

            if (objPlanModel != null)
            {
                var client = new RestClient(IceCashRequestUrl + "SaveVehicalDetails");
                //var client = new RestClient(LocalIceCashReqUrl + "SaveVehicalDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(objPlanModel);

                //request.Timeout = 5000;
                //request.ReadWriteTimeout = 5000;



                IRestResponse response = client.Execute(request);

                summaryDetialsModel = JsonConvert.DeserializeObject<SummaryDetailModel>(response.Content);
                if (summaryDetialsModel != null)
                {
                    // MessageBox.Show("Policy has been sucessfully registred.");
                    pnlThankyou.Visible = true;
                    pnlsumary.Visible = false;

                    //foreach (var item in result.ToList())
                    //{
                    //    objlistVehicalModel.Add(new VehicalModel
                    //    {
                    //        VehicalId = item.VehicalId,
                    //        VRN = item.VRN
                    //    });
                    //}
                }
            }

            return summaryDetialsModel;
        }




        public void CaclulateSummary(List<RiskDetailModel> objRiskDetail)
        {
            //List<RiskDetailModel> objRiskDetail = new List<RiskDetailModel>();

            //List<RiskDetailModel> objRiskDetail = new List<RiskDetailModel>
            //    {
            //        new RiskDetailModel { Premium =Convert.ToDecimal(45.67), ZTSCLevy =Convert.ToDecimal(3.24), StampDuty = Convert.ToDecimal(1.35),VehicleLicenceFee= Convert.ToDecimal(23.56),RadioLicenseCost=Convert.ToDecimal(23.56),IncludeRadioLicenseCost=true,Discount=Convert.ToDecimal(3),SumInsured=Convert.ToDecimal(2.6) },
            //        new RiskDetailModel { Premium =Convert.ToDecimal(5.67), ZTSCLevy =Convert.ToDecimal(7.4), StampDuty = Convert.ToDecimal(6.35),VehicleLicenceFee= Convert.ToDecimal(34.56),RadioLicenseCost=Convert.ToDecimal(3.56),IncludeRadioLicenseCost=false,Discount=Convert.ToDecimal(3),SumInsured=Convert.ToDecimal(33.56)}
            //    };


            summaryModel = new SummaryDetailModel();
            summaryModel.TotalPremium = 0.00m;
            summaryModel.TotalRadioLicenseCost = 0.00m;
            summaryModel.Discount = 0.00m;
            summaryModel.AmountPaid = 0.00m;
            summaryModel.TotalSumInsured = 0.00m;
            try
            {
                if (objRiskDetail != null && objRiskDetail.Count > 0)
                {
                    foreach (var item in objRiskDetail)
                    {
                        //summaryModel.TotalPremium += item.Premium + item.ZTSCLevy + item.StampDuty + item.VehicleLicenceFee; 

                        summaryModel.TotalPremium += item.Premium + item.ZTSCLevy + item.StampDuty;

                        if (item.IncludeRadioLicenseCost)
                        {
                            summaryModel.TotalPremium += item.RadioLicenseCost;
                            summaryModel.TotalRadioLicenseCost += item.RadioLicenseCost;
                        }
                        else
                        {
                            summaryModel.TotalPremium += item.VehicleLicenceFee;
                            summaryModel.VehicleLicencefees += item.VehicleLicenceFee;
                        }

                        summaryModel.Discount += item.Discount;

                    }

                    //summaryModel.TotalRadioLicenseCost = Math.Round(Convert.ToDecimal(summaryModel.TotalRadioLicenseCost, System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.Discount = Math.Round(Convert.ToDecimal(summaryModel.Discount, System.Globalization.CultureInfo.InvariantCulture), 2);
                    var calcualatedPremium = Math.Round(Convert.ToDecimal(summaryModel.TotalPremium, System.Globalization.CultureInfo.InvariantCulture), 2);
                    //summaryModel.TotalPremium = Math.Round(Convert.ToDecimal(calcualatedPremium - summaryModel.Discount), 2);

                    //model.MaxAmounttoPaid = Math.Round(Convert.ToDecimal(model.TotalPremium), 2);
                    //summaryModel.AmountPaid = Convert.ToDecimal(summaryModel.TotalPremium, System.Globalization.CultureInfo.InvariantCulture);
                    summaryModel.AmountPaid = Convert.ToDecimal(summaryModel.TotalPremium);

                    summaryModel.TotalStampDuty = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.StampDuty), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.TotalSumInsured = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.SumInsured), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.TotalZTSCLevies = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ZTSCLevy), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.ExcessBuyBackAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessBuyBackAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.MedicalExpensesAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.MedicalExpensesAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.PassengerAccidentCoverAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.PassengerAccidentCoverAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.RoadsideAssistanceAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.RoadsideAssistanceAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.ExcessAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessAmount), System.Globalization.CultureInfo.InvariantCulture), 2);


                    txtDiscount.Text = Convert.ToString(summaryModel.Discount);
                    //txtTotalPremium.Text = Convert.ToString(summaryModel.TotalPremium);
                    txtAmountDue.Text = Convert.ToString(summaryModel.AmountPaid);
                    txtTotalRadioCost.Text = Convert.ToString(summaryModel.TotalRadioLicenseCost);
                    txtStampDuty.Text = Convert.ToString(summaryModel.TotalStampDuty);
                    //txtTotalSumInsured.Text = Convert.ToString(summaryModel.TotalSumInsured);
                    //txtExcessAmount.Text = Convert.ToString(summaryModel.ExcessAmount);
                    txtMedicalExcessAmount.Text = Convert.ToString(summaryModel.MedicalExpensesAmount);
                    txtPassengerAccidentAmt.Text = Convert.ToString(summaryModel.PassengerAccidentCoverAmount);
                    //txtRoadsideAssitAmt.Text = Convert.ToString(summaryModel.RoadsideAssistanceAmount);
                    txtZTSCLevies.Text = Convert.ToString(summaryModel.TotalZTSCLevies);
                    txtExcessBuyBackAmt.Text = Convert.ToString(summaryModel.ExcessBuyBackAmount);
                    //txtRadioLicAmount.Text = Convert.ToString(summaryModel.TotalRadioLicenseCost);
                    txtZinaraAmount.Text = Convert.ToString(summaryModel.VehicleLicencefees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnSumContinue_Click(object sender, EventArgs e)
        {
            if (btnSumContinue.Text == "Pay")
            {
                btnSumContinue.Text = "Processing.....";
            }

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            // Save all details
            CustomerModel customerModel = new CustomerModel();
            customerModel.FirstName = txtName.Text;
            customerModel.EmailAddress = txtEmailAddress.Text;

            //pnlThankyou.Visible = true;
            //SaveCustomerVehical();


            //var summaryDetails = SaveCustomerVehical();

            //Save Payment info
            PaymentResult objResult = new PaymentResult();
            long TransactionId = 0;
            TransactionId = GenerateTransactionId();
            decimal transctionAmt = Convert.ToDecimal(txtAmountDue.Text);

            string paymentTermName = "Swipe";

            if (radioMobile.Checked)
                paymentTermName = "Mobile";

            //string TransactionId1 = Convert.ToString(TransactionId); // remove after testing
            //if(summaryDetails!=null)
            //{
            //    SavePaymentinformation(TransactionId1, summaryDetails.Id);  // remove after testing
            //}


            //if(summaryDetails != null)
            //{
            //SendSymbol(TransactionId, transctionAmt, summaryDetails.Id);
            SendSymbol(TransactionId, transctionAmt, paymentTermName);

            //}



            //End


            //List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
            //try
            //{
            //    if (objlistRisk != null && objlistRisk.Count > 0)
            //    {
            //        foreach (var item in objlistRisk)
            //        {
            //            obj.Add(new VehicleLicQuote
            //            {
            //                //VRN = txtVrn.Text,
            //                VRN = item.RegistrationNo,
            //                //VRN= ,
            //                ClientIDType = "1",
            //                IDNumber = "ABCDEFGHIJ1",
            //                DurationMonths = "4",
            //                LicFrequency = "3",
            //                RadioTVUsage = "1",
            //                RadioTVFrequency = "1"
            //            });
            //        }
            //    }

            //    if (ObjToken != null)
            //    {
            //        if (ObjToken.Response.PartnerToken != null)
            //        {
            //            ResultRootObject quoteresponse = IcServiceobj.TPILICQuote(obj, ObjToken.Response.PartnerToken);
            //        }
            //    }



            //    if (quoteresponse != null)
            //    {
            //        response.result = quoteresponse.Response.Result;
            //        if (response.result == 0)
            //        {
            //            response.message = quoteresponse.Response.Quotes[0].Message;
            //        }
            //        else
            //        {
            //            response.Data = quoteresponse;

            //            if (quoteresponse.Response.Quotes != null)
            //            {
            //                if (quoteresponse.Response.Quotes.Count != 0)
            //                {
            //                    objVehicleLicense = new List<VehicleLicenseModel>();
            //                    foreach (var item in quoteresponse.Response.Quotes.ToList())
            //                    {
            //                        string format = "yyyyMMdd";
            //                        if (item.Licence != null)
            //                        {
            //                            var LicExpiryDate = DateTime.ParseExact(item.Licence.LicExpiryDate, format, CultureInfo.InvariantCulture);
            //                            objVehicleLicense.Add(new VehicleLicenseModel
            //                            {
            //                                InsuranceID = item.InsuranceID,
            //                                VRN = item.VRN,
            //                                CombinedID = item.CombinedID,
            //                                LicenceID = item.LicenceID,
            //                                TotalAmount = Convert.ToDecimal(item.Licence.TotalAmount),
            //                                RadioTVFrequency = Convert.ToInt32(item.Licence.RadioTVFrequency),
            //                                RadioTVUsage = Convert.ToInt32(item.Licence.RadioTVUsage),
            //                                LicFrequency = Convert.ToInt32(item.Licence.LicFrequency),
            //                                NettMass = Convert.ToString(item.Licence.NettMass),

            //                                LicExpiryDate = Convert.ToDateTime(LicExpiryDate),
            //                                TransactionAmt = Convert.ToInt32(item.Licence.TransactionAmt),
            //                                ArrearsAmt = Convert.ToInt32(item.Licence.ArrearsAmt),
            //                                PenaltiesAmt = Convert.ToInt32(item.Licence.PenaltiesAmt),
            //                                AdministrationAmt = Convert.ToInt32(item.Licence.AdministrationAmt),
            //                                TotalLicAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                                RadioTVAmt = Convert.ToInt32(item.Licence.RadioTVAmt),
            //                                RadioTVArrearsAmt = Convert.ToInt32(item.Licence.RadioTVArrearsAmt),
            //                                TotalRadioTVAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                                VehicelId = objlistVehicalModel.FirstOrDefault(c => c.VRN == item.VRN).VehicalId
            //                            });
            //                        }


            //                    }
            //                    SaveVehicleLicense(objVehicleLicense);
            //                }
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}




        }

        private void txtSumInsured_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int CheckDuplicateVRNNumber()
        {
            int responsMsg = 0;
            string regNo = txtVrn.Text;
            //string regNo = "A233fdfd";

            var client = new RestClient(IceCashRequestUrl + "CheckDuplicateVRNNumber?regNo=" + regNo);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<RiskDetailModel>(response.Content);
            string resutl = result.RegistrationNo;
            if (resutl != null)
            {
                responsMsg = 1;
                return responsMsg;
            }
            else
            {
                responsMsg = 2;
            }

            return responsMsg;
        }

        public int checkEmailExist()
        {
            int responsMsg = 0;
            string EmailAddress = txtEmailAddress.Text;

            var client = new RestClient(IceCashRequestUrl + "chkEmailExist?EmailAddress=" + EmailAddress);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<EmailModel>(response.Content);
            string resutl = result.EmailAddress;
            if (resutl == "Email already Exist")
            {
                responsMsg = 1;
                return responsMsg;
                //MessageBox.Show("Email already Exist");
                //return;
            }
            else
            {
                responsMsg = 2;
            }
            return responsMsg;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //Form1 obj = new Form1();
            //List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            //objlistRisk = null;
            //pnlThankyou.Visible = false;
            ////PnlVrn.Visible = true;
            //obj.Show();
            //this.Hide();
        }

        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        public bool SaveVehicleLicense(List<VehicleLicenseModel> objVehicleLicense)
        {
            //var IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";
            var client = new RestClient(IceCashRequestUrl + "VehicleLicense");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objVehicleLicense);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Messages>(response.Content);
            return result.Suceess;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Form1 obj = new Form1();
            //List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            //objlistRisk = null;
            //pnlThankyou.Visible = false;
            ////PnlVrn.Visible = true;
            //obj.Show();
            //this.Hide();
        }

        private void btnHomeTab_Click(object sender, EventArgs e)
        {
            Form1 objFrm = new Form1();
            objFrm.Show();
            this.Close();
        }

        public void SaveVehicalMakeAndModel(string make, string model)
        {
            VehicalMakeModel obj = new VehicalMakeModel();
            obj.make = make;
            obj.model = model;
            //var LocaCashRequestUrl = "http://localhost:6220/api/ICEcash/";
            var client = new RestClient(IceCashRequestUrl + "VehicalMakeAndModel");
            //var client = new RestClient(LocaCashRequestUrl + "VehicalMakeAndModel");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            //var result = JsonConvert.DeserializeObject<Messages>(response.Content);            
        }

        //private void btnPayNow_Click(object sender, EventArgs e)
        //{
        //    //PaymentResult objResult = new PaymentResult();
        //    //long TransactionId = 0;
        //    //TransactionId=GenerateTransactionId();
        //    //decimal transctionAmt =Convert.ToDecimal(txtAmountDue.Text);
        //    //SendSymbol(TransactionId, transctionAmt);  
        //}

        public long GenerateTransactionId()
        {
            TransactionId = 0;
            //var LocaCashRequestUrl = "http://localhost:6220/api/ICEcash/";
            //var client = new RestClient(LocaCashRequestUrl + "GenerateTransactionId");
            var client = new RestClient(IceCashRequestUrl + "GenerateTransactionId");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<UniqeTransactionModel>(response.Content);
            if (result != null)
            {
                TransactionId = result.TransactionId;
            }
            return TransactionId;
        }

        // For Payment
        //public static void SendSymbol(long TransactionId, decimal transctionAmt)
        //public void SendSymbol(long TransactionId, decimal transctionAmt, int summaryId)
        public void SendSymbol(long TransactionId, decimal transctionAmt, string paymentTermName)
        {
            string xmlString = "";


            //// TransactionId = 100020; // need to do remove

            decimal amountIncents = (int)(transctionAmt * 100);

            try
            {
                //Initialze Terminal
                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action='INIT'/></Esp:Interface>";
                //InitializeTermianl(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);


                InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

                lblPaymentMsg.Text = "Please swipe card.";

                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='120'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";

                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='100016' Type='PURCHASE' TransactionAmount='10'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";


                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + amountIncents + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";

                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='100013' Type='PURCHASE' TransactionAmount='10'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";



                //              xmlString = @"<?xml version='1.0' encoding='UTF - 8'?
                //< Esp:Interface Version = '1.0' xmlns: Esp = 'http://www.mosaicsoftware.com/Postilion/eSocket.POS/' < Esp:Transaction Account = '10' ActionCode = 'APPROVE' CardNumber = '1111222233334444' CurrencyCode = '840' DateTime = '0721084817' ExpiryDate = '0912' MerchantId = '123456' MessageReasonCode = '9790' PanEntryMode = '00' PosCondition = '00' ResponseCode = '91' ServiceRestrictionCode = '101' TerminalId = 'Term1234' Track2 = '1111222233334444=09121011234' TransactionAmount = '10000' TransactionId = '123456' Type = 'PURCHASE' </ Esp:Transaction </ Esp:Interface >";



                WriteLog("transactionid: " + TransactionId);


                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + transctionAmt + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";



                

                if (SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString)) // testing condition false
                {
                    lblPaymentMsg.Text = "";
                    // Save information
                    var summaryDetails = SaveCustomerVehical();
                    if (summaryDetails != null)
                    {
                        SavePaymentinformation(TransactionId.ToString(), summaryDetails.Id, paymentTermName);
                        //  lblpayment.Text = "Successfully";

                        lblpayment.Text = "";
                        //lblpayment.Text += "Transaction ID =" + TransactionId;
                        //lblpayment.Text += "\n";
                       // lblpayment.Text = "Sucessfully ddddd";

                        lblThankyou.Text = "Thank you for Registration";
                        lblThankyou.Text += "\n";
                        lblThankyou.Text += "Transaction ID =" + TransactionId;
                        lblThankyou.Text += "\n";
                        lblThankyou.Text += responseMessage;


                        ApproveVRNToIceCash(summaryDetails.Id);

                      //  MessageBox.Show("Policy has been sucessfully registred.");
                        pnlThankyou.Visible = true;
                        pnlsumary.Visible = false;

                        btnSumContinue.Text = "Pay";


                    }
                }
                else
                {

                    lblPaymentMsg.Text = "";
                    lblPaymentMsg.Text = "Transaction ID =" + TransactionId + ". " + responseMessage;
                    lblPaymentMsg.Text += "\n";
                    lblPaymentMsg.Text += responseMessage;


                    // MessageBox.Show("Error occurred during payment.");
                    btnSumContinue.Text = "Pay";
     
                }
                //SendTransaction("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            }
            catch (Exception ex)
            {
                WriteLog("InitializeTermianl :" + ex.Message);
                lblPaymentMsg.Text += "InitializeTermianl: " + ex.Message;

                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                //closing the terminal
                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action ='CLOSE'/></Esp:Interface>";
                InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);
            }
        }


        //public static string InitializeTermianl(String hostname, int port, string message)
        public static string InitializeTermianl(String hostname, string port, string message)
        {

            try
            {
                int Port = Convert.ToInt16(port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                TcpClient client = new TcpClient(hostname, Port);

                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);

                //first 4 bytes - length!
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte(data.Length));
                writer.Write(data);

                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                var bytes = stream.Read(data, 0, data.Length);

                responseData = System.Text.Encoding.ASCII.GetString(data, 4, (bytes - 4));
                //Console.WriteLine("Success: " + responseData);
                //Console.ReadKey();
                // Close everything.
                stream.Close();
                client.Close();
                return responseData;
            }
            catch (ArgumentNullException e)
            {
                //Console.WriteLine("ArgumentNullException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }
            catch (SocketException e)
            {
                // Console.WriteLine("SocketException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }
            catch (Exception e)
            {

                ///Console.WriteLine("SocketException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }

        }

        //public string SendTransaction(String hostname, string port, string message, int summaryId)
        public bool SendTransaction(String hostname, string port, string message)
        {
            bool result = false;



            try
            {

                int Port = Convert.ToInt16(port);

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                TcpClient client = new TcpClient(hostname, Port);
                String responseData = String.Empty;
                NetworkStream stream = client.GetStream();
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    //first 4 bytes - length!
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(data.Length));
                    writer.Write(data);

                    data = new Byte[257 * 3];

                    // String to store the response ASCII representation.


                    var bytes = stream.Read(data, 0, data.Length);

                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    //Console.WriteLine("Success: " + responseData);


                //    MessageBox.Show(responseData);
                    WriteLog("responseData: " + responseData);

                    //call save payment api
                    //DataSet ds = new DataSet();
                    //ds.ReadXml(responseData);
                    //string TransactionId = ds.ToString();
                    //obj.SavePaymentinformation(TransactionId);

                    // string myXMLfile = @"../../XMLFile/xmldata.txt";


                    //DataSet ds = new DataSet();
                    //System.IO.FileStream fsReadXml = new System.IO.FileStream
                    //    (responseData, System.IO.FileMode.Open);
                    try
                    {

                        if (responseData.Contains("APPROVE"))
                        {
                            result = true;

                            WriteLog("Status: " + "contain");
                            // string TransactionId =TransactionId;

                        }
                        else
                        {
                            WriteLog("Status: " + "failure");
                        }


                         responseMessage = getmessageresponse(Convert.ToInt32(GetMessageCode(responseData)));

                        lblPaymentMsg.Text = "";
                        //lblPaymentMsg.Text = "Transaction ID =" + TransactionId + ". " + responseMessage;
                        //lblPaymentMsg.Text = "\n";
                        //lblPaymentMsg.Text += responseMessage;

                     //   getmessageresponse(int data)

                        // MessageBox.Show("EFT retrun message: " + responseMessage);



                        //ds.ReadXml(fsReadXml);
                        //if (ds != null)
                        //{
                        //    if (ds.Tables[1].Rows.Count > 0)
                        //    {
                        //        string Status = Convert.ToString(ds.Tables[1].Rows[0]["ActionCode"]);
                        //        if (Status == "APPROVE")
                        //        {
                        //            WriteLog("Status: " + "APPROVE");
                        //            string TransactionId = Convert.ToString(ds.Tables[1].Rows[0]["TransactionId"]);
                        //            var summaryDetails = SaveCustomerVehical();
                        //            if (summaryDetails != null)
                        //            {
                        //                obj.SavePaymentinformation(TransactionId, summaryDetails.Id);
                        //                lblpayment.Text = "Successfully";
                        //                ApproveVRNToIceCash(summaryDetails.Id);
                        //                if (btnSumContinue.Text == "Processing.....")
                        //                {
                        //                    btnSumContinue.Text = "Pay";
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            lblpayment.Text = "failure";
                        //        }
                        //    }
                        //}
                        //else 
                        //{
                        //    if (responseData.Contains("APPROVE"))
                        //    {
                        //        WriteLog("Status: " + "contain");
                        //        // string TransactionId =TransactionId;
                        //        var summaryDetails = SaveCustomerVehical();
                        //        if (summaryDetails != null)
                        //        {
                        //            obj.SavePaymentinformation(TransactionId.ToString(), summaryDetails.Id);
                        //            lblpayment.Text = "Successfully";
                        //            ApproveVRNToIceCash(summaryDetails.Id);
                        //            if (btnSumContinue.Text == "Processing.....")
                        //            {
                        //                btnSumContinue.Text = "Pay";
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        WriteLog("Status: " + "failure");
                        //    }
                        //}




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        WriteLog(ex.ToString());
                    }
                    finally
                    {
                        // fsReadXml.Close();
                    }
                    //end


                    //   Console.ReadKey();
                    // Close everything.
                }
                stream.Close();
                client.Close();

                return result;

            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show(e.Message);
                // Console.WriteLine("ArgumentNullException: " + e.Message);
                //  Console.ReadKey();
                // return result;
            }
            catch (SocketException e)
            {

                MessageBox.Show(e.Message);

                //Console.WriteLine("SocketException: " + e.Message);
                //Console.ReadKey();
                // return result;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                //Console.WriteLine("SocketException: " + e.Message);
                //Console.ReadKey();
                // return result;
            }
            finally
            {
                string xmlString1 = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='17000014' Action ='CLOSE'/></Esp:Interface>";
                InitializeTermianl("localhost", "", xmlString1);
            }

            return result;
        }



        public void WriteLog(string error)
        {
            //string message = string.Format("Error Time: {0}", DateTime.Now);
            //message += error;
            //message += "-----------------------------------------------------------";

            //message += Environment.NewLine;


            ////string path = System.Web.HttpContext.Current.Server.MapPath("~/LogFile.txt");

            //string path = @"../../LogFile.txt";

            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine(message);
            //    writer.Close();
            //}
        }


        public void SavePaymentinformation(string TransactionId, int summaryId, string paymentTermName)
        {
            PaymentInformationModel objpayinfo = new PaymentInformationModel();
            objpayinfo.TransactionId = TransactionId;
            objpayinfo.SummaryDetailId = summaryId;
            objpayinfo.PaymentId = paymentTermName;

            //var localRequestUrl = "http://localhost:6220/api/ICEcash/";
            //var client = new RestClient(localRequestUrl + "SavePaymentinfo");
            var client = new RestClient(IceCashRequestUrl + "SavePaymentinfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objpayinfo);
            IRestResponse response = client.Execute(request);
        }

        public void ApproveVRNToIceCash(int SummaryId = 0)
        {
            if (ObjToken != null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }
            string Phonenumber = txtPhone.Text;
            if (objlistRisk != null)
            {
                foreach (var item in objlistRisk)
                {
                    if (item.InsuranceId != null)
                    {
                        ResultRootObject quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);
                        if (quoteresponse != null)
                        {
                            if (resObject != null && resObject.Message != "ICEcash System Error [O]")
                            {
                                var res = ICEcashService.TPIPolicy(item, parternToken);
                                if (res.Response != null && res.Response.Message == "Policy Retrieved")
                                {
                                    VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                                    objVehicleUpdate.InsuranceStatus = "Approved";
                                    objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                                    UpdateVehicleInfo(objVehicleUpdate);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void UpdateVehicleInfo(VehicleUpdateModel objVehicleUpdate)
        {
            //var localRequestUrl = "http://localhost:6220/api/ICEcash/";
            var client = new RestClient(IceCashRequestUrl + "InsuranceStatus");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objVehicleUpdate);
            IRestResponse response = client.Execute(request);
        }

        private void timerMessage_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            timerMessage.Stop();
            pnlThankyou.Visible = false;

            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void pnlThankyou_Paint(object sender, PaintEventArgs e)
        {
            timerMessage.Enabled = true;
            timerMessage.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlAddMoreVehicle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlRadioZinara_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OptNext_Click(object sender, EventArgs e)
        {



            if (!RadiobtnRadioLicence.Checked && !RadiobtnZinara.Checked)
            {
                MessageBox.Show("Please select the (RadioLicence/Zinara) type");
                return;
            }
            if (VehicalIndex == -1)
            {


                if (RadiobtnRadioLicence.Checked)
                {
                    objRiskModel.RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);

                }
                else if (RadiobtnZinara.Checked)
                {
                    objRiskModel.VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                }
            }
            else
            {
                if (RadiobtnRadioLicence.Checked)
                {
                    //objlistRisk.RadioLicenseCost = Convert.ToDecimal(txtAmountDue.Text);

                    objlistRisk[VehicalIndex].RadioLicenseCost = txtAmountDue.Text == "" ? 0 : Convert.ToDecimal(txtAmountDue.Text);

                }
                else if (RadiobtnZinara.Checked)
                {
                    objRiskModel.VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                }
            }

            pnlSum.Visible = true;
            pnlAddMoreVehicle.Visible = true;

            pnlRadioZinara.Visible = false;
            pnlZinara.Visible = false;
            pnlRadio.Visible = false;
            //CalculatePremium();


            var productid = objRiskModel.ProductId;

            // add vehical list

            if (isbackclicked == false)
            {
                CalculatePremium();
            }




            if (VehicalIndex != -1)
            {
                //Update vehical list
                SetValueForUpdate();
                loadVRNPanel(); // 19_feb
                VehicalIndex = -1;
            }
            else
            {
                if (isbackclicked == false)
                {
                    // add vehical list
                    objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    objlistRisk.Add(objRiskModel);
                }

                //    //if (isbackclicked ==true)
                //    //{
                //    //    if (objlistRisk.Count== 0)
                //    //    {
                //    //        objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                //    //        objlistRisk.Add(objRiskModel);
                //    //    }
                //    //}


                //}
                isbackclicked = false;
                loadVRNPanel();

            }

            //public void checkdummydata()
            //{

            //    XmlDocument myxml = new XmlDocument();
            //    DataSet ds = new DataSet();
            //    System.IO.FileStream fsReadXml = new System.IO.FileStream
            //        (responseData, System.IO.FileMode.Open);
            //    try
            //    {
            //        ds.ReadXml(fsReadXml);
            //        if (ds != null)
            //        {
            //            if (ds.Tables[1].Rows.Count > 0)
            //            {
            //                string Status = Convert.ToString(ds.Tables[1].Rows[0]["ActionCode"]);
            //                if (Status == "APPROVE")
            //                {
            //                    WriteLog("Status: " + "APPROVE");
            //                    string TransactionId = Convert.ToString(ds.Tables[1].Rows[0]["TransactionId"]);          
            //                }
            //                else
            //                {
            //                    lblpayment.Text = "failure";
            //                }
            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //    finally
            //    {
            //        fsReadXml.Close();
            //    }

            //}

        }

        private void optBack_Click(object sender, EventArgs e)
        {

            pnlOptionalCover.Visible = true;
            pnlRadioZinara.Visible = false;
            pnlRadio.Visible = false;
            pnlZinara.Visible = false;
            //btnAddMoreVehicle.Visible = true;
            //pnlConfirm.Visible = true;
            //pnlOptionalCover.Visible = false;
            //VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);

        }

        private void RadiobtnRadioLicence_CheckedChanged(object sender, EventArgs e)
        {
            if (RadiobtnRadioLicence.Checked)
            {

                pnlRadio.Visible = true;
                pnlZinara.Visible = false;
                objRiskModel.IncludeRadioLicenseCost = true;
                //var RadioLicenceAmounts = 0;
                //if (VehicalIndex == -1)
                //{
                //    //this.Invoke(new Action(() => RadioLicenceAmounts = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId)));
                //    objRiskModel.RadioLicenseCost = RadioLicenceAmounts;
                //    txtradioAmount.Text = Convert.ToString(objRiskModel.RadioLicenseCost);
                //    objRiskModel.IncludeRadioLicenseCost = true;
                //}
                //else
                //{
                //    //this.Invoke(new Action(() => RadioLicenceAmounts = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId)));
                //    objlistRisk[VehicalIndex].RadioLicenseCost = RadioLicenceAmounts;
                //    txtradioAmount.Text = Convert.ToString(objlistRisk[VehicalIndex].RadioLicenseCost);
                //    objlistRisk[VehicalIndex].IncludeRadioLicenseCost = true;
                //}
                // objRiskModel.IncludeZineraCost = false;

            }
            else
            {
                pnlRadio.Visible = false;
            }
        }

        private void RadiobtnZinara_CheckedChanged(object sender, EventArgs e)
        {
            if (RadiobtnZinara.Checked)
            {
                pnlZinara.Visible = true;
                pnlRadio.Visible = false;
                if (txtAccessAmount.Text != "" && txtpenalty.Text != "")
                {
                    if (VehicalIndex == -1)
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);

                        //objRiskModel.IncludeZineraCost = true;
                    }
                    else
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);

                        //objlistRisk[VehicalIndex].IncludeZineraCost = true;
                    }

                }

                objRiskModel.IncludeRadioLicenseCost = false;

            }
        }

        private void ZinPaymentDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objRiskModel == null)
                return;

            if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                return;
        }

        private void RadioPaymnetTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objRiskModel == null)
                return;

            if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                return;
        }


        public string gemessageresponde(int data)
        {
            string Message = "";

            switch (data)
            {
                case 4000:
                    Message = "Customer cancellation";
                    break;
                case 4021:
                    Message = "Timeout waiting for response";
                    break;
                case 1006:
                    Message = "Under floor limit";
                    break;
                case 1007:
                    Message = "Stand-in processing at acquirer's option (under offline limit)";
                    break;
                case 9620:
                    Message = "EMV offline approved";
                    break;
                case 9621:
                    Message = "EMV offline declined";
                    break;
                case 9622:
                    Message = "EMV approved after online";
                    break;
                case 9623:
                    Message = "EMV declined after online";
                    break;
                case 9600:
                    Message = "System malfunction";
                    break;
                case 9601:
                    Message = "System malfunction - null action";
                    break;
                case 9602:
                    Message = "Component error request pipeline";
                    break;
                case 9603:
                    Message = "Component error response pipeline";
                    break;
                case 9604:
                    Message = "Database error";
                    break;
                case 9630:
                    Message = "Customer cancellation";
                    break;
                case 9631:
                    Message = "Operator cancellation";
                    break;
                case 9635:
                    Message = "Customer timeout";
                    break;
                case 9636:
                    Message = "Card reader retries exceeded";
                    break;
                case 9637:
                    Message = "No supported EMV applications";
                    break;
                case 9638:
                    Message = "Cardholder verification failure";
                    break;
                case 9639:
                    Message = "ICC Blocked";
                    break;
                case 9640:
                    Message = "ICC Transaction failed";
                    break;
                case 9641:
                    Message = "Device failure";
                    break;
                case 9642:
                    Message = "Fatal printer error";
                    break;
                case 9643:
                    Message = "Card still in slot";
                    break;
                case 9644:
                    Message = "Card insert retries exceeded";
                    break;

                case 9650:
                    Message = "Issuer disconnected";
                    break;

                case 9651:
                    Message = "Issuer timeout before response";
                    break;

                case 9660:
                    Message = "Signature did not match";
                    break;

                case 9670:
                    Message = "Batch Totals not available";
                    break;

                case 9680:
                    Message = "Key change in progress";
                    break;

                case 9700:
                    Message = "Missing transaction amount";
                    break;

                case 9701:
                    Message = "Missing card number";
                    break;

                case 9702:
                    Message = "Missing expiry date";
                    break;

                case 9703:
                    Message = "Missing PIN data";
                    break;

                case 9704:
                    Message = "Missing processing code";
                    break;

                case 9705:
                    Message = "Missing account";
                    break;

                case 9706:
                    Message = "Missing cashback amount";
                    break;

                case 9707:
                    Message = "Missing currency code";
                    break;

                case 9708:
                    Message = "Missing merchandise data";
                    break;

                case 9709:
                    Message = "Missing effective date";
                    break;

                case 9710:
                    Message = "Missing effective date";
                    break;

                case 9711:
                    Message = "Rejections due to missing data in database";
                    break;

                case 9720:
                    Message = "Original transaction not found";
                    break;

                case 9721:
                    Message = "Duplicate transaction";
                    //  Message = "Rejections due to message conditions:
                    break;


                case 9750:
                    Message = "Expired card";
                    break;


                case 9751:
                    Message = "No supported accounts";
                    break;


                case 9752:
                    Message = "No supported accounts for manual entry";
                    break;


                case 9753:
                    Message = "Card number failed Luhn check";
                    break;


                case 9754:
                    Message = "Card not yet effective";
                    break;


                case 9755:
                    Message = "No supported accounts for ICC fallback";
                    break;



                case 9756:
                    Message = "Not valid for transaction";
                    break;


                case 9757:
                    Message = "Consecutive usage not allowed";
                    break;


                case 9758:
                    Message = "Declined because of CVV or AVS failure";
                    break;



                case 9759:
                    Message = "Card number format invalid";
                    break;


                case 9760:
                    Message = "Purchase amount exceeds maximum allowed value";
                    break;


                case 9761:
                    Message = "Cashback amount exceeds maximum allowed value";
                    break;


                case 9762:
                    Message = "Transaction amount exceeds maximum allowed value";
                    break;


                case 9763:
                    Message = "Card sequence number format invalid";
                    break;


                case 9764:
                    Message = "Inconsistent data on the chip";
                    break;


                case 9765:
                    Message = "Inconsistent data track 2";
                    break;


                case 9766:
                    Message = "Invalid track 2 data";
                    break;


                case 9770:
                    Message = "Cashback amount exceeds transaction amount";
                    break;


                case 9771:
                    Message = " Cashback amount present in non - cashback transaction";
                    break;


                case 9772:
                    Message = "Cashback not permitted to cardholder";
                    break;


                case 9773:
                    Message = "Cashback account type is invalid.";
                    break;



                case 9774:
                    Message = "Cashback currency code is invalid";
                    break;

                case 9790:
                    Message = "Upstream response";
                    break;


                case 9791:
                    Message = "Administrative response";
                    break;


                case 9792:
                    Message = " Advice response";
                    break;


                case 9793:
                    Message = "Suspected format error in advice - may not be resent";
                    break;


                case 9799:
                    Message = "Unknown";
                    break;

                case (9800 - 9999):
                    Message = "Values reserved for use in customized components.";
                    break;




            }


            return Message;
        }



        public string GetMessageCode(string responseData)
        {
            string messageReasonCode = "";

            var listStrLineElements = responseData.Split('=').ToList();
            List<ResponseCodeObj> lst = new List<ResponseCodeObj>();
            var j = 0;


            foreach (var item in listStrLineElements)
            {
                ResponseCodeObj obj = new ResponseCodeObj { Name = item };

                if (j == 1)
                {
                    var splitMessageCode = item.Split(' ');
                    if (splitMessageCode.Length > 0)
                    {
                        messageReasonCode = Regex.Replace(splitMessageCode[0], @"[^0-9a-zA-Z]+", "");
                        break;
                    }
                }

                lst.Add(obj);


                //string name = "MessageReasonCode";

                if (item.Contains("ResponseCode"))
                {
                    j = 1;
                }

            }

            return messageReasonCode;
        }








    }
}
