using DecontDbContext.Models;

namespace DocumentTotalProj
{
    public class DocumentTotal
    {
        public Document document { set; get; }
        public decimal total { set; get; }

        public DocumentTotal(Document document, decimal total)
        {
            this.document = document;
            this.total = total;
        }
    }
}
