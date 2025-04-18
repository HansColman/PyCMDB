﻿using CMDB.API.Models;
using CMDB.Domain.Requests;

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
        Task<IEnumerable<MobileDTO>> ListAllFreeMobiles(string sitePart);
        Task<bool> IsMobileExisting(MobileDTO mobileDTO);
        Task AssignIdentity(AssignMobileRequest request);
        Task ReleaseIdentity(AssignMobileRequest request);
        Task AssignSubscription(AssignMobileSubscriptionRequest request);
        Task LogPdfFile(string pdfFile, int id);
        Task ReleaseSubscription(AssignMobileSubscriptionRequest assignMobileSubscription);
    }
}
