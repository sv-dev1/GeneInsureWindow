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

namespace Gene
{
    public partial class frmLicenceQuote : Form
    {
        ICEcashService IcServiceobj;
        ICEcashTokenResponse ObjToken;
        frmQuote objfrmQuote;
        string parternToken = "";
        public frmLicenceQuote()
        {
            ObjToken = new ICEcashTokenResponse();
            IcServiceobj = new ICEcashService();
            objfrmQuote = new frmQuote();


            InitializeComponent();
            this.Size = new System.Drawing.Size(1300, 720);
            PnlLicenceVrn.Visible = true;

            PnlLicenceVrn.Location = new Point(335, 100);
            PnlLicenceVrn.Size = new System.Drawing.Size(739, 238);
        }
        private void btnLicSave_Click(object sender, EventArgs e)
        {
            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            ObjToken = objfrmQuote.CheckParterTokenExpire();
            if (ObjToken != null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }

            List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
            //if (objlistRisk != null && objlistRisk.Count > 0)
            //{
            //foreach (var item in objlistRisk)
            //{
            obj.Add(new VehicleLicQuote
            {
                VRN = txtLicVrn.Text,
                IDNumber = "ABCDEFGHIJ1",
                ClientIDType="1",
                LicFrequency = "3"
            });
            //}
            //}

            ResultRootObject quoteresponse = IcServiceobj.LICQuote(obj, ObjToken.Response.PartnerToken);
            response.result = quoteresponse.Response.Result;
            if (response.result == 0)
            {
                response.message = quoteresponse.Response.Quotes[0].Message;
            }
            else
            {
                if (quoteresponse.Response.Quotes != null)
                {
                    List<VehicleLicQuoteUpdate> objLicQuoteUpdate = new List<VehicleLicQuoteUpdate>();
                    foreach (var item in quoteresponse.Response.Quotes.ToList())
                    {
                        objLicQuoteUpdate.Add(new VehicleLicQuoteUpdate
                        {
                            PaymentMethod = Convert.ToInt32("1"),
                            Status = "1",
                            DeliveryMethod = Convert.ToInt32("1"),
                            LicenceID = Convert.ToInt32(item.LicenceID)
                        });
                    }
                    ResultRootObject quoteresponseNew = IcServiceobj.LICQuoteUpdate(objLicQuoteUpdate, ObjToken.Response.PartnerToken);
                    response.result = quoteresponseNew.Response.Result;
                    if (response.result == 0)
                    {
                        response.message = quoteresponse.Response.Quotes[0].Message;
                    }

                    else
                    {
                        if (quoteresponse.Response.Quotes != null)
                        {
                            var LicenceID = quoteresponse.Response.Quotes[0].LicenceID;
                            ResultRootObject quoteresponseResult = IcServiceobj.LICResult(LicenceID,ObjToken.Response.PartnerToken);
                        }
                    }

                }
            }
        }
    }
}
