using System.ComponentModel.DataAnnotations;

namespace apistudy.Atrubuts
{
    // for one file
    //    public class AllowedExtensionsAttribute : ValidationAttribute
    //    {
    //        private readonly string _allowedExtensions;

    //        public AllowedExtensionsAttribute(string allowedExtensions)
    //        {
    //            _allowedExtensions = allowedExtensions;
    //        }

    //        protected override ValidationResult? IsValid
    //            (object? value, ValidationContext validationContext)
    //        {
    //            var file = value as IFormFile;

    //            //if (file is not null)
    //            //{
    //                var extension = Path.GetExtension(file.FileName);

    //                var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

    //                if (!isAllowed)
    //                {
    //                    return new ValidationResult($"Only {_allowedExtensions} are allowed!");
    //                }
    //            //}

    //            return ValidationResult.Success;
    //        }
    //   


    // for one file
    //using System.Linq;

    //public class AllowedExtensionsAttribute : ValidationAttribute
    //{
    //    private readonly string _allowedExtensions;

    //    public AllowedExtensionsAttribute(string allowedExtensions)
    //    {
    //        _allowedExtensions = allowedExtensions;
    //    }

    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        if (value is IFormFileCollection file)
    //        {
    //            var extension = Path.GetExtension(file.FileName);

    //            if (!string.IsNullOrEmpty(extension) && _allowedExtensions.Split(',').Contains(extension.TrimStart('.').ToLower(), StringComparer.OrdinalIgnoreCase))
    //            {
    //                return ValidationResult.Success;
    //            }
    //        }

    //        // Consider using localization or resource files for error messages
    //        return new ValidationResult($"Only {_allowedExtensions} extensions are allowed.");
    //    }
    //}
    //}
    //using Microsoft.AspNetCore.Http;

    //using System.ComponentModel.DataAnnotations;
    //using System.IO;
    //using System.Linq;

    //public class AllowedExtensionsAttribute : ValidationAttribute
    //{
    //    private readonly string _allowedExtensions;

    //    public AllowedExtensionsAttribute(string allowedExtensions)
    //    {
    //        _allowedExtensions = allowedExtensions;
    //    }

    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        if (value is IFormFileCollection files)
    //        {
    //            foreach (var file in files)
    //            {

    //                var extension = Path.GetExtension(file.FileName);

    //                var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

    //                if (isAllowed)
    //                {
    //                    return new ValidationResult($"Only {_allowedExtensions} are allowed!");
    //                }


    //            // If any file has an invalid extension, return a validation error
    //            return new ValidationResult($"Only {_allowedExtensions} extensions are allowed.");
    //            }

    //            return ValidationResult.Success;
    //        }

    //        // Return a validation error if the value is not an IFormFileCollection
    //        return new ValidationResult("Value is not an IFormFileCollection.");
    //    }
    //}
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
/// <summary>
/// forcllection of imges
/// </summary>
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFileCollection files)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);

                             var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                    

                    if (!isAllowed)
                    {
                        // If any file has an invalid extension, return a validation error
                        return new ValidationResult($"Only {_allowedExtensions} extensions are allowed.");
                    }
                }

                // Return a success result if all files have valid extensions
                return ValidationResult.Success;
            }

            // Return a validation error if the value is not an IFormFileCollection
            return new ValidationResult("Value is not an IFormFileCollection.");
        }
    }

}