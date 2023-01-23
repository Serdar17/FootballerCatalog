using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Web.Dto;

public class PlayerDto
{
    [Required(ErrorMessage = "Не указано имя")]
    [CustomFieldName(ErrorMessage = "Имя может состоять только лишь из букв")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Не указана фамилия")]
    [CustomFieldName(ErrorMessage = "Фамилия может состоять только лишь из букв")]
    public string SecondName { get; set; }
    
    [Required(ErrorMessage = "Не указан пол")]
    public string Sex { get; set; }

    [Required(ErrorMessage = "Не указано дата рождения")]
    public DateTime BirthDate { get; set; }
    
    [Required(ErrorMessage = "Не указано название команды")] 
    public string TeamName { get; set; }
    
    [Required(ErrorMessage = "Не указана страна")]
    [CustomFieldName(ErrorMessage = "Страна может состоять только лишь из букв")]
    public string Country { get; set; }
    
    public List<string> TeamNames { get; set; } = new ();
}