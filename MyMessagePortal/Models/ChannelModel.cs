using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMessagePortal.Models
{
    public class ChannelModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł musi być uzupełnione")]
        [DisplayName("Nazwa")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        [Required]
        public string CreatedById { get; set; }

        [DisplayName("Właściciel")]
        [ForeignKey("CreatedById")]
        public UserModel CreatedBy { get; set; }

        [DisplayName("Domyślny kanał")]
        public bool IsDefault { get; set; }

        [MaxLength(6, ErrorMessage = "Podaj kolor w zapisie szsnastkowym - 6 znaków")]
        [MinLength(6, ErrorMessage = "Podaj kolor w zapisie szsnastkowym - 6 znaków")]
        [DisplayName("Kolor kanału")]
        public string ChannelColor { get; set; }

        public ICollection<MessageModel> Messages { get; set; }

        public ICollection<ObservedChannelsModel> ObservedChannels { get; set; }
    }
}
