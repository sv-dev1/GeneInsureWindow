using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using GensureAPIv2.Models;
using InsuranceClaim.Models;

namespace Gene
{


    public static class GeneApiClass
    {




    }
    public class MainGenic
    {
        Int32 Id;
        String Name;
    }
    public class RootObject
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }
    public class VehUsageObject
    {
        public int Id { get; set; }
        public string VehUsage { get; set; }
    }
    public class MakeObject
    {
        public int Id { get; set; }
        public string MakeDescription { get; set; }
        public string MakeCode { get; set; }
    }
    public class Messages
    {
        public bool Suceess { get; set; }

        //public bool Error { get; set; }
    }
    public class VehicalMakeModel
    {
        public string make { get; set; }
        public string model { get; set; }
    }
    public class ModelObject
    {
        public string ModelDescription { get; set; }

        public string ModelCode { get; set; }
        public string id { get; set; }
    }

    public class VehicalModel
    {
        public int VehicalId { get; set; }
        public string VRN { get; set; }
    }

    public class CoverObject
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
    public class RadioLicenceAmount
    {
        public int RadioLicenceAmounts { get; set; }
    }

    public class GetAllCities
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }

    public class VehicleDetails
    {
        public int vehicleUsageId { get; set; }
        public decimal sumInsured { get; set; }
        public int coverType { get; set; }
        public int excessType { get; set; } = 0;
        public decimal excess { get; set; } = 0.00m;
        public decimal? AddThirdPartyAmount { get; set; }
        public int NumberofPersons { get; set; }
        public Boolean Addthirdparty { get; set; }
        public Boolean PassengerAccidentCover { get; set; }
        public Boolean ExcessBuyBack { get; set; }
        public Boolean RoadsideAssistance { get; set; }
        public Boolean MedicalExpenses { get; set; }
        public decimal? RadioLicenseCost { get; set; }
        public Boolean IncludeRadioLicenseCost { get; set; }
        public int PaymentTermid { get; set; }
        public Boolean isVehicleRegisteredonICEcash { get; set; }
        public string BasicPremiumICEcash { get; set; }
        public string StampDutyICEcash { get; set; }
        public string ZTSCLevyICEcash { get; set; }
        public int ProductId { get; set; } = 0;

    }

    public class QuoteLogic
    {
        public decimal Premium { get; set; }
        public decimal StamDuty { get; set; }
        public decimal ZtscLevy { get; set; }
        public bool Status { get; set; } = true;
        public string Message { get; set; }
        public decimal ExcessBuyBackAmount { get; set; }
        public decimal RoadsideAssistanceAmount { get; set; }
        public decimal MedicalExpensesAmount { get; set; }
        public decimal PassengerAccidentCoverAmount { get; set; }
        public decimal PassengerAccidentCoverAmountPerPerson { get; set; }
        public decimal ExcessBuyBackPercentage { get; set; }
        public decimal RoadsideAssistancePercentage { get; set; }
        public decimal MedicalExpensesPercentage { get; set; }
        public decimal ExcessAmount { get; set; }
        public decimal AnnualRiskPremium { get; set; }
        public decimal TermlyRiskPremium { get; set; }
        public decimal QuaterlyRiskPremium { get; set; }
        public decimal Discount { get; set; }
    }

    public class ProductIdModel
    {
        public int ProductId { get; set; }
    }
    public class EmailModel
    {
        public string EmailAddress { get; set; }
    }

    public class PaymentInfoModel
    {
        public long TransactionId { get; set; }
    }
    public class CustomerVehicalModel
    {
        public CustomerVehicalModel()
        {
            CustomerModel = new CustomerModel();
            PolicyDetail = new PolicyDetail();
            RiskDetailModel = new List<RiskDetailModel>();
            SummaryDetailModel = new SummaryDetailModel();
        }
        public CustomerModel CustomerModel { get; set; }   // from model
        public PolicyDetail PolicyDetail { get; set; }    // from Entity  
        public List<RiskDetailModel> RiskDetailModel { get; set; }
        public SummaryDetailModel SummaryDetailModel { get; set; }

    }
    //6 JAn Ds
    public class CustomerREVehicalModel
    {
        public CustomerREVehicalModel()
        {
            CustomerModel = new CustomerModel();
            PolicyDetail = new PolicyDetail();
            RiskDetailModel = new RiskDetailModel();
            SummaryDetailModel = new SummaryDetailModel();
        }
        public CustomerModel CustomerModel { get; set; }   // from model
        public PolicyDetail PolicyDetail { get; set; }    // from Entity  
        public RiskDetailModel RiskDetailModel { get; set; }
        public SummaryDetailModel SummaryDetailModel { get; set; }

    }
    public class PaymentResult
    {
        public string TransactionId { get; set; }
        public string ActionCode { get; set; }
    }

    public class VehicleUpdateModel
    {
        public string SummaryId { get; set; }
        public string InsuranceStatus { get; set; }
      
    }

    public class vehicledetailModel
    {
        public int ProductId { get; set; }

        public int PaymentTermId { get; set; }
    }
}
