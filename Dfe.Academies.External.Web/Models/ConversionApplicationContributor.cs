using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string firstName, string surname, SchoolRoles role, string? otherRoleNotListed)
    {
        Role = role;
        OtherRoleNotListed = otherRoleNotListed;
        Person = new Person
        {
            FirstName = firstName,
            Surname = surname
        };
    }

    public int Id { get; set; }

    public int ConversionApplicationId { get; set; }

    public SchoolRoles Role { get; set; }

    public string? OtherRoleNotListed { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; }


    // TODO:- other props, maybe UserId from auth / user email from auth?
}