using FluentValidation;
using FluentValidation.AspNetCore;
using MyAuthWork.Models;

namespace MyAuthWork.FluentValidation
{
    public class UserLoginValidator:AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz...")
                                    .NotNull().WithMessage("Kullanıcı Adı Boş Olamaz...")
                                    .MaximumLength(20).WithMessage("Max Karakter Sayısı 20 olabilir...")
                                    .MinimumLength(3).WithMessage("Min Karakter Sayısı 3 Olabilir")
                                    .Must(x => !string.IsNullOrEmpty(x) && x.Any(char.IsUpper)).WithMessage("İsminiz de Bir Büyük Harf Olmak Zorunda");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Olamaz...")
                                    .NotNull().WithMessage("Şifre Boş Olamaz...")
                                    .Must(a => !string.IsNullOrEmpty(a) && a.Any(char.IsUpper)).WithMessage("En Az Bir Büyük Harf İçermeli")
                                    .Must(a => !string.IsNullOrEmpty(a) && a.Any(char.IsLower)).WithMessage("En Az Bir Küçük Harf İçermeli")
                                    .Must(a => !string.IsNullOrEmpty(a) && a.Any(char.IsDigit)).WithMessage("En Az Bir Rakam İçermeli");


        }
    }
}
