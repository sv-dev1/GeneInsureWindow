using GensureAPIv2.Models;
using Insurance.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestSharp;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Globalization;
using InsuranceClaim.Models;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Gene
{
    public partial class frmRenewPolicy : Form
    {
        ResultRootObject quoteresponse;

        static String ApiURL = "http://geneinsureclaim2.kindlebit.com/api/Account/";
        static String IceCashRequestUrl = "http://geneinsureclaim2.kindlebit.com/api/ICEcash/";
        static String ApiURLS = "http://geneinsureclaim2.kindlebit.com/api/Renewal/";

        //static String ApiURL = "http://localhost:6220/api/Account/";
        //static String IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";
        //static String ApiURLS = "http://localhost:6220/api/Renewal/";

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

        public frmRenewPolicy()
        {

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
            pnlRenewRiskDetails.Visible = false;
            pnlRenewPersonalDetails.Visible = false;
            pnlRenewsumary.Visible = false;
            pnlRenewSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            PnlRenewVrn.Visible = true;


            // pnlSummery.
            pnlRenewSum.Location = new Point(335, 100);
            pnlRenewSum.Size = new System.Drawing.Size(1160, 1200);


            //18 Feb


            pnlRenewRadioZinara.Visible = false;
            pnlReRadio.Visible = false;
            pnlReZinara.Visible = false;


            pnlRenewRadioZinara.Location = new Point(335, 100);
            pnlRenewRadioZinara.Size = new System.Drawing.Size(1390, 750);



            //pnlReRadio.Location = new Point(222, 217);
            //pnlReRadio.Size = new System.Drawing.Size(824, 334);


            pnlReRadio.Location = new Point(14, 152);
            pnlReRadio.Size = new System.Drawing.Size(625, 335);



            //pnlReZinara.Location = new Point(222, 217);
            //pnlReZinara.Size = new System.Drawing.Size(824, 334);



            pnlReZinara.Location = new Point(656, 149);
            pnlReZinara.Size = new System.Drawing.Size(562, 340);

            //End



            pnlRenewConfirm.Visible = false;
            pnlRenewOptionalCover.Visible = false;

            PnlRenewVrn.Location = new Point(335, 100);
            PnlRenewVrn.Size = new System.Drawing.Size(1350, 638);

            pnlRenewLogo.Location = new Point(this.Width - 320, this.Height - 220);

            pnlRenewLogo.Size = new System.Drawing.Size(300, 220);

            pnlRenewRiskDetails.Location = new Point(335, 100);
            pnlRenewRiskDetails.Size = new System.Drawing.Size(1350, 638);


            pnlRenewOptionalCover.Location = new Point(335, 100);
            pnlRenewOptionalCover.Size = new System.Drawing.Size(1390, 690);



            pnlAddMoreVehicle.Location = new Point(994, 398);
            pnlAddMoreVehicle.Size = new System.Drawing.Size(263, 99);


            pnlRenewPersonalDetails.Location = new Point(355, 100);
            pnlRenewPersonalDetails.Size = new System.Drawing.Size(1450, 720);

            pnlRenewPersonalDetails2.Location = new Point(355, 100);
            pnlRenewPersonalDetails2.Size = new System.Drawing.Size(1450, 720);

            pnlRenewsumary.Location = new Point(355, 100);
            pnlRenewsumary.Size = new System.Drawing.Size(1390, 1040);
            pnlSummery.Size = new System.Drawing.Size(800, 700);

            pnlRenewConfirm.Location = new Point(335, 100);
            pnlRenewConfirm.Size = new System.Drawing.Size(1350, 750);

            pnlRenewThankyou.Location = new Point(300, 33);
            pnlRenewThankyou.Size = new System.Drawing.Size(1180, 1040);




            txtRenewVrn.Text = "Car Registration Number";
            txtRenewVrn.ForeColor = SystemColors.GrayText;


            txtZipCode.Text = "00263";
            txtZipCode.ForeColor = SystemColors.GrayText;

            bindMake();
            bindVehicleUsage();
            bindCoverType();
            bindPaymentType();

            // Checkobject();
            // loadVRNPanel();
        }

        public void loadVRNPanel()
        {

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
                    lblvehicle.Location = new Point(i, 3);
                    lblvehicle.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    lblTermly.Location = new Point(i, 50);
                    lblTermly.BackColor = Color.Transparent;
                    lblTermly.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :";
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured);

                    lblSumInsured.AutoSize = true;
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.Location = new Point(10, 140);
                    lblSumInsured.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    lblTotalpremium.Text = "Total premium :";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.BackColor = Color.Transparent;
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.Location = new Point(10, 190);
                    lblTotalpremium.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    lblDiscount.Text = "Discount :";
                    lblDiscount.Text += objlistRisk[i].Discount == null ? "0" : Convert.ToString(objlistRisk[i].Discount);
                    lblDiscount.BackColor = Color.Transparent;
                    lblDiscount.AutoSize = true;
                    lblDiscount.Location = new Point(10, 240);
                    lblDiscount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    lblZTSC.Text = "ZTSC  ";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    lblZTSC.Width = 100;

                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(10, 290);
                    lblZTSC.AutoSize = true;
                    lblZTSC.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                    lblStampDuty.Text = "Stamp Duty  ";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    lblStampDuty.Width = 100;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.BackColor = Color.Transparent;
                    lblStampDuty.Location = new Point(10, 340);
                    lblStampDuty.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

                    //Button btnEdit = new System.Windows.Forms.Button();
                    //btnEdit.Click += BtnEdit_Click;
                    //btnEdit.Text = paddedParam;
                    //btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    //btnEdit.Width = 100;
                    //btnEdit.Height = 40;

                    //btnEdit.Location = new Point(10, 415);
                    //btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    //btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    //btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    //btnEdit.BackColor = Color.Transparent;
                    //btnEdit.FlatAppearance.BorderSize = 0;
                    //Bottompnl.Controls.Add(btnEdit);




                    //Button btnDelete = new System.Windows.Forms.Button();
                    //btnDelete.Click += BtnDelete_Click;
                    //btnDelete.Text = paddedParam;
                    //btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);

                    //btnDelete.Width = 100;
                    //btnDelete.Height = 40;
                    //btnDelete.BackColor = Color.Transparent;
                    //btnDelete.Location = new Point(430, 415);

                    //btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    //btnDelete.FlatAppearance.BorderSize = 0;
                    //btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    //btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;




                    //Bottompnl.Controls.Add(btnDelete);


                    pnl.Size = new System.Drawing.Size(550, 100);
                    pnl.Location = new Point(140, (i * 140));


                    Bottompnl.Size = new System.Drawing.Size(550, 480);
                    Bottompnl.Location = new Point(140, (i * 140));

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
                    lblZTSC.Text = "ZTSC  :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    lblZTSC.Width = 100;
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
                    lblTotalpremium.Location = new Point(i + 150, 55);
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
                    lblZTSC.Text = "ZTSC  :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    // lblZTSC.Width = 70;
                    lblZTSC.AutoSize = true;
                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(i + 300, 55);
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
                    lblStampDuty.Location = new Point(i + 420, 55);
                    lblStampDuty.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    string res = "";
                    string paddedParam = res.PadRight(50);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 70;
                    btnEdit.Location = new Point(i + 540, 55);
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

                    btnDelete.Location = new Point(i + 630, 55);
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

            //objRiskModel = new RiskDetailModel();

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

        private void RequestQuote()
        {
            throw new NotImplementedException();
        }

        public void CheckToken()
        {
            if (ObjToken == null)
            {
                ObjToken = IcServiceobj.getToken();
                ProcessICECashrequest(txtRenewVrn.Text, txtSumInsured.Text, cmbMake.SelectedText, cmbModel.SelectedText, Convert.ToString(cmbPaymentTerm.SelectedValue), txtYear.Text, Convert.ToString(cmbCoverType.SelectedValue), Convert.ToString(cmbVehicleUsage.SelectedValue), "1");

            }

        }
        #region
        public void ProcessICECashrequest(String VRN, String SumINsured, String Make, String Model, String Paymentterm, String VehYear, String CoverType, String VehicleUsage, String TaxClass)
        {
            ResultRootObject quoteresponse = IcServiceobj.RequestQuote(ObjToken.Response.PartnerToken, VRN, SumINsured, Make, Model, Convert.ToInt32(Paymentterm), Convert.ToInt32(VehYear), Convert.ToInt32(CoverType), Convert.ToInt32(VehicleUsage), ObjToken.PartnerReference, customerInfo);
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
        #endregion
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
            cmbCoverType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        public void bindMake()
        {
            //var ApiURLL = "http://localhost:6220/api/Account/";
            /*var client = new RestClient(ApiURLL + "Makes");  */             //after test comment Line
            var client = new RestClient(ApiURL + "Makes");               //and Uncomment this line
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddHeader("content-type", "application/json");//, "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<MakeObject>>(response.Content);
            cmbMake.DataSource = result;
            cmbMake.DisplayMember = "MakeDescription";
            cmbMake.ValueMember = "makeCode";
            //cmbMake.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cmbMake.AutoCompleteSource = AutoCompleteSource.CustomSource;

            bindModel(Convert.ToString(cmbMake.SelectedValue));

        }

        public void bindModel(String MaKECode)
        {
            //var ApiURLL = "http://localhost:6220/api/Account/";
            var client = new RestClient(ApiURL + "Models?makeCode=" + MaKECode);//after test comment Line

            //var client = new RestClient(ApiURLL + "Models?makeCode=" + MaKECode);Uncomment this
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
            cmbPaymentTerm.DataSource = result;
            cmbPaymentTerm.DisplayMember = "name";
            cmbPaymentTerm.ValueMember = "ID";
            //  cmbPaymentTerm.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //18 Feb
            ReZinPaymentDetail.DataSource = result;
            ReZinPaymentDetail.DisplayMember = "name";
            ReZinPaymentDetail.ValueMember = "ID";

            ReRadioPaymnetTerm.DataSource = result;
            ReRadioPaymnetTerm.DisplayMember = "name";
            ReRadioPaymnetTerm.ValueMember = "ID";

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
            cmbVehicleUsage.DataSource = result;
            cmbVehicleUsage.DisplayMember = "vehUsage";
            cmbVehicleUsage.ValueMember = "id";
            cmbVehicleUsage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private void frmQuote_Load(object sender, EventArgs e)
        {
            txtZipCode.Text = "00263";
        }

        private void txtVrn_Enter(object sender, EventArgs e)
        {

        }

        private void txtVrn_Leave(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.DoWork += Worker_DoWork;
            //worker.RunWorkerAsync();
            if (btnSave.Text == "Submit")
            {
                btnSave.Text = "Processing.....";
            }
            if (txtRenewVrn.Text != "")
            {
                if (txtRenewVrn.Text != "Car Registration Number")
                {
                    bindRenewDetail(txtRenewVrn.Text);

                }
                else
                {
                    MessageBox.Show("Please Enter VRN/POLICY No.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Enter VRN/POLICY No.");
                return;
            }
            if (btnSave.Text == "Processing.....")
            {
                btnSave.Text = "Submit";
            }
        }
        //Today21Jan
        public void bindRenewDetail(string SearchTxt)
        {
            //var ApiURLS = "http://localhost:6220/api/Renewal/";
            var client = new RestClient(ApiURLS + "RenewVehicleDetail?SearchTxt=" + SearchTxt);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //objRiskModel = new RiskDetailModel();
            var result = JsonConvert.DeserializeObject<RenewalPolicyModel>(response.Content);
            if (result != null)
            {


                if (result.Cutomer.FirstName != null && result.riskdetail.RegistrationNo != null && result.SummaryDetails.TotalPremium != null)
                {
                    bindAllField(result);
                    this.PnlRenewVrn.Hide();
                    pnlRenewConfirm.Visible = true;
                    // this.pnlRenewRiskDetails.Show();

                }
                else
                {
                    MessageBox.Show("Please Enter The Correct VRN Number");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Enter The Correct VRN Number");
                return;
            }

        }
        public void bindvehiclemake(string MAkeId)
        {
            //var ApiURLS = "http://localhost:6220/api/Renewal/";
            var client = new RestClient(ApiURLS + "MakeDetail?MAkeId=" + MAkeId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            var result = (new JavaScriptSerializer()).Deserialize<MakeObject>(response.Content);

            cmbMake.ValueMember = "makeCode";



            cmbMake.SelectedIndex = cmbMake.FindString(result.MakeDescription);



            bindModel(Convert.ToString(cmbMake.SelectedValue));

            return;
        }

        public void bindAllField(RenewalPolicyModel result)
        {
            customerInfo = new CustomerModel();
            objRiskModel = new RiskDetailModel();
            summaryModel = new SummaryDetailModel();
            //Bind in Model CustomerDetail 

            customerInfo = result.Cutomer;
            objRiskModel = result.riskdetail;
            summaryModel = result.SummaryDetails;
            bindMake();
            bindvehiclemake(objRiskModel.MakeId);


            //Bind Txt Filed

            txtSumInsured.Text = Convert.ToString(result.riskdetail.SumInsured);
            cmbVehicleUsage.SelectedValue = result.riskdetail.VehicleUsage;
            if (result.riskdetail.PaymentTermId == 12)
            {
                cmbPaymentTerm.SelectedValue = 1;
                //ReZinPaymentDetail.SelectedValue = 1;
                ReRadioPaymnetTerm.SelectedValue = 1;


            }
            else
            {
                cmbPaymentTerm.SelectedValue = result.riskdetail.PaymentTermId;
                //ReZinPaymentDetail.SelectedValue = result.riskdetail.PaymentTermId;
                ReRadioPaymnetTerm.SelectedValue = result.riskdetail.PaymentTermId;
            }


            //18 Feb
            ICEcashService IcServiceobj = new ICEcashService();

            //9Jan2019
            ObjToken = CheckParterTokenExpire();

            if (ObjToken != null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }



            //End
            cmbCoverType.SelectedValue = result.riskdetail.CoverTypeId;
            cmbMake.SelectedValue = result.riskdetail.MakeId;

            //Check this  Again
            #region
            //Int32 index = cmbMake.FindStringExact(result.riskdetail.MakeId);
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

            //Int32 indexModel = cmbModel.FindString(result.riskdetail.MakeId);
            //this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));



            #endregion Check End

            this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = result.riskdetail.VehicleUsage));


            txtYear.Text = Convert.ToString(result.riskdetail.VehicleYear);
            txtChasis.Text = result.riskdetail.ChasisNumber;
            txtEngine.Text = result.riskdetail.EngineNumber;
            //CheckBox
            chkExcessBuyback.Checked = result.riskdetail.ExcessBuyBack;
            chkRoadsideAssistance.Checked = result.riskdetail.RoadsideAssistance;
            chkMedicalExpenses.Checked = result.riskdetail.MedicalExpenses;
            chkPassengerAccidentalCover.Checked = result.riskdetail.PassengerAccidentCover;
            if (result.riskdetail.PassengerAccidentCover == true)
            {
                cmbNoofPerson.Value = Convert.ToInt32(result.riskdetail.NumberofPersons);
            }
            else
            {
                cmbNoofPerson.Value = 0;
            }

            //Personal Infomation
            txtName.Text = result.Cutomer.FirstName + " " + result.Cutomer.LastName;
            txtEmailAddress.Text = result.Cutomer.EmailAddress;
            txtPhone.Text = result.Cutomer.PhoneNumber;
            if (result.Cutomer.Gender == "Male")
            {
                rdbMale.Checked = true;
            }
            else if (result.Cutomer.Gender == "Female")
            {
                rdbFemale.Checked = true;
            }
            txtDOB.Text = Convert.ToString(result.Cutomer.DateOfBirth);
            txtAdd1.Text = result.Cutomer.AddressLine1;
            txtAdd2.Text = result.Cutomer.AddressLine2;
            txtCity.Text = result.Cutomer.City;
            txtZipCode.Text = result.Cutomer.Zipcode;
            txtIDNumber.Text = result.Cutomer.NationalIdentificationNumber;






            //END

            //customerInfo.Id = result.Cutomer.Id;
            //customerInfo.FirstName = result.Cutomer.FirstName;
            //customerInfo.LastName = result.Cutomer.LastName;
            //customerInfo.Gender = result.Cutomer.Gender;
            //customerInfo.AddressLine1 = result.Cutomer.AddressLine1;
            //customerInfo.AddressLine2 = result.Cutomer.AddressLine2;

            //customerInfo.City = result.Cutomer.City;
            //customerInfo.Zipcode = result.Cutomer.Zipcode;
            //customerInfo.NationalIdentificationNumber = result.Cutomer.NationalIdentificationNumber;
            //customerInfo.UserID = result.Cutomer.UserID;
            //customerInfo.EmailAddress = result.Cutomer.EmailAddress;
            //customerInfo.DateOfBirth = result.Cutomer.DateOfBirth;
            //customerInfo.PhoneNumber = result.Cutomer.PhoneNumber;
            //Bind Policy and VehicleDetail

            //objRiskModel.Id = result.riskdetail.Id;
            //objRiskModel.ProductId = result.riskdetail.ProductId;
            //objRiskModel.Premium = result.riskdetail.Premium;
            //objRiskModel.MakeId = result.riskdetail.MakeId;
            //objRiskModel.ModelId = result.riskdetail.ModelId;
            //objRiskModel.VehicleYear = result.riskdetail.VehicleYear;

            //objRiskModel.ChasisNumber = result.riskdetail.ChasisNumber;
            //objRiskModel.Discount = result.riskdetail.Discount;
            //objRiskModel.ZTSCLevy = result.riskdetail.ZTSCLevy;
            //objRiskModel.StampDuty = result.riskdetail.StampDuty;
            //objRiskModel.EngineNumber = result.riskdetail.EngineNumber;
            //objRiskModel.CoverTypeId = result.riskdetail.CoverTypeId;
            //objRiskModel.VehicleUsage = result.riskdetail.VehicleUsage;
            //objRiskModel.SumInsured = result.riskdetail.SumInsured;
            //objRiskModel.PaymentTermId = result.riskdetail.PaymentTermId;
            //objRiskModel.PassengerAccidentCoverAmount = result.riskdetail.PassengerAccidentCoverAmount;

            //objRiskModel.MedicalExpensesAmount = result.riskdetail.MedicalExpensesAmount;
            //objRiskModel.ExcessBuyBackAmount = result.riskdetail.ExcessBuyBackAmount;
            //objRiskModel.RoadsideAssistanceAmount = result.riskdetail.RoadsideAssistanceAmount;

            //objRiskModel.PassengerAccidentCover = result.riskdetail.PassengerAccidentCover;
            //objRiskModel.MedicalExpenses = result.riskdetail.MedicalExpenses;
            //objRiskModel.ExcessBuyBack = result.riskdetail.ExcessBuyBack;
            //objRiskModel.RoadsideAssistance = result.riskdetail.RoadsideAssistance;






        }

        #region
        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    isbackclicked = false;
        //    this.Invoke(new Action(() => pictureBox1.Visible = true));
        //    // first screen where enter vrn number
        //    if (txtRenewVrn.Text == string.Empty || txtRenewVrn.Text == "Car Registration Number" || txtRenewVrn.Text.Length == 0)
        //    {
        //        MessageBox.Show("Please Enter Registration Number");
        //        return;
        //    }
        //    else
        //    {
        //        if (VehicalIndex == -1)
        //        {
        //            var vehicalDetails = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtRenewVrn.Text.Trim());
        //            if (vehicalDetails != null)
        //            {
        //                MessageBox.Show("You have already added this registration number.");
        //                VrnAlredyExist();
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            objlistRisk[VehicalIndex].RegistrationNo = txtRenewVrn.Text;
        //            var vehicalList = objlistRisk.Where(c => c.RegistrationNo == txtRenewVrn.Text.Trim()).ToList();
        //            if (vehicalList.Count > 1)
        //            {
        //                MessageBox.Show("You have already added this registration number.");
        //                VrnAlredyExist();
        //                return;
        //            }
        //        }

        //        var success = 0;
        //        success = ProcessQuote();
        //        objRiskModel.RegistrationNo = txtRenewVrn.Text;

        //        if (success == 1)
        //        {
        //            this.Invoke(new Action(() => pictureBox1.Visible = false));

        //            this.Invoke(new Action(() => pnlRenewRiskDetails.Visible = true));
        //            this.Invoke(new Action(() => PnlRenewVrn.Visible = false));
        //            this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
        //        }
        //        if (success == 2)
        //        {
        //            this.Invoke(new Action(() => pictureBox1.Visible = false));
        //            this.Invoke(new Action(() => pnlRenewRiskDetails.Visible = true));
        //            this.Invoke(new Action(() => PnlRenewVrn.Visible = false));
        //            this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
        //            MessageBox.Show("Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.");
        //        }
        //        if (success == 3)
        //        {
        //            this.Invoke(new Action(() => pictureBox1.Visible = false));
        //            this.Invoke(new Action(() => pnlRenewRiskDetails.Visible = true));
        //            this.Invoke(new Action(() => PnlRenewVrn.Visible = false));
        //            this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
        //        }
        //    }
        //}
        #endregion
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            // throw new NotImplementedException();
        }

        private void VrnAlredyExist()
        {
            this.Invoke(new Action(() => pnlRenewRiskDetails.Visible = false));
            this.Invoke(new Action(() => PnlRenewVrn.Visible = true));
            this.Invoke(new Action(() => pnlAddMoreVehicle.Visible = false));
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            PnlRenewVrn.Visible = true;
            pnlRenewSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            pnlRenewPersonalDetails.Visible = false;
            pnlRenewConfirm.Visible = false;
            pnlRenewRiskDetails.Visible = false;

            string s = (sender as Button).Text;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == s.Trim());
        }

        //public void ProcessQuote()

        #region ProcessQuote
        //public int ProcessQuote()
        //{
        //    int result = 0;
        //    objRiskModel = new RiskDetailModel();
        //    ICEcashService IcServiceobj = new ICEcashService();

        //    //9Jan2019
        //    ObjToken = CheckParterTokenExpire();

        //    if (ObjToken != null)
        //    {
        //        parternToken = ObjToken.Response.PartnerToken;
        //    }

        //    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, ""); // comment for testing

        //    //quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, "d3238e6e-65fd-4b55-b7aa-5206ad79"); // comment for testing

        //    if (quoteresponse != null)
        //    {
        //        resObject = quoteresponse.Response;
        //        //if token expire
        //        if (resObject != null && resObject.Message == "Partner Token has expired. ")
        //        {
        //            ObjToken = IcServiceobj.getToken();
        //            if (ObjToken != null)
        //            {
        //                parternToken = ObjToken.Response.PartnerToken;
        //                quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
        //                resObject = quoteresponse.Response;
        //            }
        //        }

        //        if (resObject != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
        //        {
        //            if (resObject != null && resObject.Message != "ICEcash System Error [O]")
        //            {
        //                result = 1;

        //                objRiskModel.isVehicleRegisteredonICEcash = true;

        //                //Policy Details 
        //                if (VehicalIndex != -1)
        //                {
        //                    this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
        //                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));

        //                    int cmVehicleValue = 0;
        //                    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
        //                    if (cmVehicleValue != 0)
        //                    {
        //                        this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
        //                    }
        //                    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));


        //                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
        //                    {
        //                        cmbPaymentTerm.SelectedValue = 1;
        //                    }
        //                    else
        //                    {
        //                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
        //                    }

        //                    txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName;
        //                    txtPhone.Text = "";
        //                    this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
        //                    this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
        //                    this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
        //                    this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
        //                    this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

        //                    Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);

        //                    this.Invoke(new Action(() => cmbMake.SelectedIndex = index));

        //                    this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));


        //                    Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

        //                    this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

        //                    //Bind premium amount
        //                    objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount);
        //                    objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy);
        //                    objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty);
        //                    objlistRisk[VehicalIndex].InsuranceId = resObject.Quotes[0].InsuranceID;

        //                    var discount = 0.00m;
        //                    this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

        //                    objlistRisk[VehicalIndex].Discount = discount;

        //                }
        //                else
        //                {
        //                    objRiskModel.isVehicleRegisteredonICEcash = true;
        //                    this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
        //                    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));
        //                    int cmVehicleValue = 0;

        //                    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));


        //                    if (cmVehicleValue != 0)
        //                    {
        //                        this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
        //                    }
        //                    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));


        //                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
        //                    {
        //                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = 1));
        //                    }
        //                    else
        //                    {
        //                        this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
        //                    }



        //                    this.Invoke(new Action(() => txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName));
        //                    this.Invoke(new Action(() => txtPhone.Text = ""));
        //                    this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
        //                    this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
        //                    this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
        //                    this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
        //                    this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

        //                    Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
        //                    this.Invoke(new Action(() => cmbMake.SelectedIndex = index));
        //                    int indexMake = 0;
        //                    this.Invoke(new Action(() => indexMake = cmbMake.SelectedIndex));
        //                    if (indexMake == -1)
        //                    {
        //                        bindModel("0");
        //                    }
        //                    else
        //                    {
        //                        this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));
        //                    }

        //                    //this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));

        //                    Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

        //                    this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

        //                    this.Invoke(new Action(() => objRiskModel.Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount)));
        //                    this.Invoke(new Action(() => objRiskModel.ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy)));
        //                    this.Invoke(new Action(() => objRiskModel.StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty)));
        //                    this.Invoke(new Action(() => objRiskModel.InsuranceId = resObject.Quotes[0].InsuranceID));

        //                    var discount = 0.00m;
        //                    this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

        //                    objRiskModel.Discount = discount;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            result = 2;
        //        }
        //    }

        //    else
        //    {
        //        int cmVehicleValue = 0;
        //        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);


        //        result = 3;
        //    }

        //    return result;
        //}

        #endregion
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
            pnlRenewSum.Visible = true;
            // pnlAddMoreVehicle.Visible = true;
            pnlRenewPersonalDetails.Visible = false;
            pnlAddMoreVehicle.Visible = false;

            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtRenewVrn.Text);

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
            if (VehicalIndex == -1)
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    objRiskModel.SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);
                    objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    objRiskModel.PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objRiskModel.CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                }
            }
            else
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    objlistRisk[VehicalIndex].SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);
                    objlistRisk[VehicalIndex].VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    objlistRisk[VehicalIndex].PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objlistRisk[VehicalIndex].CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                }
            }
            //pnlRenewConfirm.Visible = false;
            pnlRenewRiskDetails.Visible = false;
            pnlRenewOptionalCover.Visible = true;
            //pnlRenewRiskDetails.Visible = false;


        }

        private void btnRiskBack_Click(object sender, EventArgs e)
        {
            //PnlRenewVrn.Visible = true;
            pnlRenewConfirm.Visible = true;
            pnlRenewRiskDetails.Visible = false;

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
            if (!(rdbMale.Checked || rdbFemale.Checked))
            {
                MessageBox.Show("Please Select Gender");
                return;
            }

            if (txtName.Text != string.Empty && txtEmailAddress.Text != string.Empty && txtPhone.Text != string.Empty)
            {

                pnlRenewPersonalDetails2.Visible = true;
                pnlRenewPersonalDetails.Visible = false;
                //int result = checkEmailExist();
                //if (result == 1)
                //{
                //    MessageBox.Show("Email already Exist");
                //    return;
                //}
                //else
                //{
                //    pnlRenewPersonalDetails2.Visible = true;
                //    pnlRenewPersonalDetails.Visible = false;
                //}
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
                    else
                    {
                        label2.Visible = false;
                        txtSumInsured.Visible = false;
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
                    objlistRisk[VehicalIndex].MakeId = Convert.ToString(cmbMake.SelectedValue);
                    objlistRisk[VehicalIndex].ModelId = Convert.ToString(cmbModel.SelectedValue);
                    objlistRisk[VehicalIndex].EngineNumber = Convert.ToString(txtEngine.Text);
                    objlistRisk[VehicalIndex].ChasisNumber = Convert.ToString(txtChasis.Text);
                }
            }

            //pnlRenewOptionalCover.Visible = true;
            pnlRenewConfirm.Visible = false;
            pnlRenewRiskDetails.Visible = true;

        }

        private void btnConfBack_Click(object sender, EventArgs e)
        {
            //pnlRenewRiskDetails.Visible = true;
            PnlRenewVrn.Visible = true;
            pnlRenewConfirm.Visible = false;

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
            pnlRenewPersonalDetails.Visible = true;
            pnlRenewPersonalDetails2.Visible = false;
        }

        public void btnPer2Con_Click(object sender, EventArgs e)
        {
            if (txtAdd1.Text == string.Empty || txtAdd2.Text == string.Empty || txtCity.Text == string.Empty || txtIDNumber.Text == string.Empty || txtZipCode.Text == string.Empty)
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

            if (txtAdd1.Text != string.Empty && txtAdd2.Text != string.Empty && txtCity.Text != string.Empty && txtIDNumber.Text != string.Empty && txtZipCode.Text != string.Empty)
            {

                pnlRenewsumary.Visible = true;
                pnlRenewPersonalDetails2.Visible = false;

                customerInfo.FirstName = txtName.Text;
                customerInfo.LastName = customerInfo.LastName;
                customerInfo.EmailAddress = txtEmailAddress.Text;
                customerInfo.AddressLine2 = txtAdd2.Text;
                customerInfo.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
                customerInfo.City = txtCity.Text;
                customerInfo.PhoneNumber = txtPhone.Text;
                customerInfo.CountryCode = "+263";
                customerInfo.AddressLine1 = txtAdd1.Text;
                customerInfo.NationalIdentificationNumber = txtIDNumber.Text;
                // customerInfo.Gender = rdbFemale

                if (rdbMale.Checked)
                    customerInfo.Gender = "Male";
                else if (rdbFemale.Checked)
                    customerInfo.Gender = "Female";

                customerInfo.Zipcode = "002633";
                SetUserInput();

                //if (objListUserInput.Count > 0)
                //{
                //    UserInput objExistingInput = objListUserInput.Find(x => x.VRN == txtRenewVrn.Text);
                //    if (objExistingInput != null)
                //    {
                //        objExistingInput.VRN = txtRenewVrn.Text;
                //        objExistingInput.SumInsured = txtSumInsured.Text;

                //        objExistingInput.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
                //        objExistingInput.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
                //        objExistingInput.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);

                //        //Vehicle Type
                //        objExistingInput.Make = cmbMake.SelectedText;
                //        objExistingInput.MakeID = Convert.ToString(cmbMake.SelectedValue);
                //        objExistingInput.Model = cmbModel.SelectedText;
                //        objExistingInput.ModelID = Convert.ToString(cmbModel.SelectedValue);
                //        objExistingInput.Year = txtYear.Text;
                //        objExistingInput.ChasisNumber = txtChasis.Text;
                //        objExistingInput.EngineNumber = txtEngine.Text;


                //        //PersonalDetails1
                //        objExistingInput.Name = txtName.Text;
                //        objExistingInput.EmailAddress = txtEmailAddress.Text;
                //        objExistingInput.Phone = txtPhone.Text;
                //        objExistingInput.Gender = "";
                //        objExistingInput.DOB = txtDOB.Value.ToString("MM/dd/yyyy");
                //        //objExistingInput.DOB = txtDOB.Text;



                //        //PersonalDetails2
                //        objExistingInput.Address1 = txtAdd1.Text;
                //        objExistingInput.Address2 = txtAdd2.Text;
                //        objExistingInput.City = txtCity.Text;
                //        objExistingInput.Zip = txtZipCode.Text;
                //        objExistingInput.IDNumber = txtIDNumber.Text;


                //        //optionalCover
                //        objExistingInput.ExcessBuyback = Convert.ToInt32(chkExcessBuyback.Checked);
                //        objExistingInput.RoadsideAssistance = Convert.ToInt32(chkRoadsideAssistance.Checked);
                //        objExistingInput.MedicalExpenses = Convert.ToInt32(chkMedicalExpenses.Checked);
                //        objExistingInput.PassengerAccidentalCover = Convert.ToInt32(chkPassengerAccidentalCover.Checked);
                //        objExistingInput.NumberOfPerson = Convert.ToInt32(cmbNoofPerson.Value);
                //    }
                //    else
                //    {
                //        SetUserInput();
                //    }
                //}
                //else
                //{
                //    SetUserInput();
                //}

            }

            // calculate summary
            CaclulateSummary(objRiskModel);

            //CheckToken();
        }
        public void SetUserInput()
        {
            //UserInput objU = new UserInput();
            //objRiskModel = new RiskDetailModel();
            objRiskModel.RegistrationNo = txtRenewVrn.Text;
            objRiskModel.SumInsured = Convert.ToDecimal(txtSumInsured.Text);




            objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            objRiskModel.PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            objRiskModel.CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);



            //Vehicle Type
            objRiskModel.MakeId = cmbMake.SelectedText;
            objRiskModel.MakeId = Convert.ToString(cmbMake.SelectedValue);
            objRiskModel.ModelId = cmbModel.SelectedText;
            objRiskModel.ModelId = Convert.ToString(cmbModel.SelectedValue);
            objRiskModel.VehicleYear = Convert.ToInt32(txtYear.Text);
            objRiskModel.ChasisNumber = txtChasis.Text;
            objRiskModel.EngineNumber = txtEngine.Text;


            //PersonalDetails1
            customerInfo.FirstName = txtName.Text;
            if (customerInfo.LastName != null)
            {
                customerInfo.LastName = customerInfo.LastName;
            }

            customerInfo.EmailAddress = txtEmailAddress.Text;
            customerInfo.PhoneNumber = txtPhone.Text;
            //customerInfo.Gender = "";
            //objU.DOB = txtDOB.Text;
            //customerInfo.DateOfBirth = Convert.ToDateTime(txtDOB);



            //PersonalDetails2
            customerInfo.AddressLine1 = txtAdd1.Text;
            customerInfo.AddressLine2 = txtAdd2.Text;
            customerInfo.City = txtCity.Text;
            customerInfo.Zipcode = "00263";
            customerInfo.NationalIdentificationNumber = txtIDNumber.Text;


            //optionalCover
            objRiskModel.ExcessBuyBack = chkExcessBuyback.Checked;
            objRiskModel.RoadsideAssistance = chkRoadsideAssistance.Checked;
            objRiskModel.MedicalExpenses = chkMedicalExpenses.Checked;
            objRiskModel.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;
            objRiskModel.NumberofPersons = Convert.ToInt32(cmbNoofPerson.Value);
            //objListUserInput.Add(objU);
            //NewVRN();
        }

        private void btnSumBack_Click(object sender, EventArgs e)
        {
            pnlRenewPersonalDetails2.Visible = true;
            pnlRenewsumary.Visible = false;

        }

        private void frmQuote_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }

        private void txtVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void txtVrn_Leave_1(object sender, EventArgs e)
        {

            if (txtRenewVrn.Text.Length == 0)
            {
                txtRenewVrn.Text = "Car Registration Number";
                txtRenewVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtVrn_Enter_1(object sender, EventArgs e)
        {
            if (txtRenewVrn.Text == "Car Registration Number")
            {
                txtRenewVrn.Text = "";
                txtRenewVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void PnlVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnContionOptionalCover_Click(object sender, EventArgs e)
        {


            pnlRenewRadioZinara.Visible = true;

            if (chkRadioLicence.Checked && chkZinaraLicFee.Checked)
            {
                pnlReRadio.Visible = true;
                pnlReZinara.Visible = true;
            }

            else if (chkRadioLicence.Checked)
            {
                pnlReRadio.Visible = true;
                pnlReZinara.Visible = false;
            }
            else if (chkZinaraLicFee.Checked)
            {
                pnlReZinara.Visible = true;
                pnlReRadio.Visible = false;
            }
            else
            {
                pnlReRadio.Visible = false;
                pnlReRadio.Visible = false;

            }
            pnlRenewOptionalCover.Visible = false;
            var productid = objRiskModel.ProductId;




            //pnlRenewSum.Visible = true;
            ////pnlAddMoreVehicle.Visible = true;
            //pnlAddMoreVehicle.Visible = false;
            //pnlRenewOptionalCover.Visible = false;
            ////CalculatePremium();


            //var productid = objRiskModel.ProductId;
            //DS 4 FEB
            //ICEcashService IcServiceobj = new ICEcashService();
            //ObjToken = CheckParterTokenExpire();

            //if (ObjToken != null)
            //{
            //    parternToken = ObjToken.Response.PartnerToken;
            //}

            //quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, ""); // comment for testing
            //if (quoteresponse != null)
            //{
            //    resObject = quoteresponse.Response;
            //    //if token expire
            //    if (resObject != null && resObject.Message == "Partner Token has expired. ")
            //    {
            //        ObjToken = IcServiceobj.getToken();
            //        if (ObjToken != null)
            //        {
            //            parternToken = ObjToken.Response.PartnerToken;
            //            quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
            //            resObject = quoteresponse.Response;
            //        }
            //    }

            //    if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
            //    {

            //        if (resObject != null && resObject.Message != "ICEcash System Error [O]")
            //        {
            //            string make = resObject.Quotes[0].Vehicle.Make;
            //            string model = resObject.Quotes[0].Vehicle.Model;
            //            if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model))
            //            {
            //                SaveVehicalMakeAndModel(make, model);
            //                bindMake();
            //            }
            //            else
            //            {
            //                // set make and model if IceCash does not retrun
            //                resObject.Quotes[0].Vehicle.Make = "0";
            //                resObject.Quotes[0].Vehicle.Model = "0";
            //            }
            //        }
            //    }


            //}
            // add vehical list
            //CalculatePremium();



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
        //DS 4 FEB
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


        public void SetValueForUpdate()
        {
            // VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == objRiskModel.RegistrationNo);

            objlistRisk[VehicalIndex].RegistrationNo = txtRenewVrn.Text;
            //objlistRisk[VehicalIndex].SumInsured = sum
        }

        private void btnOptionCoverBack_Click(object sender, EventArgs e)
        {
            //pnlRenewConfirm.Visible = true;
            //pnlRenewOptionalCover.Visible = false;
            //VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtRenewVrn.Text);


            btnAddMoreVehicle.Visible = false;
            pnlRenewRiskDetails.Visible = true;
            //pnlConfirm.Visible = true;
            pnlRenewOptionalCover.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtRenewVrn.Text);
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
                PnlRenewVrn.Visible = true;
                pnlRenewSum.Visible = false;
                NewVRN();

                //btnAddMoreVehicle.Visible = false;
            }
        }

        private void cmbMake_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindModel(Convert.ToString(cmbMake.SelectedValue));
        }

        public void NewVRN()
        {
            txtRenewVrn.Text = "Car Registration Number";
            txtRenewVrn.ForeColor = SystemColors.GrayText;
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
        }

        private void btnSDContinue_Click(object sender, EventArgs e)
        {
            // 5 th screen vehical summary detals 

            if (objlistRisk.Count > 0)
            {
                pnlRenewPersonalDetails.Visible = true;
                pnlRenewSum.Visible = false;
                pnlAddMoreVehicle.Visible = false;
                txtDOB.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtDOB.MaxDate = DateTime.Today;
                txtDOB.CalendarForeColor = Color.LightGray;
            }
        }

        private void BtnSDback_Click(object sender, EventArgs e)
        {


            pnlRenewSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            pnlRenewRadioZinara.Visible = true;
            if (chkRadioLicence.Checked)
            {
                pnlReRadio.Visible = true;
            }
            if (chkZinaraLicFee.Checked)
            {
                pnlReZinara.Visible = true;
            }

            if (isVehicalDeleted)
            {
                PnlVrn.Visible = true;
                pnlRenewSum.Visible = false;
                pnlRenewOptionalCover.Visible = false;
                NewVRN();

                isVehicalDeleted = false;
            }
            else
            {
                VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtRenewVrn.Text);
            }


            isbackclicked = true;





            //pnlRenewOptionalCover.Visible = true;

            //pnlRenewSum.Visible = false;
            //pnlAddMoreVehicle.Visible = false;


            //if (isVehicalDeleted)
            //{
            //    PnlRenewVrn.Visible = true;
            //    pnlRenewSum.Visible = false;
            //    pnlRenewOptionalCover.Visible = false;
            //    NewVRN();

            //    isVehicalDeleted = false;
            //}
            //else
            //{
            //    VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtRenewVrn.Text);
            //}


            //isbackclicked = true;
        }

        private void cmbCoverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            int CoverType = 0;
            //picbxCoverType.Visible = true;
            try
            {
                if (objRiskModel == null)
                    return;

                if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                    return;
                if (Convert.ToString(cmbCoverType.SelectedValue) == Convert.ToString(objRiskModel.CoverTypeId))
                    return;
                #region get ICE cash token
                ICEcashTokenResponse ObjToken = IcServiceobj.getToken(); // uncomment this line 

                // ICEcashTokenResponse ObjToken = null;
                #endregion
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                }
                List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();
                //DS 4FEB
                quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, ""); // comment for testing

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
                            quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
                            resObject = quoteresponse.Response;
                        }
                    }
                    if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                    {
                        objVehicles.Add(new RiskDetailModel { RegistrationNo = txtRenewVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });
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
                            if (cmbCoverType.SelectedValue != null)
                            {
                                CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
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

                            string RegistrationNo = txtRenewVrn.Text;
                            string suminsured = txtSumInsured.Text;
                            string make = Convert.ToString(cmbMake.Text);
                            string model = Convert.ToString(cmbModel.Text);
                            int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                            int VehicleYear = Convert.ToInt32(txtYear.Text);
                            int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                            int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                            string PartnerReference = ObjToken.PartnerReference;

                            ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 


                            // picbxCoverType.Visible = false;
                            //picbxRiskDetail.Visible = false;
                            //ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, txtVrn.Text, txtSumInsured.Text, Convert.ToString(cmbMake.SelectedValue), Convert.ToString(cmbModel.SelectedValue), Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(txtYear), Convert.ToInt32(cmbCoverType.SelectedValue), Convert.ToInt32(cmbVehicleUsage.SelectedValue), "", customerInfo);
                            response.result = quoteresponse.Response.Result;
                            if (response.result == 0)
                            {
                                response.message = quoteresponse.Response.Quotes[0].Message;
                            }
                            else
                            {
                                response.Data = quoteresponse;

                                if (quoteresponse.Response.Quotes[0] != null)
                                {

                                    ////9Jan

                                    cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.InsuranceType);
                                    cmbVehicleUsage.SelectedValue = quoteresponse.Response.Quotes[0].Vehicle.VehicleType;
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));

                                    }
                                    //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
                                    //Check this 

                                    objRiskModel.isVehicleRegisteredonICEcash = true;

                                    if (quoteresponse.Response.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        cmbPaymentTerm.SelectedValue = 1;
                                    }
                                    else
                                    {
                                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
                                    }



                                    txtName.Text = quoteresponse.Response.Quotes[0].Client.FirstName + " " + quoteresponse.Response.Quotes[0].Client.LastName;
                                    txtPhone.Text = "";
                                    txtAdd1.Text = quoteresponse.Response.Quotes[0].Client.Address1;
                                    txtAdd2.Text = quoteresponse.Response.Quotes[0].Client.Address2;
                                    txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
                                    txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;
                                    txtYear.Text = quoteresponse.Response.Quotes[0].Vehicle.YearManufacture;

                                    Int32 index = cmbMake.FindStringExact(quoteresponse.Response.Quotes[0].Vehicle.Make);

                                    cmbMake.SelectedIndex = index;

                                    bindModel(cmbMake.SelectedValue.ToString());


                                    Int32 indexModel = cmbModel.FindString(quoteresponse.Response.Quotes[0].Vehicle.Model);

                                    cmbModel.SelectedIndex = indexModel;

                                    /////End



                                    objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount);
                                    objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy);
                                    objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty);

                                    var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));

                                    objRiskModel.Discount = discount;
                                    // Session["InsuranceId"] = quoteresponse.Response.Quotes[0].InsuranceID;


                                }
                                //else if(response.message == "Partner Token has expired. ")
                                //{
                                //    ObjToken = IcServiceobj.getToken();
                                //    if (ObjToken != null)
                                //    {
                                //        parternToken = ObjToken.Response.PartnerToken;
                                //        quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, "");
                                //        resObject = quoteresponse.Response;
                                //    }
                                //}

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(resObject.Quotes[0].Message);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = "Error occured.";
            }



        }

        private void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objRiskModel == null)
                return;

            if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                return;

            //if (cmbPaymentTerm.SelectedValue == "object.ddd")
            if (Convert.ToString(cmbPaymentTerm.SelectedValue) == Convert.ToString(objRiskModel.PaymentTermId))
                return;

            var paymenttermval = cmbPaymentTerm.SelectedValue;
            var getvrntextval = txtRenewVrn.Text;

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();

            try
            {
                #region get ICE cash token

                ICEcashTokenResponse ObjToken = IcServiceobj.getToken();
                #endregion
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                }
                quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(getvrntextval, parternToken, ""); // comment for testing

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
                            quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(getvrntextval, parternToken, "");
                            resObject = quoteresponse.Response;
                        }
                    }

                    if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                    {
                        List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();
                        objVehicles.Add(new RiskDetailModel { RegistrationNo = txtRenewVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });

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

                            string RegistrationNo = txtRenewVrn.Text;
                            string suminsured = txtSumInsured.Text;
                            string make = Convert.ToString(cmbMake.Text);
                            string model = Convert.ToString(cmbModel.Text);
                            int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                            int VehicleYear = Convert.ToInt32(txtYear.Text);
                            int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                            int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                            //string PartnerReference = ObjToken.PartnerReference;
                            //   string PartnerReference = ObjToken.PartnerReference;

                            ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo);

                            response.result = quoteresponse.Response.Result;
                            if (response.result == 0)
                            {
                                response.message = quoteresponse.Response.Quotes[0].Message;
                            }
                            else
                            {
                                response.Data = quoteresponse;

                                if (quoteresponse.Response.Quotes[0] != null)
                                {

                                    ////9Jan

                                    cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.InsuranceType);
                                    cmbVehicleUsage.SelectedValue = quoteresponse.Response.Quotes[0].Vehicle.VehicleType;
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));

                                    }
                                    //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);


                                    if (quoteresponse.Response.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        cmbPaymentTerm.SelectedValue = 1;
                                    }
                                    else
                                    {
                                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
                                    }
                                    //Check this
                                    objRiskModel.isVehicleRegisteredonICEcash = true;

                                    txtName.Text = quoteresponse.Response.Quotes[0].Client.FirstName + " " + quoteresponse.Response.Quotes[0].Client.LastName;
                                    txtPhone.Text = "";
                                    txtAdd1.Text = quoteresponse.Response.Quotes[0].Client.Address1;
                                    txtAdd2.Text = quoteresponse.Response.Quotes[0].Client.Address2;
                                    txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
                                    txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;
                                    txtYear.Text = quoteresponse.Response.Quotes[0].Vehicle.YearManufacture;

                                    Int32 index = cmbMake.FindStringExact(quoteresponse.Response.Quotes[0].Vehicle.Make);

                                    cmbMake.SelectedIndex = index;

                                    bindModel(cmbMake.SelectedValue.ToString());


                                    Int32 indexModel = cmbModel.FindString(quoteresponse.Response.Quotes[0].Vehicle.Model);

                                    cmbModel.SelectedIndex = indexModel;

                                    /////End



                                    objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount);
                                    objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy);
                                    objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty);

                                    var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));

                                    objRiskModel.Discount = discount;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(resObject.Quotes[0].Message);
                    }
                    //  ICEcashService.LICQuote(regNo, tokenObject.Response.PartnerToken);
                    //  json.Data = response;
                }
            }
            catch (Exception ex)
            {
                response.message = "Error occured.";
            }
        }

        public void bindProductid(int VehicleUsageId)
        {
            var client = new RestClient(ApiURL + "GetProductId?VehicleUsageId=" + VehicleUsageId);
            var request = new RestRequest(Method.GET);

            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<ProductIdModel>(response.Content);
            objRiskModel.ProductId = result.ProductId;

        }

        public void CalculatePremium()
        {
            VehicleDetails obj = new VehicleDetails();
            obj.vehicleUsageId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //obj.sumInsured =Convert.ToDecimal(txtSumInsured.Text);
            obj.sumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);
            obj.coverType = Convert.ToInt32(cmbCoverType.SelectedValue);
            obj.PaymentTermid = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            obj.NumberofPersons = Convert.ToInt32(cmbNoofPerson.Value);
            obj.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;
            obj.ExcessBuyBack = chkExcessBuyback.Checked;
            obj.RoadsideAssistance = chkRoadsideAssistance.Checked;
            obj.MedicalExpenses = chkMedicalExpenses.Checked;
            obj.IncludeRadioLicenseCost = objRiskModel.IncludeRadioLicenseCost;
            obj.AddThirdPartyAmount = 0.00m;

            //obj.ProductId = objRiskModel.ProductId;
            //obj.RadioLicenseCost = objRiskModel.RadioLicenseCost;
            //obj.isVehicleRegisteredonICEcash = true;
            //obj.BasicPremiumICEcash =Convert.ToString(objRiskModel.Premium);
            //obj.StampDutyICEcash = Convert.ToString(objRiskModel.StampDuty);
            //obj.ZTSCLevyICEcash = Convert.ToString(objRiskModel.ZTSCLevy);
            //obj.Addthirdparty = objRiskModel.Addthirdparty;
            //obj.AddThirdPartyAmount = objRiskModel.AddThirdPartyAmount;

            obj.ProductId = objRiskModel.ProductId;
            obj.RadioLicenseCost = objRiskModel.RadioLicenseCost;
            obj.isVehicleRegisteredonICEcash = objRiskModel.isVehicleRegisteredonICEcash;
            obj.BasicPremiumICEcash = objRiskModel.Premium == null ? "0" : Convert.ToString(objRiskModel.Premium);
            obj.StampDutyICEcash = objRiskModel.StampDuty == null ? "0" : Convert.ToString(objRiskModel.StampDuty);
            obj.ZTSCLevyICEcash = objRiskModel.ZTSCLevy == null ? "0" : Convert.ToString(objRiskModel.ZTSCLevy);
            obj.Addthirdparty = false;
            //obj.AddThirdPartyAmount = 00;

            //var client = new RestClient(ApiURL + "CalculateTotalPremium");


            //var IceCashRequestUrll = "http://localhost:6220/api/ICEcash/";

            //var client = new RestClient(IceCashRequestUrll + "CalculateTotalPremium");
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
                    objlistRisk[VehicalIndex].Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium);
                    objlistRisk[VehicalIndex].Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount);
                    objlistRisk[VehicalIndex].ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy);
                    objlistRisk[VehicalIndex].StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty);
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
                objRiskModel.Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium);
                objRiskModel.Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount);
                objRiskModel.ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy);
                objRiskModel.StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty);
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
        }

        private void chkRoadsideAssistance_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.RoadsideAssistance = chkRoadsideAssistance.Checked;
        }

        private void chkExcessBuyback_CheckedChanged(object sender, EventArgs e)
        {
        }

        public SummaryDetailModel SaveCustomerVehical()
        {
            CustomerREVehicalModel objPlanModel = new CustomerREVehicalModel();
            SummaryDetailModel summaryDetialsModel = new SummaryDetailModel();

            PolicyDetail policyDetial = new PolicyDetail();
            objlistVehicalModel = new List<VehicalModel>();

            objPlanModel.CustomerModel = customerInfo;
            objPlanModel.RiskDetailModel = objRiskModel;
            objPlanModel.PolicyDetail = policyDetial;
            objPlanModel.SummaryDetailModel = summaryModel;

            // var LocalIceCashReqUrl = "http://localhost:6220/api/Renewal/";
            //IceCashRequestUrl

            if (objPlanModel != null)
            {
                //   var client = new RestClient(LocalIceCashReqUrl + "SaveREVehicalDetail");
                var client = new RestClient(IceCashRequestUrl + "SaveREVehicalDetail");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(objPlanModel);
                IRestResponse response = client.Execute(request);
                //var result = (new JavaScriptSerializer()).Deserialize<List<VehicalModel>>(response.Content);
                summaryDetialsModel = JsonConvert.DeserializeObject<SummaryDetailModel>(response.Content);

                if (summaryDetialsModel != null)
                {
                    lblRepayment.Text = "Successfully";
                    pnlRenewThankyou.Visible = true;
                    pnlRenewsumary.Visible = false;

                }
            }

            return summaryDetialsModel;
        }

        public void CaclulateSummary(RiskDetailModel objRiskDetail)
        {
            //summaryModel = new SummaryDetailModel();
            summaryModel.TotalPremium = 0.00m;
            summaryModel.TotalRadioLicenseCost = 0.00m;
            summaryModel.Discount = 0.00m;
            summaryModel.AmountPaid = 0.00m;
            summaryModel.TotalSumInsured = 0.00m;
            try
            {
                //if (objRiskDetail != null && objRiskDetail.Count > 0)
                //{
                //    foreach (var item in objRiskDetail)
                //    {
                //        summaryModel.TotalPremium += item.Premium + item.ZTSCLevy + item.StampDuty + item.VehicleLicenceFee;
                //        if (item.IncludeRadioLicenseCost)
                //        {
                //            summaryModel.TotalPremium += item.RadioLicenseCost;
                //            summaryModel.TotalRadioLicenseCost += item.RadioLicenseCost;
                //        }
                //        summaryModel.Discount += item.Discount;
                //    }



                summaryModel.TotalPremium += objRiskDetail.Premium + objRiskDetail.ZTSCLevy + objRiskDetail.StampDuty;
                if (chkZinaraLicFee.Checked)
                {
                    summaryModel.TotalPremium += objRiskDetail.Premium + objRiskDetail.ZTSCLevy + objRiskDetail.StampDuty + objRiskDetail.VehicleLicenceFee;
                    summaryModel.VehicleLicencefees = objRiskDetail.VehicleLicenceFee;
                }
                if (objRiskDetail.IncludeRadioLicenseCost)
                {
                    summaryModel.TotalPremium += objRiskDetail.RadioLicenseCost;
                    summaryModel.TotalRadioLicenseCost += objRiskDetail.RadioLicenseCost;
                }
                summaryModel.Discount = objRiskDetail.Discount;
                summaryModel.TotalRadioLicenseCost = Math.Round(Convert.ToDecimal(summaryModel.TotalRadioLicenseCost), 2);
                summaryModel.Discount = Math.Round(Convert.ToDecimal(summaryModel.Discount), 2);
                var calcualatedPremium = Math.Round(Convert.ToDecimal(summaryModel.TotalPremium), 2);
                //summaryModel.TotalPremium = Math.Round(Convert.ToDecimal(calcualatedPremium - summaryModel.Discount), 2);

                //model.MaxAmounttoPaid = Math.Round(Convert.ToDecimal(model.TotalPremium), 2);
                summaryModel.AmountPaid = Convert.ToDecimal(summaryModel.TotalPremium);

                summaryModel.TotalStampDuty = objRiskDetail.StampDuty;
                summaryModel.TotalSumInsured = objRiskDetail.SumInsured;
                summaryModel.TotalZTSCLevies = objRiskDetail.ZTSCLevy;
                summaryModel.ExcessBuyBackAmount = objRiskDetail.ExcessBuyBackAmount;
                summaryModel.MedicalExpensesAmount = objRiskDetail.MedicalExpensesAmount;
                summaryModel.PassengerAccidentCoverAmount = objRiskDetail.PassengerAccidentCoverAmount;
                summaryModel.RoadsideAssistanceAmount = objRiskDetail.RoadsideAssistanceAmount;
                summaryModel.ExcessAmount = objRiskDetail.ExcessBuyBackAmount;

                //summaryModel.TotalStampDuty = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.StampDuty)), 2);
                //summaryModel.TotalSumInsured = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.SumInsured)), 2);
                //summaryModel.TotalZTSCLevies = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ZTSCLevy)), 2);
                //summaryModel.ExcessBuyBackAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessBuyBackAmount)), 2);
                //summaryModel.MedicalExpensesAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.MedicalExpensesAmount)), 2);
                //summaryModel.PassengerAccidentCoverAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.PassengerAccidentCoverAmount)), 2);
                //summaryModel.RoadsideAssistanceAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.RoadsideAssistanceAmount)), 2);
                //summaryModel.ExcessAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessAmount)), 2);


                txtDiscount.Text = Convert.ToString(summaryModel.Discount);
                txtTotalPremium.Text = Convert.ToString(summaryModel.TotalPremium);
                txtAmountDue.Text = Convert.ToString(summaryModel.AmountPaid);
                txtRadionLicence.Text = Convert.ToString(summaryModel.TotalRadioLicenseCost);
                txtStampDuty.Text = Convert.ToString(summaryModel.TotalStampDuty);
                //txtTotalSumInsured.Text = Convert.ToString(summaryModel.TotalSumInsured);
                //txtExcessAmount.Text = Convert.ToString(summaryModel.ExcessAmount);
                txtMedicalExcessAmount.Text = Convert.ToString(summaryModel.MedicalExpensesAmount);
                txtPassengerAccidentAmt.Text = Convert.ToString(summaryModel.PassengerAccidentCoverAmount);
                txtRoadsideAssitAmt.Text = Convert.ToString(summaryModel.RoadsideAssistanceAmount);
                txtZTSCLevies.Text = Convert.ToString(summaryModel.TotalZTSCLevies);
                txtExcessBuyBackAmt.Text = Convert.ToString(summaryModel.ExcessBuyBackAmount);
                txtReZinaraAmount.Text = Convert.ToString(summaryModel.VehicleLicencefees);
                //txtReZinaraAmount.Text = summaryModel
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long GenerateTransactionId()
        {
            long TransactionId = 0;
            //var LocaCashRequestUrl = "http://localhost:6220/api/ICEcash/";
            //var client = new RestClient(LocaCashRequestUrl + "GenerateTransactionId");
            var client = new RestClient(IceCashRequestUrl + "GenerateTransactionId");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<PaymentInfoModel>(response.Content);
            if (result != null)
            {
                TransactionId = result.TransactionId;
            }
            return TransactionId;
        }

        private void btnSumContinue_Click(object sender, EventArgs e)
        {
            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            // Save all details
            CustomerModel customerModel = new CustomerModel();
            customerModel.FirstName = txtName.Text;
            customerModel.EmailAddress = txtEmailAddress.Text;

            var _TransactionId = "100020";
            var _summaryDetails = SaveCustomerVehical();

            SavePaymentinformation(_TransactionId, _summaryDetails.Id);
            var summaryDetails = SaveCustomerVehical();

            //Payment detail

            //PaymentResult objResult = new PaymentResult();
            //long TransactionId = 0;
            //TransactionId = GenerateTransactionId();
            //decimal transctionAmt = Convert.ToDecimal(txtAmountDue.Text);
            //SendSymbol(TransactionId, transctionAmt);






            //pnlThankyou.Visible = true;
            //SaveCustomerVehical();





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

            //    ResultRootObject quoteresponse = IcServiceobj.TPILICQuote(obj, ObjToken.Response.PartnerToken);
            //    response.result = quoteresponse.Response.Result;
            //    if (response.result == 0)
            //    {
            //        response.message = quoteresponse.Response.Quotes[0].Message;
            //    }
            //    else
            //    {
            //        response.Data = quoteresponse;

            //        if (quoteresponse.Response.Quotes != null)
            //        {
            //            if (quoteresponse.Response.Quotes.Count != 0)
            //            {
            //                objVehicleLicense = new List<VehicleLicenseModel>();
            //                foreach (var item in quoteresponse.Response.Quotes.ToList())
            //                {
            //                    string format = "yyyyMMdd";
            //                    var LicExpiryDate = DateTime.ParseExact(item.Licence.LicExpiryDate, format, CultureInfo.InvariantCulture);
            //                    objVehicleLicense.Add(new VehicleLicenseModel
            //                    {
            //                        InsuranceID = item.InsuranceID,
            //                        VRN = item.VRN,
            //                        CombinedID = item.CombinedID,
            //                        LicenceID = item.LicenceID,
            //                        TotalAmount = Convert.ToDecimal(item.Licence.TotalAmount),
            //                        RadioTVFrequency = Convert.ToInt32(item.Licence.RadioTVFrequency),
            //                        RadioTVUsage = Convert.ToInt32(item.Licence.RadioTVUsage),
            //                        LicFrequency = Convert.ToInt32(item.Licence.LicFrequency),
            //                        NettMass = Convert.ToString(item.Licence.NettMass),

            //                        LicExpiryDate = Convert.ToDateTime(LicExpiryDate),
            //                        TransactionAmt = Convert.ToInt32(item.Licence.TransactionAmt),
            //                        ArrearsAmt = Convert.ToInt32(item.Licence.ArrearsAmt),
            //                        PenaltiesAmt = Convert.ToInt32(item.Licence.PenaltiesAmt),
            //                        AdministrationAmt = Convert.ToInt32(item.Licence.AdministrationAmt),
            //                        TotalLicAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                        RadioTVAmt = Convert.ToInt32(item.Licence.RadioTVAmt),
            //                        RadioTVArrearsAmt = Convert.ToInt32(item.Licence.RadioTVArrearsAmt),
            //                        TotalRadioTVAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                        VehicelId = objlistVehicalModel.FirstOrDefault(c => c.VRN == item.VRN).VehicalId
            //                    });
            //                }
            //                SaveVehicleLicense(objVehicleLicense);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public void SendSymbol(long TransactionId, decimal transctionAmt)
        {
            string xmlString = "";
            try
            {
                //Initialze Terminal
                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action='INIT'/></Esp:Interface>";
                //InitializeTermianl(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);


                InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='17000014' TransactionId='452569' Type='PURCHASE' TransactionAmount='120'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";



                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='120'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";

                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='100016' Type='PURCHASE' TransactionAmount='10'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";

                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='100013' Type='PURCHASE' TransactionAmount='10'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";



                //              xmlString = @"<?xml version='1.0' encoding='UTF - 8'?
                //< Esp:Interface Version = '1.0' xmlns: Esp = 'http://www.mosaicsoftware.com/Postilion/eSocket.POS/' < Esp:Transaction Account = '10' ActionCode = 'APPROVE' CardNumber = '1111222233334444' CurrencyCode = '840' DateTime = '0721084817' ExpiryDate = '0912' MerchantId = '123456' MessageReasonCode = '9790' PanEntryMode = '00' PosCondition = '00' ResponseCode = '91' ServiceRestrictionCode = '101' TerminalId = 'Term1234' Track2 = '1111222233334444=09121011234' TransactionAmount = '10000' TransactionId = '123456' Type = 'PURCHASE' </ Esp:Transaction </ Esp:Interface >";



                WriteLog("transactionid: 100016");


                //xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + transctionAmt + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";



                //SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString, summaryId);

                //SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);
                SendTransaction("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            }
            catch (Exception ex)
            {
                WriteLog("InitializeTermianl :" + ex.Message);

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


        public void WriteLog(string error)
        {
            string message = string.Format("Error Time: {0}", DateTime.Now);
            message += error;
            message += "-----------------------------------------------------------";

            message += Environment.NewLine;


            //string path = System.Web.HttpContext.Current.Server.MapPath("~/LogFile.txt");

            string path = @"../../LogFile.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

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

        public string SendTransaction(String hostname, string port, string message)
        {
            try
            {
                int Port = Convert.ToInt16(port);
                frmRenewPolicy obj = new frmRenewPolicy();
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

                    WriteLog("responseData: " + responseData);

                    //call save payment api
                    //DataSet ds = new DataSet();
                    //ds.ReadXml(responseData);
                    //string TransactionId = ds.ToString();
                    //obj.SavePaymentinformation(TransactionId);

                    // string myXMLfile = @"../../XMLFile/xmldata.txt";


                    DataSet ds = new DataSet();
                    System.IO.FileStream fsReadXml = new System.IO.FileStream
                        (responseData, System.IO.FileMode.Open);
                    try
                    {
                        ds.ReadXml(fsReadXml);
                        if (ds != null)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                string Status = Convert.ToString(ds.Tables[1].Rows[0]["ActionCode"]);
                                if (Status == "APPROVE")
                                {
                                    WriteLog("Status: " + "APPROVE");
                                    string TransactionId = Convert.ToString(ds.Tables[1].Rows[0]["TransactionId"]);
                                    var summaryDetails = SaveCustomerVehical();
                                    if (summaryDetails != null)
                                    {
                                        obj.SavePaymentinformation(TransactionId, summaryDetails.Id);
                                        lblpayment.Text = "Successfully";
                                        ApproveVRNToIceCash(summaryDetails.Id);
                                    }
                                }
                                else
                                {
                                    lblpayment.Text = "failure";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        WriteLog(ex.ToString());
                    }
                    finally
                    {
                        fsReadXml.Close();
                    }
                    //end

                    //only for testing



                    //   Console.ReadKey();
                    // Close everything.
                }
                stream.Close();
                client.Close();

                return responseData;

            }
            catch (ArgumentNullException e)
            {
                // Console.WriteLine("ArgumentNullException: " + e.Message);
                //  Console.ReadKey();
                return "null";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: " + e.Message);
                Console.ReadKey();
                return "null";
            }
            catch (Exception e)
            {
                Console.WriteLine("SocketException: " + e.Message);
                Console.ReadKey();
                return "null";
            }
            finally
            {
                //              string xmlString1 = @"<?xml version='1.0' encoding='UTF-8'?>
                //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='17000014' Action ='CLOSE'/></Esp:Interface>";
                //              InitializeTermianl("localhost", 23001, xmlString1);
            }

        }



        public void SavePaymentinformation(string TransactionId, int summaryId)
        {
            PaymentInformationModel objpayinfo = new PaymentInformationModel();
            objpayinfo.TransactionId = TransactionId;
            objpayinfo.SummaryDetailId = summaryId;

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
            var localRequestUrl = "http://localhost:6220/api/ICEcash/";
            //IceCashRequestUrl

            var client = new RestClient(localRequestUrl + "InsuranceStatus");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objVehicleUpdate);
            IRestResponse response = client.Execute(request);
        }


        public int CheckDuplicateVRNNumber()
        {
            int responsMsg = 0;
            string regNo = txtRenewVrn.Text;
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
            Form1 obj = new Form1();
            List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            objlistRisk = null;
            pnlRenewThankyou.Visible = false;
            //PnlVrn.Visible = true;
            obj.Show();
            this.Hide();
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
            Form1 obj = new Form1();
            List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            objlistRisk = null;
            pnlRenewThankyou.Visible = false;
            //PnlVrn.Visible = true;
            obj.Show();
            this.Hide();
        }

        private void lblpayment_Click(object sender, EventArgs e)
        {

        }

        private void pnlRenewThankyou_Paint(object sender, PaintEventArgs e)
        {
            timerMessage.Enabled = true;
            timerMessage.Start();
        }

        private void timerMessage_Tick_1(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            timerMessage.Stop();
            pnlThankyou.Visible = false;

            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void ReRadiobtnRadioLicence_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRadioLicence.Checked)
            {

                pnlReRadio.Visible = true;
                pnlReZinara.Visible = false;
                objRiskModel.IncludeRadioLicenseCost = true;

            }
            else
            {
                pnlReRadio.Visible = false;
            }
        }

        private void ReRadiobtnZinara_CheckedChanged(object sender, EventArgs e)
        {


            if (chkZinaraLicFee.Checked)
            {
                pnlReZinara.Visible = true;
                pnlReRadio.Visible = false;
                if (txtReAccessAmount.Text != "" && txtReRepenalty.Text != "")
                {
                    if (VehicalIndex == -1)
                    {
                        var amount = txtReAccessAmount.Text;
                        var amount1 = txtReRepenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtReZinTotalAmount.Text = Convert.ToString(totalamouny);

                        //objRiskModel.IncludeZineraCost = true;
                    }
                    else
                    {
                        var amount = txtReAccessAmount.Text;
                        var amount1 = txtReRepenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtReZinTotalAmount.Text = Convert.ToString(totalamouny);

                        //objlistRisk[VehicalIndex].IncludeZineraCost = true;
                    }

                }

                objRiskModel.IncludeRadioLicenseCost = false;

            }

        }

        private void optReBack_Click(object sender, EventArgs e)
        {

            pnlRenewOptionalCover.Visible = true;
            pnlRenewRadioZinara.Visible = false;
            pnlReRadio.Visible = false;
            pnlReZinara.Visible = false;

        }

        private void OptReNext_Click(object sender, EventArgs e)
        {



            if (!chkRadioLicence.Checked && !chkZinaraLicFee.Checked)
            {
                MessageBox.Show("Please select the (RadioLicence/Zinara) type");
                return;
            }
            if (VehicalIndex == -1)
            {


                if (chkRadioLicence.Checked)
                {
                    objRiskModel.RadioLicenseCost = txtReRadioAmount.Text == "" ? 0 : Convert.ToDecimal(txtReRadioAmount.Text);

                }
                if (chkZinaraLicFee.Checked)
                {
                    objRiskModel.VehicleLicenceFee = txtReZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtReZinTotalAmount.Text);
                }
            }
            else
            {
                if (chkRadioLicence.Checked)
                {
                    //objlistRisk.RadioLicenseCost = Convert.ToDecimal(txtAmountDue.Text);

                    objlistRisk[VehicalIndex].RadioLicenseCost = txtAmountDue.Text == "" ? 0 : Convert.ToDecimal(txtAmountDue.Text);

                }
                else if (chkZinaraLicFee.Checked)
                {
                    objRiskModel.VehicleLicenceFee = txtReZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtReZinTotalAmount.Text);
                }
            }

            pnlRenewSum.Visible = true;
            pnlAddMoreVehicle.Visible = true;

            pnlRenewRadioZinara.Visible = false;
            pnlReZinara.Visible = false;
            pnlReRadio.Visible = false;
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



        }

        private void chkRadioLicence_CheckedChanged(object sender, EventArgs e)
        {

            if (chkRadioLicence.Checked)
            {

                pnlReRadio.Visible = true;
                txtReAccessAmount.Text = "0";
                txtReRepenalty.Text = "0";
                txtReRadioAmount.Text = "0";
                ObjToken = CheckParterTokenExpire();
                if (chkZinaraLicFee.Checked)
                {
                    pnlReZinara.Visible = true;

                }
                else
                {
                    pnlReZinara.Visible = false;
                }
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                }
                var quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
                if (quoteresponse.Response.Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                {
                    var _quoteresponse = IcServiceobj.ZineraLICQuote(txtRenewVrn.Text, parternToken, quoteresponse.Response.Quotes[0].Client.IDNumber);
                    var _resObjects = _quoteresponse.Response;
                    if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                    {
                        //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                        //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                        this.Invoke(new Action(() => txtReAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                        this.Invoke(new Action(() => txtReRepenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                        this.Invoke(new Action(() => txtReRadioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                    }

                    objRiskModel.IncludeRadioLicenseCost = true;
                }
            }
            else
            {
                pnlReRadio.Visible = false;
                objRiskModel.IncludeRadioLicenseCost = false;
            }
        }

        private void chkZinaraLicFee_CheckedChanged(object sender, EventArgs e)
        {

            if (chkZinaraLicFee.Checked)
            {
                pnlReZinara.Visible = true;
                //pnlReRadio.Visible = false;
                txtReAccessAmount.Text = "0";
                txtReRepenalty.Text = "0";
                txtReRadioAmount.Text = "0";

                if (txtReAccessAmount.Text != "" && txtReRepenalty.Text != "")
                {
                    if (VehicalIndex == -1)
                    {

                        if (ObjToken != null)
                        {
                            parternToken = ObjToken.Response.PartnerToken;
                        }
                        var quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
                        if (quoteresponse.Response.Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                        {
                            var _quoteresponse = IcServiceobj.ZineraLICQuote(txtRenewVrn.Text, parternToken, quoteresponse.Response.Quotes[0].Client.IDNumber);
                            var _resObjects = _quoteresponse.Response;
                            if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                            {
                                //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                                //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                                this.Invoke(new Action(() => txtReAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                                this.Invoke(new Action(() => txtReRepenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                                this.Invoke(new Action(() => txtReRadioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                            }

                            //objRiskModel.IncludeRadioLicenseCost = true;
                        }


                        var amount = txtReAccessAmount.Text;
                        var amount1 = txtReRepenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtReZinTotalAmount.Text = Convert.ToString(totalamouny);
                        if (chkRadioLicence.Checked)
                        {
                            pnlReRadio.Visible = true;
                        }
                        else
                        {
                            pnlReRadio.Visible = false;
                        }

                        //objRiskModel.IncludeZineraCost = true;
                    }
                    else
                    {
                        if (ObjToken != null)
                        {
                            parternToken = ObjToken.Response.PartnerToken;
                        }
                        var quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtRenewVrn.Text, parternToken, "");
                        if (quoteresponse.Response.Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                        {
                            var _quoteresponse = IcServiceobj.ZineraLICQuote(txtRenewVrn.Text, parternToken, quoteresponse.Response.Quotes[0].Client.IDNumber);
                            var _resObjects = _quoteresponse.Response;
                            if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                            {
                                //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                                //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                                this.Invoke(new Action(() => txtReAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                                this.Invoke(new Action(() => txtReRepenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                                this.Invoke(new Action(() => txtReRadioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                            }




                            var amount = txtReAccessAmount.Text;
                            var amount1 = txtReRepenalty.Text;
                            var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                            txtReZinTotalAmount.Text = Convert.ToString(totalamouny);

                            //objlistRisk[VehicalIndex].IncludeZineraCost = true;
                        }

                    }
                    if (chkRadioLicence.Checked)
                    {
                        objRiskModel.IncludeRadioLicenseCost = true;
                    }
                    else
                    {
                        objRiskModel.IncludeRadioLicenseCost = false;
                    }


                }
            }
            else
            {
                pnlReZinara.Visible = false;
            }
        }
    }
}

