using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Menus;

public sealed record CreateMenuRequest(
    string Name,
    string Description,
    double Average,
    int Rating,
    List<MenuSectionRequest> Sections);

public sealed record MenuSectionRequest(
    string Name,
    string Description,
    List<MenuItemRequest> Items
    );

public sealed record MenuItemRequest(
    string Name,
    string Description);