using System.ComponentModel.DataAnnotations;

namespace Holy_Man_API.Models
{
    public class DepartmentModel
    {
       
        [Key]
        public int Id { get; set; }
        public string Department { get; set; }
        public bool Status { get; set; }
    }
}
