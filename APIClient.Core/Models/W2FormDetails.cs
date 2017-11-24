using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class W2FormDetails
    {
        #region Form Details
        /// <summary>
        /// Total taxable wages or salary, before any payroll deductions, that was paid to the employee during the year.
        /// </summary>
        [DataMember]
        public decimal Line1 { get; set; }
        /// <summary>
        /// Total federal income tax withheld from the employee's wages for the year. Include the 20% excise tax withheld on excess parachute payments.
        /// </summary>
        [DataMember]
        public decimal Line2 { get; set; }
        /// <summary>
        /// Total wages paid (before payroll deductions) subject to employee social security tax. 
        /// </summary>
        [DataMember]
        public decimal Line3 { get; set; }
        /// <summary>
        /// Total employee social security tax (not your share) withheld, including social security tax on tips.
        /// </summary>
        [DataMember]
        public decimal Line4 { get; set; }
        /// <summary>
        /// Medicare wages and tips reported. The wages and tips subject to Medicare tax are the same as those subject to social security tax except that there is no wage base limit for Medicare tax.
        /// </summary>
        [DataMember]
        public decimal Line5 { get; set; }
        /// <summary>
        /// Total employee Medicare tax (not your share) withheld.
        /// </summary>
        [DataMember]
        public decimal Line6 { get; set; }
        /// <summary>
        /// Tips that the employee reported to you even if you did not have enough employee funds to collect the social security tax for the tips.
        /// </summary>
        [DataMember]
        public decimal Line7 { get; set; }
        /// <summary>
        /// If you are a food or beverage establishment, show the tips allocated to the employee.
        /// </summary>
        [DataMember]
        public decimal Line8 { get; set; }

        /// <summary>
        /// 16 digit verification code given by the IRS to the Payroll Service Providers (PSPs) 
        /// </summary>
        [DataMember]
        public string Line9 { get; set; }
        /// <summary>
        /// Total amount of dependent care benefits that you paid or was incurred by you for your employee.
        /// </summary>
        [DataMember]
        public decimal Line10 { get; set; }
        /// <summary>
        /// Report distributions to an employee from a nonqualified plan
        /// </summary>
        [DataMember]
        public decimal Line11NonqualifiedPlan { get; set; }
        /// <summary>
        /// Report distributions to an employee from a nongovernmental section 457(b) plan
        /// </summary>
        [DataMember]
        public decimal Line11NonSectionPlan { get; set; }
        /// <summary>
        /// Line 12a Code
        /// </summary>
        [DataMember]
        public string Line12aCd { get; set; }
        /// <summary>
        /// Line 12a Amount
        /// </summary>
        [DataMember]
        public decimal Line12aAmt { get; set; }
        /// <summary>
        /// Line 12b Code
        /// </summary>
        [DataMember]
        public string Line12bCd { get; set; }
        /// <summary>
        /// Line 12b Amount
        /// </summary>
        [DataMember]
        public decimal Line12bAmt { get; set; }
        /// <summary>
        /// Line 12c Code
        /// </summary>
        [DataMember]
        public string Line12cCd { get; set; }

        /// <summary>
        /// Line 12c Amount
        /// </summary>
        [DataMember]
        public decimal Line12cAmt { get; set; }
        /// <summary>
        /// Line 12d Code
        /// </summary>
        [DataMember]
        public string Line12dCd { get; set; }

        /// <summary>
        /// Line 12d Amount
        /// </summary>
        [DataMember]
        public decimal Line12dAmt { get; set; }
        /// <summary>
        /// Statutory employees whose earnings are subject to social security and Medicare taxes but not subject to federal income tax withholding. Statutory employees are usually salespeople or other employees who work on commission. This will reflect whether the employee is a Statutory employee or not
        /// </summary>
        [DataMember]
        public bool Line13IsStatutoryEmployee { get; set; }
        /// <summary>
        /// This will show whether the employee is a member of a retirement plan or not.
        /// </summary>
        [DataMember]
        public bool Line13IsRetirementPlan { get; set; }
        /// <summary>
        /// This will show whether your business is reporting third-party sick pay or not
        /// </summary>
        [DataMember]
        public bool Line13IsThirdPartySickpay { get; set; }
        /// <summary>
        /// Any additional information of benefits the employer wants the employee to have and cannot be reported in boxes 1-13 can be listed here. Examples would be auto allowance, travel reimbursement, relocation expenses, job uniforms, after-tax employee contributions to an HSA, etc.
        /// </summary>
        [DataMember]
        public string Line14 { get; set; }

        #region State Details

        [DataMember]
        public string Line15State1Cd { get; set; }

        /// <summary>
        /// State 1 - Employer’s state ID number
        /// </summary>
        [DataMember]
        public string Line15State1IDNumber { get; set; }

        /// <summary>
        /// State 1 - Wages, tips, etc
        /// </summary>
        [DataMember]
        public decimal Line16State1Wages { get; set; }

        /// <summary>
        /// State 1 - Income Tax
        /// </summary>
        [DataMember]
        public decimal Line17State1Tax { get; set; }

        /// <summary>
        /// State 1 - Local wages, tips, etc
        /// </summary>
        [DataMember]
        public decimal Line18Local1Wages { get; set; }

        /// <summary>
        /// State 1 - Local income tax
        /// </summary>
        [DataMember]
        public decimal Line19Local1Tax { get; set; }

        /// <summary>
        /// State 1 - Locality name
        /// </summary>
        [DataMember]
        public string Line20Locality1Name { get; set; }

        //State 2
        [DataMember]
        public string Line15State2Cd { get; set; }

        /// <summary>
        /// State 2 - Employer’s state ID number
        /// </summary>
        [DataMember]
        public string Line15State2IdNumber { get; set; }

        /// <summary>
        /// State 2 - Wages, tips, etc
        /// </summary>
        [DataMember]
        public decimal Line16State2Wages { get; set; }

        /// <summary>
        /// State 2 - Income Tax
        /// </summary>
        [DataMember]
        public decimal Line17State2Tax { get; set; }

        /// <summary>
        /// State 2 - Local wages, tips, etc
        /// </summary>
        [DataMember]
        public decimal Line18Local2Wages { get; set; }

        /// <summary>
        /// State 2 - Local income tax
        /// </summary>
        [DataMember]
        public decimal Line19Local2Tax { get; set; }

        /// <summary>
        /// State 2 - Locality name
        /// </summary>
        [DataMember]
        public string Line20Locality2Name { get; set; }

        #endregion

        #endregion
    }
}
