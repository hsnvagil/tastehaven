﻿namespace Taste_Haven_API.Models.Dto;

public class OrderHeaderUpdateDto
{
    public int OrderHeaderId { get; set; }
    public string PickupName { get; set; }
    public string PickupPhoneNumber { get; set; }
    public string PickupEmail { get; set; }
    public string StripePaymentIntentID { get; set; }
    public string Status { get; set; }
}