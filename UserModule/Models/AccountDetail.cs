using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UserModule.Models;

public partial class AccountDetail
{
    public long AccountId { get; set; }

    public int AccountTypeId { get; set; }

    public int UserId { get; set; }

    public double CurrentBalance { get; set; }

    [DefaultValue(1)]
    public int Status { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public DateTime OpenDate { get; set; } = DateTime.Now;

    [SwaggerSchema(ReadOnly = true)]
    [DefaultValue("")]
    public DateTime? ClosedDate { get; set; }
}
