using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyMessagePortal.Models
{
    public class UserModel : IdentityUser
    {
        public ICollection<MessageModel> UserMessages { get; set; }

        public ICollection<ChannelModel> UserChannels { get; set; }

        public ICollection<ObservedChannelsModel> ObservedChannels { get; set; }
    }
}
