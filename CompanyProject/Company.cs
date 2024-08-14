using System;

namespace CompanyProject
{
    public class Company
    {
        public string companyCIF { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyCounty { get; set; }
        public string companyPhone { get; set; }

        public override string ToString()
        {
            return "CIF: " + this.companyCIF +
                "\nName: " + this.companyName +
                "\nAddress: " + this.companyAddress +
                "\nCounty: " + this.companyCounty +
                "\nPhone: " + this.companyPhone;
        }
    }
}
