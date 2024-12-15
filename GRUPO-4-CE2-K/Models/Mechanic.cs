using GRUPO_4_CE2_K.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RepairRequest> AssignedRequests { get; set; }
    }

}
