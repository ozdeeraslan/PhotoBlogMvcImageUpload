﻿using System.ComponentModel.DataAnnotations;

namespace PhotoBlogUygulama.Attributes
{
    public class GecerliResimAttribute : ValidationAttribute
    {
        public double MaxDosyaBoyutuMb { get; set; } = 1;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dosya = (IFormFile?)value;

            if (dosya == null)
                return ValidationResult.Success;

            if(!dosya.ContentType.StartsWith("image/"))
            {
                return new ValidationResult("Gecersiz resim dosyasi.");
            }
            else if (dosya.Length > MaxDosyaBoyutuMb * 1024 * 1024) 
            {
                return new ValidationResult($"Max dosya boyutu: {MaxDosyaBoyutuMb} MB");
            }

            return ValidationResult.Success;
        }
    }
}
