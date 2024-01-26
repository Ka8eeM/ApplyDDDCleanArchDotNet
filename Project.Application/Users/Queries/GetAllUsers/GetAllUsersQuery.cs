using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Users.Commons;
using Project.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Users.Queries.GetAllUsers;
public sealed record GetAllUsersQuery(
    string? FirstName,
    string? LastName,
    DateTime? CreateAt) : IRequest<Result<List<UserDto>>>;
