using System;
using System.Collections.Generic;

namespace UserModule.Models;

public partial class TransactionMode
{
    public int TransactionModeId { get; set; }

    public string TransactionModeName { get; set; } = null!;
}
