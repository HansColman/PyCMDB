﻿namespace CMDB.API.Models
{
    public class ModelDTO
    {
        public int Active { get; set; }
        public string? DeactivateReason { get; set; }
        public int? LastModifiedAdminId { get; set; }

        public ICollection<LogDTO> Logs { get; set; }

        public ModelDTO()
        {
            Logs = new List<LogDTO>();
        }
    }
}
