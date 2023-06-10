using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("orders", Schema = "ordering")]
[Index("BranchId", Name = "IX_orders_BranchId")]
[Index("CancelTypeId", Name = "IX_orders_CancelTypeId")]
[Index("DeliveryMethodId", Name = "IX_orders_DeliveryMethodId")]
[Index("DriverId", Name = "IX_orders_DriverId")]
[Index("OrderStatusId", "DriverId", Name = "IX_orders_OrderStatusId_DriverId")]
[Index("PaymentMethodId", Name = "IX_orders_PaymentMethodId")]
[Index("SourceTypeId", Name = "IX_orders_SourceTypeId")]
public partial class Order
{
    [Key]
    public Guid Id { get; set; }

    [Column("Address_AddressId")]
    public int? AddressAddressId { get; set; }

    [Column("Address_FullAddress")]
    [StringLength(250)]
    public string? AddressFullAddress { get; set; }

    [Column("Address_Phone")]
    [StringLength(11)]
    public string? AddressPhone { get; set; }

    [Column("Address_Coordinate")]
    [StringLength(100)]
    public string? AddressCoordinate { get; set; }

    [StringLength(10)]
    public string InvoiceNo { get; set; } = null!;

    public string? UserAgent { get; set; }

    [StringLength(50)]
    public string? ExternalInvoiceNo { get; set; }

    public int PreparingForecast { get; set; }

    public int WaitForDriverForecast { get; set; }

    public int DeliveringForecast { get; set; }

    public Guid? DriverId { get; set; }

    public bool IsDriverPinned { get; set; }

    public DateTime? ValidatedTime { get; set; }

    public DateTime? PrintedTime { get; set; }

    public DateTime? PreparedTime { get; set; }

    public DateTime? DeliveringTime { get; set; }

    public DateTime? ShippedTime { get; set; }

    public DateTime? DriverReturnTime { get; set; }

    public DateTime? CanceledTime { get; set; }

    public string? Subscription { get; set; }

    [StringLength(60)]
    public string? BuyerName { get; set; }

    public string? Extension { get; set; }

    public string? CallId { get; set; }

    public int BranchId { get; set; }

    public int? OldId { get; set; }

    public byte Shift { get; set; }

    public int OrderStatusId { get; set; }

    [StringLength(250)]
    public string? CancelDescription { get; set; }

    public DateTime? ReserveTime { get; set; }

    public int Discount { get; set; }

    public int DeliveryCost { get; set; }

    public int RoundValue { get; set; }

    public int OtherOrdersPrice { get; set; }

    public int Gross { get; set; }

    [Column("VAT")]
    public int Vat { get; set; }

    public int UsedCredit { get; set; }

    public DateTime? DeliveryTime { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    public Guid? CouponId { get; set; }

    public Guid? RelatedOrderId { get; set; }

    public int? PosId { get; set; }

    public int? PagerNo { get; set; }

    public int TotalCost { get; set; }

    public int? CashReceived { get; set; }

    [StringLength(18)]
    public string? TransactionNo { get; set; }

    [StringLength(50)]
    public string? TransactionError { get; set; }

    public Guid? BuyerId { get; set; }

    public int? CancelTypeId { get; set; }

    [StringLength(50)]
    public string? DeliveryId { get; set; }

    public int DeliveryMethodId { get; set; }

    [StringLength(50)]
    public string? DeliveryName { get; set; }

    public DateTime OrderDate { get; set; }

    public int PaymentMethodId { get; set; }

    public Guid? ReferrerId { get; set; }

    public int SourceTypeId { get; set; }

    public Guid UserId { get; set; }

    public int DeliveryDiscount { get; set; }

    public int PackagingCost { get; set; }

    public Guid? PartnerId { get; set; }

    public int PayableCost { get; set; }

    public Guid? CancelerUserId { get; set; }

    public byte? GuestCount { get; set; }

    public int? TableId { get; set; }

    public int? ManualPickupForecast { get; set; }

    public int? ManualTotalTimeForecast { get; set; }

    public int ServiceCharge { get; set; }

    [StringLength(100)]
    public string? DriverName { get; set; }

    [StringLength(100)]
    public string? ExternalDriverStatus { get; set; }

    public DateTime? ExternalDriverStatusTime { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Orders")]
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("CancelTypeId")]
    [InverseProperty("Orders")]
    public virtual Canceltype? CancelType { get; set; }

    [ForeignKey("DeliveryMethodId")]
    [InverseProperty("Orders")]
    public virtual Deliverymethod DeliveryMethod { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    public virtual Orderstatus OrderStatus { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Orders")]
    public virtual Paymentmethod PaymentMethod { get; set; } = null!;

    [ForeignKey("SourceTypeId")]
    [InverseProperty("Orders")]
    public virtual Sourcetype SourceType { get; set; } = null!;
}
