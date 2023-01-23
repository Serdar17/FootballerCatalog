using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Player
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
    
    public string Sex { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string TeamName { get; set; }
    
    public string Country { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return true;
        }

        Player p = (Player) obj;
        return FirstName == p.FirstName && p.SecondName == p.SecondName;
    }
}