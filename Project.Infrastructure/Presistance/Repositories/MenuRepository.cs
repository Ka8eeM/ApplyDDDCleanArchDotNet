using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Errors;
using Project.Domain.MenuAggregate.ValueObjects;
using Project.Infrastructure.Presistance.Specifications;
using Project.Infrastructure.Presistance.Specifications.MenuSpecifications;

namespace Project.Infrastructure.Presistance.Repositories;

internal class MenuRepository : IMenuRepository
{
    private readonly DbSet<Menu> _menuSet;
    private readonly ApplicationDbContext _applicationDbContext;
    public MenuRepository(ApplicationDbContext context)
    {
        _applicationDbContext = context;
        _menuSet = context.Menus;
    }

    public async Task Add(Menu menu)
    {
        await _menuSet.AddAsync(menu);
    }

    public async Task<List<Menu>> GetAll()
    {
        var result = await _menuSet
            .AsNoTracking()
            .ToListAsync();
        return result;
    }

    public async Task<Menu?> GetMenuById(Guid menuId)
    {
        try
        {
            var m = await _menuSet.ToListAsync();
            var menu = ApplySpecfication(new GetMenuByIdIncludeSectionsSpecification(menuId));
            return default;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }

    }

    private IQueryable<Menu> ApplySpecfication(
        Specification<Menu> specification)
    {
        return SpecificationEvaluator.GetQuery(_menuSet, specification);
    }

    public void Remove(Menu menu)
    {
        _menuSet.Remove(menu);
    }

    public void Update(Menu menu)
    {
        _menuSet.Update(menu);
    }
}