﻿namespace MSIT155site.Models.DTO
{
    public class SpotsPagingDTO
    {
        public int TotalPages {  get; set; }
        public List<SpotImagesSpot>? SpotsResult { get; set; }
        public List<Category>? CategoryResult { get; set; }
    }
}
