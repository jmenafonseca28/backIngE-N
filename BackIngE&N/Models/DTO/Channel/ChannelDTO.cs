using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackIngE_N.Models.DTO.Channel {
    public class ChannelDTO {
        public Guid? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? TvgId { get; set; }
        public string? TvgName { get; set; }
        public int? TvgChannelNumber { get; set; }
        public string? Logo { get; set; }
        public Guid PlayListId { get; set; }
        public int orderList { get; set; }

    }
}
