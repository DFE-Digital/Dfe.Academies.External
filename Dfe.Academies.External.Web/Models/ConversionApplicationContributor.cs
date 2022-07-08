<<<<<<< HEAD
﻿using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string firstName, string surname, SchoolRoles role, string? otherRoleNotListed)
    {
        FirstName = firstName;
        Surname = surname;
        Role = role;
        OtherRoleNotListed = otherRoleNotListed;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }

    public string Surname { get; set; }

    public SchoolRoles Role { get; set; }

    public string? OtherRoleNotListed { get; set; }
=======
﻿namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public string Role { get; set; }
>>>>>>> Model changes / service changes required for application overview page

    // TODO:- other props, maybe UserId from auth / user email from auth?
}