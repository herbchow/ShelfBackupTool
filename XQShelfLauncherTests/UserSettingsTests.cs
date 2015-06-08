using NUnit.Framework;
using XQShelfLauncher;

namespace XQShelfLauncherTests
{
    public class UserSettingsTests
    {
        private UserSettings _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UserSettings();
        }

        [Test]
        public void should_not_throw_with_load_when_file_doesnt_exist()
        {
            _sut.Load();
        }
    }
}
