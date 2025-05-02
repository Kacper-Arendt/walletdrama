using System.ComponentModel.DataAnnotations;

namespace Teams.Core.Commands.Dtos;

public class UpdateTeamDetailsDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string Name { get; set; }
}