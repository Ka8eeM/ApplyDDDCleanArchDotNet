using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Users.GetAllUsers;

public sealed record GetAllUsersRequest(
    string? FirstName,
    string? LastName,
    DateTime? CreateAt);
