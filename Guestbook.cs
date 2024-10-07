using System.Text.Json;

namespace guestbook
{
    public class Guestbook
    {
        private string guestbookFile = @"guestbook.json";

        //skapa tom lista att lagra inlägg i
        private List<Post> posts = new List<Post>();

        //Constructor
        public Guestbook()
        {
            //Kolla om filen existerar; om ja, deserialisera innehållet
            if (File.Exists(guestbookFile))
            {
                string jsonText = File.ReadAllText(guestbookFile);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonText)!; //Lagra inlägg i tomma listan
            }
        }

        /* --- METODER --- */
        //Metod för att hämta alla inlägg
        public List<Post> getPosts()
        {
            return posts;
        }

        //Metod för att spara ändring till fil
        private void saveChanges()
        {
            //Skriv över filen med hela listan av inlägg, serialiserade
            File.WriteAllText(guestbookFile, JsonSerializer.Serialize(posts));
        }

        //Metod för att lägga till nya inlägg
        public void addPost(string auth, string postText)
        {
            Post newPost = new();
            newPost.Author = auth;
            newPost.Comment = postText;

            //datum då den las till
            newPost.Posted = DateTime.Today.ToString("yyyy-MM-dd");

            posts.Add(newPost); //Lägg till i listan
            saveChanges(); //Anropa funktion som sparar ändringar
        }

        //Metod för att ta bort ett inlägg
        public void deletePost(int index)
        {
            posts.RemoveAt(index);
            saveChanges();
        }
    }
}
