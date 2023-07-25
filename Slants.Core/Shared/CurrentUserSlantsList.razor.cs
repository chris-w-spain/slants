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
    public partial class CurrentUserSlantsList
    {
        [Inject] private ISlantsService _slantService { get; set; } = default!;
        private readonly IList<Slant> _slants = new List<Slant>();

        protected async override Task OnInitializedAsync()
        {
            // getting all slants
            var slants = await _slantService.GetCurrentUserSlantsAsync();
            foreach(var slant in slants)
            {
                _slants.Add(slant);
            }

            this.StateHasChanged();
        }
    }
}