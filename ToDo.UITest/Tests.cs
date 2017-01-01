using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ToDo.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        public void Repl()
        {
            // Invoke the REPL so that we can explore the user interface
            app.Repl();
        }

        [Test]
        public void AppLaunches()
        {
            //app.Repl();
            //app.Screenshot("First screen.");

            //ActionMenuItemView

            

            //app.WaitForElement(c => c.Marked("action_bar_title").Text("Enter Credit Card Number"));
            //app.EnterText(c => c.Marked("creditCardNumberText"), new string('8', 15));



            app.Screenshot("Click ADD NEW Button.");

            //app.Tap(app.Query(c => c.Class("ActionMenuItemView").Text("Add New")));

            app.Tap(c => c.Class("AppCompatButton").Text("Add New"));
            app.Screenshot("Add New Item Screen.");

            app.WaitForElement(c => c.Class("FormsTextView").Text("Task Description?"));

            app.EnterText(c => c.Class("EntryEditText"), String.Format("Task - {0}", DateTime.Now.ToString("mmm-dd hh:MM:ss")));
        
            app.Tap(c => c.Class("AppCompatButton").Text("Save"));

            /* Assert */
            //AppResult[] result = app.Query(c => c.Class("TextView").Text("Credit card number is too short.")); //.Text("Credit card number is too long."));
            // Assert.IsTrue(result.Any(), "The error message is not being displayed.");
        }
    }
}

