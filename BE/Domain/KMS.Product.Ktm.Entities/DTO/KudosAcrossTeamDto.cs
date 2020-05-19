namespace KMS.Product.Ktm.Entities.DTO
{
    public class KudosAcrossTeamDto
    {
        public TeamDto Team { get; set; }
        public KudosSummaryInfoDto SentKudos { get; set; }
        public KudosSummaryInfoDto ReceivedKudos { get; set; }
    }
}
