using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class InstitutionContactSetupDTO : BaseEntityDTO<Guid>
    {
        public InstitutionContactSetupDTO()
        {
        }

        [Required]
        [StringLength(200)]
        public string PhoneType { get; set; }
        [StringLength(200)]
        public string CommType { get; set; }
        [StringLength(200)]
        public string DefaultPhone { get; set; }
        public int? CountryPrefix { get; set; }
        public int? CityPrefix { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public int? Extension { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string Remarks { get; set; }
    }
}
