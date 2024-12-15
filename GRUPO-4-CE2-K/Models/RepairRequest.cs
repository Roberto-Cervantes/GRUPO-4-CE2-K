using GRUPO_4_CE2_K.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class RepairRequest
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string CarPlate { get; set; }

        public int RepairTypeId { get; set; }
        public RepairType RepairType { get; set; } // Relación con tipos de reparación

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; } // Relación con mecánicos

        public bool IsCompleted { get; set; } = false; // Estado de la solicitud
    }

}
