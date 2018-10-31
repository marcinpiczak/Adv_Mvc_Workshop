using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMessagePortal.Models;

namespace MyMessagePortal.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść musi być uzupełnione")]
        [DisplayName("Treść")]
        [MinLength(10)]
        public string Text { get; set; }

        [DisplayName("Data dodania")]
        public DateTime DateAdded { get; set; }

        public string CreatedById { get; set; }

        [DisplayName("Napisał")]
        [ForeignKey("CreatedById")]
        public UserModel CreatedBy { get; set; }

        public int ChannelId { get; set; }

        [DisplayName("Kanał")]
        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
    }
}
