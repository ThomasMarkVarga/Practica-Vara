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
            return "{\n" + this.companyCIF + "\n"
                + this.companyName + "\n"
                + this.companyAddress + "\n"
                + this.companyCounty + "\n"
                + this.companyPhone + "\n}";
        }
    }
}
