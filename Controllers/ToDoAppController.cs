using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {

        private ApplicationContext _context;

        public ToDoAppController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetNotes")]
        public List<Note> GetNotes()
        {
            List<Note> notes = _context.Notes.ToList();
            return notes;
        }


        [HttpPost]
        [Route("AddNotes")]

        public async Task<Note> AddNotes([FromForm] string NoteName, [FromForm] string NoteText, [FromForm] string blobURL, [FromForm] string TextFromImage)
        {
            await _context.Notes.AddAsync(new Note(NoteName, NoteText, blobURL, TextFromImage));

            await _context.SaveChangesAsync();

            return _context.Notes.ToList().Last();
        }



        [HttpDelete]
        [Route("DeleteNotes")]
        public async Task<string> DeleteNotes([FromForm] int Id)
        {
            string s = "";
            try
            {
                _context.Remove(_context.Notes.Single(a => a.NoteId == Id));

                await _context.SaveChangesAsync();

                s = "Note with " + Id + " was deleted";
            }
            catch (Exception ex)
            {
                s = ex.Message;
            }
            

            return s;
        }


        public static async Task UploadFromFileAsync(BlobContainerClient containerClient, string localFilePath)
        {
            string fileName = Path.GetFileName(localFilePath);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(localFilePath, true);
        }
    }
}
