using System;
using System.Collections.Generic;

namespace UserModule.Models;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string TransactionCode { get; set; } = null!;

    public string TransactionTypeName { get; set; } = null!;
}
