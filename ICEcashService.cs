﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;
//using Insurance.Domain;
using GensureAPIv2.Models;
using RestSharp;
using Gene;

namespace Insurance.Service
{
    public class ICEcashService
    {

        //tEST SANDBOX urL 
        public static string PSK = "127782435202916376850511";
        public static string SandboxIceCashApi = "http://api-test.icecash.com/request/20523588";

        // Live url
        //public static string PSK = "565205790573235453203546";
        //public static string LiveIceCashApi = "https://api.icecash.co.zw/request/20350763";

        private static string GetSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);
            SHA512Managed hashString = new SHA512Managed();
            string encodedData = Convert.ToBase64String(message);
            string hex = "";
            hashValue = hashString.ComputeHash(UE.GetBytes(encodedData));
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public ICEcashTokenResponse getToken()
        {

            ICEcashTokenResponse json = null;
            try
            {
                string _json = "";
                Arguments objArg = new Arguments();
                objArg.PartnerReference = Guid.NewGuid().ToString();
                objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
                objArg.Version = "2.0";
                objArg.Request = new FunctionObject { Function = "PartnerToken" };

                _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

                //string  = json.Reverse()
                string reversejsonString = new string(_json.Reverse().ToArray());
                string reversepartneridString = new string(PSK.Reverse().ToArray());

                string concatinatedString = reversejsonString + reversepartneridString;

                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

                string GetSHA512encrypted = SHA512(returnValue);

                string MAC = "";

                for (int i = 0; i < 16; i++)
                {
                    MAC += GetSHA512encrypted.Substring((i * 8), 1);
                }

                MAC = MAC.ToUpper();


                ICERootObject objroot = new ICERootObject();
                objroot.Arguments = objArg;
                objroot.MAC = MAC;
                objroot.Mode = "SH";

                var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);


                JObject jsonobject = JObject.Parse(data);

                var client = new RestClient(SandboxIceCashApi);
                //  var client = new RestClient(LiveIceCashApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                json = JsonConvert.DeserializeObject<ICEcashTokenResponse>(response.Content);
            }
            catch (Exception ex)
            {
                json = new ICEcashTokenResponse() { Date = "", PartnerReference = "", Version = "", Response = new TokenReposone() { Result = "0", Message = "A Connection Error Occured ! Please add manually", ExpireDate = "", Function = "", PartnerToken = "" } };
            }

            return json;
        }

        public ResultRootObject checkVehicleExistsWithVRN(string RegistrationNo, string PartnerToken, string PartnerReference)
        {

            //  PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleObjectVRN> obj = new List<VehicleObjectVRN>();
            obj.Add(new VehicleObjectVRN { VRN = RegistrationNo });


            QuoteArgumentsVRN objArg = new QuoteArgumentsVRN();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new QuoteFunctionObjectVRN { Function = "TPIQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();


            ICEQuoteRequestVRN objroot = new ICEQuoteRequestVRN();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //var client = new RestClient("http://api-test.icecash.com/request/20523588");


            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            return json;
        }


        public ResultRootObject RequestQuote(string PartnerToken, string RegistrationNo, string suminsured, string make, string model, int PaymentTermId, int VehicleYear, int CoverTypeId, int VehicleUsage, string PartnerReference, CustomerModel CustomerInfo)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";
            make = RemoveSpecialChars(make);
            model = RemoveSpecialChars(model);

            List<VehicleObject> obj = new List<VehicleObject>();

            //foreach (var item in listofvehicles)
            //{

            //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = "", Town = CustomerInfo.City, Address1 = CustomerInfo.AddressLine1, Address2 = CustomerInfo.AddressLine2, CompanyName = "", FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, IDNumber = CustomerInfo.NationalIdentificationNumber, MSISDN = CustomerInfo.CountryCode + CustomerInfo.PhoneNumber });


            //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = ""  });


            obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths =Convert.ToString (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToString(suminsured), YearManufacture = Convert.ToString(VehicleYear), InsuranceType = Convert.ToString(CoverTypeId), VehicleType = Convert.ToString(VehicleUsage), TaxClass =Convert.ToString("1"), Make = make, Model = model, EntityType = "" });


            //}

            QuoteArguments objArg = new QuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString(); ;
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new QuoteFunctionObject { Function = "TPIQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequest objroot = new ICEQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            // var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);




            //if (json.Response.Quotes != null && json.Response.Quotes.Count > 0)
            //{
            //    if (json.Response.Quotes[0].Policy != null)
            //    {
            //        var Setting = InsuranceContext.Settings.All();
            //        var DiscountOnRenewalSettings = Setting.Where(x => x.keyname == "Discount On Renewal").FirstOrDefault();
            //        var premium = Convert.ToDecimal(json.Response.Quotes[0].Policy.CoverAmount);
            //    }
            //}


            return json;
        }

        public ResultRootObject TPILICQuote(List<VehicleLicQuote> listVehicleLic, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicInsuraceObject> obj = new List<VehicleLicInsuraceObject>();
            //  var CustomerInfo = (CustomerModel)HttpContext.Current.Session["CustomerDataModal"];

            var CustomerInfo = new CustomerModel();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicInsuraceObject
                {
                    VRN = item.VRN,
                    //EntityType= "",
                    ClientIDType = item.ClientIDType,
                    IDNumber = item.IDNumber,
                    DurationMonths = item.DurationMonths,
                    LicFrequency = item.LicFrequency,
                    RadioTVUsage = item.RadioTVUsage,
                    RadioTVFrequency = item.RadioTVFrequency
                });
            }

            LICInsuranceQuoteArguments objArg = new LICInsuranceQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICInsuranceQuoteFunctionObject { Function = "TPILICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICInsuranceQuoteRequest objroot = new LICInsuranceQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            return json;

        }

        public ResultRootObject LICQuote(List<VehicleLicQuote> listVehicleLic, string PartnerToken)
        {

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicInsuraceObject> obj = new List<VehicleLicInsuraceObject>();

            var CustomerInfo = new CustomerModel();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicInsuraceObject
                {
                    VRN = item.VRN,
                    //EntityType= "",
                    ClientIDType = item.ClientIDType,
                    IDNumber = item.IDNumber,
                    DurationMonths = item.DurationMonths,
                    LicFrequency = item.LicFrequency,
                    RadioTVUsage = item.RadioTVUsage,
                    RadioTVFrequency = item.RadioTVFrequency
                });
            }

            LICInsuranceQuoteArguments objArg = new LICInsuranceQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICInsuranceQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICInsuranceQuoteRequest objroot = new LICInsuranceQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            return json;

        }
        public ResultRootObject LICQuoteUpdate(List<VehicleLicQuoteUpdate> listVehicleLic, string PartnerToken)
        {

            //string PSK = "127782435202916376850511";
            string _json = "";
           
            List<VehicleLicUpdateObject> obj = new List<VehicleLicUpdateObject>();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicUpdateObject
                {               
                    LicenceID =Convert.ToString(item.LicenceID),
                    DeliveryMethod = Convert.ToString(item.DeliveryMethod),
                    Status = item.Status,
                    PaymentMethod = Convert.ToString(item.PaymentMethod)   
                });
            }

            LICQuoteUpdateArguments objArg = new LICQuoteUpdateArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICQuoteUpdateFunctionObject { Function = "LicQuoteUpdate", Vehicles = obj};

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteUpdateRequest objroot = new LICQuoteUpdateRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            return json;
        }


        public ResultRootObject LICResult(string LicenceID, string PartnerToken)
        {
            string _json = "";
            //List<VehicleLicUpdateObject> obj = new List<VehicleLicUpdateObject>();

            LICResultArguments objArg = new LICResultArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICResultFunctionObject { Function = "LICResult", LicenceID = LicenceID };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICResultRequest objroot = new LICResultRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            return json;
        }

        public static ResultRootObject TPIQuoteUpdate(string Phonenumber, RiskDetailModel vehicleDetails, string PartnerToken, int? paymentMethod)
        {
            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;

            List<QuoteDetial> qut = new List<QuoteDetial>();

            qut.Add(new QuoteDetial { InsuranceID = item.InsuranceId, Status = "1" });

            var quotesDetial = new RequestTPIQuoteUpdate { Function = "TPIQuoteUpdate", PaymentMethod = Convert.ToString("1"), Identifier = "1", MSISDN = "01" + Phonenumber, Quotes = qut };

            QuoteArgumentsTPIQuote objArg = new QuoteArgumentsTPIQuote();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = quotesDetial;



            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPIQuote objroot = new ICEQuoteRequestTPIQuote();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //var client = new RestClient(LiveIceCashApi);
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            return json;
        }

        public static ResultRootObject TPIPolicy(RiskDetailModel vehicleDetails, string PartnerToken)
        {

            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;
            TPIPolicyDetial qut = new TPIPolicyDetial { InsuranceID = item.InsuranceId, Function = "TPIPolicy" };

            QuoteArgumentsTPIPolicy objArg = new QuoteArgumentsTPIPolicy();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = qut;

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPIPolicy objroot = new ICEQuoteRequestTPIPolicy();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //  var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            return json;
        }

        public string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            // You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }



        //14-Feb-2019

        public ResultRootObject ZineraLICQuote(string RegistrationNo,string parternToken,string IDsNumber)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicObject> obj = new List<VehicleLicObject>();
            //var CustomerInfo = (CustomerModel)HttpContext.Current.Session["CustomerDataModal"];

            //foreach (var item in listofvehicles)
            //{
            obj.Add(new VehicleLicObject
            {
                VRN = RegistrationNo,
                IDNumber = IDsNumber,
                ClientIDType = "1",
                //FirstName = CustomerInfo.FirstName,
                //LastName = CustomerInfo.LastName,
                //Address1 = CustomerInfo.AddressLine1,
                //Address2 = CustomerInfo.AddressLine2,
                SuburbID = "2",
                LicFrequency = "3",
                RadioTVUsage = "",
                RadioTVFrequency = ""
            });
            //}

            LICQuoteArguments objArg = new LICQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = parternToken;
            objArg.Request = new LICQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteRequest objroot = new LICQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);
           
            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            return json;

        }


    }
    
    public class Arguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public FunctionObject Request { get; set; }
    }
    public class FunctionObject
    {
        public string Function { get; set; }
    }
    public class ICERootObject
    {
        public Arguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class VehicleObjectVRN
    {
        public string VRN { get; set; }

    }

    public class LICInsuranceQuoteRequest
    {
        public LICInsuranceQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class LICQuoteUpdateRequest
    {
        public LICQuoteUpdateArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class LICResultRequest
    {
        public LICResultArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class LICInsuranceQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICInsuranceQuoteFunctionObject Request { get; set; }
    }


    public class LICQuoteUpdateArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICQuoteUpdateFunctionObject Request { get; set; }
    }


    public class LICResultArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICResultFunctionObject Request { get; set; }
    }


    public class LICResultFunctionObject
    {
        public string Function { get; set; }
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public string LicenceID { get; set; }
    }


    public class LICQuoteUpdateFunctionObject
    {
        public string Function { get; set; }      
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public List<VehicleLicUpdateObject> Vehicles { get; set; }
    }



    public class LICInsuranceQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleLicInsuraceObject> Vehicles { get; set; }
    }


    public class VehicleLicUpdateObject
    {    
        public string LicenceID { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class VehicleLicInsuraceObject
    {
        public string VRN { get; set; }
        public string EntityType { get; set; }
        public string ClientIDType { get; set; }
        public string IDNumber { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }


        public string SuburbID { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }
        public string VehicleValue { get; set; }

        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }
        public string NettMass { get; set; }
        public string LicExpiryDate { get; set; }
        public string TransactionAmt { get; set; }
        public string ArrearsAmt { get; set; }
        public string PenaltiesAmt { get; set; }
        public string AdministrationAmt { get; set; }
        public string TotalLicAmt { get; set; }
        public string RadioTVAmt { get; set; }
        public string RadioTVArrearsAmt { get; set; }
        public string TotalRadioTVAmt { get; set; }

        public string TotalAmount { get; set; }
    }

    public class VehicleObjectWithNullable
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
        public string DurationMonths { get; set; }
        public string VehicleValue { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
    }

    public class QuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public QuoteFunctionObject Request { get; set; }
    }

    public class QuoteArgumentsVRN
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public QuoteFunctionObjectVRN Request { get; set; }
    }



    public class QuoteFunctionObjectVRN
    {
        public string Function { get; set; }
        public List<VehicleObjectVRN> Vehicles { get; set; }
    }
    public class QuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleObject> Vehicles { get; set; }
    }
    public class ICEQuoteRequest
    {
        public QuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class ICEQuoteRequestVRN
    {
        public QuoteArgumentsVRN Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class TokenReposone
    {
        public string Function { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public string PartnerToken { get; set; }
        public string ExpireDate { get; set; }
    }
    public class ICEcashTokenResponse
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public TokenReposone Response { get; set; }

        public Quote Quotes { get; set; }
    }
    public class Quote
    {
        public string VRN { get; set; }
        public string InsuranceID { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
    }
    public class QuoteResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public List<Quote> Quotes { get; set; }
    }
    public class ICEcashQuoteResponse
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public QuoteResponse Response { get; set; }
    }

    public class ResultClient
    {
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
    }

    public class ResultPolicy
    {
        public string InsuranceType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DurationMonths { get; set; }
        public string Amount { get; set; }
        public string StampDuty { get; set; }
        public string GovernmentLevy { get; set; }
        public string CoverAmount { get; set; }
        public string PremiumAmount { get; set; }
    }
    public class ResultVehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
        public int VehicleType { get; set; }
        public string VehicleValue { get; set; }
    }
    public class ResultQuote
    {
        public string VRN { get; set; }
        public string InsuranceID { get; set; }
        public int Result { get; set; }
        public string LicenceID { get; set; }
        public string CombinedID { get; set; }
        public string Message { get; set; }
        public decimal TotalLicAmt { get; set; }
        public decimal PenaltiesAmt { get; set; }
        public decimal RadioTVAmt { get; set; }
        public ResultPolicy Policy { get; set; }
        public ResultClient Client { get; set; }
        public ResultVehicle Vehicle { get; set; }
        public VehicleLicInsuraceObject Licence { get; set; }
    }
    public class ResultResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public List<ResultQuote> Quotes { get; set; }
    }
    public class ResultRootObject
    {
        public decimal LoyaltyDiscount { get; set; }
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public ResultResponse Response { get; set; }
    }
    public class ResultRootObjects
    {
        public decimal LoyaltyDiscount { get; set; }
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public ResultResponse Response { get; set; }
    }


    public class RequestTPIQuoteUpdate
    {
        public string Function { get; set; }
        public string PaymentMethod { get; set; }
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public List<QuoteDetial> Quotes { get; set; }
    }
    public class QuoteDetial
    {
        public string InsuranceID { get; set; }

        public string Status { get; set; }

    }

    public class QuoteArgumentsTPIQuote
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public RequestTPIQuoteUpdate Request { get; set; }
    }

    public class ICEQuoteRequestTPIQuote
    {
        public QuoteArgumentsTPIQuote Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class checkVRNwithICEcashResponse
    {
        public int result { get; set; }
        public string message { get; set; }
        public ResultRootObject Data { get; set; }
    }

    public class VehicleObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
        //public int DurationMonths { get; set; }
        //public int VehicleValue { get; set; }
        //public int InsuranceType { get; set; }
        //public int VehicleType { get; set; }

        public string DurationMonths { get; set; }
        public string VehicleValue { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }

        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
        //public int TaxClass { get; set; }
        //public int YearManufacture { get; set; }
    }

    public class ICEQuoteRequestTPIPolicy
    {
        public QuoteArgumentsTPIPolicy Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class TPIPolicyDetial
    {
        public string InsuranceID { get; set; }
        public string Function { get; set; }
    }

    public class QuoteArgumentsTPIPolicy
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public TPIPolicyDetial Request { get; set; }
    }
    public class VehicleLicObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string ClientIDType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SuburbID { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }

    }
    public class LICQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICQuoteFunctionObject Request { get; set; }
    }
    public class LICQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleLicObject> Vehicles { get; set; }
    }
    public class LICQuoteRequest
    {
        public LICQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

}
