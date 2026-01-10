
#if DEBUG
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Hierarchy;
using InteractiveTerminalAPI.UI.Screen;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveTerminalAPI.UI.Application
{
    internal class ExampleApplication : InteractiveTerminalApplication<CursorElement>
    {
        public override void Initialization()
        {
            CursorElement[] hieElements = [
                new CursorElement()
                  {
                     Name = "Hope you're having such AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA day!"
                  },
                new CursorElement() // This text element is here to give space between the text and the user prompt
                  {
                     Name = "Me too!"
                  },
                ];
            CursorMenu<CursorElement> hieCursorMenu = new CursorMenu<CursorElement>()
            {
                cursorIndex = 0, // Whatever prompt we want the cursor to be selecting when the screen appears, since we only have one, it will use that one
                elements = hieElements // The collection of cursor elements we wish to have in this menu
            };
            CursorElement[] elements = new CursorElement[1]; // Since we will only have one user prompt to interact
            elements[0] =
                  new BaseCursorHierarchyElement()
                  {
                      Title = "Hello",
                      innerCursorMenu = hieCursorMenu
                  };
            CursorMenu<CursorElement> cursorMenu = new CursorMenu<CursorElement>()
            {
                cursorIndex = 0, // Whatever prompt we want the cursor to be selecting when the screen appears, since we only have one, it will use that one
                elements = elements // The collection of cursor elements we wish to have in this menu
            };
            IScreen screen = new BoxedScreen()
            {

                Title = "Hello World!", // Title is the text that is displayed in the box on top of the screen
                elements =
                [
                new TextElement()
                  {
                     Text = "Hope you're having such a lovely day!"
                  },
                new TextElement() // This text element is here to give space between the text and the user prompt
                  {
                     Text = " "
                  },
                new TextElement() // This text element is here to give space between the text and the user prompt
                  {
                     Text = " "
                  },
                  new TextHierarchyElement()
                  {
                      Title = "Hello",
                      textElements = hieElements
                  },
              cursorMenu
                ]
            };
            currentCursorMenu = cursorMenu; // To tell the application which cursor menu it should start using
            currentScreen = screen; // To tell the application which screen to display
        }
    }
}
#endif
