namespace XUnitCompleteExample.Identity.Models
{
    public class SuccessBody<T> : ApiResponseBody
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public List<T> Data { get; set; } = new();


        public SuccessBody()
        {
        }

        public SuccessBody(T item)
        {
            AddData(item);
        }
        public SuccessBody(List<T> itemsList)
        {
            AddData(itemsList);
        }

        public void AddData(T item)
        {
            Data.Add(item);
        }

        public void AddData(List<T> itemsList)
        {
            Data.AddRange(itemsList);
        }
    }
}
