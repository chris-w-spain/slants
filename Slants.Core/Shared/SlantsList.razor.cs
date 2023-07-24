using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Slants.Services;

namespace Slants.Core.Shared
{
    public partial class SlantsList
    {
        [Inject] private ISlantsService _slantService { get; set; } = default!;
        IList<Slant> _slants = new List<Slant>();

        protected async override Task OnInitializedAsync()
        {
            // getting all slants
            var slants = await _slantService.GetSlantsAsync();
            foreach(var slant in slants)
            {
                _slants.Add(slant);
            }

            this.StateHasChanged();
        }
    }
}