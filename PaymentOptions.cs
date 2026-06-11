using System.ComponentModel.DataAnnotations;

namespace TmsApi;

public class PaymentOptions
{
    // TODO1: Create a class called PaymentOptions with two properties:
    
    // GatewayUrl (string, required - use [Required] attribute)
    [Required(ErrorMessage = "The GatewayUrl field is required.")]
    public string GatewayUrl { get; set; } = string.Empty;

    // MaxDepositBirr (decimal, range 100-100000 - use [Range] attribute)
    [Range(100.0, 100000.0, ErrorMessage = "MaxDepositBirr must be between 100 and 100,000 Ethiopian Birr.")]
    public decimal MaxDepositBirr { get; set; }
}