namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public long Id { get; set; }
    public string Name { get; set; }

    public string Role { get; set; }

    // TODO:- other props, maybe UserId from auth / user email from auth?
}