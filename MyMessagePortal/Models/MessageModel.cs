using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMessagePortal.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        public string Text { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        [Required]
        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public UserModel CreatedBy { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
    }
}
