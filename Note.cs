namespace WebApplication1
{
    public class Note
    {
        public int NoteId { get; set; }
        public string? NoteName { get; set; }
        public string? NoteText { get; set; }
        public string? BlobURL { get; set; }
        public string? TextFromImage { get; set; }

        public Note(int noteId, string? noteName, string? noteText, string? blobURL, string? textFromImage)
        {
            NoteId = noteId;
            NoteName = noteName;
            NoteText = noteText;
            BlobURL = blobURL;
            TextFromImage = textFromImage;
        }

        public Note(string? noteName, string? noteText, string? blobURL, string? textFromImage)
        {
            NoteName = noteName;
            NoteText = noteText;
            BlobURL = blobURL;
            TextFromImage = textFromImage;
        }

        public Note(string? noteName, string? noteText)
        {
            NoteName = noteName;
            NoteText = noteText;
        }
    }
}
