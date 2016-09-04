﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class GuardTests
    {
        [Test]
        public void EqualsTo()
        {
            //not throwing
            Guard.EqualsTo(1, 1);
            Guard.EqualsTo(1, 1, "message");
            var i = 100;
            Guard.EqualsTo(1, 1, () => i++);
            i.ShouldEqual(100);

            //default behavior
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.EqualsTo(10, 2));
            //with message
            var exceptionMEssage = "mesage";
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.EqualsTo(10, 2, exceptionMEssage), exceptionMEssage);
            //with custom action
            Guard.EqualsTo(10, 2, () => i++);
            i.ShouldEqual(101);

        }

        [Test]
        public void GreaterThan()
        {
            //not throwing
            Guard.GreaterThan(11, 2);
            Guard.GreaterThan(11, 2, "message");
            var i = 100;
            Guard.GreaterThan(11, 2, ()=> i++);
            i.ShouldEqual(100);

            //default behavior
            typeof(InvalidOperationException).ShouldBeThrownBy(()=>Guard.GreaterThan(1, 2));
            //with message
            var exceptionMEssage = "mesage";
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.GreaterThan(1, 2, exceptionMEssage), exceptionMEssage);
            //with custom action
            Guard.GreaterThan(10, 23, () => i++);
            i.ShouldEqual(101);

        }
        [Test]
        public void GreaterThanOrEqualsTo()
        {
            //not throwing
            Guard.GreaterThanOrEqualsTo(11, 2);
            Guard.GreaterThanOrEqualsTo(2, 2);
            Guard.GreaterThanOrEqualsTo(11, 2, "message");
            Guard.GreaterThanOrEqualsTo(2, 2, "message");

            var i = 100;
            Guard.GreaterThanOrEqualsTo(11, 2, () => i++);
            i.ShouldEqual(100);

            Guard.GreaterThanOrEqualsTo(2, 2, () => i++);
            i.ShouldEqual(100);

            //default behavior
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.GreaterThanOrEqualsTo(10, 20));

            //with message
            var exceptionMEssage = "mesage";
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.GreaterThanOrEqualsTo(10, 23, exceptionMEssage), exceptionMEssage);
            //with custom action
            Guard.GreaterThan(10, 23, () => i++);
            i.ShouldEqual(101);
        }

        [Test]
        public void SmallerThan()
        {
            //not throwing
            Guard.SmallerThan(1, 2);
            Guard.SmallerThan(1, 2, "message");
            var i = 100;
            Guard.SmallerThan(1, 2, () => i++);
            i.ShouldEqual(100);

            //default behavior
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.SmallerThan(10, 2));
            //with message
            var exceptionMEssage = "mesage";
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.SmallerThan(10, 2, exceptionMEssage), exceptionMEssage);
            //with custom action
            Guard.SmallerThan(10, 2, () => i++);
            i.ShouldEqual(101);

        }

        [Test]
        public void SmallerThanOrEqualsTo()
        {
            //not throwing
            Guard.SmallerThanOrEqualsTo(1, 2);
            Guard.SmallerThanOrEqualsTo(2, 2);
            Guard.SmallerThanOrEqualsTo(1, 2, "message");
            Guard.SmallerThanOrEqualsTo(2, 2, "message");

            var i = 100;
            Guard.SmallerThanOrEqualsTo(1, 2, () => i++);
            i.ShouldEqual(100);

            Guard.SmallerThanOrEqualsTo(2, 2, () => i++);
            i.ShouldEqual(100);

            //default behavior
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.SmallerThanOrEqualsTo(100, 20));

            //with message
            var exceptionMEssage = "mesage";
            typeof(InvalidOperationException).ShouldBeThrownBy(() => Guard.SmallerThanOrEqualsTo(130, 23, exceptionMEssage), exceptionMEssage);
            //with custom action
            Guard.SmallerThan(150, 23, () => i++);
            i.ShouldEqual(101);
        }

        [Test]
        public void MustFollows_DoesNotTriggerAction()
        {
            var str = "test";
            Guard.MustFollow(str.Length == 4, () => str = str.ToUpper());
            "test".ShouldEqual(str);
        }

        [Test]
        public void MustFollows_TriggerAction()
        {
            var str = "test";
            Guard.MustFollow(() => str.Length == 0, () => str = str.ToUpper());
            "TEST".ShouldEqual(str);
        }

        [Test]
        public void MustFollows_ThrowsWithMessage()
        {
            var message = "test";
            typeof(Exception).ShouldBeThrownBy(() => Guard.MustFollow(() => message.Length == 0, message),
                message);
        }

        [Test]
        public void NotEmpty_TriggersAction()
        {
            var x = 0;
            Guard.NotEmpty(new List<string>(), () => x++);
            1.ShouldEqual(x);
        }

        [Test]
        public void HasValue_DoesNotTriggersAction()
        {
            var x = 0;
            Guard.HasValue("test", () => x++);
            0.ShouldEqual(x);
        }

        [Test]
        public void HasValue_TriggersAction()
        {
            var x = 0;
            Guard.HasValue("", () => x++);
            1.ShouldEqual(x);
        }

        [Test]
        public void HasValue_ThrowsOnNullString()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => Guard.HasValue(default(string)));
            //with message
            const string msg = "exception Message";
            typeof(NullReferenceException).ShouldBeThrownBy(() => Guard.HasValue(default(string), msg), msg);
        }

        [Test]
        public void HasValue_ThrowsExceptionOnEmptyString()
        {
            typeof(ArgumentNullException).ShouldBeThrownBy(
                () => Guard.HasValue("", () => { throw new ArgumentNullException(); }));

            //with message
            typeof(ArgumentException).ShouldBeThrownBy(() => Guard.HasValue("", "test"),
                "test\r\nParameter name: source");
        }

        [Test]
        public void NotNull_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => Guard.NotNull((object) null));
        }

        [Test]
        public void NotNull_ThrowsNullReferenceExceptionWithMessage()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => Guard.NotNull((object) null, "message"), "message");
        }

        [Test]
        public void NotNull_TriggerAction()
        {
            var x = 0;
            Guard.NotNull((object) null, () => x++);
            1.ShouldEqual(x);
        }

        [Test]
        public void FileExists_Throws()
        {
            //create and delete file
            var file = Path.GetTempFileName();
            Thread.Sleep(500);
            File.Delete(file);
            Thread.Sleep(500);

            typeof(FileNotFoundException).ShouldBeThrownBy(() => Guard.FileExists(file), file);
            const string msg = "Message";
            typeof(FileNotFoundException).ShouldBeThrownBy(() => Guard.FileExists(file, msg), msg);

            var i = 100;
            Guard.FileExists(file, () => i = 10);
            i.ShouldEqual(10);
        }

        [Test]
        public void FileExists_NotThrowing()
        {
            //create and delete file
            var file = Path.GetTempFileName();
            Thread.Sleep(500);


            try
            {
                Guard.FileExists(file);
                Guard.FileExists(file, "message");

                var i = 100;
                Guard.FileExists(file, () => i = 10);
                i.ShouldEqual(100);
            }
            finally
            {
                File.Delete(file);
            }
        }

        [Test]
        public void DirectoryExists_Throws()
        {
            //create and delete file
            var path = Path.GetTempFileName();
            Thread.Sleep(500);
            File.Delete(path);
            Thread.Sleep(500);

            typeof(DirectoryNotFoundException).ShouldBeThrownBy(() => Guard.DirectoryExists(path), path);
            const string msg = "Message";
            typeof(DirectoryNotFoundException).ShouldBeThrownBy(() => Guard.DirectoryExists(path, msg), msg);

            var i = 100;
            Guard.DirectoryExists(path, () => i = 10);
            i.ShouldEqual(10);
        }

        [Test]
        public void DirectoryExists_NotThrowing()
        {
            //create and delete file
            var path = Path.GetTempFileName();
            Thread.Sleep(500);
            File.Delete(path);
            Thread.Sleep(500);
            Directory.CreateDirectory(path);

            try
            {
                Guard.DirectoryExists(path);
                Guard.DirectoryExists(path, "message");

                var i = 100;
                Guard.DirectoryExists(path, () => i = 10);
                i.ShouldEqual(100);
            }
            finally
            {
                Directory.Delete(path);
            }
        }

        public void ContainsKey_Throws()
        {
            //on null dictionary
            typeof(NullReferenceException).ShouldBeThrownBy(
                () => Guard.ContainsKey((IDictionary<object, object>) null, 9));
        }

        [Test]
        public void ContainsKey_KeyNotExists()
        {
            var dictionary = new Dictionary<int, int> {{1, 1}, {2, 2}, {3, 4}};

            typeof(KeyNotFoundException).ShouldBeThrownBy(() => Guard.ContainsKey(dictionary, 9));
        }

        [Test]
        public void ContainsKey_KeyExists()
        {
            var dictionary = new Dictionary<int, int> {{1, 1}, {2, 2}, {3, 4}};
            Assert.DoesNotThrow(() => Guard.ContainsKey(dictionary, 1));
        }
    }
}