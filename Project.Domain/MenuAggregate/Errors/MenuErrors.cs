using Project.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.MenuAggregate.Errors;

public class MenuErrors
{
    public static Error MenuNotFound = new Error("MenuNotFound", "Can't find the required menu");

    public static Error MenusAreEmpty = new Error("MenusAreEmpty", "No Menus were found");
}