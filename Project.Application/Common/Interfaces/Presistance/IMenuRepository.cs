using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate;

namespace Project.Application.Common.Interfaces.Presistance;
public interface IMenuRepository
{
    Task Add(Menu menu);
    Task<Menu?> GetMenuById(Guid menuId);

    void Remove(Menu menu);
    void Update(Menu menu);

    Task<List<Menu>> GetAll();
}