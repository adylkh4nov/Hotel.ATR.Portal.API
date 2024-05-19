namespace WebAppAPI.Models
{
	public class Response
	{
		public string Status { get; set; }
		public string Message { get; set; }


		public Response(string Status, string Message)
		{
			this.Status = Status;
			this.Message = Message;
		}
    }

	
}
