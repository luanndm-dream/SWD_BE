﻿using Microsoft.AspNetCore.Http;

namespace BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
public interface IBlobStorageRepository
{
    Task<string> SaveImageOnBlobStorage(IFormFile image, string name, string type);
    Task DeleteImageFromBlobStorage(string imageUrl);
    Task RestoreContainer(string imageUrl);
}
