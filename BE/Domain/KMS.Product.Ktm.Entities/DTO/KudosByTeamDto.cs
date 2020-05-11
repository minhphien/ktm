namespace KMS.Product.Ktm.Entities.DTO
{
    public class KudosByTeamDto : TeamDto
    {
        public EmployeeInfoDto Employee { get; set; }
        public KudosSummaryInfoDto SentKudos { get; set; }
        public KudosSummaryInfoDto ReceivedKudos { get; set; }
    }
}
