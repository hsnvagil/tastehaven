namespace Taste_Haven_API.Models.Dto;

public class OrderHeaderResponseDto
{
    public IEnumerable<OrderHeader> OrderHeaders { get; set; }
    public int TotalRecords { get; set; }
}