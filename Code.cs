using System;

class Program
{
    static string[,] notes = new string[100, 2]; // 100 notes max, 2 fields: title and content
    static int noteCount = 0;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n[1] Create Note\n[2] View One Note\n[3] View All Notes\n[4] Update Note\n[5] Delete Note\n[6] Exit");
            Console.Write("Choose: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateNote(); break;
                case "2": ViewOneNote(); break;
                case "3": ViewAllNotes(); break;
                case "4": UpdateNote(); break;
                case "5": DeleteNote(); break;
                case "6": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void CreateNote()
    {
        if (noteCount >= 100)
        {
            Console.WriteLine("Note limit reached.");
            return;
        }

        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Content: ");
        string content = Console.ReadLine();

        notes[noteCount, 0] = title;
        notes[noteCount, 1] = content;
        noteCount++;

        Console.WriteLine("Note created.");
    }

    static void ViewOneNote()
    {
        if (noteCount == 0)
        {
            Console.WriteLine("No notes available.");
            return;
        }

        ShowNoteList();
        Console.Write("Enter note number to view: ");
        if (TryGetNoteIndex(out int index))
        {
            Console.WriteLine($"\nNote #{index + 1}");
            Console.WriteLine($"Title: {notes[index, 0]}");
            Console.WriteLine($"Content: {notes[index, 1]}");
        }
    }

    static void ViewAllNotes()
    {
        if (noteCount == 0)
        {
            Console.WriteLine("No notes available.");
            return;
        }

        for (int i = 0; i < noteCount; i++)
        {
            Console.WriteLine($"\nNote #{i + 1}");
            Console.WriteLine($"Title: {notes[i, 0]}");
            Console.WriteLine($"Content: {notes[i, 1]}");
        }
    }

    static void UpdateNote()
    {
        if (noteCount == 0)
        {
            Console.WriteLine("No notes to update.");
            return;
        }

        ShowNoteList();
        Console.Write("Enter note number to update: ");
        if (TryGetNoteIndex(out int index))
        {
            Console.Write("New Title: ");
            notes[index, 0] = Console.ReadLine();
            Console.Write("New Content: ");
            notes[index, 1] = Console.ReadLine();
            Console.WriteLine("Note updated.");
        }
    }

    static void DeleteNote()
    {
        if (noteCount == 0)
        {
            Console.WriteLine("No notes to delete.");
            return;
        }

        ShowNoteList();
        Console.Write("Enter note number to delete: ");
        if (TryGetNoteIndex(out int index))
        {
            // Shift notes up to fill the deleted spot
            for (int i = index; i < noteCount - 1; i++)
            {
                notes[i, 0] = notes[i + 1, 0];
                notes[i, 1] = notes[i + 1, 1];
            }

            // Clear the last one
            notes[noteCount - 1, 0] = null;
            notes[noteCount - 1, 1] = null;

            noteCount--;
            Console.WriteLine("Note deleted.");
        }
    }

    static void ShowNoteList()
    {
        Console.WriteLine("\nAvailable Notes:");
        for (int i = 0; i < noteCount; i++)
        {
            Console.WriteLine($"[{i + 1}] {notes[i, 0]}");
        }
    }

    static bool TryGetNoteIndex(out int index)
    {
        if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= noteCount)
        {
            index = number - 1;
            return true;
        }

        Console.WriteLine("Invalid note number.");
        index = -1;
        return false;
    }
}