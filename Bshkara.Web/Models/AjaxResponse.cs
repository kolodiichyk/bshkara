namespace Bshkara.Web.Models
{
    public class AjaxResponse
    {
        public AjaxResponse(string error = null)
        {
            Success = string.IsNullOrWhiteSpace(error);
            Error = error;
        }

        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}