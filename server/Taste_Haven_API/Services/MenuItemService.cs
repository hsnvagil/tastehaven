#region

using AutoMapper;
using Taste_Haven_API.Middlewares.Exceptions;
using Taste_Haven_API.Models;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Repositories;
using Taste_Haven_API.Utility;

#endregion

namespace Taste_Haven_API.Services;

public interface IMenuItemService
{
    Task<MenuItem> GetByIdAsync(int id);
    Task<int> AddAsync(MenuItemCreateDto menuItem);
    Task UpdateAsync(int id, MenuItemUpdateDto menuItem);
    Task DeleteAsync(int id);
    Task<IEnumerable<MenuItem>> GetAllAsync();
}

public class MenuItemService(IMenuItemRepository menuItemRepository, IBlobService blobService, IMapper mapper)
    : IMenuItemService
{
    public async Task<MenuItem> GetByIdAsync(int id)
    {
        if (id <= 0) throw new InvalidMenuItemIdException();

        var menuItem = await menuItemRepository.GetByIdAsync(id);
        if (menuItem == null) throw new MenuItemNotFoundException();

        return menuItem;
    }

    public async Task<int> AddAsync(MenuItemCreateDto dto)
    {
        if (dto.File == null || dto.File.Length == 0) throw new ArgumentException("Invalid file upload.");

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
        var menuItem = mapper.Map<MenuItem>(dto);
        menuItem.Image = await blobService.UploadBlob(fileName, SD.SD_Storage_Container, dto.File);

        return await menuItemRepository.AddAsync(menuItem);
    }

    public async Task UpdateAsync(int id, MenuItemUpdateDto dto)
    {
        var menuItemFromDb = await menuItemRepository.GetByIdAsync(id);
        if (menuItemFromDb == null) throw new MenuItemNotFoundException();


        if (dto.File is { Length: > 0 })
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
            await blobService.DeleteBlob(menuItemFromDb.Image.Split('/').Last(), SD.SD_Storage_Container);
            menuItemFromDb.Image =
                await blobService.UploadBlob(fileName, SD.SD_Storage_Container, dto.File);
        }

        mapper.Map(dto, menuItemFromDb);

        await menuItemRepository.UpdateAsync(menuItemFromDb);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new InvalidMenuItemIdException();

        var entity = await menuItemRepository.GetByIdAsync(id);

        if (entity == null) throw new MenuItemNotFoundException();

        await blobService.DeleteBlob(entity.Image.Split('/').Last(), SD.SD_Storage_Container);
        await menuItemRepository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await menuItemRepository.GetAllAsync();
    }
}