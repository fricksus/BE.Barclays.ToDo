using System.ComponentModel.DataAnnotations;
using Barclays.ToDo.Services.Enums;

namespace Barclays.ToDo.Services.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        [Required]
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}