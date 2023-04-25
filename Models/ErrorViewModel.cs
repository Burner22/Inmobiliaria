namespace Inmobiliaria2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		public Exception? Error { get; internal set; }
	}
}