using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.UserAggregate;

namespace Project.Infrastructure.Presistance.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Guid AddAsync(User user)
    {
        _context.Add(user);
        return user.Id;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public User? GetUserByEmailAsync(string email)
    {
        return _context.Set<User>().SingleOrDefault(u => u.Email == email);
    }

    public User? GetUserById(Guid userId)
    {
        return _context.Set<User>().SingleOrDefault(u => u.Id == userId);
    }

    public Guid UpdateAsync(User user)
    {
        var ret = _context.Set<User>().Update(user);
        return ret.Entity.Id;
    }
}