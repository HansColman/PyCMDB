﻿using CMDB.API.Models;

namespace CMDB.API.Services
{
    public interface IMobileRepository 
    {
        Task<IEnumerable<MobileDTO>> GetAll();
        Task<IEnumerable<MobileDTO>> GetAll(string searchStr);
        Task<MobileDTO?> GetById(int id);
        MobileDTO Create(MobileDTO mobileDTO);
        Task<MobileDTO> Update(MobileDTO mobileDTO);
        Task<MobileDTO> Delete(MobileDTO mobileDTO, string reason);
        Task<MobileDTO> Activate(MobileDTO mobileDTO);
        Task<IEnumerable<MobileDTO>> ListAllFreeMobiles();
    }
}
