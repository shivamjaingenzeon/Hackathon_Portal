namespace hackathonbackend.Models
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public bool IsCompany { get; set; }
        public T Data { get; set; }

       

      
    }

}