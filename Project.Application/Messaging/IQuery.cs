using MediatR;
using Project.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}


