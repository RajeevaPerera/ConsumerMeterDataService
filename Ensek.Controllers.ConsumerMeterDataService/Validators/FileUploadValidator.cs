using Ensek.Models.ConsumerMeterDataService.External;
using FluentValidation;

namespace Ensek.Controllers.ConsumerMeterDataService.Validators;

public class FileUploadValidator : AbstractValidator<FileUpload>
{
    public FileUploadValidator() =>
        RuleFor(request => request.FileDetails)
            .NotEmpty();
}