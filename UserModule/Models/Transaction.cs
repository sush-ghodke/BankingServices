using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace UserModule.Models;

public partial class Transaction
{

    public long TransactionId { get; set; }

    public long AccountId { get; set; }

    public double Amount { get; set; }

    public int TransactionTypeId { get; set; }


    [SwaggerSchema(ReadOnly = true)]

    public DateTime TransactionDate { get; set; }
}
