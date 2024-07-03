using System;
using System.Collections.Generic;

namespace UserModule.Models;

public partial class AccountTypeMaster
{
    public int AccountTypeId { get; set; }

    public string AccountTypeName { get; set; } = null!;
}
