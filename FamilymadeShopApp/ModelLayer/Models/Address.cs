using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class Address
    {
        private string _postalCode;

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Invalid Zip Code")]
        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _postalCode = value;
                }
                else
                {
                    string trimmedValue = value.Trim();

                    if (trimmedValue.Length == 6 && char.IsDigit(trimmedValue[3]) && char.IsLetter(trimmedValue[4]))
                    {
                        _postalCode = trimmedValue.Insert(4, " ");
                    }
                    else
                    {
                        _postalCode = trimmedValue;
                    }

                    _postalCode = _postalCode.Substring(0, 4) + _postalCode.Substring(4).ToUpper();
                }
            }
        }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "Netherlands";


        public override string ToString()
        {
            return $"{AddressLine}, {PostalCode} {City}, {Country}";
        }

        public static Address Parse(string fullAddress)
        {
            var address = new Address();

            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                return address;
            }

            var parts = fullAddress.Split(',');

            if (parts.Length == 3)
            {
                address.AddressLine = parts[0].Trim();

                var postalAndCity = parts[1].Trim().Split(' ');

                if (postalAndCity.Length >= 2)
                {
                    address.PostalCode = postalAndCity[0].Trim();
                    address.City = string.Join(' ', postalAndCity.Skip(1)).Trim();
                }
                else
                {
                    throw new FormatException("Invalid postal code and city format.");
                }

                address.Country = parts[2].Trim();
            }
            else
            {
                throw new FormatException("Invalid address format.");
            }

            return address;
        }
    }
}
