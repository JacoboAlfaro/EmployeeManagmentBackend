using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Models
{
    public record FileUpload(
        Stream Content,
        string FileName,
        string ContentType
    );
}
