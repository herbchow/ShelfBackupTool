
using System;
using FluentAssertions;
using NUnit.Framework;
using XQShelfLauncher;

namespace XQShelfLauncherTests
{
    public class ContentBackupTests
    {
        private ContentBackup _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ContentBackup();
        }

        [Test]
        public void should_throw_if_not_initialized_with_path_on_restore()
        {
            _sut.Invoking(s => s.Restore("something")).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void should_throw_if_not_initialized_with_path_on_save()
        {
            _sut.Invoking(s => s.Save("something")).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void should_throw_if_not_initialized_with_path_on_findbackups()
        {
            _sut.Invoking(s => s.FindBackupsInPath()).ShouldThrow<InvalidOperationException>();
        }
    }
}
