using System;

namespace MessageAPIObjectProject
{
    public class MessageObjectAPI
    {
        public StatusCode status { get; set; }

        public string SuccessMessage { get; set; }
        public string WarningMessage { get; set; }
        public string ErrorMessage { get; set; }

        public MessageObjectAPI()
        {
            this.status = StatusCode.OK;
            this.SuccessMessage = "";
            this.WarningMessage = "";
            this.ErrorMessage = "";
        }

    }
    public enum StatusCode { OK = 1, NotFound = 2, NoContent = 3, BadRequest = 4}
}
