using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Gene
{
    public partial class frmClaimRegister : Form
    {
        //static String ApiURL = "http://geneinsureclaim2.kindlebit.com/api/Claimant/";
        //static String ApiURLS = "http://geneinsureclaim2.kindlebit.com/api/Account/";


        static String ApiURL = "http://localhost:6220/api/Claimant/";
        static String ApiURLS = "http://localhost:6220/api/Account/";
        ClaimRegisterModel objClaimModel;
        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";
        public frmClaimRegister()
        {
            InitializeComponent();
            //this.Size = new System.Drawing.Size(1300, 720);
            this.Size = new System.Drawing.Size(1300, 720);
            pnlLogo.Location = new Point(this.Width - 320, this.Height - 220);
            pnlLogo.Size = new System.Drawing.Size(300, 220);

            pnlClaimNotification.Location = new Point(335, 33);
            pnlClaimNotification.Size = new System.Drawing.Size(1350, 1140);
            //ClaimCustomerDetail

            pnlClamantDetail.Location = new Point(335, 33);
            pnlClamantDetail.Size = new System.Drawing.Size(1350, 1140);
            //Msg
            PnlMsg.Location = new Point(335, 33);
            PnlMsg.Size = new System.Drawing.Size(1350, 1040);
            DisableClaimPanel();
            pnlClaimNotification.Visible = true;
            pnlClamantDetail.Visible = false;
            PnlMsg.Visible = false;
            bindMake();
            //PnlMsg

         

        }

        public void bindClaimDetail(string SearchText)
        {
            //ApiURL = "http://localhost:6220/api/Claimant/";
            var LocalApiURL = "http://localhost:6220/api/Claimant/";
            var client = new RestClient(LocalApiURL + "ClaimDetail?SearchText=" + SearchText);
            //var client = new RestClient(ApiURL + "ClaimDetail?SearchText=" + SearchText);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<ClaimRegisterModel>(response.Content);
            if (result != null)
            {
                if (result.PolicyNumber != null)
                {
                    EnableClaimPanel();
                    txtPolicyNo.Text = result.PolicyNumber;
                    txtVRNNo.Text = result.VRNNumber;
                    txtCustomerName.Text = result.CustomerName;
                    objClaimModel.UserId = result.UserId;
                    if (result.CoverStartDate != null)
                    {
                        txtStartDate.Text = Convert.ToString(result.CoverStartDate);
                    }
                    else
                    {
                        txtStartDate.Text = Convert.ToString(DateTime.Now);
                    }
                    if (result.CoverEndDate != null)
                    {
                        //txtEndDate.Text = Convert.ToString(Convert.ToDateTime(result.CoverEndDate).ToShortDateString());
                        txtEndDate.Text = Convert.ToString(result.CoverEndDate);
                    }
                    else
                    {
                        txtEndDate.Text = Convert.ToString(DateTime.Now);
                        
                    }
                }
                else
                {
                    MessageBox.Show("VRN/Policy No doesn't Exist.Please Try Again");
                    EmptyAlltext();
                    DisableClaimPanel();
                    return;
                }

            }
            else
            {
                MessageBox.Show("VRN/Policy No doesn't Exist.Please Try Again");
                DisableTextfield();
                return;
            }

            //cmbCoverType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }
        public void DisableClaimPanel()
        {
            lblPolicyNo.Visible = false;
            txtPolicyNo.Visible = false;
            lblVRNNumber.Visible = false;
            txtVRNNo.Visible = false;
            lblCustomerName.Visible = false;
            txtCustomerName.Visible = false;
            lblCoverStartDate.Visible = false;
            txtStartDate.Visible = false;
            lblCoverEndDate.Visible = false;
            txtEndDate.Visible = false;
            //btnCancel.Visible = false;
            //btnNext.Visible = false;
            lblformat.Visible = false;
            lblFormat1.Visible = false;
        }
        public void EnableClaimPanel()
        {
            lblPolicyNo.Visible = true;
            txtPolicyNo.Visible = true;
            lblVRNNumber.Visible = true;
            txtVRNNo.Visible = true;
            lblCustomerName.Visible = true;
            txtCustomerName.Visible = true;
            lblCoverStartDate.Visible = true;
            txtStartDate.Visible = true;
            lblCoverEndDate.Visible = true;
            txtEndDate.Visible = true;
            //btnCancel.Visible = true;
            //btnNext.Visible = true;
            lblformat.Visible = true;
            lblFormat1.Visible = true;
        }

        private void pnlClaimNotification_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmClaimRegister_Load(object sender, EventArgs e)
        {

        }

        private void txtSearchVrnPolicy_TextChanged(object sender, EventArgs e)
        {
            objClaimModel = new ClaimRegisterModel();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (txtSearchVrnPolicy.Text != "")
            {

                bindClaimDetail(txtSearchVrnPolicy.Text);
            }
            else
            {
                MessageBox.Show("Please Enter VRN/POLICY No.");
                return;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtPolicyNo.Text != "")
            {
                objClaimModel.PolicyNumber = txtPolicyNo.Text;
                objClaimModel.CoverStartDate = txtStartDate.Text;
                objClaimModel.CoverEndDate = txtEndDate.Text;
                objClaimModel.VRNNumber = txtVRNNo.Text;
                objClaimModel.CustomerName = txtCustomerName.Text;
                pnlClaimNotification.Visible = false;
                pnlClamantDetail.Visible = true;
                if (objClaimModel.ThirdPartyInvolvement == true)
                {
                    EndableTextField();
                }
                if (objClaimModel.ThirdPartyInvolvement == false || objClaimModel.ThirdPartyInvolvement == null)
                {
                    DisableTextfield();
                }
            }

            else
            {
                MessageBox.Show("Please Enter the Vrn/Policy No In Search Textbox ");
                return;
            }

            //DisableTextfield();

            //pnlClaimNotification.Visible = false;
            //pnlClamantDetail.Visible = false;
            //PnlMsg.Visible = true;
            //datePickDateOfLoss.CustomFormat = " ";
        }

        public void bindMake()
        {
            //ApiURLS = "http://geneinsureclaim2.kindlebit.com/api/Account/";
            var client = new RestClient(ApiURLS + "Makes");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<MakeObject>>(response.Content);
            comboMake.DataSource = result;
            comboMake.DisplayMember = "MakeDescription";
            comboMake.ValueMember = "makeCode";
            //cmbMake.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cmbMake.AutoCompleteSource = AutoCompleteSource.CustomSource;

            bindModel(Convert.ToString(comboMake.SelectedValue));

        }

        public void bindModel(String MaKECode)
        {
            //ApiURL = "http://localhost:6220/api/Account/";
            var client = new RestClient(ApiURLS + "Models?makeCode=" + MaKECode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<ModelObject>>(response.Content);
            comboModel.DataSource = result;
            comboModel.DisplayMember = "modeldescription";
            comboModel.ValueMember = "ModelCode";
            //cmbPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }
        public void DisableTextfield()
        {
            txtContactDetail.Visible = false;
            lblContactDetail.Visible = false;
            lblThirdPartyName.Visible = false;
            txtThirdPartyName.Visible = false;
            lblMake.Visible = false;
            comboMake.Visible = false;
            lblModel.Visible = false;
            comboModel.Visible = false;
            lblThirdPartyEstimatValOfLoss.Visible = false;
            txthirdPartyEstimatValOfLoss.Visible = false;
            lblThirdPartyDamageVal.Visible = false;
            txtThirdPartyDamageVal.Visible = false;
        }
        public void EndableTextField()
        {
            txtContactDetail.Visible = true;
            lblContactDetail.Visible = true;
            lblThirdPartyName.Visible = true;
            txtThirdPartyName.Visible = true;
            lblMake.Visible = true;
            comboMake.Visible = true;
            lblModel.Visible = true;
            comboModel.Visible = true;
            lblThirdPartyEstimatValOfLoss.Visible = true;
            txthirdPartyEstimatValOfLoss.Visible = true;
            lblThirdPartyDamageVal.Visible = true;
            txtThirdPartyDamageVal.Visible = true;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlClaimNotification.Visible = true;
            pnlClamantDetail.Visible = false;

        }


        public  void SaveClaimantDetail(ClaimRegisterModel ClaimModel)
        {
            bool successmsg = false;

            // loader display

            successmsg =  SavePolicy(ClaimModel);

            // loader hide

            if (successmsg == true)
            {
                pnlClaimNotification.Visible = false;
                pnlClamantDetail.Visible = false;
                PnlMsg.Visible = true;
                EmptyAlltext();
            }
            //Thread.Sleep(10000);   //1sec=1000ms

            //pnlClaimNotification.Visible = true;
            //pnlClamantDetail.Visible = false;
            //PnlMsg.Visible = false;
        }

        private  bool SavePolicy(ClaimRegisterModel ClaimModel)
        {
            try
            {
                var LocalIceCashReqUrl = "http://localhost:6220/api/Claimant/";
                var client = new RestClient(LocalIceCashReqUrl + "SaveClaimDetails");

                //var client = new RestClient(ApiURL + "SaveClaimDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", Pwd);
                request.AddHeader("username", username);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(ClaimModel);
                IRestResponse response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<Messages>(response.Content);
                return result.Suceess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void EmptyAlltext()
        {

            txtSearchVrnPolicy.Text = "";
            txtPolicyNo.Text = "";
            txtVRNNo.Text = "";
            txtCustomerName.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtClaimantName.Text = "";
            txtPlaceOfLoss.Text = "";
            datePickDateOfLoss.CustomFormat = " ";
            txtEstimatValofLoss.Text = "";
            txtDescriptionofloss.Text = "";
            radioButtonYes.Checked = false;
            radioButtonNo.Checked = false;
            txtContactDetail.Text = "";
            txtThirdPartyName.Text = "";
            txthirdPartyEstimatValOfLoss.Text = "";
            txtThirdPartyDamageVal.Text = "";
            bindMake();
            DisableClaimPanel();
        }






        private void pnlClamantDetail_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txthirdPartyEstimatValOfLoss_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtThirdPartyDamageVal_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblThirdPartyDamageVal_Click(object sender, EventArgs e)
        {

        }

        private void lblThirdPartyEstimatValOfLoss_Click(object sender, EventArgs e)
        {

        }

        private void txtMake_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblModel_Click(object sender, EventArgs e)
        {

        }

        private void lblMake_Click(object sender, EventArgs e)
        {

        }

        private void txtThirdPartyName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactDetail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblContactDetail_Click(object sender, EventArgs e)
        {

        }

        private void lblThirdPartyName_Click(object sender, EventArgs e)
        {

        }

        private void txtEstimatValofLoss_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            EndableTextField();
            objClaimModel.ThirdPartyInvolvement = true;
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            DisableTextfield();
            txtContactDetail.Text = "";
            txtThirdPartyName.Text = "";
            txthirdPartyEstimatValOfLoss.Text = "";
            txtThirdPartyDamageVal.Text = "";
            objClaimModel.ThirdPartyInvolvement = false;
        }

        private void lblThirdPartyInvolvement_Click(object sender, EventArgs e)
        {

        }

        private void lblEstimatedValueOfLoss_Click(object sender, EventArgs e)
        {

        }

        private void txtPlaceOfLoss_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDescriptionOfLoss_Click(object sender, EventArgs e)
        {

        }

        private void lblPlaceOfLoss_Click(object sender, EventArgs e)
        {

        }

        private void txtClaimantName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblClaimantName_Click(object sender, EventArgs e)
        {

        }

        private void lblDateOfLoss_Click(object sender, EventArgs e)
        {

        }

        private void datePickDateOfLoss_ValueChanged(object sender, EventArgs e)
        {
            datePickDateOfLoss.CustomFormat = "MM/dd/yyyy";




        }

        private void comboMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindModel(Convert.ToString(comboMake.SelectedValue));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Dateloss = datePickDateOfLoss.Text;
                var format = "MM/dd/yyyy";
                //var format = "dd/MM/yyyy";
                var GetStartDate = DateTime.ParseExact(txtStartDate.Text, format, CultureInfo.InvariantCulture);
                var GetEndDate = DateTime.ParseExact(txtEndDate.Text, format, CultureInfo.InvariantCulture);


                //DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
                //DateTime enddate = Convert.ToDateTime(txtEndDate.Text);


                //DateTime startdate = Convert.ToDateTime((GetStartDate).ToString("MM/dd/yyyy"));
                //DateTime enddate = Convert.ToDateTime((GetEndDate).ToString("MM/dd/yyyy"));


                DateTime startdate = GetStartDate;
                DateTime enddate = GetEndDate;

               // DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
               // DateTime enddate = Convert.ToDateTime(txtEndDate.Text);

                if (startdate!=null && enddate!=null)
                {
                    if (radioButtonYes.Checked == true || radioButtonNo.Checked == true)
                    {
                        if (Dateloss != " ")
                        {
                            if (Convert.ToDateTime(Dateloss) >= startdate)
                            {
                                if (enddate >= Convert.ToDateTime(Dateloss))
                                {
                                    //MessageBox.Show("Claim Is Approved");

                                }
                                else
                                {
                                    MessageBox.Show("Your date of loss doesn't exist in policy start/end date.");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Your date of loss doesn't exist in policy start/end date.");
                                return;
                            }
                            if (txtClaimantName.Text == "")
                            {
                                MessageBox.Show("Claimant name is required field");
                                return;
                            }
                            else
                            {
                                objClaimModel.ClaimantName = txtClaimantName.Text;
                            }
                            if (txtPlaceOfLoss.Text == "")
                            {
                                MessageBox.Show("Place of loss is required field");
                                return;
                            }
                            else
                            {
                                objClaimModel.PlaceOfLoss = txtPlaceOfLoss.Text;
                            }
                            if (txtEstimatValofLoss.Text == "")
                            {
                                MessageBox.Show("Estimated val of loss is required field");
                                return;
                            }
                            else
                            {
                                objClaimModel.EstimatedValueOfLoss = Convert.ToDecimal(txtEstimatValofLoss.Text);
                            }
                            if (txtDescriptionofloss.Text == "")
                            {
                                MessageBox.Show("Description of loss is required field");
                                return;
                            }
                            else
                            {
                                objClaimModel.DescriptionOfLoss = txtDescriptionofloss.Text;
                            }

                            objClaimModel.DateOfLoss = Convert.ToDateTime(datePickDateOfLoss.Text);
                            if (radioButtonYes.Checked)
                            {
                                objClaimModel.ThirdPartyInvolvement = true;
                                if (txtContactDetail.Text == "")
                                {
                                    MessageBox.Show("Contact detail is required field");
                                    return;
                                }
                                else
                                {
                                    objClaimModel.ThirdPartyContactDetails = txtContactDetail.Text;
                                }
                                if (txtThirdPartyName.Text == "")
                                {
                                    MessageBox.Show("Third party name is required field");
                                    return;
                                }
                                else
                                {
                                    objClaimModel.ThirdPartyName = txtThirdPartyName.Text;
                                }
                                if (txthirdPartyEstimatValOfLoss.Text == "")
                                {
                                    MessageBox.Show("Third party estimated val of loss is required field");
                                    return;
                                }
                                else
                                {
                                    objClaimModel.ThirdPartyEstimatedValueOfLoss = Convert.ToDecimal(txthirdPartyEstimatValOfLoss.Text);
                                }
                                if (txtThirdPartyDamageVal.Text == "")
                                {
                                    MessageBox.Show("Third party damage value is required field");
                                    return;
                                }
                                else
                                {
                                    objClaimModel.ThirdPartyDamageValue = Convert.ToDecimal(txtThirdPartyDamageVal.Text);
                                }
                                objClaimModel.ThirdPartyMakeId = Convert.ToString(comboMake.SelectedValue);
                                objClaimModel.ThirdPartyModelId = Convert.ToString(comboModel.SelectedValue);
                            }
                            else
                            {
                                objClaimModel.ThirdPartyInvolvement = false;

                            }

                            SaveClaimantDetail(objClaimModel);

                        }
                        else
                        {
                            if (radioButtonNo.Checked)
                            {
                                objClaimModel.ThirdPartyMakeId = "";
                                objClaimModel.ThirdPartyModelId = "";
                            }
                            MessageBox.Show("Please select date of loss");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter the required fields.");
                        return;
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Please try again");
                return;
            }
        }

        private void lblSuccesMsg_Click(object sender, EventArgs e)
        {

        }

        private void PnlMsg_Paint(object sender, PaintEventArgs e)
        {

            timerMessage.Enabled = true;
            timerMessage.Start();
        }
       
        private void timerMessage_Tick(object sender, EventArgs e)
        {
           
            Thread.Sleep(10000);
            timerMessage.Stop();
            pnlClaimNotification.Visible = false;
            pnlClamantDetail.Visible = false;
            PnlMsg.Visible = false;

            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
            //pnlClaimNotification[0-];

        }

        private void txtEstimatValofLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txthirdPartyEstimatValOfLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtThirdPartyDamageVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlClaimNotification.Visible = false;
            pnlClamantDetail.Visible = false;
            PnlMsg.Visible = false;
            EmptyAlltext();
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
