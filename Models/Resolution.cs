using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class Resolution
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";

        public string Title { get; set; } = "";

        public string BodyText { get; set; } = "";
    }
}
