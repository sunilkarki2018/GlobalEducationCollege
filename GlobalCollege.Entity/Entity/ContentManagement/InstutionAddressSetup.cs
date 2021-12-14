using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("InstutionAddressSetup", Schema = "ContentManagement")]
    public class InstutionAddressSetup : BaseEntity<Guid>
    {
        public InstutionAddressSetup()
        {
        }
        [Required]
        [StringLength(200)]
        public string InstitutionAddressType { get; set; }
        [StringLength(200)]
        public string DefaultAddress { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(200)]
        public string Country { get; set; }
        [Required]
        [StringLength(200)]
        public string State { get; set; }
        [Required]
        [StringLength(200)]
        public string Town { get; set; }
        [Required]
        [StringLength(200)]
        public string City { get; set; }
        [Required]
        public int WardNumber { get; set; }
        [StringLength(200)]
        public string HouseNumber { get; set; }
        public string GPSLocation { get; set; }
        public int? ZIP { get; set; }
        [StringLength(200)]
        public string ColonyName { get; set; }
        [StringLength(200)]
        public string BuildingName { get; set; }
        [StringLength(200)]
        public string NearbyLandmark { get; set; }
        public int? DistanceFromLandmark { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string Remarks { get; set; }

    }
}
