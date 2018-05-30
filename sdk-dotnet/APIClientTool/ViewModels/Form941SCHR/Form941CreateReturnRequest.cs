using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIClientTool.ViewModels.Form941CoreModel;

namespace APIClientTool.ViewModels.Form941SCHR
{
    public class Form941CreateReturnRequest
    {
        public List<Form941SchRRecords> Form941SchRRecords { get; set; }
        public string SubmissionId { get; set; }
    }
    
    public class USAddress
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object State { get; set; }
        public object ZipCd { get; set; }
    }

    public class ForeignAddress
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object ProvinceOrStateNm { get; set; }
        public string Country { get; set; }
        public object PostalCd { get; set; }
    }

    public class Business
    {
        public object BusinessId { get; set; }
        public object BusinessNm { get; set; }
        public object TradeNm { get; set; }
        public bool IsEIN { get; set; }
        public object EINorSSN { get; set; }
        public object Email { get; set; }
        public object ContactNm { get; set; }
        public object Phone { get; set; }
        public object PhoneExtn { get; set; }
        public object Fax { get; set; }
        public object BusinessType { get; set; }
        public ViewModels.Form941CoreModel.SigningAuthority SigningAuthority { get; set; }
        public object KindOfEmployer { get; set; }
        public object KindOfPayer { get; set; }
        public bool IsBusinessTerminated { get; set; }
        public bool IsForeign { get; set; }
        public USAddress USAddress { get; set; }
        public ForeignAddress ForeignAddress { get; set; }
    }

    public class ThirdPartyDesignee
    {
        public object Name { get; set; }
        public object Phone { get; set; }
        public object PIN { get; set; }
    }

    public class OnlineSignaturePIN
    {
        public object PIN { get; set; }
    }

    public class ReportingAgentPIN
    {
        public object PIN { get; set; }
    }

    public class SignatureDetails
    {
        public string SignatureType { get; set; }
        public OnlineSignaturePIN OnlineSignaturePIN { get; set; }
        public ReportingAgentPIN ReportingAgentPIN { get; set; }
        public object Form8453EMP { get; set; }
    }

    public class USAddress2
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object State { get; set; }
        public object ZipCd { get; set; }
    }

    public class ForeignAddress2
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object ProvinceOrStateNm { get; set; }
        public object Country { get; set; }
        public object PostalCd { get; set; }
    }

    public class BusinessClosedDetails
    {
        public object Name { get; set; }
        public object FinalDateWagesPaid { get; set; }
        public bool IsForeign { get; set; }
        public USAddress2 USAddress { get; set; }
        public ForeignAddress2 ForeignAddress { get; set; }
    }

    public class USAddress3
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object State { get; set; }
        public object ZipCd { get; set; }
    }

    public class ForeignAddress3
    {
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object City { get; set; }
        public object ProvinceOrStateNm { get; set; }
        public object Country { get; set; }
        public object PostalCd { get; set; }
    }

    public class BusinessTransferredDetails
    {
        public object Name { get; set; }
        public object BusinessChangeType { get; set; }
        public object DateOfChange { get; set; }
        public object NewBusinessType { get; set; }
        public object NewBusinessName { get; set; }
        public bool IsForeign { get; set; }
        public USAddress3 USAddress { get; set; }
        public ForeignAddress3 ForeignAddress { get; set; }
    }

    public class BusinessStatusDetails
    {
        public bool IsBusinessClosed { get; set; }
        public BusinessClosedDetails BusinessClosedDetails { get; set; }
        public bool IsBusinessTransferred { get; set; }
        public BusinessTransferredDetails BusinessTransferredDetails { get; set; }
        public bool IsSeasonalEmployer { get; set; }
    }

    public class ReturnHeader
    {
        public object ReturnType { get; set; }
        public bool MoreClients { get; set; }
        public object TaxYr { get; set; }
        public object Qtr { get; set; }
        public Business Business { get; set; }
        public bool IsThirdPartyDesignee { get; set; }
        public ThirdPartyDesignee ThirdPartyDesignee { get; set; }
        public SignatureDetails SignatureDetails { get; set; }
        public BusinessStatusDetails BusinessStatusDetails { get; set; }
    }

    public class Form8974IncomeTaxDetails
    {
        public object IncomeTaxPeriodEndDate { get; set; }
        public object IncomeTaxReturnFiledForm { get; set; }
        public object IncomeTaxReturnFiledDate { get; set; }
        public object Form6765EIN { get; set; }
        public double Form6765Line44Amt { get; set; }
        public double PreviousPeriodRemaingCreditAmt { get; set; }
        public double RemainingCredit { get; set; }
    }

    public class Form8974
    {
        public List<Form8974IncomeTaxDetails> Form8974IncomeTaxDetails { get; set; }
        public object Line7 { get; set; }
        public object Line8 { get; set; }
        public object Line9 { get; set; }
        public object Line10 { get; set; }
        public object Line11 { get; set; }
        public object PayerIndicatorType { get; set; }
        public object Line12 { get; set; }
    }

    public class Form941MainFilerData
    {
        public double WagesAmt { get; set; }
        public double FedIncomeTaxWHAmt { get; set; }
        public double TotSSMdcrTaxAmt { get; set; }
        public double TaxOnUnreportedTips3121qAmt { get; set; }
        public double PayrollTaxCreditAmt { get; set; }
        public double TotTaxAmt { get; set; }
        public bool IsPayrollTaxCredit { get; set; }
        public Form8974 Form8974 { get; set; }
        public double TotTaxDepositAmt { get; set; }
    }

    public class AggregateForm941Data
    {
        public int EmployeeCnt { get; set; }
        public double WagesAmt { get; set; }
        public double FedIncomeTaxWHAmt { get; set; }
        public bool WagesNotSubjToSSMedcrTaxInd { get; set; }
        public double SocialSecurityTaxCashWagesAmt_Col1 { get; set; }
        public double TaxableSocSecTipsAmt_Col1 { get; set; }
        public double TaxableMedicareWagesTipsAmt_Col1 { get; set; }
        public double TxblWageTipsSubjAddnlMedcrAmt_Col1 { get; set; }
        public double SocialSecurityTaxAmt_Col2 { get; set; }
        public double TaxOnSocialSecurityTipsAmt_Col2 { get; set; }
        public double TaxOnMedicareWagesTipsAmt_Col2 { get; set; }
        public double TaxOnWageTipsSubjAddnlMedcrAmt_Col2 { get; set; }
        public double TotSSMdcrTaxAmt { get; set; }
        public double TaxOnUnreportedTips3121qAmt { get; set; }
        public double TotalTaxBeforeAdjustmentAmt { get; set; }
        public double CurrentQtrFractionsCentsAmt { get; set; }
        public double CurrentQuarterSickPaymentAmt { get; set; }
        public double CurrQtrTipGrpTermLifeInsAdjAmt { get; set; }
        public double TotalTaxAfterAdjustmentAmt { get; set; }
        public double PayrollTaxCreditAmt { get; set; }
        public double TotTaxAmt { get; set; }
        public double TotTaxDepositAmt { get; set; }
        public double BalanceDueAmt { get; set; }
        public double OverpaidAmt { get; set; }
        public object OverPaymentRecoveryType { get; set; }
        public string FilerType { get; set; }
    }

    public class IRSPayment
    {
        public object BankRoutingNum { get; set; }
        public object AccountType { get; set; }
        public object BankAccountNum { get; set; }
        public object Phone { get; set; }
    }

    public class MonthlyDepositor
    {
        public double TaxLiabilityMonth1 { get; set; }
        public double TaxLiabilityMonth2 { get; set; }
        public double TaxLiabilityMonth3 { get; set; }
    }

    public class DepositScheduleType
    {
        public string DepositorType { get; set; }
        public MonthlyDepositor MonthlyDepositor { get; set; }
        public object SemiWeeklyDepositor { get; set; }
        public double TotalQuarterTaxLiabilityAmt { get; set; }
    }

    public class ReturnData
    {
        public Form941MainFilerData Form941MainFilerData { get; set; }
        public object Form941AddClientDetails { get; set; }
        public object Form941UpdateClientDetails { get; set; }
        public object Form941DeleteClientDetails { get; set; }
        public AggregateForm941Data AggregateForm941Data { get; set; }
        public object IRSPaymentType { get; set; }
        public IRSPayment IRSPayment { get; set; }
        public DepositScheduleType DepositScheduleType { get; set; }
    }

    public class Form941SchRRecords
    {
        public object Sequence { get; set; }
        public ReturnHeader ReturnHeader { get; set; }
        public ReturnData ReturnData { get; set; }
        public object RecordId { get; set; }
    }
    
}