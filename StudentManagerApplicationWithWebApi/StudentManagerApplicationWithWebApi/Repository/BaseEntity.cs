using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace StudentManagerApplicationWithWebApi.Repository
{
    public class BaseEntity<Tprimary>
    {
        public Tprimary Id { get; set; }
    }
}
