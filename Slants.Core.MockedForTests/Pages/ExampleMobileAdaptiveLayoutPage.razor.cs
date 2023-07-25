using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Slants.Core.Layouts;

namespace Slants.Core.MockedForTests.Pages
{
    public partial class ExampleMobileAdaptiveLayoutPage
    {
        [CascadingParameter(Name = "CurrentLayout")] public MobileAdaptiveLayout CurrentLayout { get; set; } = default!;
    }
}