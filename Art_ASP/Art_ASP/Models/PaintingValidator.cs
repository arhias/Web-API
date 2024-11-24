using FluentValidation;

namespace Art_ASP.Data
{
    public class PaintingValidator : AbstractValidator<Painting>
    {
        public PaintingValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Поле 'Назва' є обов'язковим.")
                .MaximumLength(100).WithMessage("Назва не може перевищувати 100 символів.");

            RuleFor(p => p.Author)
                .NotEmpty().WithMessage("Поле 'Автор' є обов'язковим.")
                .MaximumLength(50).WithMessage("Автор не може перевищувати 50 символів.");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("Поле 'Посилання на зображення' є обов'язковим.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Невірний формат URL.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Опис не може перевищувати 500 символів.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Ціна повинна бути більше 0.");
        }
    }
}
