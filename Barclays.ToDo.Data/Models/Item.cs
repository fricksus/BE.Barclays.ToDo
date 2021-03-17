using Barclays.ToDo.Data.Enums;

namespace Barclays.ToDo.Data.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}