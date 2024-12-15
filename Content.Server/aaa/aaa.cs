using Content.Shared.Inventory;
namespace Content.Server.Cameras;

public sealed partial class CamerasSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;
    [Dependency] private readonly EntityManager _entManager = default!;
    public void TakePhoto(EntityUid user)
    {
        // Логика фотографирования
        var photo = CreatePhoto(user);
        // Передаем фото пользователю
        GiveToUser(photo, user);
    }

    private EntityUid CreatePhoto(EntityUid user)
    {
        // Создаем объект фотографии
        var xformSystem = _entManager.System<SharedTransformSystem>();
        var photoEntity = EntityManager.SpawnEntity("PhotoEntity", xformSystem.GetMapCoordinates(user));
        return photoEntity;
    }

    private void GiveToUser(EntityUid photo, EntityUid user)
    {
        // Логика передачи фотографии пользователю
        if (TryComp(user, out InventoryComponent? inventoryComp))
        {
            _inventory.TryEquip(user, photo, "0", inventory: inventoryComp);
        }
    }
}
