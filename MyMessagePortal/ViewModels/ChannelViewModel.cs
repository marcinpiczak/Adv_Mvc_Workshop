using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyMessagePortal.Models;

namespace MyMessagePortal.ViewModels
{
    public class ChannelViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł musi być uzupełnione")]
        [DisplayName("Nazwa")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<MessageModel> Messages { get; set; }
    }
}
