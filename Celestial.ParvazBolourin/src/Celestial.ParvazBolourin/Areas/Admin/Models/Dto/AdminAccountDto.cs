using System.ComponentModel.DataAnnotations;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;

public class AddRoleToUserDto
{
    [Required] public string UserId { get; set; }

    [Required] public string RoleName { get; set; }
}

public class MakeRoleDto
{
    [Required] public string RoleName { get; set; }
}