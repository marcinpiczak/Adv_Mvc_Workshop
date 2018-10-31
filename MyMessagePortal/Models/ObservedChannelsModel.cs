using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMessagePortal.Models
{
    public class ObservedChannelsModel
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [ForeignKey("ChannelId")]
        public ChannelModel Channel { get; set; }
    }
}
