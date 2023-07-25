using Slants.Core.Layouts;
using Slants.Core.MockedForTests.Pages;

namespace Slants.Core.Tests
{
    public class MobileAdaptiveLayoutTests
    {
        [Fact]
        public void RendersDefault()
        {
            var ctx = new TestContext();
            ctx.JSInterop.SetupModule("./_content/Slants.Core/js/MobileAdaptiveLayout.js");
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var layout = ctx.RenderComponent<MobileAdaptiveLayout>();
            var cut = ctx.RenderComponent<ExampleMobileAdaptiveLayoutPage>(p =>
                p.AddCascadingValue("CurrentLayout", layout.Instance));

            var mainContentElement = cut.Find(".test__maincontent");
            mainContentElement?.MarkupMatches("<p class=\"test__maincontent\">desktop content</p>");
        }

        [Fact]
        public async Task RendersMobile()
        {
            var ctx = new TestContext();
            ctx.JSInterop.SetupModule("./_content/Slants.Core/js/MobileAdaptiveLayout.js");
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var layout = ctx.RenderComponent<MobileAdaptiveLayout>();
            var cut = ctx.RenderComponent<ExampleMobileAdaptiveLayoutPage>(p =>
                p.AddCascadingValue("CurrentLayout", layout.Instance));

            await cut.Instance.CurrentLayout.UpdateWindowWidth(750);
            cut.Render();

            var mainContentElement = cut.Find(".test__maincontent");
            mainContentElement?.MarkupMatches("<p class=\"test__maincontent\">mobile content</p>");
        }

        [Fact]
        public async Task RendersDesktop()
        {
            var ctx = new TestContext();
            ctx.JSInterop.SetupModule("./_content/Slants.Core/js/MobileAdaptiveLayout.js");
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;

            var layout = ctx.RenderComponent<MobileAdaptiveLayout>();
            await layout.Instance.UpdateWindowWidth(800);

            var cut = ctx.RenderComponent<ExampleMobileAdaptiveLayoutPage>(p =>
                p.AddCascadingValue("CurrentLayout", layout.Instance));

            var mainContentElement = cut.Find(".test__maincontent");
            mainContentElement?.MarkupMatches("<p class=\"test__maincontent\">desktop content</p>");
        }

    }
}