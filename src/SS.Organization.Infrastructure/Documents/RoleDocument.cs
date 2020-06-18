namespace SS.Organizations.Infrastructure.Documents.Organizations
{
    public class RoleDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleClaimsDocument[] Claims { get; set; }
    }
}
