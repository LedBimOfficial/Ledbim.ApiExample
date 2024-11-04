using FluentValidation;
using Ledbim.Core.Base.Responses;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Ledbim.ApiExample.Application.Commands.Users
{
    public class UserCreateRequest : IRequest<ApiResponse>
    {
        [SwaggerSchema(Description = "Kullanıcının tam adı, ilk ve soy isim içermelidir. Eğer isim null gelirse güncelleme yapılacak anlamına gelir, Değilse yenisini ekler")]
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [SwaggerSchema(Format = "email", Description = "Kullanıcının geçerli bir email adresi olmalıdır.")]
        public string Mail { get; set; } = null!;

        [SwaggerSchema(Description = "Kullanıcının şifresi.")]
        public string Password { get; set; } = null!;
    }

    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(I => I.FullName).NotNull().MaximumLength(50);
            RuleFor(I => I.Mail).NotNull().EmailAddress().MaximumLength(64);
            RuleFor(I => I.Password).NotNull().MaximumLength(64);
        }
    };
}