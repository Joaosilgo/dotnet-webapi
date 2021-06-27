namespace dotnet_webapi.Models
{
    public class EventParameters
    {
        const int max_PageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > max_PageSize) ? max_PageSize : value;
            }
        }
    }
}