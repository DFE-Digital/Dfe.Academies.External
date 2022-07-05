namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationContributor
{
    public ConversionApplicationContributor(string name)
    {
        Name = name;
    }

    public long Id { get; set; }
    public string Name { get; set; }    

    // TODO:- other props, maybe UserId from auth / user email from auth?
}