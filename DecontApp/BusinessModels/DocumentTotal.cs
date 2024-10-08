namespace BusinessModels
{
    public class DocumentTotal
    {
        public Document document {  get; set; }
        public decimal total { get; set; }

        public DocumentTotal(Document document, decimal total)
        {
            this.document = document;
            this.total = total;
        }
    }
}
