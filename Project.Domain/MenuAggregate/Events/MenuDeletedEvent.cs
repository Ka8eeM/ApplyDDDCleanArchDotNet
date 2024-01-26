using Project.Domain.Common.Premitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.MenuAggregate.Events;

public record MenuDeletedEvent(Menu menu) : IDomainEvent;