namespace CareHub.Models.CommonFunctions
{
    public class PaginationModel
    {
        public int PerPage { get; set; }
        public int PageNo { get; set; }//CurrentPage

        public int? TotalPages { get; set; }
        public int? TotalCount { get; set; }
        public dynamic? Data { get; set; }

    }
}
