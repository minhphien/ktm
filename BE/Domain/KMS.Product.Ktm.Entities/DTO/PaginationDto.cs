namespace KMS.Product.Ktm.Entities.DTO
{
    public abstract class PaginationDto
    {
        public int Total { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
