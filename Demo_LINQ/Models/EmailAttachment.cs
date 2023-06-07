using System;
using System.Collections.Generic;

namespace Demo_LINQ.Models;

public partial class EmailAttachment
{
    public int Id { get; set; }

    public string SenderMailId { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public byte[]? PdfAttachment { get; set; }

    public byte[]? ImageAttachment { get; set; }
}
