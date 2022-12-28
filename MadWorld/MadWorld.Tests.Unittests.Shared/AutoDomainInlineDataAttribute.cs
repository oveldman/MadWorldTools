using AutoFixture.Xunit2;

namespace MadWorld.Tests.Unittests.Shared;

public class AutoDomainInlineDataAttribute : InlineAutoDataAttribute
{
    public AutoDomainInlineDataAttribute(params object[] objects) : base(new AutoDomainDataAttribute(), objects) { }
}