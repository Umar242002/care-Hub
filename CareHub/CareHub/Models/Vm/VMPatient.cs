namespace CareHub.Models.Vm
{
    public class VMPatient
    {
        public int PerPage { get; set; }
        public int PageNo { get; set; }
        public int? TotalPages { get; set; } = 0;
        public int? TotalCount { get; set; } = 0;
        public string? PatName { get; set; }
        public string? Location { get; set; }
        public string? CreatedTo { get; set; }
        public string? CreatedFrom { get; set; }
        public bool? IsActive { get; set; } = null;
    }
    public class VMDoctor
    {
        public int PerPage { get; set; }
        public int PageNo { get; set; }
        public int? TotalPages { get; set; } = 0;
        public int? TotalCount { get; set; } = 0;
        public string? DocName { get; set; }
        public string? Location { get; set; }
        public string? CreatedTo { get; set; }
        public string? CreatedFrom { get; set; }
        public bool? IsActive { get; set; } = null;
    }
}
