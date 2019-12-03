using forgit.Interfaces;
using forgit.Providers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace forgit.tests.Commands
{
    public class CloneTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly ISettings settings = new Settings("cloneSettings.json");

        [Fact]

    }
}
