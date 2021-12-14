using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ContactForScholarshipDTO : BaseEntityDTO<Guid>
    {
        public ContactForScholarshipDTO()
        {
        }

        [Required]
        public Guid ScholarSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string PhoneType { get; set; }
        [Required]
        [StringLength(200)]
        public string CommType { get; set; }
        [Required]
        [StringLength(200)]
        public string DefaultPhone { get; set; }
        [Required]
        [StringLength(200)]
        public string CountryPrefix { get; set; }
        [Required]
        [StringLength(200)]
        public string CityPrefix { get; set; }
        [Required]
        [StringLength(200)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(200)]
        public string Extension { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        public string Remarks { get; set; }
        public virtual ScholarSetupDTO ScholarSetup { get; set; }
    }
}
